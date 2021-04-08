using System.Threading.Tasks;
using Api.Application.Controllers;
using Api.Domain.Interfaces.Services.Municipio;
using Xunit;
using Moq;
using Api.Domain.DTOs.Municipio;
using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Api.Application.Test.Municipio.QuandoRequisitarGetAll
{
  public class Retorno_BadRequest
  {
    private MunicipiosController _controller;

    [Fact(DisplayName = "É possível realizar o GetAll")]
    public async Task E_Possivel_Invocar_a_Controller_GetAll()
    {
      var serviceMock = new Mock<IMunicipioService>();
      var Nome = Faker.Address.City();
      var codIBGE = Faker.RandomNumber.Next(1000000, 9999999);
      var UfId = Guid.NewGuid();

      serviceMock.Setup(c => c.GetAll()).ReturnsAsync(
          new List<MunicipioDto>
          {
            new MunicipioDto
            {
                Id = Guid.NewGuid(),
                Nome = Nome,
                CodIBGE = codIBGE,
                UfId = UfId
            },
            new MunicipioDto
            {
                Id = Guid.NewGuid(),
                Nome = Nome,
                CodIBGE = codIBGE,
                UfId = UfId
            }
          }
        );

      _controller = new MunicipiosController(serviceMock.Object);
      _controller.ModelState.AddModelError("Id", "Formato invalido");

      var result = await _controller.GetAll();
      Assert.True(result is BadRequestObjectResult);
    }
  }
}
