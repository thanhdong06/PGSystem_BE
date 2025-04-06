using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PGSystem_DataAccessLayer.DTO.ResponseModel
{
    public class PregnancyRecordResponse
    {
        public int PID { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly DueDate { get; set; }
        public string Status { get; set; }
        public DateTime CreateAt { get; set; }
        public int MemberMemberID { get; set; }
        public List<FetusResponse> Fetuses { get; set; }

    }
}
