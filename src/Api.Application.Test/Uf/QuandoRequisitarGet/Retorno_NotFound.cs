using System.Threading.Tasks;
using Api.Application.Controllers;
using Api.Domain.Interfaces.Services.Uf;
using Xunit;
using Moq;
using Api.Domain.DTOs.Uf;
using System;
using Microsoft.AspNetCore.Mvc;

namespace Api.Application.Test.Uf.QuandoRequisitarGet
{
  public class Retorno_NotFound
  {
    private UfsController _controller;

    [Fact(DisplayName = "É possível realizar o Get")]
    public async Task E_Possivel_Invocar_a_Controller_Get()
    {
      var serviceMock = new Mock<IUfService>();

      serviceMock.Setup(c => c.Get(It.IsAny<Guid>())).Returns(Task.FromResult((UfDto)null));

      _controller = new UfsController(serviceMock.Object);

      var result = await _controller.Get(Guid.NewGuid());
      Assert.True(result is NotFoundResult);


    }
  }
}
