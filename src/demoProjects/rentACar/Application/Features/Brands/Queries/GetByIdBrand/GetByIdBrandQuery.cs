using Application.Features.Brands.Dtos;
using Application.Features.Brands.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Brands.Queries.GetByIdBrand
{
    public class GetByIdBrandQuery:IRequest<BrandGetByIdDto>
    {
        public int Id{ get; set; }//Id ye göre ARAMA YAPICAZ BU YÜZDE PARAMETRE OLARAK Id ALICAZ

        public class GetByIdBrandQueryHandler : IRequestHandler<GetByIdBrandQuery, BrandGetByIdDto>//Döndürüceğim tip BrandGetByIdDto,Queryim de GetByIdBrandQuery
        {

            private readonly IBrandRepository _brandRepository;
            private readonly IMapper _mapper;//Automapper için.Dto nesneleri ile Entity nesneleri mapler
            private readonly BrandBusinessRules _brandBusinessRules;

            public GetByIdBrandQueryHandler(IBrandRepository brandRepository, IMapper mapper , BrandBusinessRules brandBusinessRules)
            {
                _brandRepository = brandRepository;
                _mapper = mapper;
                _brandBusinessRules = brandBusinessRules;

            }

            public async Task<BrandGetByIdDto> Handle(GetByIdBrandQuery request, CancellationToken cancellationToken)//burdaki request GetByIdBrandQuery alanı
            {
     
               Brand? brand=await _brandRepository.GetAsync(b => b.Id == request.Id);//veritabanından yolladığım seye karşılık bir şey dönücek bu yüzden Dönüş tipi Brand.Soru işareti koymalıyız null gelsede kabul ediyorum onu demek için

                await _brandBusinessRules.BrandShouldExistWhenRequested(brand);//iş kontrolü yaptık böyle bir brand var mı yok mu

                //ama bize BrandGetByIdDto lazım bu yüzden mapliyoruz
                BrandGetByIdDto brandGetByIdDto = _mapper.Map<BrandGetByIdDto>(brand);//brand i BrandGetByIdDto a çevir
                return brandGetByIdDto; 
            }
        }
    }
}
