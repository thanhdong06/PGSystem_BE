using AutoMapper;
using PGSystem_DataAccessLayer.DTO.RequestModel;
using PGSystem_DataAccessLayer.DTO.ResponseModel;
using PGSystem_DataAccessLayer.Entities;
using PGSystem_Repository.Admin;
using PGSystem_Repository.Fetuss;
using PGSystem_Repository.PregnancyRecords;
using PGSystem_Repository.TransactionRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PGSystem_Service.Fetuses
{
    public class FetusService : IFetusService
    {
        private readonly IFetusRepository _fetusRepository;
        private readonly IPregnancyRecordRepository _pregnancyRecordRepository;
        private readonly IMapper _mapper;

        public FetusService(IFetusRepository fetusRepository, IMapper mapper, IPregnancyRecordRepository pregnancyRecordRepository)
        {
            _pregnancyRecordRepository = pregnancyRecordRepository;
            _fetusRepository = fetusRepository;
            _mapper = mapper;
        }
        public async Task<FetusResponse> CreateFetusAsync(FetusRequest request)
        {
            var pregnancyRecord = await _pregnancyRecordRepository.GetByIdAsync(request.PregnancyRecordId);
            if (pregnancyRecord == null)
            {
                throw new Exception("Pregnancy record not found.");
            }

            // 2. Kiểm tra nickname có bị trùng trong hồ sơ này không
            var isDuplicate = await _fetusRepository.ExistsAsync(
                f => f.PregnancyRecordId == request.PregnancyRecordId && f.Nickname == request.Nickname
            );

            if (isDuplicate)
            {
                throw new Exception("A fetus with this nickname already exists in the same pregnancy record.");
            }

            var fetus = _mapper.Map<Fetus>(request);
            var created = await _fetusRepository.AddAsync(fetus);
            return _mapper.Map<FetusResponse>(created);
        }

        public async Task<List<FetusResponse>> GetFetusesByPregnancyRecordIdAsync(int pregnancyRecordId)
        {
            var fetuses = await _fetusRepository.GetFetusByPregnancyRecordId(pregnancyRecordId);
            return _mapper.Map<List<FetusResponse>>(fetuses);
        }

        public async Task<FetusMeasurementResponse> CreateFetusMeasurementAsync(FetusMeasurementRequest request, int fetusId)
        {
            var isDuplicate = await _fetusRepository.ExistsAsync(
                f => f.FetusId == fetusId);
            if (isDuplicate)
            {
                throw new Exception("A fetus with this ID Not found.");
            }
            var fetusMeasurement = new FetusMeasurement
            {
                Length = request.Length,
                HeadCircumference = request.HeadCircumference,
                WeightEstimate = request.WeightEstimate,
                DateMeasured = request.DateMeasured,
                FetusId = fetusId
            };

            var createdMeasurement = await _fetusRepository.AddMeasurementAsync(fetusMeasurement);

            return _mapper.Map<FetusMeasurementResponse>(createdMeasurement);
        }

        public async Task<FetusMeasurement> UpdateFetusMeasurementAsync(int measurementId, FetusMeasurementUpdateRequest request)
        {
            var existingMeasurement = await _fetusRepository.GetMeasurementByIdAsync(measurementId);
            if (existingMeasurement == null)
            {
                throw new KeyNotFoundException("Measurement not found");
            }

            existingMeasurement.Length = request.Length;
            existingMeasurement.HeadCircumference = request.HeadCircumference;
            existingMeasurement.WeightEstimate = request.WeightEstimate;
            existingMeasurement.UpdatedAt = DateTime.Now;

            await _fetusRepository.UpdateAsync(existingMeasurement);

            return existingMeasurement;
        }
    }
}
