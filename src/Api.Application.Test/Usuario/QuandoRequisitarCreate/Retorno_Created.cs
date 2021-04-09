using System.Threading.Tasks;
using Api.Application.Controllers;
using Api.Domain.Interfaces.Services.User;
using Xunit;
using Moq;
using Api.Domain.DTOs.User;
using System;
using Microsoft.AspNetCore.Mvc;

namespace Api.Application.Test.Usuario.QuandoRequisitarCreate
{
  public class Retorno_Created
  {
    private UsersController _controller;

    [Fact(DisplayName = "É possível realizar o created")]
    public async Task E_Possivel_Invocar_a_Controller_Create()
    {
      var serviceMock = new Mock<IUserService>();
      var nome = Faker.Name.FullName();
      var email = Faker.Internet.Email();

      serviceMock.Setup(c => c.Post(It.IsAny<UserDTOCreate>())).ReturnsAsync(
        new UserDTOCreateResult
        {
          Id = Guid.NewGuid(),
          Name = nome,
          Email = email,
          CreateAt = DateTime.UtcNow
        }
      );

      _controller = new UsersController(serviceMock.Object);

      Mock<IUrlHelper> url = new Mock<IUrlHelper>();
      url.Setup(x => x.Link(It.IsAny<string>(), It.IsAny<object>())).Returns("http://localhost:5000");
      _controller.Url = url.Object;

      var userDTOCreate = new UserDTOCreate
      {
        Name = nome,
        Email = email
      };

      var result = await _controller.Post(userDTOCreate);
      Assert.True(result is CreatedResult);

      var resultValue = ((CreatedResult)result).Value as UserDTOCreateResult;
      Assert.NotNull(resultValue);
      Assert.Equal(userDTOCreate.Name, resultValue.Name);
      Assert.Equal(userDTOCreate.Email, resultValue.Email);



    }

  }
}
