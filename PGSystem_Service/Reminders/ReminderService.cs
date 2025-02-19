using PGSystem_DataAccessLayer.DTO.ResponseModel;
using PGSystem_Repository.Reminders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PGSystem_Service.Reminders
{
    public class ReminderService : IReminderService
    {
        private readonly IReminderRepository _reminderRepository;

        public ReminderService(IReminderRepository reminderRepository)
        {
            _reminderRepository = reminderRepository;
        }
        public async Task<List<ReminderResponse>> GetAllRemindersAsync()
        {
            var reminders = await _reminderRepository.GetAllRemindersAsync();

            return reminders.Select(r => new ReminderResponse
            {
                Title = r.Title,
                Description = r.Description,
                DateTime = r.DateTime,
            }).ToList();
        }
    }
}
