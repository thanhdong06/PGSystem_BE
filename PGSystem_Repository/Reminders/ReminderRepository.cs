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

        public async Task<IEnumerable<Reminder>> GetAllAsync()
        {
            return await _context.Reminders
                .Where(r => !r.IsDeleted)
                .ToListAsync();
        }
        public async Task<Reminder> CreateRemindersAsync(Reminder entity)
        {
            _context.Reminders.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Reminder> GetReminderByRID(int rid)
        {
            return await _context.Reminders.FirstOrDefaultAsync(re => re.RID == rid && !re.IsDeleted);
        }
        public async Task UpdateAsync(Reminder reminder)
        {
            _context.Reminders.Update(reminder);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
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
