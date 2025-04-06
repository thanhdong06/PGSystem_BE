using PGSystem_DataAccessLayer.DTO.RequestModel;
using PGSystem_DataAccessLayer.DTO.ResponseModel;
using PGSystem_DataAccessLayer.Entities;
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
        Task<List<FetusResponse>> GetFetusesByPregnancyRecordIdAsync(int pregnancyRecordId);
        Task<FetusMeasurementResponse> CreateFetusMeasurementAsync(FetusMeasurementRequest request, int fetusId);
    }
}
