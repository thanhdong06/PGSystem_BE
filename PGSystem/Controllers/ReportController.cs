using Microsoft.AspNetCore.Mvc;
using PGSystem.ResponseType;
using PGSystem_DataAccessLayer.DTO.ResponseModel;
using PGSystem_Service.GrowthRecords;

namespace PGSystem.Controllers
{
    [ApiController]
    [Route("api/Report")]
    public class ReportController : Controller
    {
        private readonly IGrowthRecordService _growthRecordService;

        public ReportController(IGrowthRecordService growthRecordService)
        {
            _growthRecordService = growthRecordService;
        }
        [HttpGet("Chart")]
        public async Task<ActionResult<JsonResponse<List<GrowthRecordResponse>>>> GetGrowthChart(int memberId)
        {
            try
            {
                var chartData = await _growthRecordService.GetGrowthChartAsync(memberId);

                if (chartData == null || chartData.Count == 0)
                {
                    return NotFound(new JsonResponse<List<GrowthRecordResponse>>(null, 404, "No growth data found"));
                }

                return Ok(new JsonResponse<List<GrowthRecordResponse>>(chartData, 200, "Growth chart data list successfully"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new JsonResponse<List<GrowthRecordResponse>>(null, 500, "An error occurred while fetching growth chart data"));
            }
        }    
    }
}
