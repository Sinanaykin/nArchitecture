using Application.Features.Models.Models;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Requests;
using Core.Persistence.Dynamic;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Models.Queries.GetListModelByDynamic
{
    public class GetListModelByDynamicQuery: IRequest<ModelListModel>
    {
        public Dynamic Dynamic { get; set; }//Bunu gönderirsek Core katmanındaki oluşturdugumuz yapıya göre yap dinamik halde sorgu atabiliriz
        public PageRequest PageRequest { get; set; } //dataları istiyoruz ama sayfa bilgisi içinde bunu eklememiz lazım kaçıncı sayfa ve bir sayfada kaç data olsun

        public class GetListModelByDynamicQueryHandler : IRequestHandler<GetListModelByDynamicQuery, ModelListModel>//Döndürüceğim tip ModelListModel,Queryim de GetListModelQuery
        {

            private readonly IModelRepository _modelRepository;
            private readonly IMapper _mapper;//Automapper için.Dto nesneleri ile Entity nesneleri mapler

            public GetListModelByDynamicQueryHandler(IModelRepository modelRepository, IMapper mapper)
            {
                _modelRepository = modelRepository;
                _mapper = mapper;

            }

            public async Task<ModelListModel> Handle(GetListModelByDynamicQuery request, CancellationToken cancellationToken)//burdaki request PageRequest alanı
            {

                IPaginate<Model> models = await _modelRepository.GetListByDynamicAsync(request.Dynamic,//burda dinamik bir  sorgu yapacağımı belirtiyorum
                                                include:
                                                m => m.Include(c => c.Brand),//Burda BrandName çekebilmek için Model üzerinden Brand a gidebilmemiz lazım
                                                index: request.PageRequest.Page,//2 tane paramatre bekliyor bu metot biri index biri size
                                                size: request.PageRequest.PageSize
                                                  );
                //Burda zaten db den direk veri aldımız için maplenicek bir şey yok ilk aşamada
                //sonra ordan geleni bizim ModelListModel ile mapledik son kullanıcı böyle görücek diye zaten dto muz ModelListModel içinde mevcut


                ModelListModel mappedModelListModel = _mapper.Map<ModelListModel>(models);   //models ı ModelListModel e ceviricez maplice yani 


                return mappedModelListModel;
            }
        }
    }
}
