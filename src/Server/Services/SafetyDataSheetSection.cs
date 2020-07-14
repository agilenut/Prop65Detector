using System;
using System.Text;
using Prop65Detector.Shared;

namespace Prop65Detector.Server.Services
{
    /// <summary>
    /// Represents a section of a <see cref="SafetyDataSheet"/>
    /// </summary>
    public class SafetyDataSheetSection
    {
        private const int NotFound = -1;
        private int _startIndex = NotFound;
        private int _endIndex = NotFound;

        private readonly ITokenMatcher _start;
        private readonly ITokenMatcher _end;
        private readonly StringBuilder _text = new StringBuilder();

        private bool StartFound => _startIndex != NotFound;
        private bool EndFound => _endIndex != NotFound;
        public bool Completed => EndFound;
        public string Text => _text.ToString();

        /// <summary>
        /// Initializes a new instance of the <see cref="SafetyDataSheetSection"/> class.
        /// </summary>
        /// <param name="start">The start.</param>
        /// <param name="end">The end.</param>
        public SafetyDataSheetSection(ITokenMatcher start, ITokenMatcher end)
        {
            _start = start ?? throw new ArgumentNullException(nameof(start));
            _end = end ?? throw new ArgumentNullException(nameof(end));
        }

        /// <summary>
        /// Extracts the content of the matching.
        /// </summary>
        /// <param name="content">The content.</param>
        public void ExtractMatchingContent(string content)
        {
            if(Completed)
            {
                return;
            }

            if (!StartFound)
            {
                _startIndex = _start.Match(content);
            }

            if (StartFound && !EndFound)
            {
                _endIndex = _end.Match(content);
            }

            // ReSharper disable once InvertIf
            if (StartFound)
            {
                var endIndex = EndFound ? _endIndex : content.Length - 1;
                var matchedText = content[_startIndex..endIndex];
                _text.Append(matchedText);
            }
        }
    }
}
