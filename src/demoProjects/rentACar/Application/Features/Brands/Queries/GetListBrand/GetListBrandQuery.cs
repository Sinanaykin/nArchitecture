using Application.Features.Brands.Models;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Requests;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Brands.Queries.GetListBrand
{
    public class GetListBrandQuery:IRequest<BrandListModel>//Burda direk dto değilde model kullanıcaz çünkü sayfalamada yapıcaz.Modelin içine hem dto hem sayfalama yapısı eklicez hemde joinler olucak
    {

        public PageRequest PageRequest { get; set; } //dataları istiyoruz ama sayfa bilgisi içinde bunu eklememiz lazım kaçıncı sayfa ve bir sayfada kaç data olsun

        public class GetListBrandQueryHandler : IRequestHandler<GetListBrandQuery, BrandListModel>//Döndürüceğim tip BrandListModel,Queryim de GetListBrandQuery
        {

            private readonly IBrandRepository _brandRepository;
            private readonly IMapper _mapper;//Automapper için.Dto nesneleri ile Entity nesneleri mapler

            public GetListBrandQueryHandler(IBrandRepository brandRepository, IMapper mapper)
            {
                _brandRepository = brandRepository;
                _mapper = mapper;

            }

            public async Task<BrandListModel> Handle(GetListBrandQuery request, CancellationToken cancellationToken)//burdaki request PageRequest alanı
            {
               IPaginate<Brand> brands=await _brandRepository.GetListAsync(index:request.PageRequest.Page,size:request.PageRequest.PageSize);//2 tane paramatre bekliyor bu metot biri index biri size

                //Burda zaten db den direk veri aldımız için maplenicek bir şey yok ilk aşamada
                //sonra ordan geleni bizim BrandListModel ile mapledik son kullanıcı böyle görücek diye zaten dto muz BrandListModel içinde mevcut

                BrandListModel mappedBrandListModel = _mapper.Map<BrandListModel>(brands);//brands ı BrandListModel e ceviricez maplice yani
                return mappedBrandListModel;
            }
        }
    }
}
