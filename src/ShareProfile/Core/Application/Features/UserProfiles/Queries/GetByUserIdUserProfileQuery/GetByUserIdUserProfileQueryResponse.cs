namespace Application.Features.UserProfiles.Queries.GetByUserIdUserProfileQuery
{
    public class GetByUserIdUserProfileQueryResponse
    {
        public string? LinkedInProfile { get; set; }
        public string? InstagramProfile { get; set; }

        public GetByUserIdUserProfileQueryResponse(string? linkedInProfile, string? instagramProfile)
        {
            LinkedInProfile = linkedInProfile;
            InstagramProfile = instagramProfile;
        }
    }
}
