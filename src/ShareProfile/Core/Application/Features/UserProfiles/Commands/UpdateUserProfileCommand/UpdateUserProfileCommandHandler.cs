using Application.Interfaces.Caching;
using Application.Interfaces.Repositories;
using Application.Wrappers;
using AutoMapper;
using MediatR;
using Newtonsoft.Json;
using System.Text;

namespace Application.Features.UserProfiles.Commands.UpdateUserProfileCommand
{
    public class UpdateUserProfileCommandHandler : IRequestHandler<UpdateUserProfileCommandRequest, Response<UpdateUserProfileCommandResponse>>
    {
        private readonly IMapper _mapper;
        private readonly IUserProfileRepository _userProfileRepository;

        private readonly ICacheService _cacheService;
        public UpdateUserProfileCommandHandler(IMapper mapper, IUserProfileRepository userProfileRepository, ICacheService cacheService)
        {
            _mapper = mapper;
            _userProfileRepository = userProfileRepository;
            _cacheService = cacheService;
        }

        public async Task<Response<UpdateUserProfileCommandResponse>> Handle(UpdateUserProfileCommandRequest request, CancellationToken cancellationToken)
        {
            var userProfileToCheck = await _userProfileRepository.GetAsync(p => p.UserId == request.UserId);
            if (userProfileToCheck != null)
            {
                userProfileToCheck.LinkedInProfile = request.LinkedInProfile;
                userProfileToCheck.InstagramProfile = request.InstagramProfile;
                var updatedUserProfile = await _userProfileRepository.UpdateAsync(userProfileToCheck);
                var response = _mapper.Map<UpdateUserProfileCommandResponse>(updatedUserProfile);
                byte[] serializeData = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(response));
                await _cacheService.SetCache("getByUserIdUserProfile/" + request.UserId.ToString(), serializeData, cancellationToken);
                await _cacheService.RemoveCache("getListUserProfile", cancellationToken);
                return new Response<UpdateUserProfileCommandResponse>(true);
            }
            return new Response<UpdateUserProfileCommandResponse>("asdd");


        }
    }
}
