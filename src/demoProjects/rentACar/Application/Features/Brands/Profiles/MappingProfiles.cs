using Application.Features.Brands.Commands.CreateBrand;
using Application.Features.Brands.Dtos;
using Application.Features.Brands.Models;
using AutoMapper;
using Core.Persistence.Paging;
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
            CreateMap<IPaginate<Brand>, BrandListModel>().ReverseMap(); //IPaginate<Brand> i BrandListModel a çevir maple.ReverseMap ile tam tersi durumda geçerli demek
            CreateMap<Brand, BrandListDto>().ReverseMap();
            CreateMap<Brand, BrandGetByIdDto>().ReverseMap();
        }
    }
}
