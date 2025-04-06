using PGSystem_DataAccessLayer.DTO.RequestModel;
using PGSystem_DataAccessLayer.DTO.ResponseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PGSystem_Service.PregnancyRecords
{
    public interface IPregnancyRecordService
    {
        Task<PregnancyRecordResponse> CreatePregnancyRecordAsync(PregnancyRecordRequest request);
        Task<List<PregnancyRecordResponse>> GetByMemberIdAsync(int memberId);
        Task<PregnancyRecordResponse> ClosePregnancyRecordAsync(int pregnancyRecordId, int memberId);

    }
}
