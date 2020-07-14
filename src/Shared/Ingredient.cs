namespace Prop65Detector.Shared
{
    /// <summary>
    /// A material ingredient.
    /// </summary>
    public class Ingredient
    {
        /// <summary>
        /// Gets or sets the cas number.
        /// </summary>
        public string CasNumber { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <returns>
        /// A <see cref="string" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return $"{CasNumber} {Name}";
        }
    }
}
