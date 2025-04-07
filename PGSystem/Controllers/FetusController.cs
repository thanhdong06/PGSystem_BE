using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PGSystem.ResponseType;
using PGSystem_DataAccessLayer.DTO.RequestModel;
using PGSystem_Service.Fetuses;
using System.Text.Json;

namespace PGSystem.Controllers
{
    [Route("api/Fetus")]
    [ApiController]
    public class FetusController : Controller
    {
        private readonly IFetusService _fetusService;

        public FetusController(IFetusService fetusService)
        {
            _fetusService = fetusService;
        }

        [Authorize(Roles = "Member")]
        [HttpPost("fetuses")]
        public async Task<ActionResult> CreateFetus([FromBody] FetusRequest request)
        {
            try
            {
                var created = await _fetusService.CreateFetusAsync(request);
           
                return Ok(new
                {
                    success = true,
                    message = "Fetus records retrieved successfully",
                    data = created
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    success = false,
                    message = "Failed to create fetus",
                    error = ex.Message
                });
            }
        }

        [Authorize(Roles = "Member")]
        [HttpGet("Fetuses")]
        public async Task<ActionResult> GetFetusesByPregnancyRecordId([FromQuery]int pregnancyRecordId)
        {
            try
            {
                var fetuses = await _fetusService.GetFetusesByPregnancyRecordIdAsync(pregnancyRecordId);

                if (fetuses == null || !fetuses.Any())
                {
                    return NotFound(new
                    {
                        success = false,
                        message = "No fetuses found for this pregnancy record"
                    });
                }

                return Ok(new
                {
                    success = true,
                    message = "Fetuses retrieved successfully",
                    data = fetuses
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    success = false,
                    message = "Failed to retrieve fetuses",
                    error = ex.Message
                });
            }
        }

        [Authorize(Roles = "Member")]
        [HttpPost("FetusMeasurements")]
        public async Task<ActionResult> CreateFetusMeasurement([FromQuery]int fetusId, [FromBody] FetusMeasurementRequest request)
        {
            try
            {
                var createdMeasurement = await _fetusService.CreateFetusMeasurementAsync(request, fetusId);

                return Ok(new
                {
                    success = true,
                    message = "Fetus measurement created successfully",
                    data = createdMeasurement
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    success = false,
                    message = "Failed to create fetus measurement",
                    error = ex.Message
                });
            }
        }

        [Authorize(Roles = "Member")]
        [HttpPut("Measurements")]
        public async Task<ActionResult> UpdateFetusMeasurement([FromQuery]int measurementId, [FromBody] FetusMeasurementUpdateRequest request)
        {
            try
            {
                var updatedMeasurement = await _fetusService.UpdateFetusMeasurementAsync(measurementId, request);

                return Ok(new
                {
                    success = true,
                    message = "Fetus measurement updated successfully",
                    data = new
                    {
                        updatedMeasurement.MeasurementId,
                        updatedMeasurement.Length,
                        updatedMeasurement.HeadCircumference,
                        updatedMeasurement.WeightEstimate,
                        updatedMeasurement.DateMeasured,
                        updatedMeasurement.UpdatedAt,
                        updatedMeasurement.Week,
                        warnings = updatedMeasurement.Warnings ?? new List<string>()
                    }
                });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new
                {
                    success = false,
                    message = ex.Message
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    success = false,
                    message = "Failed to update fetus measurement",
                    error = ex.Message
                });
            }
        }

        [Authorize(Roles = "Member")]
        [HttpGet("Measurements/Fetus")]
        public async Task<ActionResult> GetFetusMeasurements([FromQuery] int fetusId)
        {
            try
            {
                var measurements = await _fetusService.GetFetusMeasurementsByFetusIdAsync(fetusId);

                return Ok(new
                {
                    success = true,
                    message = "Fetus measurements list successfully",
                    data = measurements.Select(m => new
                    {
                        m.MeasurementId,
                        m.DateMeasured,
                        m.Length,
                        m.HeadCircumference,
                        m.WeightEstimate,
                        m.CreatedAt,
                        m.UpdatedAt
                    })
                });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new
                {
                    success = false,
                    message = ex.Message
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    success = false,
                    message = "Failed to retrieve fetus measurements",
                    error = ex.Message
                });
            }
        }

    }
}
