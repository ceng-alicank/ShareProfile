using Application.Interfaces.Caching;
using Application.Interfaces.Repositories;
using Application.Wrappers;
using MediatR;

namespace Application.Features.UserProfiles.Commands.DeleteUserProfileCommand
{
    public class DeleteUserProfileCommandHandler : IRequestHandler<DeleteUserProfileCommandRequest, Response<DeleteUserProfileCommandResponse>>
    {
        private readonly IUserProfileRepository _userProfileRepository;
        private readonly ICacheService _cacheService;

        public DeleteUserProfileCommandHandler(IUserProfileRepository userProfileRepository, ICacheService cacheService)
        {
            _userProfileRepository = userProfileRepository;
            _cacheService = cacheService;
        }

        public async Task<Response<DeleteUserProfileCommandResponse>> Handle(DeleteUserProfileCommandRequest request, CancellationToken cancellationToken)
        {
            var userProfileToCheck = await _userProfileRepository.GetAsync(p => p.UserId == request.UserId);
            if (userProfileToCheck !=null)
            {
                await _userProfileRepository.DeleteAsync(userProfileToCheck);
                await _cacheService.RemoveCache("getByUserIdUserProfile/" + request.UserId.ToString(), cancellationToken);
                await _cacheService.RemoveCache("getListUserProfile", cancellationToken);
                return new Response<DeleteUserProfileCommandResponse>(true);
            }
            return new Response<DeleteUserProfileCommandResponse>("Requested user not found");
        }
    }
}
