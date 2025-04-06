using PGSystem_DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PGSystem_Repository.Fetuss
{
    public interface IFetusRepository
    {
        Task<Fetus> AddAsync(Fetus fetus);
        Task<List<Fetus>> GetByPregnancyRecordId(int pregnancyRecordId);
    }
}
