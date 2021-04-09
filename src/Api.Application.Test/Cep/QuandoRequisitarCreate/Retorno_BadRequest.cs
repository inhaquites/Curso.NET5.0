using System.Threading.Tasks;
using Api.Application.Controllers;
using Api.Domain.Interfaces.Services.Cep;
using Xunit;
using Moq;
using Api.Domain.DTOs.Cep;
using System;
using Microsoft.AspNetCore.Mvc;

namespace Api.Application.Test.Cep.QuandoRequisitarCreate
{
  public class Retorno_BadRequest
  {
    private CepsController _controller;

    [Fact(DisplayName = "É possível realizar o created")]
    public async Task E_Possivel_Invocar_a_Controller_Create()
    {
      var serviceMock = new Mock<ICepService>();
      var Cep = Faker.RandomNumber.Next(1, 10000).ToString();
      var Logradouro = Faker.Address.StreetName();
      var Numero = Faker.RandomNumber.Next(1, 10000).ToString();
      var MunicipioId = Guid.NewGuid();

      serviceMock.Setup(c => c.Post(It.IsAny<CepDtoCreate>())).ReturnsAsync(
        new CepDtoCreateResult
        {
          Id = Guid.NewGuid(),
          Cep = Cep,
          Logradouro = Logradouro,
          Numero = Numero,
          MunicipioId = MunicipioId,
          CreateAt = DateTime.UtcNow
        }
      );

      _controller = new CepsController(serviceMock.Object);
      _controller.ModelState.AddModelError("Name", "É um campo Obrigatório");

      Mock<IUrlHelper> url = new Mock<IUrlHelper>();
      url.Setup(x => x.Link(It.IsAny<string>(), It.IsAny<object>())).Returns("http://localhost:5000");
      _controller.Url = url.Object;

      var userDTOCreate = new CepDtoCreate
      {
        Cep = Cep,
        Logradouro = Logradouro,
        Numero = Numero,
        MunicipioId = MunicipioId

      };

      var result = await _controller.Post(userDTOCreate);
      Assert.True(result is BadRequestObjectResult);

    }
  }
}
