using AutoMapper;
using Group4_Project.DTOs;
using Group4_Project.Models;

namespace Group4_Project.Profiles
{
    public class SupplierProfile : Profile
    {
        public SupplierProfile()
        {
            CreateMap<Supplier, SupplierReadDTO>();
            CreateMap<SupplierCreateDTO, Supplier>();
            CreateMap<SupplierUpdateDTO, Supplier>();
        }
    }
}
