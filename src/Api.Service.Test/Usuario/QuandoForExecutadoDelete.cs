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
  public class QuandoForExecutadoDelete : UsuarioTestes
  {
    private IUserService _service;
    private Mock<IUserService> _serviceMock;


    [Fact(DisplayName = "É possível executar o método DELETE")]
    public async Task E_Possivel_Executar_Metodo_DELETE()
    {
      _serviceMock = new Mock<IUserService>();
      _serviceMock.Setup(c => c.Delete(IdUsuario)).ReturnsAsync(true);
      _service = _serviceMock.Object;

      var deletado = await _service.Delete(IdUsuario);
      Assert.True(deletado);

      _serviceMock = new Mock<IUserService>();
      _serviceMock.Setup(c => c.Delete(It.IsAny<Guid>())).ReturnsAsync(false);
      _service = _serviceMock.Object;

      deletado = await _service.Delete(Guid.NewGuid());
      Assert.False(deletado);

    }

  }
}
