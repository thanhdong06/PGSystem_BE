using PGSystem_DataAccessLayer.DTO.RequestModel;
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
        Task<ReminderResponse?> GetReminderByRIDAsync(int rid);
        Task<ReminderResponse> CreateReminderAsync(ReminderRequest request);
        Task<ReminderResponse?> UpdateReminderAsync(int rid, ReminderRequest request);
        Task<bool> DeleteRemindersAsync(int rid);
    }
}
