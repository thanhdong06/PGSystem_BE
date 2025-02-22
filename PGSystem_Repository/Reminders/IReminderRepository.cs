using PGSystem_DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PGSystem_Repository.Reminders
{
    public interface IReminderRepository
    {
        Task<List<Reminder>> GetAllRemindersAsync();
        Task<Reminder> GetReminderByMemberID(int mid);
        Task<Reminder> CreateRemindersAsync(Reminder entity);
        Task<Reminder> UpdateRemindersAsync(Reminder reminder);
        Task<bool> DeleteReminders(int rid);
    }
}
