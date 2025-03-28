﻿using PGSystem_DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PGSystem_Repository.Reminders
{
    public interface IReminderRepository
    {
        Task<IEnumerable<Reminder>> GetAllAsync();
        Task<Reminder> GetReminderByRID(int rid);
        Task<Reminder> CreateReminderAsync(Reminder reminder);
        Task<bool> DeleteReminders(int rid);
        Task UpdateAsync(Reminder reminder);
        Task SaveChangesAsync();
    }
}
