using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PGSystem_DataAccessLayer.Entities
{
    public class Admin
    {
        public int AdminID { get; set; }
        public string? AdminName { get; set; }
        public User User { get; set; }

    }
}
