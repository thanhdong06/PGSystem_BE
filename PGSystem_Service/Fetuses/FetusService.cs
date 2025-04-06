using AutoMapper;
using PGSystem_DataAccessLayer.DTO.RequestModel;
using PGSystem_DataAccessLayer.DTO.ResponseModel;
using PGSystem_DataAccessLayer.Entities;
using PGSystem_Repository.Admin;
using PGSystem_Repository.Fetuss;
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
        private readonly IMapper _mapper;

        public FetusService(IFetusRepository fetusRepository, IMapper mapper)
        {
            _fetusRepository = fetusRepository;
            _mapper = mapper;
        }
        public async Task<FetusResponse> CreateFetusAsync(FetusRequest request)
        {
            var fetus = _mapper.Map<Fetus>(request);
            var created = await _fetusRepository.AddAsync(fetus);
            return _mapper.Map<FetusResponse>(created);
        }
    }
}
