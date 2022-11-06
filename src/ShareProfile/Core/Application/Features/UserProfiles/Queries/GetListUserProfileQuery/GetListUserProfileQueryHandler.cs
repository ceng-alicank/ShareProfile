using Application.Features.UserProfiles.Models;
using Application.Services.Repositories;
using AutoMapper;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.UserProfiles.Queries.GetListUserProfileQuery
{
    public class GetListUserProfileQueryHandler : IRequestHandler<GetListUserProfileQueryRequest, UserProfileListModel>
    {
        private readonly IUserProfileRepository _userProfileRepository;
        private readonly IMapper _mapper;

        public GetListUserProfileQueryHandler(IUserProfileRepository userProfileRepository, IMapper mapper)
        {
            _userProfileRepository = userProfileRepository;
            _mapper = mapper;
        }

        public async Task<UserProfileListModel> Handle(GetListUserProfileQueryRequest request, CancellationToken cancellationToken)
        {
            IPaginate<UserProfile> userProfiles = await _userProfileRepository.GetListAsync(
                                             index: request.PageRequest.Page,
                                             size: request.PageRequest.PageSize
                                             );
            return _mapper.Map<UserProfileListModel>(userProfiles);
        }
    }
}
