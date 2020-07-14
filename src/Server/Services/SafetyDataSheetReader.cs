using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Prop65Detector.Shared;
using System;

namespace Prop65Detector.Server.Services
{
    /// <summary>
    /// A service capable of reading a safety data sheet.
    /// </summary>
    /// <seealso cref="ISafetyDataSheetReader" />
    public class SafetyDataSheetReader : ISafetyDataSheetReader
    {
        private static readonly Regex Section3Regex = new Regex(".*Composition.*Information.*Ingredients", RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.Compiled);
        private static readonly Regex Section4Regex = new Regex(".*First.*Aid.*Measures", RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.Compiled);
        private static readonly Regex CasNumberRegex = new Regex("([0-9]{2,7})-([0-9]{2})-[0-9]", RegexOptions.Compiled);
        private readonly IProp65Cache _prop65Cache;

        /// <summary>
        /// Initializes a new instance of the <see cref="SafetyDataSheetReader"/> class.
        /// </summary>
        /// <param name="prop65Cache">The prop65 cache.</param>
        public SafetyDataSheetReader(IProp65Cache prop65Cache)
        {
            _prop65Cache = prop65Cache ?? throw new ArgumentNullException(nameof(prop65Cache));
        }

        /// <summary>
        /// Reads the specified stream.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <returns>
        /// A hydrated safety data sheet.
        /// </returns>
        /// <remarks>
        /// The algorithm for this method loops on each page of the given PDF until it can match the section headers.
        /// Once it finds section 3 and copies the CAS numbers out, it will exit to avoid unnecessary processing time.
        /// </remarks>
        public SafetyDataSheet Read(Stream stream)
        {
            using var pdfReader = new PdfReader(stream);
            using var pdfDocument = new PdfDocument(pdfReader);

            var section3 = new SafetyDataSheetSection(new RegExTokenMatcher(Section3Regex), new RegExTokenMatcher(Section4Regex));

            var numberOfPages = pdfDocument.GetNumberOfPages();
            var pageNumber = 1;
            while (pageNumber <= numberOfPages && !section3.Completed)
            {
                var page = pdfDocument.GetPage(pageNumber++);
                var pageText = PdfTextExtractor.GetTextFromPage(page);

                pageText = Encoding.UTF8.GetString(Encoding.Convert(Encoding.Default, Encoding.UTF8, Encoding.Default.GetBytes(pageText)));
                section3.ExtractMatchingContent(pageText);
            }

            var sectionCasNumbers = GetCasNumbersFromSection(section3).ToList();
            var successfullyParsed = sectionCasNumbers.Any();
            var matchedProp65Ingredients = _prop65Cache.GetProp65Ingredients(sectionCasNumbers);
            
            var result = new SafetyDataSheet
            {
                ParsedSuccessfully = successfullyParsed,
                Ingredients = matchedProp65Ingredients
            };

            return result;
        }

        private static IEnumerable<string> GetCasNumbersFromSection(SafetyDataSheetSection section)
        {
            return CasNumberRegex.Matches(section.Text)
                .Select(m => m.Value);
        }
    }
}
