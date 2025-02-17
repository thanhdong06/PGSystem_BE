using Microsoft.AspNetCore.Mvc;
using PGSystem.ResponseType;
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

        [HttpGet("Reminder")]
        public async Task<IActionResult> GetAllReminder()
        {
            var reminders = await _reminderService.GetAllRemindersAsync();

            if (reminders == null || reminders.Count == 0)
            {
                return NotFound(new JsonResponse<List<ReminderResponse>>(new List<ReminderResponse>(), StatusCodes.Status404NotFound, "No reminders found"));
            }
            return Ok(new JsonResponse<List<ReminderResponse>>(reminders, StatusCodes.Status200OK, "Reminder list retrieved successfully"));
        }
    }
}
