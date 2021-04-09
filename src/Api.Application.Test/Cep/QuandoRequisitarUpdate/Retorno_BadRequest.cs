using System.Threading.Tasks;
using Api.Application.Controllers;
using Api.Domain.Interfaces.Services.Cep;
using Xunit;
using Moq;
using Api.Domain.DTOs.Cep;
using System;
using Microsoft.AspNetCore.Mvc;

namespace Api.Application.Test.Cep.QuandoRequisitarUpdate
{
  public class Retorno_BadRequest
  {
    private CepsController _controller;

    [Fact(DisplayName = "É possível realizar o update")]
    public async Task E_Possivel_Invocar_a_Controller_update()
    {
      var serviceMock = new Mock<ICepService>();
      var Cep = Faker.RandomNumber.Next(1, 10000).ToString();
      var Logradouro = Faker.Address.StreetName();
      var Numero = Faker.RandomNumber.Next(1, 10000).ToString();
      var MunicipioId = Guid.NewGuid();

      serviceMock.Setup(c => c.Put(It.IsAny<CepDtoUpdate>())).ReturnsAsync(
        new CepDtoUpdateResult
        {
          Id = Guid.NewGuid(),
          Cep = Cep,
          Logradouro = Logradouro,
          Numero = Numero,
          MunicipioId = MunicipioId,
          UpdateAt = DateTime.UtcNow
        }
      );

      _controller = new CepsController(serviceMock.Object);
      _controller.ModelState.AddModelError("Name", "É um campo Obrigatório");

      Mock<IUrlHelper> url = new Mock<IUrlHelper>();
      url.Setup(x => x.Link(It.IsAny<string>(), It.IsAny<object>())).Returns("http://localhost:5000");
      _controller.Url = url.Object;

      var cepDtoUpdate = new CepDtoUpdate
      {
        Cep = Cep,
        Logradouro = Logradouro,
        Numero = Numero,
        MunicipioId = MunicipioId

      };

      var result = await _controller.Put(cepDtoUpdate);
      Assert.True(result is BadRequestObjectResult);

    }
  }
}
