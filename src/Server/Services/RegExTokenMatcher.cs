using System;
using System.Text.RegularExpressions;

namespace Prop65Detector.Server.Services
{
    /// <summary>
    /// A regular expression implementation of <see cref="ITokenMatcher"/>.
    /// </summary>
    /// <seealso cref="ITokenMatcher" />
    public class RegExTokenMatcher : ITokenMatcher
    {
        private readonly Regex _regex;

        /// <summary>
        /// Initializes a new instance of the <see cref="RegExTokenMatcher"/> class.
        /// </summary>
        /// <param name="regex">The regex.</param>
        public RegExTokenMatcher(Regex regex)
        {
            _regex = regex ?? throw new ArgumentNullException(nameof(regex));
        }

        /// <summary>
        /// Finds a matching token in the given <paramref name="content" /> if it exists.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <returns>
        /// The index of the matched token in the <paramref name="content" />, or <c>-1</c> if it does not exist.
        /// </returns>
        public int Match(string content)
        {
            var match = _regex.Match(content);

            return match.Success ? match.Index : -1;
        }
    }
}
