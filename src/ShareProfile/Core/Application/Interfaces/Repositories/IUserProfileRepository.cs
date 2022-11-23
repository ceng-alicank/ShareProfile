using Core.Persistence.Repositories;
using Domain.Entities;

namespace Application.Interfaces.Repositories
{
    public interface IUserProfileRepository : IAsyncRepository<UserProfile>
    {
    }
}
