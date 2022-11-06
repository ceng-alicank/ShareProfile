using Application.Features.UserProfiles.Commands.CreateUserProfileCommand;
using Application.Features.UserProfiles.Commands.UpdateUserProfileCommand;
using Application.Features.UserProfiles.Dtos;
using Application.Features.UserProfiles.Models;
using AutoMapper;
using Core.Persistence.Paging;
using Domain.Entities;

namespace Application.Mapping
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            CreateMap<CreateUserProfileCommandRequest, UserProfile>();
            CreateMap<UserProfile, CreateUserProfileCommandResponse>();
            CreateMap<UpdateUserProfileCommandRequest, UserProfile>();
            CreateMap<UserProfile, UpdateUserProfileCommandResponse>();

            CreateMap<IPaginate<UserProfile>, UserProfileListModel>().ReverseMap();
            CreateMap<UserProfile, UserProfileListDto>().ReverseMap();

        }
    }

}
