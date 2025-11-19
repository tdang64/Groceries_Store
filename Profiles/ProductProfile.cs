using AutoMapper;
using Group4_Project.Models;
using Group4_Project.DTOs;

public class ProductProfile : Profile
{
    public ProductProfile()
    {
 
        CreateMap<Product, ProductReadDTO>();

       
        CreateMap<ProductCreateDTO, Product>();

       
        CreateMap<ProductUpdateDTO, Product>();
    }
}
