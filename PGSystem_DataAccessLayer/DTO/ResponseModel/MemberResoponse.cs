﻿using PGSystem_DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PGSystem_DataAccessLayer.DTO.ResponseModel
{
    public class MemberResponse
    {
        public int MemberID { get; set; }
        public int UserId { get; set; }
        public int MembershipId { get; set; }
    }
}
