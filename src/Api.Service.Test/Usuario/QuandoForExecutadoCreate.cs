using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Domain.DTOs.User;
using Api.Domain.Interfaces.Services.User;
using Moq;
using Xunit;

namespace Api.Service.Test.Usuario
{
  public class QuandoForExecutadoCreate : UsuarioTestes
  {
    private IUserService _service;
    private Mock<IUserService> _serviceMock;


    [Fact(DisplayName = "É possível executar o método CREATE")]
    public async Task E_Possivel_Executar_Metodo_CREATE()
    {
      _serviceMock = new Mock<IUserService>();
      _serviceMock.Setup(c => c.Post(userDTOCreate)).ReturnsAsync(userDTOCreateResult);
      _service = _serviceMock.Object;

      var result = await _service.Post(userDTOCreate);
      Assert.NotNull(result);
      Assert.Equal(NomeUsuario, result.Name);
      Assert.Equal(EmailUsuario, result.Email);







    }
  }
}
