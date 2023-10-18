using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PaintAndShow.Data.IRepasitories;
using PaintAndShow.Domain.Entities;
using PaintAndShow.Service.DTOs.Friends;
using PaintAndShow.Service.DTOs.Photos;
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


    public async Task<PhotoResultDto> AddAsync(string name, string des, AttachmentCrDto dto)
    {
        Photo existUser = await this.photoRepository.SelectAsync(u => u.PhotoName.Equals(name));
        if (existUser is not null)
            throw new AlreadyExistException($"This photo is already exists with name = {name}");


        string aniqPath = photoWriter.GetPhoto(dto.formFile.FileName);
        PhotoCreationDto photo = new PhotoCreationDto
        {
            PhotoName = name,
            Description = des,
            Path = dto.formFile.FileName
        };
        var mappedPhoto = this.mapper.Map<Photo>(photo);
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

}
