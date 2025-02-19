using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PGSystem_DataAccessLayer.DTO.RequestModel
{
    public class PregnancyRecordRequest
    {
        public DateOnly StartDate { get; set; }
        public DateOnly DueDate { get; set; }
        public int MemberMemberID { get; set; }
    }
}
