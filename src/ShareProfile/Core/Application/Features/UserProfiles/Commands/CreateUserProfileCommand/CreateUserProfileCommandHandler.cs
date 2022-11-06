using Application.Services.CachingService;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Newtonsoft.Json;
using System.Text;

namespace Application.Features.UserProfiles.Commands.CreateUserProfileCommand
{
    public class CreateUserProfileCommandHandler:IRequestHandler<CreateUserProfileCommandRequest,  CreateUserProfileCommandResponse>
    {
        private readonly IUserProfileRepository _userProfileRepository;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly ICacheService _cacheService;

        public CreateUserProfileCommandHandler(IUserProfileRepository userProfileRepository, IMapper mapper, IUserRepository userRepository, ICacheService cacheService)
        {
            _userProfileRepository = userProfileRepository;
            _mapper = mapper;
            _userRepository = userRepository;
            _cacheService = cacheService;
        }
        public async Task<CreateUserProfileCommandResponse> Handle(CreateUserProfileCommandRequest request, CancellationToken cancellationToken)
        {
            
            
            var userToCheck = await _userRepository.GetAsync(u => u.Id == request.UserId);
            if (userToCheck == null) return null;
            var userProfileToCheck = await _userProfileRepository.GetAsync(u => u.UserId == request.UserId);
            if (userProfileToCheck != null) return null;
            UserProfile mappedUserProfile = _mapper.Map<UserProfile>(request);
            var addedUserProfile = await _userProfileRepository.AddAsync(mappedUserProfile);
            var response = _mapper.Map<CreateUserProfileCommandResponse>(addedUserProfile);

            byte[] serializeData = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(response));
            await _cacheService.AddCache("getByUserIdUserProfile/" + request.UserId.ToString(), serializeData, cancellationToken);
            return response;
        }
    }
}
