using Application.Behaviors.Logging;
using Application.Features.UserProfiles.Models;
using Application.Requests;
using Application.Wrappers;
using MediatR;

namespace Application.Features.UserProfiles.Queries.GetListUserProfileQuery
{
    public class GetListUserProfileQueryRequest:IRequest<Response<UserProfileListModel>> , ILoggableRequest 
    {
        public PageRequest PageRequest { get; set; }
    }
}
