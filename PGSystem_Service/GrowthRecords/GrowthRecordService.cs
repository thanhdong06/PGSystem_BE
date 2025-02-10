using AutoMapper;
using Azure.Core;
using Microsoft.EntityFrameworkCore;
using PGSystem_DataAccessLayer.DBContext;
using PGSystem_DataAccessLayer.DTO.RequestModel;
using PGSystem_DataAccessLayer.DTO.ResponseModel;
using PGSystem_DataAccessLayer.Entities;
using PGSystem_Repository.GrowthRecords;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PGSystem_Service.GrowthRecords
{
    public class GrowthRecordService : IGrowthRecordService
    {
        private readonly IGrowthRecordRepository _growthRecordRepository;
        private readonly IMapper _mapper;

        public GrowthRecordService(IGrowthRecordRepository growthRecordRepository, IMapper mapper)
        {
            _growthRecordRepository = growthRecordRepository;
            _mapper = mapper;
        }
        public async Task<GrowthRecordResponse> UpdateGrowthRecordAsync(GrowthRecordRequest request)
        {
            var existingRecord = await _growthRecordRepository.GetGrowthRecordByPidAsync(request.PID, request.Week);

            if (existingRecord == null)
            {
                throw new KeyNotFoundException($"No growth record found for PID {request.PID} at week {request.Week}.");
            }

            existingRecord.Weight = request.Weight;
            existingRecord.Height = request.Height;
            existingRecord.UpdateAt = DateTime.Now;

            var updatedRecord = await _growthRecordRepository.UpdateGrowthRecordAsync(existingRecord);

            return _mapper.Map<GrowthRecordResponse>(updatedRecord);
        }
    }
}
