using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PGSystem.ResponseType;
using PGSystem_DataAccessLayer.DTO.RequestModel;
using PGSystem_DataAccessLayer.DTO.ResponseModel;
using PGSystem_Service.PregnancyRecords;

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
    }
}
