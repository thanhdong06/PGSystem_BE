﻿using PGSystem_DataAccessLayer.DTO.RequestModel;
using PGSystem_DataAccessLayer.DTO.ResponseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PGSystem_Service.Fetuses
{
    public interface IFetusService
    {
        Task<FetusResponse> CreateFetusAsync(FetusRequest request);

    }
}
