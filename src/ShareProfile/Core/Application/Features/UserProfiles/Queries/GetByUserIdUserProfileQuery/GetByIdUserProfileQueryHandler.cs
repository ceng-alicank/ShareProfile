using Application.Services.Repositories;
using AutoMapper;
using MediatR;

namespace Application.Features.UserProfiles.Queries.GetByUserIdUserProfileQuery
{
    public class GetByUserIdUserProfileQueryHandler : IRequestHandler<GetByUserIdUserProfileQueryRequest, GetByUserIdUserProfileQueryResponse>
    {
        private readonly IUserProfileRepository _userProfileRepository;
        private readonly IUserRepository _userRepository;

        public GetByUserIdUserProfileQueryHandler(IUserProfileRepository userProfileRepository, IUserRepository userRepository)
        {
            _userProfileRepository = userProfileRepository;
            _userRepository = userRepository;
        }

        public async Task<GetByUserIdUserProfileQueryResponse> Handle(GetByUserIdUserProfileQueryRequest request, CancellationToken cancellationToken)
        {
            var userProfileToCheck = await _userProfileRepository.GetAsync(p => p.UserId == request.UserId);
            if (userProfileToCheck == null) return null;
            return new GetByUserIdUserProfileQueryResponse(
                linkedInProfile: userProfileToCheck.LinkedInProfile,
                instagramProfile: userProfileToCheck.InstagramProfile
                );
        }
    }
}
