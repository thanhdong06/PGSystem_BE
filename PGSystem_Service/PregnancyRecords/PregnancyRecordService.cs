using AutoMapper;
using PGSystem_DataAccessLayer.DTO.RequestModel;
using PGSystem_DataAccessLayer.DTO.ResponseModel;
using PGSystem_DataAccessLayer.Entities;
using PGSystem_Repository.PregnancyRecords;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PGSystem_Service.PregnancyRecords
{
    public class PregnancyRecordService : IPregnancyRecordService
    {
        private readonly IPregnancyRecordRepository _repository;
        private readonly IMapper _mapper;

        public PregnancyRecordService(IPregnancyRecordRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<PregnancyRecordResponse> CreatePregnancyRecordAsync(PregnancyRecordRequest request)
        {
            var entity = _mapper.Map<PregnancyRecord>(request);
            entity.CreateAt = entity.UpdateAt = DateTime.UtcNow;

            var createdRecord = await _repository.AddAsync(entity);

            return _mapper.Map<PregnancyRecordResponse>(createdRecord);
        }
    }
}
