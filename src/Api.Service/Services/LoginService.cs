using Api.Domain.DTOs;
using Api.Domain.Entities;
using Api.Domain.Interfaces.Services.User;
using Api.Domain.Repository;
using Api.Domain.Security;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

namespace Api.Service.Services
{
  public class LoginService : ILoginService
  {
    private readonly IUserRepository _repository;
    private readonly SigningConfigurations _signingConfiguration;
    public LoginService(IUserRepository repository,
                        SigningConfigurations signingConfiguration
                        )
    {
      _repository = repository;
      _signingConfiguration = signingConfiguration;
    }

    public async Task<object> FindByLogin(LoginDTO user)
    {
      if (user != null && !string.IsNullOrWhiteSpace(user.Email))
      {
        var baseUser = await _repository.FindByLogin(user.Email);
        if (baseUser != null)
        {
          var identity = new ClaimsIdentity(
              new GenericIdentity(baseUser.Email),
              new[]
              {
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.UniqueName, user.Email),
              }
          );
          DateTime createDate = DateTime.Now;
          DateTime expirationDate = createDate + TimeSpan.FromSeconds(Convert.ToInt32(Environment.GetEnvironmentVariable("Seconds")));

          var handler = new JwtSecurityTokenHandler();
          string token = CreateToken(identity, createDate, expirationDate, handler);

          return SuccessObject(createDate, expirationDate, token, baseUser);
        }
      }

      return new
      {
        authenticated = false,
        message = "Falha ao autenticar"
      };
    }

    private string CreateToken(ClaimsIdentity identity, DateTime createDate, DateTime expirationDate, JwtSecurityTokenHandler handler)
    {
      var securityToken = handler.CreateToken(new SecurityTokenDescriptor
      {
        Issuer = Environment.GetEnvironmentVariable("Issuer"),
        Audience = Environment.GetEnvironmentVariable("Audience"),
        SigningCredentials = _signingConfiguration.SigningCredentials,
        Subject = identity,
        NotBefore = createDate,
        Expires = expirationDate
      });

      return handler.WriteToken(securityToken);
    }

    private object SuccessObject(DateTime createDate, DateTime expirationDate, string token, UserEntity user)
    {
      return new
      {
        authenticated = true,
        created = createDate.ToString("yyyy-MM-dd HH:mm:ss"),
        expiration = expirationDate.ToString("yyyy-MM-dd HH:mm:ss"),
        accessToken = token,
        userName = user.Email,
        name = user.Name,
        message = "Usuário Logado com sucesso"
      };
    }

  }
}
