using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Prop65Detector.Shared;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Prop65Detector.Server.Services
{
    /// <summary>
    /// A cache of Prop 65 data based on a tab delimited txt file on disk.
    /// </summary>
    /// <seealso cref="IProp65Cache" />
    public class Prop65Cache : IProp65Cache
    {
        private const string DataPath = @"Data";
        private const string FileName = @"Prop65Ingredients.txt";

        private readonly object _lockObject = new object();
        private readonly IHostEnvironment _host;
        private readonly ILogger<Prop65Cache> _logger;
        private Dictionary<string, Ingredient> _cache;

        /// <summary>
        /// Gets the cache a lazy loaded cache of Prop 65 ingredients.
        /// </summary>
        private Dictionary<string, Ingredient> Cache
        {
            get
            {
                lock (_lockObject)
                {
                    if (_cache != null)
                    {
                        return _cache;
                    }

                    var cache = new Dictionary<string, Ingredient>();

                    var path = Path.Combine(_host.ContentRootPath, DataPath, FileName);
                    var lines = File.ReadAllLines(path);
                    for (int i = 1; i < lines.Length; i++)
                    {
                        var line = lines[i];
                        var values = line.Split('\t', StringSplitOptions.RemoveEmptyEntries);

                        if (values.Length != 2)
                        {
                            throw new InvalidOperationException($"Unable to parse '{FileName}'. Found {values.Length} values on line {i + 1}");
                        }

                        var cas = values[1];
                        var name = values[0];

                        if (cache.ContainsKey(cas))
                        {
                            _logger.LogWarning($"Found duplicate CAS number '{cas}' while loading '{FileName}'");
                            continue;
                        }

                        cache.Add(cas, new Ingredient { CasNumber = cas, Name = name });
                    }

                    _cache = cache;

                    _logger.LogInformation($"Loaded {_cache.Count()} values from {FileName}");
                }

                return _cache;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Prop65Cache"/> class.
        /// </summary>
        /// <param name="host">The host.</param>
        /// <param name="logger">The logger.</param>
        public Prop65Cache(IHostEnvironment host, ILogger<Prop65Cache> logger)
        {
            _host = host ?? throw new ArgumentNullException(nameof(host));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Gets the prop 65 ingredients that match the given <paramref name="casNumbers"/>.
        /// </summary>
        /// <param name="casNumbers">The cas numbers.</param>
        /// <returns>
        /// A collection of <see cref="Ingredient" /> instances.
        /// </returns>
        public IEnumerable<Ingredient> GetProp65Ingredients(IEnumerable<string> casNumbers)
        {
            return casNumbers
                .Where(n => Cache.ContainsKey(n))
                .Select(n => Cache[n]);
        }
    }
}
