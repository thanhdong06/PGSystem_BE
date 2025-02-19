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
    }
}
