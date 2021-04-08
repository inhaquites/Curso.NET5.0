using System.Threading.Tasks;
using Api.Application.Controllers;
using Api.Domain.Interfaces.Services.Uf;
using Xunit;
using Moq;
using Api.Domain.DTOs.Uf;
using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Api.Application.Test.Uf.QuandoRequisitarGetAll
{
  public class Retorno_BadRequest
  {
    private UfsController _controller;

    [Fact(DisplayName = "É possível realizar o GetAll")]
    public async Task E_Possivel_Invocar_a_Controller_GetAll()
    {
      var serviceMock = new Mock<IUfService>();
      serviceMock.Setup(c => c.GetAll()).ReturnsAsync(
          new List<UfDto>
          {
            new UfDto
            {
                Id = Guid.NewGuid(),
                Sigla = "RS",
                Nome = "Rio Grande do Sul",
            },
            new UfDto
            {
                Id = Guid.NewGuid(),
                Sigla = "SP",
                Nome = "São Paulo",
            }
          }
        );

      _controller = new UfsController(serviceMock.Object);
      _controller.ModelState.AddModelError("Id", "Formato Invalido");
      var result = await _controller.GetAll();
      Assert.True(result is BadRequestObjectResult);
    }
  }
}
