using Application.Features.UserProfiles.Models;
using Application.Services.CachingService;
using Application.Services.Repositories;
using Application.Wrappers;
using AutoMapper;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;
using Newtonsoft.Json;
using System.Text;

namespace Application.Features.UserProfiles.Queries.GetListUserProfileQuery
{
    public class GetListUserProfileQueryHandler : IRequestHandler<GetListUserProfileQueryRequest, Response<UserProfileListModel>>
    {
        private readonly IUserProfileRepository _userProfileRepository;
        private readonly ICacheService _cacheService;
        private readonly IMapper _mapper;
        public GetListUserProfileQueryHandler(IUserProfileRepository userProfileRepository, ICacheService cacheService, IMapper mapper)
        {
            _userProfileRepository = userProfileRepository;
            _cacheService = cacheService;
            _mapper = mapper;
        }
        public async Task<Response<UserProfileListModel>> Handle(GetListUserProfileQueryRequest request, CancellationToken cancellationToken)
        {
            var checkCache = await _cacheService.GetCache("getListUserProfile", cancellationToken);
            if (checkCache != null)
            {
                var responseData = JsonConvert.DeserializeObject<UserProfileListModel>(Encoding.Default.GetString(checkCache));
                return new Response<UserProfileListModel>(responseData);

            }
            IPaginate<UserProfile> userProfiles = await _userProfileRepository.GetListAsync(
                                             index: request.PageRequest.Page,
                                             size: request.PageRequest.PageSize
                                             );
            var userProfileListModel = _mapper.Map<UserProfileListModel>(userProfiles);
            byte[] serializeData = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(userProfileListModel));
            await _cacheService.SetCache("getListUserProfile", serializeData, cancellationToken);
            return new Response<UserProfileListModel>(userProfileListModel);
        }
    }
}
