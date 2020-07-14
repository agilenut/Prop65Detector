namespace Prop65Detector.Shared
{
    /// <summary>
    /// Validation constants and utilities.
    /// </summary>
    public static class Validation
    {
        /// <summary>
        /// The maximum file size in MB.
        /// </summary>
        // ReSharper disable once InconsistentNaming
        public const int MaxFileSizeInMB = 10;

        /// <summary>
        /// The maximum file size in bytes.
        /// </summary>
        public const int MaxFileSizeInBytes = MaxFileSizeInMB * 1024 * 1024;

        /// <summary>
        /// Determines whether the given <paramref name="size"/> is valid.
        /// </summary>
        /// <param name="size">The size.</param>
        /// <returns>
        ///   <c>true</c> if the size is valid; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsValidFileSize(long size)
        {
            return size < MaxFileSizeInBytes;
        }
    }
}
