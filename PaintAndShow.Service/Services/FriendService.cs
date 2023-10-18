using AutoMapper;
using Microsoft.EntityFrameworkCore;
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
    public async Task<FriendResultDto> AddAsync(string name, long id)
    {
        User existUser = await this.userRepository.SelectAsync(u => u.UserName.Equals(name));
        if (existUser is null)
            throw new NotFoundException($"This user is already exists with username = {name}");

        FriendCreationDto dto = new FriendCreationDto();
        dto.UserId = id;
        dto.FriendsId = existUser.Id;
        var mappedFriend = this.mapper.Map<Friend>(dto);
        await this.friendRepository.CreateAsync(mappedFriend);
        await this.userRepository.SaveAsync();

        var result = this.mapper.Map<FriendResultDto>(mappedFriend);
        return result;
    }

    public async Task<bool> RemoveAsync(string username , long id)
    {
        User existUser = await this.userRepository.SelectAsync(u => u.UserName.Equals(username))
            ?? throw new NotFoundException($"This user is not found with name = {username}");
        long friendId = existUser.Id;
        IQueryable<Friend> idlar = this.friendRepository.SelectAll();
        var snow = idlar.FirstOrDefault(x => x.UserId == id && x.FriendsId == friendId)
            ?? throw new NotFoundException("This user is not your friend");

        this.friendRepository.Delete(snow);
        await this.userRepository.SaveAsync();
        return true;
    }

    public Task<IEnumerable<User>> RetrieveAllAsync()
    {
        throw new NotImplementedException();
    }
}
