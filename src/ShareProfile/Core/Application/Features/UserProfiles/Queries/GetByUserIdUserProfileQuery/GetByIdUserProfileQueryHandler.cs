using Application.Services.CachingService;
using Application.Services.Repositories;
using Application.Wrappers;
using AutoMapper;
using MediatR;
using Newtonsoft.Json;
using System.Text;

namespace Application.Features.UserProfiles.Queries.GetByUserIdUserProfileQuery
{
    public class GetByUserIdUserProfileQueryHandler : IRequestHandler<GetByUserIdUserProfileQueryRequest, Response<GetByUserIdUserProfileQueryResponse>>
    {
        private readonly IUserProfileRepository _userProfileRepository;
        private readonly ICacheService _cacheService;
        private readonly IMapper _mapper;

        public GetByUserIdUserProfileQueryHandler(IUserProfileRepository userProfileRepository, ICacheService cacheService, IMapper mapper)
        {
            _userProfileRepository = userProfileRepository;
            _cacheService = cacheService;
            _mapper = mapper;
        }

        public async Task<Response<GetByUserIdUserProfileQueryResponse>> Handle(GetByUserIdUserProfileQueryRequest request, CancellationToken cancellationToken)
        {
            var checkCache = await _cacheService.GetCache("getByUserIdUserProfile/" + request.UserId.ToString(), cancellationToken);
            if (checkCache != null)
            {
                var responseData =  JsonConvert.DeserializeObject<GetByUserIdUserProfileQueryResponse>(Encoding.Default.GetString(checkCache));
                return new Response<GetByUserIdUserProfileQueryResponse>(responseData);
            }
            var userProfileToCheck = await _userProfileRepository.GetAsync(p => p.UserId == request.UserId);
            if (userProfileToCheck != null)
            {
                var responseData = _mapper.Map<GetByUserIdUserProfileQueryResponse>(userProfileToCheck);
                byte[] serializeData = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(responseData));
                await _cacheService.SetCache("getByUserIdUserProfile/" + request.UserId.ToString(), serializeData, cancellationToken);
                return new Response<GetByUserIdUserProfileQueryResponse>
                    (responseData);
            }
            return new Response<GetByUserIdUserProfileQueryResponse>("There is no data ");
        }
    }
}
