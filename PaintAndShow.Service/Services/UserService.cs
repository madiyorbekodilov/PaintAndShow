using AutoMapper;
using PaintAndShow.Service.Helpers;
using PaintAndShow.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using PaintAndShow.Data.IRepasitories;
using PaintAndShow.Service.DTOs.Users;
using PaintAndShow.Service.Interfaces;
using PaintAndShow.Service.Exceptions;

namespace PaintAndShow.Service.Services;

public class UserService : IUserService
{
    private readonly IMapper mapper;
    private readonly IRepasitory<User> userRepository;
    public UserService(IRepasitory<User> userRepository, IMapper mapper)
    {
        this.mapper = mapper;
        this.userRepository = userRepository;
    }
    public async Task<UserResultDto> AddAsync(UserCreationDto dto)
    {
        User existUser = await this.userRepository.SelectAsync(u => u.UserName.Equals(dto.UserName));
        if (existUser is not null)
            throw new AlreadyExistException($"This user is already exists with username = {dto.UserName}");

        var mappedUser = this.mapper.Map<User>(dto);
        mappedUser.Password = PasswordHash.Encrypt(mappedUser.Password);
        await this.userRepository.CreateAsync(mappedUser);
        await this.userRepository.SaveAsync();

        var result = this.mapper.Map<UserResultDto>(mappedUser);
        return result;
    }

    public async Task<UserResultDto> ModifyAsync(UserUpdateDto dto)
    {
        User existUser = await this.userRepository.SelectAsync(u => u.Id.Equals(dto.Id))
           ?? throw new NotFoundException($"This user is not found with ID = {dto.Id}");

        this.mapper.Map(dto, existUser);
        existUser.Password = PasswordHash.Encrypt(dto.Password);
        this.userRepository.Update(existUser);
        await this.userRepository.SaveAsync();

        var result = this.mapper.Map<UserResultDto>(existUser);
        return result;
    }

    public async Task<bool> RemoveAsync(long id)
    {
        User existUser = await this.userRepository.SelectAsync(u => u.Id.Equals(id))
            ?? throw new NotFoundException($"This user is not found with ID = {id}");

        this.userRepository.Delete(existUser);
        await this.userRepository.SaveAsync();
        return true;
    }

    public async Task<UserResultDto> RetrieveByIdAsync(long id)
    {
        User existUser = await this.userRepository.SelectAsync(u => u.Id.Equals(id))
           ?? throw new NotFoundException($"This user is not found with ID = {id}");

        var result = this.mapper.Map<UserResultDto>(existUser);
        return result;
    }
    public async Task<IEnumerable<UserResultDto>> RetrieveAllAsync()
    {
        var users = await this.userRepository.SelectAll().ToListAsync();
        var mappedUsers = this.mapper.Map<List<UserResultDto>>(users);
        return mappedUsers;
    }

}
