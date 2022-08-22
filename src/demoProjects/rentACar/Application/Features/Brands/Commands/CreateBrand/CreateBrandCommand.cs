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

namespace Application.Features.Brands.Commands.CreateBrand
{
    public class CreateBrandCommand:IRequest<CreatedBrandDto>//IRequest Madiatr içinde bir yapı ordan implemente ediyoruz.Parametee olarak oluşturdugumuz dto yu yolluyoruz
    {
        public string Name { get; set; }//Ekliceğimiz alan Name alanı o yüzden bunu alıyoruz .ID otomatik ondan almıcaz
        public class CreateBrandCommandHandler : IRequestHandler<CreateBrandCommand, CreatedBrandDto>//Handle ediceğimiz command CreateBrandCommand diğeri(CreatedBrandDto) de yanıt
        {
           private readonly IBrandRepository _brandRepository;
           private readonly IMapper _mapper;//Automapper için.Dto nesneleri ile Entity nesneleri mapler
            private readonly BrandBusinessRules _brandBusinessRules;//validation için bunuda enjeckte ettik
            public CreateBrandCommandHandler(IBrandRepository brandRepository, IMapper mapper, BrandBusinessRules brandBusinessRules)
            {
                _brandRepository = brandRepository;
                _mapper = mapper;
                _brandBusinessRules = brandBusinessRules;
            }

            public async Task<CreatedBrandDto> Handle(CreateBrandCommand request, CancellationToken cancellationToken)
            {
                await _brandBusinessRules.BrandNameCanNotBeDuplicatedWhenInserted(request.Name);//Validationu sağlıyor mu ona bakıcaz sağlamaz sa zaten hata döner

                //neyi neye mapliceğini MappingProfiles da belirtiyoruz
                Brand mappedBrand = _mapper.Map<Brand>(request);//Requestle yani CreateBrandCommand (Name) gelen ile db dekini yani entity(Brand daki Name) mizi eşle.Brand i requeste çevir
                Brand createdBrand = await _brandRepository.AddAsync(mappedBrand);//Veri tabanına yukarıda mapplediğimiz mappedBrand i yolla
                CreatedBrandDto createdBrandDto = _mapper.Map<CreatedBrandDto>(createdBrand);//Veritabanından geleni dto ya ceviricez son kullanıcıya döndürmek istediğimiz şekilde

                return createdBrandDto;
            }
        }
    }
}
