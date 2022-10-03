using Application.Features.Auths.Dtos;
using Application.Features.Auths.Rules;
using Application.Services.AuthService;
using Application.Services.Repositories;
using Core.Security.Dtos;
using Core.Security.Entities;
using Core.Security.Hashing;
using Core.Security.JWT;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auths.Commands.Register
{
    public class RegisterCommand:IRequest<RegisteredDto>//geri dönüş eğeri RegisteredDto
    {
        public UserForRegisterDto UserForRegisterDto { get; set; }//kayıt için istenen veriler alıcaz direk clasdan cektik
        public string IpAddress { get; set; }

        public class RegisterCommandHandler : IRequestHandler<RegisterCommand, RegisteredDto>////Handle ediceğimiz command RegisterCommand diğeri(RegisteredDto) de yanıt yani onu tüketen.RegisterCommand i tüketen handle metod
        {
            private readonly AuthBusinessRules _authBusinessRules;
            private readonly IUserRepository _userRepository;
            private readonly IAuthService _authService;

            public RegisterCommandHandler(AuthBusinessRules authBusinessRules, IUserRepository userRepository, IAuthService authService)
            {
                _authBusinessRules = authBusinessRules;
                _userRepository = userRepository;
                _authService = authService;
            }

            public async Task<RegisteredDto> Handle(RegisterCommand request, CancellationToken cancellationToken)
            {
                await _authBusinessRules.EmailCanNotBeDuplicatedWhenRegistered(request.UserForRegisterDto.Email);//Busines ı aldık burda
                byte[] passwordHash, passwordSalt;
                HashingHelper.CreatePasswordHash(request.UserForRegisterDto.Password, out passwordHash, out passwordSalt);//passwordü yolluyoruz request den hash ve salt yapıyoruz passwordü yani yukarıdakine set ediyoruz

                User newUser = new()//Yeni bir user tanımladık aşağıda manuel mapleme yaptık 
                {
                    Email = request.UserForRegisterDto.Email,
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt,
                    FirstName = request.UserForRegisterDto.FirstName,
                    LastName = request.UserForRegisterDto.LastName,
                    Status = true


                };
                User createdUser = await _userRepository.AddAsync(newUser);//metoda user yolladık user olustu

                AccessToken createdAccessToken = await _authService.CreateAccessToken(createdUser);//metoda olusan userı yollayıp accesstoken olusturduk
                RefreshToken createdRefreshToken = await _authService.CreateRefreshToken(createdUser, request.IpAddress);//metoda olusan user ve ipadressi yolladık RefreshToken olusturduk
                RefreshToken addRefreshToken= await _authService.AddRefreshToken(createdRefreshToken);//metoda olusan refreshtokenı gönderdik veritabanına  ekleme işlemini için

                RegisteredDto registeredDto = new()
                {
                    RefreshToken = addRefreshToken,
                    AccessToken = createdAccessToken,
                };
                return registeredDto;

;            }
        }
    }

}
