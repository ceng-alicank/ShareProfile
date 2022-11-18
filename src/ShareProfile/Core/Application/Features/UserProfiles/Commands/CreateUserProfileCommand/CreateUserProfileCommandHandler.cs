using Application.Services.CachingService;
using Application.Services.Repositories;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Newtonsoft.Json;
using System.Text;

namespace Application.Features.UserProfiles.Commands.CreateUserProfileCommand
{
    public class CreateUserProfileCommandHandler:IRequestHandler<CreateUserProfileCommandRequest, Response<CreateUserProfileCommandResponse>>
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
        public async Task<Response<CreateUserProfileCommandResponse>> Handle(CreateUserProfileCommandRequest request, CancellationToken cancellationToken)
        {
            var userToCheck = await _userRepository.GetAsync(u => u.Id == request.UserId);
            if (userToCheck != null)
            {
                var userProfileToCheck = await _userProfileRepository.GetAsync(u => u.UserId == request.UserId);
                if (userProfileToCheck == null)
                {
                    UserProfile mappedUserProfile = _mapper.Map<UserProfile>(request);
                    var addedUserProfile = await _userProfileRepository.AddAsync(mappedUserProfile);
                    var responseData = _mapper.Map<CreateUserProfileCommandResponse>(addedUserProfile);
                    byte[] serializeData = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(responseData));
                    await _cacheService.SetCache("getByUserIdUserProfile/" + request.UserId.ToString(), serializeData, cancellationToken);
                    await _cacheService.RemoveCache("getListUserProfile" , cancellationToken);
                    return new Response<CreateUserProfileCommandResponse>(responseData);
                }
            }
            return new Response<CreateUserProfileCommandResponse>("Somethings have gone wrong");
        }
    }
}
