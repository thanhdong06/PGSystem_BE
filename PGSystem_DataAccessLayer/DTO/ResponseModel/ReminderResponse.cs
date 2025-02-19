using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PGSystem_DataAccessLayer.DTO.ResponseModel
{
    public class ReminderResponse
    {
        public string Title { get; set; }
        public string? Description { get; set; }
        public DateTime DateTime { get; set; }
    }
}
