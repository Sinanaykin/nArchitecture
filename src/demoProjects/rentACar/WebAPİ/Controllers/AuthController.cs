using Application.Features.Auths.Commands.Register;
using Application.Features.Auths.Dtos;
using Core.Security.Dtos;
using Core.Security.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPİ.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : BaseController
    {
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] UserForRegisterDto userForRegisterDto)
        {
            RegisterCommand registeredCommand = new()
            {
                UserForRegisterDto = userForRegisterDto,
                IpAddress = GetIpAddress()
            };
            RegisteredDto result = await Mediator.Send(registeredCommand);
            SetRefreshTokenCookie(result.RefreshToken);//aşağıdaki metoda result.RefreshToken ı yolladık
            return Created("", result.AccessToken);//geriye bunu döndürdük
        }
        private void SetRefreshTokenCookie(RefreshToken refreshToken)//Refresh token ı cookilere eklemek için metod yazıcaz
        {
            CookieOptions cookieOptions = new()//bunuda cookieoptions ile yaparız
            {
                HttpOnly = true,//sadece http isteklerinde bu hareketi yap demek
                Expires = DateTime.Now.AddDays(7)//expired süresini vericez
            };
            Response.Cookies.Append("refreshToken", refreshToken.Token, cookieOptions);//response un cookilerine ekle bu değerleri.
            //cookieOptions ayarlarında  refreshToken.Token ı refreshToken ismi ile set et demek

        }
        

    }
}
