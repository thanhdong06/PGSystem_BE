﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PGSystem_DataAccessLayer.DTO.RequestModel
{
    public class BlogRequest
    {
        public string Title { get; set; }
        public string? Content { get; set; }
        public string? Type { get; set; }
        public int AID { get; set; }
    }
}
