using Prop65Detector.Shared;
using System.Collections.Generic;

namespace Prop65Detector.Server.Services
{
    /// <summary>
    /// A cache of Prop 65 data.
    /// </summary>
    public interface IProp65Cache
    {
        /// <summary>
        /// Gets the prop 65 ingredients that match the given <paramref name="casNumbers"/>.
        /// </summary>
        /// <param name="casNumbers">The cas numbers.</param>
        /// <returns>
        /// A collection of <see cref="Ingredient" /> instances.
        /// </returns>
        IEnumerable<Ingredient> GetProp65Ingredients(IEnumerable<string> casNumbers);
    }
}