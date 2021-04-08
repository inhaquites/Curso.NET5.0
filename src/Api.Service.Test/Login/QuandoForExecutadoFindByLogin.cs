using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Domain.DTOs;
using Api.Domain.DTOs.User;
using Api.Domain.Interfaces.Services.User;
using Moq;
using Xunit;

namespace Api.Service.Test.Login
{
  public class QuandoForExecutadoFindByLogin
  {
    private ILoginService _service;
    private Mock<ILoginService> _serviceMock;

    [Fact(DisplayName = "É possível executar o método FindByLogin")]
    public async Task E_Possivel_Executar_Metodo_FindByLogin()
    {
      var email = Faker.Internet.Email();
      var objetoRetorno = new
      {
        authenticated = true,
        created = DateTime.UtcNow,
        expiration = DateTime.UtcNow.AddHours(8),
        accessToken = Guid.NewGuid(),
        userName = email,
        name = Faker.Name.FullName(),
        message = "Usuário Logado com sucesso"
      };

      var loginDto = new LoginDTO
      {
        Email = email
      };

      _serviceMock = new Mock<ILoginService>();

      _serviceMock.Setup(c => c.FindByLogin(loginDto)).ReturnsAsync(objetoRetorno);
      _service = _serviceMock.Object;

      var result = await _service.FindByLogin(loginDto);
      Assert.NotNull(result);



    }




  }
}
