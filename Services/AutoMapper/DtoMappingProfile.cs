using AutoMapper;
using Domain.Models;
using Shared.DTOs.MainDTOs;
using Shared.DTOs.ViewModels;

namespace Services.Concretes.AutoMapper;

public class DtoMappingProfile : Profile
{
    public DtoMappingProfile()
    {
        CreateMap<Category, CategoryDto>();
        CreateMap<Category, CategoryViewModel>();
        CreateMap<SubCategory, SubCategoryDto>();
        CreateMap<SubCategory, SubCategoryViewModel>()
            .ForMember(d => d.CategoryName, opt => opt.MapFrom(s => s.Category!.CategoryName));
    }
}
