using Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.UserProfiles.Queries.GetByUserIdUserProfileQuery
{
    public class GetByUserIdUserProfileQueryRequest:IRequest<Response<GetByUserIdUserProfileQueryResponse>>
    {
        public int UserId { get; set; }
    }
}
