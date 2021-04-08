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
  public class Retorno_Get
  {
    private UfsController _controller;

    [Fact(DisplayName = "É possível realizar o Get")]
    public async Task E_Possivel_Invocar_a_Controller_Get()
    {
      var serviceMock = new Mock<IUfService>();
      var sigla = "RS";
      var nome = "Rio Grande do Sul";

      serviceMock.Setup(c => c.Get(It.IsAny<Guid>())).ReturnsAsync(
          new UfDto
          {
            Id = Guid.NewGuid(),
            Sigla = sigla,
            Nome = nome
          }
        );

      _controller = new UfsController(serviceMock.Object);

      var result = await _controller.Get(Guid.NewGuid());
      Assert.True(result is OkObjectResult);

      var resultValue = ((OkObjectResult)result).Value as UfDto;
      Assert.NotNull(resultValue);
      Assert.Equal(sigla, resultValue.Sigla);
      Assert.Equal(nome, resultValue.Nome);



    }
  }
}
