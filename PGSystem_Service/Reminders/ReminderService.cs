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
        public async Task<ReminderResponse> CreateReminderAsync(ReminderRequest request)
        {
            var entity = _mapper.Map<Reminder>(request);
            entity.CreateAt = entity.UpdateAt = DateTime.UtcNow;

            var createdReminder = await _reminderRepository.CreateRemindersAsync(entity);

            return _mapper.Map<ReminderResponse>(createdReminder);
        }

        public async Task<ReminderResponse> UpdateRemindersAsync(ReminderRequest request)
        {
            var existingReminder = await _reminderRepository.GetReminderByRidAndTitle(request.RID, request.Title);

            if (existingReminder== null)
            {
                throw new KeyNotFoundException($"No reminder found for PID {request.RID} have {request.Title}.");
            }

            existingReminder.Description = request.Description;
            existingReminder.DateTime = request.DateTime;
            existingReminder.UpdateAt = DateTime.Now;

            var updateReminder = await _reminderRepository.UpdateRemindersAsync(existingReminder);

            return _mapper.Map<ReminderResponse>(updateReminder);
        }

        public async Task<bool> DeleteRemindersAsync(int rid)
        {
            return await _reminderRepository.DeleteReminders(rid);
        }
    }
}
