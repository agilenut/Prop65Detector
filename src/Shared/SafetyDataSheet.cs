using System.Collections.Generic;

namespace Prop65Detector.Shared
{
    /// <summary>
    /// A safety data sheet.
    /// </summary>
    public class SafetyDataSheet
    {
        /// <summary>
        /// Gets or sets a value indicating whether the document was parsed successfully.
        /// </summary>
        public bool ParsedSuccessfully { get; set; }

        /// <summary>
        /// Gets or sets the ingredients.
        /// </summary>
        public IEnumerable<Ingredient> Ingredients { get; set; } = new List<Ingredient>();
    }
}
