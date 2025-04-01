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
        [JsonIgnore]
        public int MemberMemberID { get; set; }
    }
}
