using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PGSystem_DataAccessLayer.DTO.ResponseModel
{
    public class SystemReportResponse
    {
        public int TotalUsers { get; set; }
        public int TotalAdmins { get; set; }
        public int TotalMembers { get; set; }
        public decimal TotalTransactions { get; set; }
        public DateTime ReportDate { get; set; }
    }
}
