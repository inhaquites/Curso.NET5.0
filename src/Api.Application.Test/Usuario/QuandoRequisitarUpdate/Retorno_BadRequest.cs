using System.Threading.Tasks;
using Api.Application.Controllers;
using Api.Domain.Interfaces.Services.User;
using Xunit;
using Moq;
using Api.Domain.DTOs.User;
using System;
using Microsoft.AspNetCore.Mvc;

namespace Api.Application.Test.Usuario.QuandoRequisitarUpdate
{
  public class Retorno_BadRequest
  {
    private UsersController _controller;

    [Fact(DisplayName = "É possível realizar o updated")]
    public async Task E_Possivel_Invocar_a_Controller_Update()
    {
      var serviceMock = new Mock<IUserService>();
      var nome = Faker.Name.FullName();
      var email = Faker.Internet.Email();

      serviceMock.Setup(c => c.Put(It.IsAny<UserDTOUpdate>())).ReturnsAsync(
        new UserDTOUpdateResult
        {
          Id = Guid.NewGuid(),
          Name = nome,
          Email = email,
          UpdateAt = DateTime.UtcNow
        }
      );

      _controller = new UsersController(serviceMock.Object);
      _controller.ModelState.AddModelError("Email", "É um campo obrigatorio");

      var userDtoUpdate = new UserDTOUpdate
      {
        Id = Guid.NewGuid(),
        Name = nome,
        Email = email
      };

      var result = await _controller.Put(userDtoUpdate);
      Assert.True(result is BadRequestObjectResult);
      Assert.False(_controller.ModelState.IsValid);





    }
  }
}
