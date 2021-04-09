using System.Threading.Tasks;
using Api.Application.Controllers;
using Api.Domain.Interfaces.Services.User;
using Xunit;
using Moq;
using Api.Domain.DTOs.User;
using System;
using Microsoft.AspNetCore.Mvc;

namespace Api.Application.Test.Usuario.QuandoRequisitarDelete
{
  public class Retorno_Deleted
  {
    private UsersController _controller;

    [Fact(DisplayName = "É possível realizar o Deleted")]
    public async Task E_Possivel_Invocar_a_Controller_Delete()
    {
      var serviceMock = new Mock<IUserService>();

      serviceMock.Setup(c => c.Delete(It.IsAny<Guid>())).ReturnsAsync(true);

      _controller = new UsersController(serviceMock.Object);

      var result = await _controller.Delete(Guid.NewGuid());
      Assert.True(result is OkObjectResult);

      var resultValue = ((OkObjectResult)result).Value;
      Assert.NotNull(resultValue);
      Assert.True((Boolean)resultValue);
    }
  }
}
