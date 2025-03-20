using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PGSystem_DataAccessLayer.DTO.ResponseModel
{
  public class DeleteMemberResponse
    {
        public int UserUID { get; set; }
        public string Message { get; set; }
    }
}
