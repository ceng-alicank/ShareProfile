using Application.Services.CachingService;
using Application.Services.Repositories;
using MediatR;

namespace Application.Features.UserProfiles.Commands.DeleteUserProfileCommand
{
    public class DeleteUserProfileCommandHandler : IRequestHandler<DeleteUserProfileCommandRequest, DeleteUserProfileCommandResponse>
    {
        private readonly IUserProfileRepository _userProfileRepository;
        private readonly ICacheService _cacheService;

        public DeleteUserProfileCommandHandler(IUserProfileRepository userProfileRepository, ICacheService cacheService)
        {
            _userProfileRepository = userProfileRepository;
            _cacheService = cacheService;
        }

        public async Task<DeleteUserProfileCommandResponse> Handle(DeleteUserProfileCommandRequest request, CancellationToken cancellationToken)
        {
            await _cacheService.RemoveCache("getByUserIdUserProfile/" + request.UserId.ToString(), cancellationToken);
            var userProfileToCheck = await _userProfileRepository.GetAsync(p => p.UserId == request.UserId);
            if (userProfileToCheck !=null)
            {
                await _userProfileRepository.DeleteAsync(userProfileToCheck);
                return new DeleteUserProfileCommandResponse(true);
            }
            return new DeleteUserProfileCommandResponse(false);
        }
    }
}
