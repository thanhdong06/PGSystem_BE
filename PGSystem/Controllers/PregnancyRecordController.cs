using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PGSystem.ResponseType;
using PGSystem_DataAccessLayer.DTO.RequestModel;
using PGSystem_DataAccessLayer.DTO.ResponseModel;
using PGSystem_Service.PregnancyRecords;
using System.Text.Json;

namespace PGSystem.Controllers
{
    [ApiController]
    [Route("api/Pregnancy-record")]
    public class PregnancyRecordController : Controller
    {
        private readonly IPregnancyRecordService _pregnancyRecordService;

        public PregnancyRecordController(IPregnancyRecordService pregnancyRecordService)
        {
            _pregnancyRecordService = pregnancyRecordService;
        }

        [Authorize(Roles = "Member")]
        [HttpPost("Create")]
        public async Task<ActionResult<JsonResponse<string>>> CreatePregnancyRecord([FromBody] PregnancyRecordRequest request)
        {
            try
            {
                var memberIdClaim = User.FindFirst("MemberId")?.Value;
                if (string.IsNullOrEmpty(memberIdClaim) || !int.TryParse(memberIdClaim, out int memberId))
                {
                    return Unauthorized(new JsonResponse<string>("Unauthorized: MemberId not found or invalid", 401, null));
                }

                request.MemberMemberID = memberId;

                await _pregnancyRecordService.CreatePregnancyRecordAsync(request);
                return Ok(new JsonResponse<string>(null, 200, "Pregnancy record created successfully"));
            }
            catch (Exception ex)
            {
                return BadRequest(new JsonResponse<string>("Something went wrong, please contact the admin", 400, ex.Message));
            }
        }

        [Authorize(Roles = "Member")]
        [HttpGet("Records")]
        public async Task<ActionResult> GetPregnancyRecords()
        {
            try
            {
                var memberIdClaim = User.FindFirst("MemberId")?.Value;
                if (string.IsNullOrEmpty(memberIdClaim) || !int.TryParse(memberIdClaim, out int memberId))
                {
                    return Unauthorized(new JsonResponse<string>("Unauthorized: MemberId not found or invalid", 401, null));
                }

                var records = await _pregnancyRecordService.GetByMemberIdAsync(memberId);

                if (records == null || !records.Any())
                {
                    return NotFound(new { message = "No records found for the given MemberId." });
                }

                return Ok(new
                {
                    success = true,
                    message = "Pregnancy records retrieved successfully",
                    data = records
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Error occurred", error = ex.Message });
            }
        }

        [Authorize(Roles = "Member")]
        [HttpPut("ClosePregnancyRecord")]
        public async Task<IActionResult> ClosePregnancyRecord([FromQuery] int pregnancyRecordId)
        {
            try
            {
                var memberIdClaim = User.FindFirst("MemberId")?.Value;
                if (string.IsNullOrEmpty(memberIdClaim) || !int.TryParse(memberIdClaim, out int memberId))
                {
                    return Unauthorized("Unauthorized: MemberId not found or invalid");
                }

                var closedRecord = await _pregnancyRecordService.ClosePregnancyRecordAsync(pregnancyRecordId, memberId);

                return Ok(new
                {
                    success = true,
                    message = "Pregnancy record closed successfully",
                    data = closedRecord
                });
            }
            catch (KeyNotFoundException ex)
            {
                return BadRequest(new
                {
                    success = false,
                    message = ex.Message
                });
            }
        }
    }
}
