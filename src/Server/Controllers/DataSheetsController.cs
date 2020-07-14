using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Prop65Detector.Server.Services;
using Prop65Detector.Shared;

namespace Prop65Detector.Server.Controllers
{
    /// <summary>
    /// API controller for <see cref="SafetyDataSheet"/> instances.
    /// </summary>
    /// <seealso cref="ControllerBase" />
    [Route("api/[controller]")]
    [ApiController]
    public class DataSheetsController : ControllerBase
    {
        private readonly ISafetyDataSheetReader _reader;
        private readonly ILogger<DataSheetsController> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="DataSheetsController"/> class.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="logger">The logger.</param>
        public DataSheetsController(ISafetyDataSheetReader reader, ILogger<DataSheetsController> logger)
        {
            _reader = reader ?? throw new ArgumentNullException(nameof(reader));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Reads the specified <paramref name="file"/> and converts it into a <see cref="SafetyDataSheet"/> response.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <returns>The result.</returns>
        [HttpPost]
        public IActionResult Post([FromForm(Name = "file")] IFormFile file)
        {
            if(file == null)
            {
                return BadRequest(new ApiError("Multi-part form content not found"));
            }

            if(!Validation.IsValidFileSize(file.Length))
            {
                return BadRequest(new ApiError($"File may not exceed {Validation.MaxFileSizeInMB}MB"));
            }

            try
            {
                using var stream = file.OpenReadStream();
                var dataSheet = _reader.Read(stream);

                if (!dataSheet.ParsedSuccessfully)
                {
                    return BadRequest(
                        new ApiError("Unable to find section 3. Document may need OCR from a service such as https://ocr.space"));
                }

                return Ok(dataSheet);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to parse posted file '{file.FileName}'");
                return StatusCode(500);
            }
        }
    }
}
