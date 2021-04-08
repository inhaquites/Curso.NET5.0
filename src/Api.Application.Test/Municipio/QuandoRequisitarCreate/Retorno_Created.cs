using System.Threading.Tasks;
using Api.Application.Controllers;
using Api.Domain.Interfaces.Services.Municipio;
using Xunit;
using Moq;
using Api.Domain.DTOs.Municipio;
using System;
using Microsoft.AspNetCore.Mvc;

namespace Api.Application.Test.Municipio.QuandoRequisitarCreate
{
  public class Retorno_Created
  {
    private MunicipiosController _controller;

    [Fact(DisplayName = "É possível realizar o created")]
    public async Task E_Possivel_Invocar_a_Controller_Create()
    {
      var serviceMock = new Mock<IMunicipioService>();
      var Nome = Faker.Address.City();
      var CodIBGE = Faker.RandomNumber.Next(1000000, 9999999);
      var UfId = Guid.NewGuid();

      serviceMock.Setup(c => c.Post(It.IsAny<MunicipioDtoCreate>())).ReturnsAsync(
        new MunicipioDtoCreateResult
        {
          Id = Guid.NewGuid(),
          Nome = Nome,
          CodIBGE = CodIBGE,
          UfId = UfId,
          CreateAt = DateTime.UtcNow
        }
      );

      _controller = new MunicipiosController(serviceMock.Object);


      Mock<IUrlHelper> url = new Mock<IUrlHelper>();
      url.Setup(x => x.Link(It.IsAny<string>(), It.IsAny<object>())).Returns("http://localhost:5000");
      _controller.Url = url.Object;

      var municipioDTOCreate = new MunicipioDtoCreate
      {
        Nome = Nome,
        CodIBGE = CodIBGE,
        UfId = UfId

      };

      var result = await _controller.Post(municipioDTOCreate);
      Assert.True(result is CreatedResult);

    }
  }
}
