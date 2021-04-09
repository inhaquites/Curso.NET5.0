using System.Threading.Tasks;
using Api.Application.Controllers;
using Api.Domain.Interfaces.Services.Municipio;
using Xunit;
using Moq;
using Api.Domain.DTOs.Municipio;
using System;
using Microsoft.AspNetCore.Mvc;

namespace Api.Application.Test.Municipio.QuandoRequisitarGet
{
  public class Retorno_Get
  {
    private MunicipiosController _controller;

    [Fact(DisplayName = "É possível realizar o Get")]
    public async Task E_Possivel_Invocar_a_Controller_Get()
    {
      var serviceMock = new Mock<IMunicipioService>();
      var Nome = Faker.Address.City();
      var codIBGE = Faker.RandomNumber.Next(1000000, 9999999);
      var UfId = Guid.NewGuid();

      serviceMock.Setup(c => c.Get(It.IsAny<Guid>())).ReturnsAsync(
          new MunicipioDto
          {
            Id = Guid.NewGuid(),
            Nome = Nome,
            CodIBGE = codIBGE,
            UfId = UfId
          }
        );

      _controller = new MunicipiosController(serviceMock.Object);

      var result = await _controller.Get(Guid.NewGuid());
      Assert.True(result is OkObjectResult);
    }
  }
}
