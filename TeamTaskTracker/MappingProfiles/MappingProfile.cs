using AutoMapper;
using TeamTaskTracker.DTOs;
using TeamTaskTracker.DTOs.User;
using TeamTaskTracker.Models;

namespace TeamTaskTracker.MappingProfiles;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<User, UserDto>()
            .ForMember(dest => dest.Password, opt => opt.Ignore()) // don’t expose password
            .ReverseMap()
            .ForMember(dest => dest.PasswordHash, opt => opt.Ignore()); // don’t map raw password to hash
        CreateMap<User, UserResponseDto>();
        CreateMap<Project, ProjectDto>().ReverseMap();
        CreateMap<TaskProject, TaskProjectDto>().ReverseMap();
        CreateMap<TaskActivity, TaskActivityDto>().ReverseMap();
    }
}
