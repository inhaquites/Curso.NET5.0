using System.Threading.Tasks;
using Api.Application.Controllers;
using Api.Domain.Interfaces.Services.Municipio;
using Xunit;
using Moq;
using Api.Domain.DTOs.Municipio;
using System;
using Microsoft.AspNetCore.Mvc;

namespace Api.Application.Test.Municipio.QuandoRequisitarGetCompleteByIBGE
{
  public class Retorno_Get
  {
    private MunicipiosController _controller;

    [Fact(DisplayName = "É possível realizar o Get Complete By IBGE")]
    public async Task E_Possivel_Invocar_a_Controller_Get_Complete_By_IBGE()
    {
      var serviceMock = new Mock<IMunicipioService>();
      var Nome = Faker.Address.City();
      var codIBGE = Faker.RandomNumber.Next(1000000, 9999999);
      var UfId = Guid.NewGuid();

      serviceMock.Setup(c => c.GetCompleteByIBGE(It.IsAny<int>())).ReturnsAsync(
          new MunicipioDtoCompleto
          {
            Id = Guid.NewGuid(),
            Nome = Nome,
            CodIBGE = codIBGE,
            UfId = UfId
          }
        );

      _controller = new MunicipiosController(serviceMock.Object);

      var result = await _controller.GetCompleteByIBGE(1);
      Assert.True(result is OkObjectResult);
    }
  }
}
