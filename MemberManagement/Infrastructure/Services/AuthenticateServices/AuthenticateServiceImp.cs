using Application.Common.HTTPResponse;
using Application.Common.Interfaces.Repositories.MemberRepo;
using Application.Common.Interfaces.Repositories.TokenRepo;
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
        private readonly IMemberRepo _memberRepository;
        private readonly ITokenRepo _tokenRepository;
        private const float lifeTimeOfToken = 15;
        private const float lifeTimeOfRefreshToken = 20;
        public AuthenticateServiceImp(IConfiguration configuration, IMemberRepo memberRepository, ITokenRepo tokenRepository)
        {
            _configuration = configuration;
            _memberRepository = memberRepository;
            _tokenRepository = tokenRepository;
        }
        public AuthenticateGettingDto GetJWT(LoginRequestDto parLogin)
        {

            try
            {
                var account = _memberRepository.GetMemberByUserNameAndPassword(parLogin.UserName, parLogin.Password);
                if (account == null)
                {
                    throw new MemberManagementException(ResponseMessage.LoginFail);
                }
                return GetJwtTokenByAccount(account);
            }
            catch (Exception e)
            {
                return new AuthenticateGettingDto
                {
                    Message = e.Message,
                    IsSuccess = false
                };
            }
        }
        public AuthenticateGettingDto VerifyJWT(RefreshTokenDto authenticateRequest)
        {
            try
            {
                // is token exist in db ?
                var token = _tokenRepository.GetTokenByTokenCodeAndRefreshTokenCode(authenticateRequest.TokenCode, authenticateRequest.TokenRefeshCode);
                if (token == null)
                {
                    throw new MemberManagementException(ResponseMessage.RefreshTokenNotValid);
                }

                // is token revoked (after logout)
                if (token.IsRevoked)
                {
                    throw new MemberManagementException(ResponseMessage.TokenHasBeenRevoked);
                }

                // is token expired, If no -> dont allow refresh
                if (token.DeathTime > DateTime.UtcNow)
                {
                    throw new MemberManagementException(ResponseMessage.TokenNotExpired);
                }

                // is token expired, If yes -> dont allow refresh, user have to login
                if (token.ExpiryDate < DateTime.UtcNow)
                {
                    throw new MemberManagementException(ResponseMessage.TokenExpired);
                }

                // is token used, If yes -> dont allow refresh
                if (token.IsUsed)
                {
                    throw new MemberManagementException(ResponseMessage.TokenUsed);
                }

                // can refresh token and it will be create new token that replace old token
                token.IsUsed = true;
                _tokenRepository.UpdateRefreshToken(token);

                // Trả về 1 Token mới
                var member = _memberRepository.GetMemberByEmail(token.Email);
                return GetJwtTokenByAccount(member);
            }
            catch (Exception e)
            {
                return new AuthenticateGettingDto
                {
                    TokenRefeshCode = null,
                    TokenCode = null,
                    IsSuccess = false,
                    Message = e.Message
                };
            }
        }
        public bool RevokeToken(RefreshTokenDto authenticateRequest)
        {
            try
            {
                int res = _tokenRepository.UpdateRevokedStatusForToken(authenticateRequest.TokenRefeshCode);
                if (res > 0)
                {
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }


        #region Private function
        private AuthenticateGettingDto GetJwtTokenByAccount(Member member)
        {
            try
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

                _tokenRepository.AddNewRefreshToken(refreshToken);

                return new AuthenticateGettingDto
                {
                    IsSuccess = true,
                    TokenCode = resultJWT,
                    TokenRefeshCode = refreshToken.Token
                };
            }
            catch (Exception e)
            {
                return new AuthenticateGettingDto
                {
                    IsSuccess = false,
                };
            }
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