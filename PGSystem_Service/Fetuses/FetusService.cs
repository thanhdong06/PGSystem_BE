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
            var isDuplicate = await _fetusRepository.ExistsDateAsync(f => f.DateMeasured == request.DateMeasured && f.FetusId == fetusId);
            if (isDuplicate)
            {
                throw new Exception("You have finished measuring today!");
            }

            var fetus = await _fetusRepository.GetFetusWithPregnancyAsync(fetusId);
            if (fetus == null || fetus.PregnancyRecord == null)
            {
                throw new Exception("Fetus or pregnancy record not found.");
            }

            var startDateTime = fetus.PregnancyRecord.StartDate.ToDateTime(TimeOnly.MinValue);
            var measuredDateTime = request.DateMeasured.ToDateTime(TimeOnly.MinValue);
            var week = (measuredDateTime - startDateTime).Days / 7;

            var lengthThreshold = await _thresholdRepository.GetThresholdByNameAsync("Length", week);
            var headCircumferenceThreshold = await _thresholdRepository.GetThresholdByNameAsync("HeadCircumference", week);
            var weightEstimateThreshold = await _thresholdRepository.GetThresholdByNameAsync("WeightEstimate", week);

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

            var fetus = await _fetusRepository.GetFetusWithPregnancyAsync(existingMeasurement.FetusId);
            if (fetus == null || fetus.PregnancyRecord == null)
            {
                throw new Exception("Fetus or Pregnancy record not found");
            }
            var startDateTime = fetus.PregnancyRecord.StartDate.ToDateTime(TimeOnly.MinValue);
            var measuredDateTime = existingMeasurement.DateMeasured.ToDateTime(TimeOnly.MinValue);
            var currentWeek = (measuredDateTime - startDateTime).Days / 7;

            var previousMeasurement = await _fetusRepository.GetLatestBeforeWeekAsync(fetus.FetusId, currentWeek);
            if (previousMeasurement != null)
            {
                if (request.Length < previousMeasurement.Length)
                {
                    throw new InvalidOperationException("Chiều dài không được nhỏ hơn lần đo tuần trước.");
                }
                if (request.HeadCircumference < previousMeasurement.HeadCircumference)
                {
                    throw new InvalidOperationException("Vòng đầu không được nhỏ hơn lần đo tuần trước.");
                }
                if (request.WeightEstimate < previousMeasurement.WeightEstimate)
                {
                    throw new InvalidOperationException("Cân nặng ước tính không được nhỏ hơn lần đo tuần trước.");
                }
            }

            var lengthThreshold = await _thresholdRepository.GetThresholdByNameAsync("Length", currentWeek);
            var headCircumferenceThreshold = await _thresholdRepository.GetThresholdByNameAsync("HeadCircumference", currentWeek);
            var weightEstimateThreshold = await _thresholdRepository.GetThresholdByNameAsync("WeightEstimate", currentWeek);

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
