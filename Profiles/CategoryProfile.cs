using AutoMapper;
using Group4_Project.Models;
using Group4_Project.DTOs;

public class CategoryProfile : Profile
{
    public CategoryProfile()
    {
    
        CreateMap<Category, CategoryReadDTO>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));

       
        CreateMap<CategoryCreateDTO, Category>();

      
        CreateMap<CategoryUpdateDTO, Category>();
    }
}
