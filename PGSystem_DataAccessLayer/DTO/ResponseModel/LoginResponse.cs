﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PGSystem_DataAccessLayer.DTO.ResponseModel
{
    public class LoginResponse
    {
        public string Token { get; set; }
        public ResponseUser User { get; set; }
    }
}
