using System;
using System.Collections.Generic;
using Api.Domain.DTOs.User;

namespace Api.Service.Test.Usuario
{
  public class UsuarioTestes
  {
    public static string NomeUsuario { get; set; }
    public static string EmailUsuario { get; set; }
    public static string NomeUsuarioAlterado { get; set; }
    public static string EmailUsuarioAlterado { get; set; }
    public static Guid IdUsuario { get; set; }


    public List<UserDTO> listaUserDto = new List<UserDTO>();
    public UserDTO userDto;
    public UserDTOCreate userDTOCreate;
    public UserDTOCreateResult userDTOCreateResult;
    public UserDTOUpdate userDTOUpdate;
    public UserDTOUpdateResult userDTOUpdateResult;


    public UsuarioTestes()
    {
      IdUsuario = Guid.NewGuid();
      NomeUsuario = Faker.Name.FullName();
      EmailUsuario = Faker.Internet.Email();
      NomeUsuarioAlterado = Faker.Name.FullName();
      EmailUsuarioAlterado = Faker.Internet.Email();

      for (int i = 0; i < 10; i++)
      {
        var dto = new UserDTO()
        {
          Id = Guid.NewGuid(),
          Name = Faker.Name.FullName(),
          Email = Faker.Internet.Email()
        };
        listaUserDto.Add(dto);
      }

      userDto = new UserDTO
      {
        Id = IdUsuario,
        Name = NomeUsuario,
        Email = EmailUsuario
      };

      userDTOCreate = new UserDTOCreate
      {
        Name = NomeUsuario,
        Email = EmailUsuario
      };

      userDTOCreateResult = new UserDTOCreateResult
      {
        Id = IdUsuario,
        Name = NomeUsuario,
        Email = EmailUsuario,
        CreateAt = DateTime.UtcNow
      };

      userDTOUpdate = new UserDTOUpdate
      {
        Id = IdUsuario,
        Name = NomeUsuarioAlterado,
        Email = EmailUsuarioAlterado
      };

      userDTOUpdateResult = new UserDTOUpdateResult
      {
        Id = IdUsuario,
        Name = NomeUsuarioAlterado,
        Email = EmailUsuarioAlterado,
        UpdateAt = DateTime.UtcNow
      };

    }


  }
}
