using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PGSystem_DataAccessLayer.Entities
{
    public class RStatus
    {
        public int SID { get; set; }
        public string SName { get; set; }

        public ICollection<Reminder> Reminders { get; set; }
    }
}
