﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PGSystem_DataAccessLayer.DTO.RequestModel
{
    public class LoginGoogleRequest
    {
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Role { get; set; }
    }
}
