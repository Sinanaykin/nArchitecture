using Application.Features.Models.Dtos;
using Application.Features.Models.Models;
using AutoMapper;
using Core.Persistence.Paging;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Models.Profiles
{
    public class MappingProfiles:Profile
    {
        public MappingProfiles()
        {
            CreateMap<Model, ModelListDto>().ForMember(c=>c.BrandName,opt=>opt.MapFrom(c=>c.Brand.Name)).ReverseMap(); //Model i ModelListDto a çevir maple.ReverseMap ile tam tersi durumda geçerli demek
            //ForMember(c=>c.BrandName,opt=>opt.MapFrom(c=>c.Brand.Name) =>Ama burda c nin BrandName ini c nin içindeki Brand içinde ki Name alanında al diyoruz ve map le
            CreateMap<IPaginate<Model>, ModelListModel>().ReverseMap();//IPaginate<Model> i ModelListModel a çevir maple.ReverseMap ile tam tersi durumda geçerli demek
        }
    }
}
