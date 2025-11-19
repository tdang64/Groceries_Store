using AutoMapper;
using Group4_Project.Models;
using Group4_Project.DTOs;

public class SupplierProfile : Profile
{
    public SupplierProfile()
    {
        CreateMap<Supplier, SupplierReadDTO>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));

        CreateMap<SupplierCreateDTO, Supplier>();

        CreateMap<SupplierUpdateDTO, Supplier>();
    }
}
