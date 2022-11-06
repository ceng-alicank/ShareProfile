using Application.Services.CachingService;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Newtonsoft.Json;
using System.Text;

namespace Application.Features.UserProfiles.Commands.UpdateUserProfileCommand
{
    public class UpdateUserProfileCommandHandler : IRequestHandler<UpdateUserProfileCommandRequest, UpdateUserProfileCommandResponse>
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

        public async Task<UpdateUserProfileCommandResponse> Handle(UpdateUserProfileCommandRequest request, CancellationToken cancellationToken)
        {
            await _cacheService.RemoveCache("getByUserIdUserProfile/" + request.UserId.ToString(), cancellationToken);

            var userProfileToCheck = await _userProfileRepository.GetAsync(p => p.UserId == request.UserId);
            if (userProfileToCheck == null) return null;

            userProfileToCheck.LinkedInProfile = request.LinkedInProfile;
            userProfileToCheck.InstagramProfile = request.InstagramProfile;
            var updatedUserProfile = await _userProfileRepository.UpdateAsync(userProfileToCheck);
            var response = _mapper.Map<UpdateUserProfileCommandResponse>(updatedUserProfile);

            byte[] serializeData = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(response));
            await _cacheService.AddCache("getByUserIdUserProfile/" + request.UserId.ToString(), serializeData, cancellationToken);

            return response;

        }
    }
}
