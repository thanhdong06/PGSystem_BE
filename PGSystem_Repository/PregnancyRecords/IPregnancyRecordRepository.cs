using PGSystem_DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PGSystem_Repository.PregnancyRecords
{
    public interface IPregnancyRecordRepository
    {
        Task<PregnancyRecord> AddAsync(PregnancyRecord entity);
    }
}
