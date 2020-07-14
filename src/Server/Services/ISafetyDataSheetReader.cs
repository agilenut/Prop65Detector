using Prop65Detector.Shared;
using System.IO;

namespace Prop65Detector.Server.Services
{
    /// <summary>
    /// A service capable of reading a safety data sheet.
    /// </summary>
    public interface ISafetyDataSheetReader
    {
        /// <summary>
        /// Reads the specified stream.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <returns>A hydrated safety data sheet.</returns>
        SafetyDataSheet Read(Stream stream);
    }
}