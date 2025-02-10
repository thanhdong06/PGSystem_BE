﻿using Microsoft.AspNetCore.Mvc;
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

        [HttpPost("Update")]
        public async Task<IActionResult> UpdateGrowthRecord([FromBody] GrowthRecordRequest request)
        {
            if (request == null)
            {
                return BadRequest(new JsonResponse<string>(null, 400, "Invalid request data"));
            }
            try
            {
                var updatedRecord = await _growthRecordService.UpdateGrowthRecordAsync(request);

                return Ok(new JsonResponse<GrowthRecordResponse>(updatedRecord, 200, "Growth record updated successfully"));
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
    }
}
