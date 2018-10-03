using System.Threading.Tasks;
using SampleApp.Entities;

namespace SampleApp.Services.Interfaces
{
    public interface ILawAcceptanceService
    {
        Task<bool> AcceptBusinessRequirement(AcceptBusinessRequirement accept);
    }
}
