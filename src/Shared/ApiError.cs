namespace Prop65Detector.Shared
{
    /// <summary>
    /// An API error model.
    /// </summary>
    public class ApiError
    {
        /// <summary>
        /// Gets or sets the error message.
        /// </summary>
        public string Error { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ApiError"/> class.
        /// </summary>
        public ApiError()
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ApiError"/> class.
        /// </summary>
        /// <param name="errorMessage">The error message.</param>
        public ApiError(string errorMessage)
        {
            Error = errorMessage;
        }
    }
}
