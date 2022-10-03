using Core.Security.Entities;
using Core.Security.JWT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.AuthService
{
    public interface IAuthService
    {
        public Task<AccessToken> CreateAccessToken(User user);//Kullanıcı için AccessToken OLuşturma
        public Task<RefreshToken> CreateRefreshToken(User user,string ipAddress);////Kullanıcı için RefreshToken OLuşturma
        public Task<RefreshToken> AddRefreshToken(RefreshToken refreshToken);//Kullanıcı için RefreshTokenı db ye ekleme


    }
}
