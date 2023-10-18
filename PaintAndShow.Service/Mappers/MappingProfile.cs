using AutoMapper;
using PaintAndShow.Domain.Entities;
using PaintAndShow.Service.DTOs.Users;
using PaintAndShow.Service.DTOs.Photos;
using PaintAndShow.Service.DTOs.Friends;

namespace PaintAndShow.Service.Mappers;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // User
        CreateMap<User, UserResultDto>().ReverseMap();
        CreateMap<UserCreationDto, User>().ReverseMap();
        CreateMap<UserUpdateDto, User>().ReverseMap();

        // Photo
        CreateMap<Photo, PhotoResultDto>().ReverseMap();
        CreateMap<PhotoCreationDto, Photo>().ReverseMap();
        CreateMap<PhotoUpdateDto, Photo>().ReverseMap();
        // Friend
        CreateMap<Friend, FriendResultDto>().ReverseMap();
        CreateMap<FriendCreationDto, Friend>().ReverseMap();
        CreateMap<FriendUpdateDto, Friend>().ReverseMap();
    }
}
