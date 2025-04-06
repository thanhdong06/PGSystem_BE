using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PGSystem_DataAccessLayer.DTO.RequestModel
{
    public class PregnancyRecordRequest
    {
        public DateOnly? StartDate { get; set; } // Có thể null, sẽ dùng Now nếu không có
        public string Status { get; set; } = "Đang theo dõi";
        public List<FetusRequest> Fetuses { get; set; } = new();

        [JsonIgnore]
        public int MemberMemberID { get; set; }
    }
}
