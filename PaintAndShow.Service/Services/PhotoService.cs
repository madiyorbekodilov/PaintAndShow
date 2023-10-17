using AutoMapper;
using PaintAndShow.Data.IRepasitories;
using PaintAndShow.Domain.Entities;
using PaintAndShow.Service.DTOs.Photos;
using PaintAndShow.Service.DTOs.Users;
using PaintAndShow.Service.Exceptions;
using PaintAndShow.Service.Helpers;
using PaintAndShow.Service.Interfaces;

namespace PaintAndShow.Service.Services;

public class PhotoService : IPhotoService
{
    private readonly IMapper mapper;
    private readonly IRepasitory<Photo> photoRepository;
    public PhotoService(IRepasitory<Photo> photoRepository, IMapper mapper)
    {
        this.mapper = mapper;
        this.photoRepository = photoRepository;
    }
    public async Task<PhotoResultDto> AddAsync(PhotoCreationDto dto)
    {
        Photo existUser = await this.photoRepository.SelectAsync(u => u.PhotoName.Equals(dto.PhotoName));
        if (existUser is not null)
            throw new AlreadyExistException($"This photo is already exists with name = {dto.PhotoName}");

        var mappedPhoto = this.mapper.Map<Photo>(dto);
        await this.photoRepository.CreateAsync(mappedPhoto);
        await this.photoRepository.SaveAsync();

        var result = this.mapper.Map<PhotoResultDto>(mappedPhoto);
        return result;
    }

    public Task<bool> RemoveAsync(long id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<PhotoResultDto>> RetrieveAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<PhotoResultDto> RetrieveByIdAsync(long id)
    {
        throw new NotImplementedException();
    }
}
