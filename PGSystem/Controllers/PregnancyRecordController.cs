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

        [HttpPost("Create")]
        public async Task<ActionResult<JsonResponse<string>>> CreatePregnancyRecord([FromBody] PregnancyRecordRequest request)
        {
            try
            {
                var memberId = User.FindFirst("MemberId")?.Value;
                if (string.IsNullOrEmpty(memberId))
                {
                    return Unauthorized(new JsonResponse<string>("Unauthorized: MemberId not found in token", 401, null));
                }

                request.MemberMemberID = int.Parse(memberId);

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
