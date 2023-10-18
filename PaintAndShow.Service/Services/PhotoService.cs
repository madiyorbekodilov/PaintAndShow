using AutoMapper;
using Microsoft.EntityFrameworkCore;
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
    PhotoWriter photoWriter = new PhotoWriter();
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


        string aniqPath = photoWriter.GetPhoto(dto.Path);
        var mappedPhoto = this.mapper.Map<Photo>(dto);
        mappedPhoto.Path = aniqPath;
        await this.photoRepository.CreateAsync(mappedPhoto);
        await this.photoRepository.SaveAsync();

        var result = this.mapper.Map<PhotoResultDto>(mappedPhoto);
        return result;
    }

    public async Task<bool> RemoveAsync(string photoName)
    {
        Photo existUser = await this.photoRepository.SelectAsync(u => u.PhotoName.Equals(photoName))
            ?? throw new NotFoundException($"This photo is not found with name = {photoName}");

        this.photoRepository.Delete(existUser);
        await this.photoRepository.SaveAsync();
        return true;
    }

    public async Task<IEnumerable<PhotoResultDto>> RetrieveAllAsync()
    {
        var photos = await this.photoRepository.SelectAll().ToListAsync();
        var mappedPhotos = this.mapper.Map<List<PhotoResultDto>>(photos);
        return mappedPhotos;
    }

    public async Task<PhotoResultDto> RetrieveByIdAsync(string photoName)
    {
        Photo existPhoto = await this.photoRepository.SelectAsync(u => u.PhotoName.Equals(photoName))
           ?? throw new NotFoundException($"This photo is not found with Name = {photoName}");

        var result = this.mapper.Map<PhotoResultDto>(existPhoto);
        return result;
    }
}
