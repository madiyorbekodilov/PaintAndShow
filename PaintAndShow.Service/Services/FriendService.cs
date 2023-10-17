using AutoMapper;
using PaintAndShow.Data.IRepasitories;
using PaintAndShow.Domain.Entities;
using PaintAndShow.Service.DTOs.Friends;
using PaintAndShow.Service.DTOs.Users;
using PaintAndShow.Service.Exceptions;
using PaintAndShow.Service.Helpers;
using PaintAndShow.Service.Interfaces;
using System.Xml;

namespace PaintAndShow.Service.Services;

public class FriendService : IFrieandService
{
    private readonly IMapper mapper;
    private readonly IRepasitory<Friend> friendRepository;
    private readonly IRepasitory<User> userRepository;
    public FriendService(IRepasitory<Friend> friendRepository, IRepasitory<User> userRepository, IMapper mapper)
    {
        this.mapper = mapper;
        this.userRepository = userRepository;
        this.friendRepository = friendRepository;
    }
    public async Task<FriendResultDto> AddAsync(string name)
    {
        User existUser = await this.userRepository.SelectAsync(u => u.UserName.Equals(name));
        if (existUser is not null)
            throw new NotFoundException($"This user is already exists with username = {name}");

        FriendCreationDto dto = new FriendCreationDto();
        dto.UserId = 1;
        dto.FriendsId = existUser.Id;
        var mappedFriend = this.mapper.Map<Friend>(dto);
        await this.friendRepository.CreateAsync(mappedFriend);
        await this.userRepository.SaveAsync();

        var result = this.mapper.Map<FriendResultDto>(mappedFriend);
        return result;
    }

    public async Task<bool> RemoveAsync(long username)
    {
        User existUser = await this.userRepository.SelectAsync(u => u.UserName.Equals(username))
            ?? throw new NotFoundException($"This user is not found with name = {username}");

        // shetini togrla
        this.userRepository.Delete(existUser);
        await this.userRepository.SaveAsync();
        return true;
    }

    public Task<IEnumerable<User>> RetrieveAllAsync()
    {
        throw new NotImplementedException();
    }
}
