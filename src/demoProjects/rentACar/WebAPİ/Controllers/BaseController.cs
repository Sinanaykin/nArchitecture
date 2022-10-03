using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebAPİ.Controllers
{
    public class BaseController : ControllerBase //ControllerBase DEN İnherite olan BaseController olusturduk biz controller larımızda bunu inherite edicez
    {
        //Mediatr a bütün controllerda ihtiyaç olucağı için bu yapıyı kurduk ve ControllerdaBase Controller ile bunu inherite alıcaz
        protected IMediator? Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();//Bize IMediator nesnesinin instance ı nı olusturucak.Kısacası İnjectionu Contollerda her controller için yapmak yerine burda bir kre yapıp burdan kalıtım alıyoruz Controllerda
        private IMediator? _mediator;

        protected string? GetIpAddress()//protected yapıcaz çünkü sadece inherite eden yerlerde kullanıcaz.İp adresi almak için metod yazdık
        {
            if(Request.Headers.ContainsKey("X-Forwarded-For")) return Request.Headers["X-Forwarded-For"];
            return HttpContext.Connection.RemoteIpAddress?.MapToIPv4().ToString();
        }

    }
}
