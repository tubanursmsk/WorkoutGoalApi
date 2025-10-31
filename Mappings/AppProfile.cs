using AutoMapper;
using WorkoutGoalApi.Models;
using WorkoutGoalApi.Dto.UserDto;

namespace WorkoutGoalApi.Mappings
{
    public class AppProfile : Profile
    {
        public AppProfile()
        {
            // User
            CreateMap<UserRegisterDto, User>();
            CreateMap<UserLoginDto, User>(); 
            CreateMap<User, UserJwtDto>(); // userdan userJwtDTO'ya dönüşüm
        }
    }
}