using Application.Features.Models.Models;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Requests;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Models.Queries.GetListModel
{
    public class GetListModelQuery : IRequest<ModelListModel>//Burda direk dto değilde model kullanıcaz çünkü sayfalamada yapıcaz.Modelin içine hem dto hem sayfalama yapısı eklicez hemde joinler olucak
    {

        public PageRequest PageRequest { get; set; } //dataları istiyoruz ama sayfa bilgisi içinde bunu eklememiz lazım kaçıncı sayfa ve bir sayfada kaç data olsun

        public class GetListModelQueryHandler : IRequestHandler<GetListModelQuery, ModelListModel>//Döndürüceğim tip ModelListModel,Queryim de GetListModelQuery
        {

            private readonly IModelRepository _modelRepository;
            private readonly IMapper _mapper;//Automapper için.Dto nesneleri ile Entity nesneleri mapler

            public GetListModelQueryHandler(IModelRepository modelRepository, IMapper mapper)
            {
                _modelRepository = modelRepository;
                _mapper = mapper;

            }

            public async Task<ModelListModel> Handle(GetListModelQuery request, CancellationToken cancellationToken)//burdaki request PageRequest alanı
            {

                IPaginate<Model> models = await _modelRepository.GetListAsync(include:
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