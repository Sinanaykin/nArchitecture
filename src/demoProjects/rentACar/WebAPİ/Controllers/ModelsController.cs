using Application.Features.Models.Queries.GetListModelByDynamic;
using Application.Features.Models.Models;
using Application.Features.Models.Queries.GetListModel;
using Core.Application.Requests;
using Core.Persistence.Dynamic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPİ.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModelsController : BaseController
    {

        [HttpGet]
        public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)//parametre olarak PageRequest alıcaz sayfa sayısı ve veri sayısı göndeicez
        {
            GetListModelQuery getListModelQuery = new GetListModelQuery { PageRequest = pageRequest };//Burda PageRequest i GetListModelQuery e ceviriyoruz
            ModelListModel result = await Mediator.Send(getListModelQuery);//Mediatr ada  çevirdiğimizi(getListModelQuery) yolluyoruz
            return Ok(result);
        }

        [HttpPost("GetList/ByDynamic")]
        public async Task<IActionResult> GetListByDynamic([FromQuery] PageRequest pageRequest,[FromBody] Dynamic dynamic)//parametre olarak PageRequest alıcaz sayfa sayısı ve veri sayısı göndeicez birde dynamic query i yollıcaz
        {
            GetListModelByDynamicQuery getListByDynamicModelQuery = new GetListModelByDynamicQuery { PageRequest = pageRequest,Dynamic=dynamic };
            ModelListModel result = await Mediator.Send(getListByDynamicModelQuery);//Mediatr ada  çevirdiğimizi(getListModelQuery) yolluyoruz
            return Ok(result);
        }
    }
}
