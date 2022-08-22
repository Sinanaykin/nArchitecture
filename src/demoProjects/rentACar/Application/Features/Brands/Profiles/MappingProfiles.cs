using Application.Features.Brands.Commands.CreateBrand;
using Application.Features.Brands.Dtos;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Brands.Profiles
{
    public class MappingProfiles : Profile//Buraya mapleme profilleri yazılır. Automapper kütüphanesinden gelir Profile
    {
        public MappingProfiles()
        {
            CreateMap<Brand, CreatedBrandDto>().ReverseMap(); //Brand i CreatedBrandDto a çevir maple.ReverseMap ile tam tersi durumda geçerli demek
            CreateMap<Brand, CreateBrandCommand>().ReverseMap();//Brand i CreateBrandCommand a çevir maple.ReverseMap ile tam tersi durumda geçerli demek
        
        }
    }
}
