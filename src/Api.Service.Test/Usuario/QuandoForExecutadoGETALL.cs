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
  public class QuandoForExecutadoGETALL : UsuarioTestes
  {

    private IUserService _service;
    private Mock<IUserService> _serviceMock;


    [Fact(DisplayName = "É possível executar o método GET ALL")]
    public async Task E_Possivel_Executar_Metodo_GET_ALL()
    {
      _serviceMock = new Mock<IUserService>();
      _serviceMock.Setup(c => c.GetAll()).ReturnsAsync(listaUserDto);
      _service = _serviceMock.Object;

      var result = await _service.GetAll();
      Assert.NotNull(result);
      Assert.True(result.Count() == 10);

      var _listResult = new List<UserDTO>();
      _serviceMock = new Mock<IUserService>();
      _serviceMock.Setup(c => c.GetAll()).ReturnsAsync(_listResult.AsEnumerable);
      _service = _serviceMock.Object;

      var _resultEmpty = await _service.GetAll();
      Assert.Empty(_resultEmpty);
      Assert.True(_resultEmpty.Count() == 0);
    }

  }
}
