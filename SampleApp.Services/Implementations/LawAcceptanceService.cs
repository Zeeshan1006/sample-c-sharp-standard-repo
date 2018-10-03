using System.Threading.Tasks;
using SampleApp.Repository.Interfaces;
using SampleApp.Services.Interfaces;
using SampleApp.Entities;

namespace SampleApp.Services.Implementations
{
    public class LawAcceptanceService : ILawAcceptanceService
    {
        private readonly ILawAcceptanceRepository _lawAcceptanceRepository;

        public LawAcceptanceService()
        {

        }

        public LawAcceptanceService(ILawAcceptanceRepository lawAcceptanceRepository)
        {
            _lawAcceptanceRepository = lawAcceptanceRepository;
        }

        public async Task<bool> AcceptBusinessRequirement(AcceptBusinessRequirement accept)
        {
            var result = await _lawAcceptanceRepository.AcceptBusinessReq(accept);
            return result;
        }
    }
}
