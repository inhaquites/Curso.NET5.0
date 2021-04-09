using System.Threading.Tasks;
using Api.Application.Controllers;
using Api.Domain.Interfaces.Services.Municipio;
using Xunit;
using Moq;
using Api.Domain.DTOs.Municipio;
using System;
using Microsoft.AspNetCore.Mvc;

namespace Api.Application.Test.Municipio.QuandoRequisitarUpdate
{
  public class Retorno_Created
  {
    private MunicipiosController _controller;

    [Fact(DisplayName = "É possível realizar o Update")]
    public async Task E_Possivel_Invocar_a_Controller_Update()
    {
      var serviceMock = new Mock<IMunicipioService>();
      var Nome = Faker.Address.City();
      var CodIBGE = Faker.RandomNumber.Next(1000000, 9999999);
      var UfId = Guid.NewGuid();

      serviceMock.Setup(c => c.Put(It.IsAny<MunicipioDtoUpdate>())).ReturnsAsync(
        new MunicipioDtoUpdateResult
        {
          Id = Guid.NewGuid(),
          Nome = Nome,
          CodIBGE = CodIBGE,
          UfId = UfId,
          UpdateAt = DateTime.UtcNow
        }
      );

      _controller = new MunicipiosController(serviceMock.Object);


      Mock<IUrlHelper> url = new Mock<IUrlHelper>();
      url.Setup(x => x.Link(It.IsAny<string>(), It.IsAny<object>())).Returns("http://localhost:5000");
      _controller.Url = url.Object;

      var municipioDtoUpdate = new MunicipioDtoUpdate
      {
        Nome = Nome,
        CodIBGE = CodIBGE,
        UfId = UfId

      };

      var result = await _controller.Put(municipioDtoUpdate);
      Assert.True(result is OkObjectResult);


    }
  }
}
