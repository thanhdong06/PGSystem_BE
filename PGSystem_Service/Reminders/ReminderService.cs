using AutoMapper;
using PGSystem_DataAccessLayer.DTO.RequestModel;
using PGSystem_DataAccessLayer.DTO.ResponseModel;
using PGSystem_DataAccessLayer.Entities;
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
        private readonly IMapper _mapper;

        public ReminderService(IReminderRepository reminderRepository, IMapper mapper)
        {
            _reminderRepository = reminderRepository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<ReminderResponse>> GetAllRemindersAsync()
        {
            var reminders = await _reminderRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<ReminderResponse>>(reminders);
        }
        public async Task<ReminderResponse> CreateReminderAsync(ReminderRequest request)
        {
            var newReminder = new Reminder
            {
                Title = request.Title,
                //Description = request.Description,
                DateTime = request.DateTime,
                SID = 1,
                MemberID = request.MemberID,
                CreateAt = DateTime.UtcNow,
                UpdateAt = DateTime.UtcNow,
                IsDeleted = false
            };

            var createdReminder = await _reminderRepository.CreateReminderAsync(newReminder);

            return new ReminderResponse
            {
                RID = createdReminder.RID,
                Title = createdReminder.Title,
                //Description = createdReminder.Description,
                DateTime = createdReminder.DateTime,
                //SID = createdReminder.SID,
                MemberID = createdReminder.MemberID,
                //IsDeleted = createdReminder.IsDeleted
            };
        }

        public async Task<ReminderResponse?> UpdateReminderAsync(int rid, ReminderUpdateRequest request)
        {
            var reminder = await _reminderRepository.GetReminderByRID(rid);
            if (reminder == null)
            {
                return null;
            }

            // Cập nhật thông tin
            reminder.Title = request.Title;
            //reminder.Description = request.Description;
            reminder.DateTime = request.DateTime;
            reminder.SID = 1;
            reminder.UpdateAt = DateTime.UtcNow;

            await _reminderRepository.UpdateAsync(reminder);
            await _reminderRepository.SaveChangesAsync();

            return _mapper.Map<ReminderResponse>(reminder);
        }

        public async Task<bool> DeleteRemindersAsync(int rid)
        {
            return await _reminderRepository.DeleteReminders(rid);
        }

        public async Task<ReminderResponse?> GetReminderByRIDAsync(int rid)
        {
            var reminder = await _reminderRepository.GetReminderByRID(rid);
            if (reminder == null)
            {
                return null;
            }
            return _mapper.Map<ReminderResponse>(reminder);
        }
    }
}
