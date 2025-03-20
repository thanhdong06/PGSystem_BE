using Microsoft.AspNetCore.Mvc;
using PGSystem.ResponseType;
using PGSystem_DataAccessLayer.DTO.RequestModel;
using PGSystem_DataAccessLayer.DTO.ResponseModel;
using PGSystem_Service.Reminders;

namespace PGSystem.Controllers
{
    [Route("api/Reminder")]
    [ApiController]
    public class ReminderController : Controller
    {
        private readonly IReminderService _reminderService;
        public ReminderController(IReminderService reminderService)
        {
            _reminderService = reminderService;
        }
        [HttpGet("all")]
        public async Task<IActionResult> GetAllReminders()
        {
            var reminders = await _reminderService.GetAllRemindersAsync();
            if (reminders == null || !reminders.Any())
            {
                return NotFound(new JsonResponse<List<ReminderResponse>>(new List<ReminderResponse>(), StatusCodes.Status404NotFound, "No reminders found"));
            }
            return Ok(reminders);
        }

        //[HttpGet("Reminder")]
        //public async Task<IActionResult> GetAllReminder()
        //{
        //    var reminders = await _reminderService.GetAllRemindersAsync();

        //    if (reminders == null || reminders.Count == 0)
        //    {
        //        return NotFound(new JsonResponse<List<ReminderResponse>>(new List<ReminderResponse>(), StatusCodes.Status404NotFound, "No reminders found"));
        //    }
        //    return Ok(new JsonResponse<List<ReminderResponse>>(reminders, StatusCodes.Status200OK, "Reminder list retrieved successfully"));
        //}

        [HttpPost("create")]
        public async Task<IActionResult> CreateReminder([FromBody] ReminderRequest request)
        {
            if (request == null)
                return BadRequest(new { message = "Data invalid" });

            var reminder = await _reminderService.CreateReminderAsync(request);
            return Ok(reminder);
        }

        [HttpPut("{rid}")]
        public async Task<IActionResult> UpdateReminder(int rid, [FromBody] ReminderUpdateRequest request)
        {
            if (request == null)
            {
                return BadRequest("Invalid data");
            }

            var updatedReminder = await _reminderService.UpdateReminderAsync(rid, request);
            if (updatedReminder == null)
            {
                return NotFound($"Reminder with RID {rid} does not exist.");
            }

            return Ok(updatedReminder);
        }

        [HttpDelete]
        public async Task<ActionResult<JsonResponse<string>>> DeleteReminders(int rid)
        {
            try
            {
                var isDeleted = await _reminderService.DeleteRemindersAsync(rid);

                if (!isDeleted)
                {
                    return NotFound(new JsonResponse<string>(null, 404, "Reminder not found"));
                }

                return Ok(new JsonResponse<string>(null, 200, "Reminder deleted successfully"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new JsonResponse<string>(null, 500, "An error occurred while deleting the reminder"));
            }
        }
        [HttpGet("{rid}")]
        public async Task<IActionResult> GetReminderByRID(int rid)
        {
            var reminder = await _reminderService.GetReminderByRIDAsync(rid);
            if (reminder == null)
            {
                return NotFound($"Reminder with  RID {rid} does not exist");
            }
            return Ok(reminder);
        }
    }
}
