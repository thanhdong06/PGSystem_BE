using AutoMapper;
using AutoMapper.Execution;
using PGSystem_DataAccessLayer.DTO.RequestModel;
using PGSystem_DataAccessLayer.DTO.ResponseModel;
using PGSystem_DataAccessLayer.Entities;
using PGSystem_Repository.Fetuss;
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
        private readonly IFetusRepository _fetusRepository;
        private readonly IMapper _mapper;

        public PregnancyRecordService(IFetusRepository fetusRepository,IPregnancyRecordRepository pregnancyRepository,IGrowthRecordRepository growthRecordRepository, IMapper mapper)
        {
            _pregnancyRepository = pregnancyRepository;
            _growthRecordRepository = growthRecordRepository;
            _fetusRepository = fetusRepository;
            _mapper = mapper;
        }
        public async Task<PregnancyRecordResponse> CreatePregnancyRecordAsync(PregnancyRecordRequest request)
        {
            var entity = _mapper.Map<PregnancyRecord>(request);

            entity.MemberMemberID = request.MemberMemberID;
            entity.StartDate = request.StartDate ?? DateOnly.FromDateTime(DateTime.Now);
            entity.DueDate = entity.StartDate.AddDays(280);
            entity.Status = "Tracking";
            entity.CreateAt = entity.UpdateAt = DateTime.Now;

            var createdRecord = await _pregnancyRepository.AddAsync(entity);

            foreach (var fetusReq in request.Fetuses)
            {
                var fetus = new Fetus
                {
                    Nickname = fetusReq.Nickname,
                    PregnancyRecordId = createdRecord.PID,
                };
                await _fetusRepository.AddAsync(fetus); 
            }

            return _mapper.Map<PregnancyRecordResponse>(createdRecord);
        }

        public async Task<PregnancyRecordResponse> ClosePregnancyRecordAsync(int pregnancyRecordId, int memberId)
        {
            var pregnancyRecord = await _pregnancyRepository.GetByIdAsync(pregnancyRecordId);

            if (pregnancyRecord == null)
            {
                throw new KeyNotFoundException("Pregnancy record not found");
            }
            if (pregnancyRecord.MemberMemberID != memberId)
            {
                throw new UnauthorizedAccessException("You do not have permission to close this pregnancy record.");
            }
            if (pregnancyRecord.Status == "Closed")
            {
                throw new InvalidOperationException("This pregnancy record is already closed.");
            }

            pregnancyRecord.Status = "Closed";
            pregnancyRecord.UpdateAt = DateTime.Now;

            await _pregnancyRepository.UpdateAsync(pregnancyRecord);

            return _mapper.Map<PregnancyRecordResponse>(pregnancyRecord);
        }

        public async Task<List<PregnancyRecordResponse>> GetByMemberIdAsync(int memberId)
        {
            var records = await _pregnancyRepository.GetPregnancyRecordByMemberIDAsync(memberId);
            return _mapper.Map<List<PregnancyRecordResponse>>(records);
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
