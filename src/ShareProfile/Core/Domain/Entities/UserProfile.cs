using Core.Persistence.Repositories;
using Core.Security.Entities;

namespace Domain.Entities
{
    public  class UserProfile : Entity
    {
        public string? LinkedInProfile { get; set; }
        public string? InstagramProfile { get; set; }
        public int UserId { get; set; }
        public virtual User? User { get; set; }
    }
}
