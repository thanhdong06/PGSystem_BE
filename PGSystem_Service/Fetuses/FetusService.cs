using AutoMapper;
using Azure;
using PGSystem_DataAccessLayer.DTO.RequestModel;
using PGSystem_DataAccessLayer.DTO.ResponseModel;
using PGSystem_DataAccessLayer.Entities;
using PGSystem_Repository.Admin;
using PGSystem_Repository.Fetuss;
using PGSystem_Repository.PregnancyRecords;
using PGSystem_Repository.ThresholdRepository;
using PGSystem_Repository.TransactionRepository;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PGSystem_Service.Fetuses
{
    public class FetusService : IFetusService
    {
        private readonly IFetusRepository _fetusRepository;
        private readonly IPregnancyRecordRepository _pregnancyRecordRepository;
        private readonly IThresholdRepository _thresholdRepository;
        private readonly IMapper _mapper;

        public FetusService(IThresholdRepository thresholdRepository, IFetusRepository fetusRepository, IMapper mapper, IPregnancyRecordRepository pregnancyRecordRepository)
        {
            _pregnancyRecordRepository = pregnancyRecordRepository;
            _fetusRepository = fetusRepository;
            _thresholdRepository = thresholdRepository;
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
            var isDuplicate = await _fetusRepository.ExistsDateAsync(f => f.DateMeasured == request.DateMeasured);
            if (isDuplicate)
            {
                throw new Exception("You have finished measuring today!");
            }

            var lengthThreshold = await _thresholdRepository.GetThresholdByNameAsync("Length");
            var headCircumferenceThreshold = await _thresholdRepository.GetThresholdByNameAsync("HeadCircumference");
            var weightEstimateThreshold = await _thresholdRepository.GetThresholdByNameAsync("WeightEstimate");

            var warnings = new List<string>();

            if (lengthThreshold != null)
            {
                if (request.Length < lengthThreshold.MinValue || request.Length > lengthThreshold.MaxValue)
                {
                    warnings.Add(lengthThreshold.WarningMessage);
                }
            }
            if (headCircumferenceThreshold != null)
            {
                if (request.HeadCircumference < headCircumferenceThreshold.MinValue ||
                     request.HeadCircumference > headCircumferenceThreshold.MaxValue)
                {
                    warnings.Add(headCircumferenceThreshold.WarningMessage);
                }
            }
            if (weightEstimateThreshold != null)
            {
                if (request.WeightEstimate < weightEstimateThreshold.MinValue ||
                    request.WeightEstimate > weightEstimateThreshold.MaxValue)
                {
                    warnings.Add(weightEstimateThreshold.WarningMessage);
                }
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
            var response = _mapper.Map<FetusMeasurementResponse>(createdMeasurement);

            if (warnings.Any())
            {
                response.Warnings = warnings;
            }

            return response;
        }

        public async Task<FetusMeasurementResponse> UpdateFetusMeasurementAsync(int measurementId, FetusMeasurementUpdateRequest request)
        {
            var existingMeasurement = await _fetusRepository.GetMeasurementByIdAsync(measurementId);
            if (existingMeasurement == null)
            {
                throw new KeyNotFoundException("Measurement not found");
            }
            var lengthThreshold = await _thresholdRepository.GetThresholdByNameAsync("Length");
            var headCircumferenceThreshold = await _thresholdRepository.GetThresholdByNameAsync("HeadCircumference");
            var weightEstimateThreshold = await _thresholdRepository.GetThresholdByNameAsync("WeightEstimate");

            var warnings = new List<string>();

            if (lengthThreshold != null)
            {
                if (request.Length < lengthThreshold.MinValue || request.Length > lengthThreshold.MaxValue)
                {
                    warnings.Add(lengthThreshold.WarningMessage);
                }
            }
            if (headCircumferenceThreshold != null)
            {
                if (request.HeadCircumference < headCircumferenceThreshold.MinValue ||
                     request.HeadCircumference > headCircumferenceThreshold.MaxValue)
                {
                    warnings.Add(headCircumferenceThreshold.WarningMessage);
                }
            }
            if (weightEstimateThreshold != null)
            {
                if (request.WeightEstimate < weightEstimateThreshold.MinValue ||
                    request.WeightEstimate > weightEstimateThreshold.MaxValue)
                {
                    warnings.Add(weightEstimateThreshold.WarningMessage);
                }
            }

            existingMeasurement.Length = request.Length;
            existingMeasurement.HeadCircumference = request.HeadCircumference;
            existingMeasurement.WeightEstimate = request.WeightEstimate;
            existingMeasurement.UpdatedAt = DateTime.Now;

            await _fetusRepository.UpdateAsync(existingMeasurement);
            var response = _mapper.Map<FetusMeasurementResponse>(existingMeasurement);
            if (warnings.Any())
            {
                response.Warnings = warnings;
            }

            return response;
        }

        public async Task<List<FetusMeasurement>> GetFetusMeasurementsByFetusIdAsync(int fetusId)
        {
            var measurements = await _fetusRepository.GetAllMeasurementByFetusId(fetusId);

            if (measurements == null || !measurements.Any())
            {
                throw new KeyNotFoundException("No measurements found with this fetusId");
            }

            return measurements;
        }
    }
}
