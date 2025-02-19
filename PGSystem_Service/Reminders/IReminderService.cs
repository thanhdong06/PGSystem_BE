using PGSystem_DataAccessLayer.DTO.ResponseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PGSystem_Service.Reminders
{
    public interface IReminderService
    {
        Task<List<ReminderResponse>> GetAllRemindersAsync();
    }
}
