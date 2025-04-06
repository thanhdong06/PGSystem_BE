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
        public async Task<ActionResult<JsonResponse<string>>> CreateFetus([FromBody] FetusRequest request)
        {
            try
            {
                var created = await _fetusService.CreateFetusAsync(request);

                var json = JsonSerializer.Serialize(created, new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    WriteIndented = true
                });

                return Ok(new JsonResponse<string>("Fetus created successfully", 200, json));
            }
            catch (Exception ex)
            {
                return BadRequest(new JsonResponse<string>("Failed to create fetus", 400, ex.Message));
            }
        }
    }
}
