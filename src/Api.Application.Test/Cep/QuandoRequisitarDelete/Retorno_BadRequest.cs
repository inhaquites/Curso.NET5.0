using System.Threading.Tasks;
using Api.Application.Controllers;
using Api.Domain.Interfaces.Services.Cep;
using Xunit;
using Moq;
using Api.Domain.DTOs.Cep;
using System;
using Microsoft.AspNetCore.Mvc;

namespace Api.Application.Test.Cep.QuandoRequisitarDelete
{
  public class Retorno_BadRequest
  {
    private CepsController _controller;

    [Fact(DisplayName = "É possível realizar o Deleted")]
    public async Task E_Possivel_Invocar_a_Controller_Delete()
    {
      var serviceMock = new Mock<ICepService>();

      serviceMock.Setup(c => c.Delete(It.IsAny<Guid>())).ReturnsAsync(true);

      _controller = new CepsController(serviceMock.Object);
      _controller.ModelState.AddModelError("Id", "Formato inválido");

      var result = await _controller.Delete(Guid.NewGuid());
      Assert.True(result is BadRequestObjectResult);
    }
  }
}
