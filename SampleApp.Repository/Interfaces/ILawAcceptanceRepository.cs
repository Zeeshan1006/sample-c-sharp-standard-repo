using SampleApp.Entities;
using System.Threading.Tasks;

namespace SampleApp.Repository.Interfaces
{
    public interface ILawAcceptanceRepository
    {
        Task<bool> AcceptBusinessReq(AcceptBusinessRequirement accept);
    }
}
