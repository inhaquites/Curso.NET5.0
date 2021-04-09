using System.Threading.Tasks;
using Api.Application.Controllers;
using Api.Domain.Interfaces.Services.Cep;
using Xunit;
using Moq;
using Api.Domain.DTOs.Cep;
using System;
using Microsoft.AspNetCore.Mvc;

namespace Api.Application.Test.Cep.QuandoRequisitarGet
{
  public class Retorno_BadRequest
  {
    private CepsController _controller;

    [Fact(DisplayName = "É possível realizar o Get")]
    public async Task E_Possivel_Invocar_a_Controller_Get()
    {
      var serviceMock = new Mock<ICepService>();
      var Cep = Faker.RandomNumber.Next(1, 10000).ToString();
      var Logradouro = Faker.Address.StreetName();
      var Numero = Faker.RandomNumber.Next(1, 10000).ToString();
      var MunicipioId = Guid.NewGuid();

      serviceMock.Setup(c => c.Get(It.IsAny<Guid>())).ReturnsAsync(
          new CepDto
          {
            Id = Guid.NewGuid(),
            Cep = Cep,
            Logradouro = Logradouro,
            Numero = Numero,
            MunicipioId = MunicipioId
          }
        );

      _controller = new CepsController(serviceMock.Object);
      _controller.ModelState.AddModelError("Id", "Formato invalido");

      var result = await _controller.Get(Guid.NewGuid());
      Assert.True(result is BadRequestObjectResult);
    }
  }
}
