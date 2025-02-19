using Microsoft.EntityFrameworkCore;
using PGSystem_DataAccessLayer.DBContext;
using PGSystem_DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PGSystem_Repository.Reminders
{
    public class ReminderRepository : IReminderRepository
    {
        private readonly AppDBContext _context;

        public ReminderRepository(AppDBContext context)
        {
            _context = context;
        }

        public async Task<List<Reminder>> GetAllRemindersAsync()
        {
            {
                return await _context.Reminders.ToListAsync();
            }
        }
        public async Task<Reminder> CreateRemindersAsync(Reminder entity)
        {
            _context.Reminders.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Reminder> GetReminderByRidAndTitle(int rid, string title)
        {
            return await _context.Reminders.FirstOrDefaultAsync(re => re.RID == rid && re.Title.Equals(title) && !re.IsDeleted);
        }
        public async Task<Reminder> UpdateRemindersAsync(Reminder reminder)
        {
            _context.Reminders.Update(reminder);
            await _context.SaveChangesAsync();
            return reminder;
        }
        public async Task<bool> DeleteReminders(int rid)
        {
            var reminder = await _context.Reminders.FirstOrDefaultAsync(re => re.RID == rid && !re.IsDeleted);

            if (reminder == null)
            {
                return false;
            }
            reminder.IsDeleted = true;
            await _context.SaveChangesAsync();

            return true;
        }

    }


}
