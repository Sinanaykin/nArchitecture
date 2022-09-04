using Application.Features.Brands.Commands.CreateBrand;
using Application.Features.Brands.Dtos;
using Application.Features.Brands.Models;
using Application.Features.Brands.Queries.GetByIdBrand;
using Application.Features.Brands.Queries.GetListBrand;
using Core.Application.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPİ.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandsController : BaseController
    {
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateBrandCommand createBrandCommand)
        {
            CreatedBrandDto result = await Mediator.Send(createBrandCommand);
            return Created("", result);
        }


        [HttpGet]
        public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)//parametre olarak PageRequest alıcaz sayfa sayısı ve veri sayısı göndeicez
        {
            GetListBrandQuery getListBrandQuery = new() { PageRequest = pageRequest };//Burda PageRequest i GetListBrandQuery e ceviriyoruz
            BrandListModel result = await Mediator.Send(getListBrandQuery);//Mediatr ada  çevirdiğimizi(getListBrandQuery) yolluyoruz
            return Ok(result);
        }


        [HttpGet("{Id}")]
        public async Task<IActionResult> GetById([FromRoute] GetByIdBrandQuery getByIdBrandQuery)//FromRoute ile [HttpGet("{id}")] bunu oku diyoruz
        {
            BrandGetByIdDto brandGetByIdDto = await Mediator.Send(getByIdBrandQuery);//Mediatr ada  çevirdiğimizi(getListBrandQuery) yolluyoruz
            return Ok(brandGetByIdDto);
        }
    }
}
