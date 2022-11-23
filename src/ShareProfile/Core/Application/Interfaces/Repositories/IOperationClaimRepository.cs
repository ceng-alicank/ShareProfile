using Core.Persistence.Repositories;
using Core.Security.Entities;

namespace Application.Interfaces.Repositories
{
    public interface IOperationClaimRepository : IAsyncRepository<OperationClaim>
    {
    }
}
