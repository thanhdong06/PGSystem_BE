﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PGSystem_DataAccessLayer.DTO.RequestModel
{
    public class ReminderUpdateRequest
    {
        public string Title { get; set; }
        //public string? Description { get; set; }
        public DateTime DateTime { get; set; }
    }
}
