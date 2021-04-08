using System;
using System.Threading.Tasks;
using Api.Domain.DTOs.User;
using Api.Domain.Interfaces.Services.User;
using Moq;
using Xunit;

namespace Api.Service.Test.Usuario
{
  public class QuandoForExecutadoGET : UsuarioTestes
  {
    private IUserService _service;
    private Mock<IUserService> _serviceMock;

    [Fact(DisplayName = "É possível executar o método GET")]
    public async Task E_Possivel_Executar_Metodo_GET()
    {
      _serviceMock = new Mock<IUserService>();
      _serviceMock.Setup(c => c.Get(IdUsuario)).ReturnsAsync(userDto);
      _service = _serviceMock.Object;


      var result = await _service.Get(IdUsuario);
      Assert.NotNull(result);
      Assert.True(result.Id == IdUsuario);
      Assert.Equal(NomeUsuario, result.Name);


      _serviceMock = new Mock<IUserService>();
      _serviceMock.Setup(c => c.Get(It.IsAny<Guid>())).Returns(Task.FromResult((UserDTO)null));
      _service = _serviceMock.Object;

      var _record = await _service.Get(IdUsuario);
      Assert.Null(_record);




    }

  }
}
