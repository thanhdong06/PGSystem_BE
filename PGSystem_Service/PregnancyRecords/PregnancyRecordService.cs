using AutoMapper;
using PGSystem_DataAccessLayer.DTO.RequestModel;
using PGSystem_DataAccessLayer.DTO.ResponseModel;
using PGSystem_DataAccessLayer.Entities;
using PGSystem_Repository.GrowthRecords;
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
        private readonly IPregnancyRecordRepository _pregnancyRepository;
        private readonly IGrowthRecordRepository _growthRecordRepository;
        private readonly IMapper _mapper;

        public PregnancyRecordService(IPregnancyRecordRepository pregnancyRepository,IGrowthRecordRepository growthRecordRepository, IMapper mapper)
        {
            _pregnancyRepository = pregnancyRepository;
            _growthRecordRepository = growthRecordRepository;
            _mapper = mapper;
        }
        public async Task<PregnancyRecordResponse> CreatePregnancyRecordAsync(PregnancyRecordRequest request)
        {
            var entity = _mapper.Map<PregnancyRecord>(request);
            entity.MemberMemberID = request.MemberMemberID;


            entity.StartDate = DateOnly.FromDateTime(DateTime.Now);
            entity.DueDate = entity.StartDate.AddDays(280);

            entity.CreateAt = entity.UpdateAt = DateTime.Now;

            var createdRecord = await _pregnancyRepository.AddAsync(entity);
            await CreateGrowthRecordsForPregnancy(createdRecord.PID);

            return _mapper.Map<PregnancyRecordResponse>(createdRecord);
        }

        public async Task CreateGrowthRecordsForPregnancy(int pregnancyRecordId)
        {
            var growthRecords = new List<GrowthRecord>();

            for (int week = 1; week <= 40; week++)
            {
                var growthRecord = new GrowthRecord
                {
                    PID = pregnancyRecordId,
                    Week = week,
                    Weight = 0,
                    Height = 0,
                    UpdateAt = DateTime.Now
                };
                growthRecords.Add(growthRecord);
            }
            foreach (var record in growthRecords)
            {
                await _growthRecordRepository.AddAsync(record);
            }
        }
    }
}
