using AutoMapper;
using PaintAndShow.Data.IRepasitories;
using PaintAndShow.Domain.Entities;
using PaintAndShow.Service.DTOs.Friends;
using PaintAndShow.Service.DTOs.Users;
using PaintAndShow.Service.Exceptions;
using PaintAndShow.Service.Interfaces;

namespace PaintAndShow.Service.Services;

public class FriendService : IFrieandService
{
    private readonly IMapper mapper;
    private readonly IUserService userService;
    private readonly IRepasitory<User> userRepository;
    private readonly IRepasitory<Friend> friendRepository;
    public FriendService(IRepasitory<Friend> friendRepository,
        IRepasitory<User> repasitory,
        IUserService userService,
        IMapper mapper)
    {
        this.mapper = mapper;
        this.userService = userService;
        this.userRepository = repasitory;
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

    public async Task<bool> RemoveAsync(string username, long id)
    {
        User existUser = await this.userRepository.SelectAsync(u => u.UserName.Equals(username))
            ?? throw new NotFoundException($"This user is not found with name = {username}");
        long friendId = existUser.Id;
        IQueryable<Friend> idlar = this.friendRepository.SelectAll();
        var snow = idlar.FirstOrDefault(x => x.UserId == id && x.FriendsId == friendId)
            ?? throw new NotFoundException("This user is not your friend");

        this.friendRepository.Delete(snow);
        await this.friendRepository.SaveAsync();
        return true;
    }

    public async Task<List<UserResultDto>> RetrieveAllAsync(long id)
    {
        var users = this.friendRepository.SelectAll();

        List<long> friends = new();
        List<UserResultDto> dost = new List<UserResultDto>();

        foreach (var user in users)
        {
            
            friends.Add(user.FriendsId);
            
        }
        foreach(var idd in friends)
        {
            var data = await this.userService.RetrieveByIdAsync(idd);
            dost.Add(data);
        }
        return dost;
    }
}
