using Microsoft.AspNetCore.Mvc;
using PGSystem.ResponseType;
using PGSystem_DataAccessLayer.DTO.RequestModel;
using PGSystem_DataAccessLayer.DTO.ResponseModel;
using PGSystem_Service.GrowthRecords;

namespace PGSystem.Controllers
{
    [ApiController]
    [Route("api/Growth-record")]
    public class GrowthRecordController : Controller
    {
        private readonly IGrowthRecordService _growthRecordService;

        public GrowthRecordController(IGrowthRecordService growthRecordService)
        {
            _growthRecordService = growthRecordService;
        }
        [HttpGet]
        public async Task<ActionResult<JsonResponse<GrowthRecordResponse>>> GetGrowthRecordById(int id)
        {
            try
            {
                var record = await _growthRecordService.GetGrowthRecordByGIDAsync(id);

                if (record == null)
                {
                    return NotFound(new JsonResponse<GrowthRecordResponse>(null, 404, "Growth record not found"));
                }

                return Ok(new JsonResponse<GrowthRecordResponse>(record, 200, "Growth record detail successfully"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new JsonResponse<GrowthRecordResponse>(null, 500, "An error occurred while retrieving the growth record"));
            }
        }
        [HttpGet("List")]
        public async Task<ActionResult<JsonResponse<List<GrowthRecordResponse>>>> GetGrowthRecords(int pid)
        {
            try
            {
                var records = await _growthRecordService.ListGrowthRecordsAsync(pid);

                if (records == null || records.Count == 0)
                {
                    return NotFound(new JsonResponse<List<GrowthRecordResponse>>(null, 404, "Growth record not found"));
                }

                return Ok(new JsonResponse<List<GrowthRecordResponse>>(records, 200, "Growth record list successfully"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new JsonResponse<List<GrowthRecordResponse>>(null, 500, "An error occurred while listing the growth record"));
            }
        }

        [HttpPost("Update-PID-Week")]
        public async Task<ActionResult<JsonResponse<string>>> UpdateGrowthRecord([FromBody] GrowthRecordRequest request)
        {
            try
            {
                if (request == null)
                {
                    return BadRequest(new JsonResponse<string>(null, 400, "Invalid request data"));
                }

                var updatedRecord = await _growthRecordService.UpdateGrowthRecordAsync(request);

                if (updatedRecord == null)
                {
                    return NotFound(new JsonResponse<string>(null, 404, "Growth record not found"));
                }

                return Ok(new JsonResponse<string>(null, 200, "Growth record updated successfully"));
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new JsonResponse<string>(null, 404, ex.Message));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new JsonResponse<string>(null, 500, "An error occurred while updating the growth record"));
            }
        }
        [HttpDelete]
        public async Task<ActionResult<JsonResponse<string>>> DeleteGrowthRecord(int id)
        {
            try
            {
                var isDeleted = await _growthRecordService.DeleteGrowthRecordAsync(id);

                if (!isDeleted)
                {
                    return NotFound(new JsonResponse<string>(null, 404, "Growth record not found"));
                }

                return Ok(new JsonResponse<string>(null, 200, "Growth record deleted successfully"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new JsonResponse<string>(null, 500, "An error occurred while deleting the growth record"));
            }
        }

    }
}
