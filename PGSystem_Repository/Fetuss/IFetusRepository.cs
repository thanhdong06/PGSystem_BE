﻿using PGSystem_DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PGSystem_Repository.Fetuss
{
    public interface IFetusRepository
    {
        Task<Fetus> AddAsync(Fetus fetus);
        Task<List<Fetus>> GetFetusByPregnancyRecordId(int pregnancyRecordId);
        Task<FetusMeasurement> AddMeasurementAsync(FetusMeasurement fetus);
        Task<bool> ExistsAsync(Expression<Func<Fetus, bool>> predicate);
        Task<bool> ExistsDateAsync(Expression<Func<FetusMeasurement, bool>> predicate);
        Task<FetusMeasurement> GetMeasurementByIdAsync(int measurementId);
        Task UpdateAsync(FetusMeasurement fetusMeasurement);
        Task<List<FetusMeasurement>> GetAllMeasurementByFetusId(int fetusId);
        Task<Fetus> GetFetusWithPregnancyAsync(int fetusId);
        Task<FetusMeasurement?> GetLatestBeforeWeekAsync(int fetusId, int currentWeek, int excludeMeasurementId);
        Task<FetusMeasurement?> GetPreviousMeasurementAsync(int fetusId, int currentWeek);
    }
}
