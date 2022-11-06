using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.UserProfiles.Dtos
{
    public class UserProfileListDto
    {
        public int UserId { get; set; }
        public string InstagramProfile { get; set; }
        public string LinkedInProfile { get; set; }
    }
}
