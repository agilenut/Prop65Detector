namespace Prop65Detector.Server.Services
{
    /// <summary>
    /// Finds matching tokens in a given <c>string.</c>
    /// </summary>
    public interface ITokenMatcher
    {
        /// <summary>
        /// Finds a matching token in the given <paramref name="content"/> if it exists.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <returns>The index of the matched token in the <paramref name="content"/>, or <c>-1</c> if it does not exist.</returns>
        int Match(string content);
    }
}