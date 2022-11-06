using Application.Features.UserProfiles.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.UserProfiles.Models
{
    public class UserProfileListModel
    {
        public IList<UserProfileListDto> Items { get; set; }
    }
}
