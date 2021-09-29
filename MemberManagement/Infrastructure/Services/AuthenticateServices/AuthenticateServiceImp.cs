using Application;
using Application.Common.HTTPResponse;
using Application.Common.Interfaces.Repositories;
using Application.Common.Interfaces.Repositories.MemberRepositories;
using Application.Common.Interfaces.Repositories.TokenRepositories;
using Application.Common.Interfaces.Services.TokenServices;
using Application.Dtos.TokenDtos;
using Domain.Entities;
using Infrastructure.UserDefineException;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Infrastructure.Services.AuthenticateServices
{
    public class AuthenticateServiceImp : IAuthenticateService
    {
        private readonly IConfiguration _configuration;
        private readonly ITokenRepository _refreshTokenRepository;
        private readonly IMemberRepository _memberRepository;
        private readonly IUnitOfWork _unitOfWork;
        private const float lifeTimeOfToken = 15;
        private const float lifeTimeOfRefreshToken = 20;
        public AuthenticateServiceImp(IConfiguration configuration, ITokenRepository refreshTokenRepository, IMemberRepository memberRepository,IUnitOfWork unitOfWork)
        {
            _configuration = configuration;
            _refreshTokenRepository = refreshTokenRepository;
            _memberRepository = memberRepository;
            _unitOfWork = unitOfWork;
        }
        public AuthenticateGettingDto GetJWT(LoginRequestDto parLogin)
        {

            var account = _memberRepository.GetMemberByUserNameAndPassword(parLogin.UserName, parLogin.Password);
            if (account == null)
            {
                throw new AppException(ResponseMessage.LoginFail);
            }
            return GetJwtTokenByAccount(account);

        }
        public AuthenticateGettingDto VerifyJWT(RefreshTokenDto authenticateRequest)
        {

            // is token exist in db ?
            var token = _refreshTokenRepository.GetTokenByTokenCodeAndRefreshTokenCode(authenticateRequest.TokenCode, authenticateRequest.TokenRefeshCode);
            if (token == null)
            {
                throw new AppException(ResponseMessage.RefreshTokenNotValid);
            }

            // is token revoked (after logout)
            if (token.IsRevoked)
            {
                throw new AppException(ResponseMessage.TokenHasBeenRevoked);
            }

            // is token expired, If no -> dont allow refresh
            if (token.DeathTime > DateTime.UtcNow)
            {
                throw new AppException(ResponseMessage.TokenNotExpired);
            }

            // is token expired, If yes -> dont allow refresh, user have to login
            if (token.ExpiryDate < DateTime.UtcNow)
            {
                throw new AppException(ResponseMessage.TokenExpired);
            }

            // is token used, If yes -> dont allow refresh
            if (token.IsUsed)
            {
                throw new AppException(ResponseMessage.TokenUsed);
            }

            // can refresh token and it will be create new token that replace old token
            token.IsUsed = true;
            _refreshTokenRepository.Update(token);

            // Trả về 1 Token mới
            var member = _memberRepository.GetById(token.Email);
            return GetJwtTokenByAccount(member);

        }
        public bool RevokeToken(RefreshTokenDto authenticateRequest)
        {

            _refreshTokenRepository.UpdateRevokedStatusForToken(authenticateRequest.TokenRefeshCode);
           var ressult= _unitOfWork.Commit();
            if (ressult > 0)
            {
                return true;
            }
            return false;

        }


        #region Private function
        private AuthenticateGettingDto GetJwtTokenByAccount(Member member)
        {

            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                        new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                        new Claim("Email", member.Email),
                        new Claim("Name", member.Name),
                    }),
                Expires = DateTime.UtcNow.AddMinutes(lifeTimeOfToken),

                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"])), SecurityAlgorithms.HmacSha256),
                Audience = _configuration["Jwt:Audience"],
                Issuer = _configuration["Jwt:Issuer"]
            };
            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            string resultJWT = "Bearer " + jwtTokenHandler.WriteToken(token);

            // Refresh Token
            var refreshToken = new RefreshToken()
            {
                JwtId = resultJWT,
                IsUsed = false,
                Email = member.Email,
                AddedDate = DateTime.UtcNow,
                DeathTime = DateTime.UtcNow.AddMinutes(lifeTimeOfToken),
                ExpiryDate = DateTime.UtcNow.AddMinutes(lifeTimeOfRefreshToken),
                IsRevoked = false,
                Token = RandomString(25) + Guid.NewGuid()
            };

            _refreshTokenRepository.Insert(refreshToken);

            return new AuthenticateGettingDto
            {
                IsSuccess = true,
                TokenCode = resultJWT,
                TokenRefeshCode = refreshToken.Token
            };

        }
        private string RandomString(int length)
        {
            var random = new Random();
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
            .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        #endregion

    }
}