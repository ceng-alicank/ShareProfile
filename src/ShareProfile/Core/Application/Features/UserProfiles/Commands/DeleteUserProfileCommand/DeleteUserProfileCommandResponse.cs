namespace Application.Features.UserProfiles.Commands.DeleteUserProfileCommand
{
    public class DeleteUserProfileCommandResponse
    {
        public bool IsDeleted { get; set; }

        public DeleteUserProfileCommandResponse(bool isDeleted)
        {
            IsDeleted = isDeleted;
        }
    }
}
