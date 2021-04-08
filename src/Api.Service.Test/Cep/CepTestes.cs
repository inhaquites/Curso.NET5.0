using System;
using Api.Domain.DTOs.Municipio;
using System.Collections.Generic;
using Api.Domain.DTOs.Cep;
using Api.Domain.DTOs.Uf;

namespace Api.Service.Test.Cep
{
  public class CepTestes
  {
    public static Guid IdCep { get; set; }
    public static string Cep { get; set; }
    public static string Logradouro { get; set; }
    public static string Numero { get; set; }
    public static string CepAlterado { get; set; }
    public static string LogradouroAlterado { get; set; }
    public static string NumeroAlterado { get; set; }

    public static Guid MunicipioId { get; set; }
    //public MunicipioDtoCompleto Municipio;
    public List<CepDto> listaCepDto = new List<CepDto>();
    public CepDto cepDto;
    public CepDtoCreate cepDtoCreate;
    public CepDtoCreateResult cepDtoCreateResult;
    public CepDtoUpdate cepDtoUpdate;
    public CepDtoUpdateResult cepDtoUpdateResult;

    public CepTestes()
    {
      IdCep = Guid.NewGuid();
      Cep = Faker.RandomNumber.Next(1, 10000).ToString();
      Logradouro = Faker.Address.StreetName();
      Numero = Faker.RandomNumber.Next(1, 10000).ToString();
      CepAlterado = Faker.RandomNumber.Next(1, 10000).ToString();
      LogradouroAlterado = Faker.Address.StreetName();
      NumeroAlterado = Faker.RandomNumber.Next(1, 10000).ToString();
      MunicipioId = Guid.NewGuid();

      for (int i = 0; i < 10; i++)
      {
        var dto = new CepDto()
        {
          Id = Guid.NewGuid(),
          Cep = Faker.RandomNumber.Next(1, 10000).ToString(),
          Logradouro = Faker.Address.StreetName(),
          Numero = Faker.RandomNumber.Next(1, 10000).ToString(),
          MunicipioId = Guid.NewGuid(),
          Municipio = new MunicipioDtoCompleto
          {
            Id = MunicipioId,
            Nome = Faker.Address.City(),
            CodIBGE = Faker.RandomNumber.Next(1000000, 9999999),
            UfId = Guid.NewGuid(),
            Uf = new UfDto
            {
              Id = Guid.NewGuid(),
              Nome = Faker.Address.UsState(),
              Sigla = Faker.Address.UsState().Substring(1, 3)
            }
          }
        };
        listaCepDto.Add(dto);
      }

      cepDto = new CepDto
      {
        Id = IdCep,
        Cep = Cep,
        Logradouro = Logradouro,
        Numero = Numero,
        MunicipioId = MunicipioId,
        Municipio = new MunicipioDtoCompleto
        {
          Id = MunicipioId,
          Nome = Faker.Address.City(),
          CodIBGE = Faker.RandomNumber.Next(1000000, 9999999),
          UfId = Guid.NewGuid(),
          Uf = new UfDto
          {
            Id = Guid.NewGuid(),
            Nome = Faker.Address.UsState(),
            Sigla = Faker.Address.UsState().Substring(1, 3)
          }
        }
      };



      cepDtoCreate = new CepDtoCreate
      {
        Cep = Cep,
        Logradouro = Logradouro,
        Numero = Numero,
        MunicipioId = MunicipioId
      };

      cepDtoCreateResult = new CepDtoCreateResult
      {
        Id = IdCep,
        Cep = Cep,
        Logradouro = Logradouro,
        Numero = Numero,
        MunicipioId = MunicipioId,
        CreateAt = DateTime.UtcNow
      };

      cepDtoUpdate = new CepDtoUpdate
      {
        Id = IdCep,
        Cep = CepAlterado,
        Logradouro = LogradouroAlterado,
        Numero = NumeroAlterado,
        MunicipioId = MunicipioId
      };

      cepDtoUpdateResult = new CepDtoUpdateResult
      {
        Id = IdCep,
        Cep = CepAlterado,
        Logradouro = LogradouroAlterado,
        Numero = NumeroAlterado,
        MunicipioId = MunicipioId,
        UpdateAt = DateTime.UtcNow
      };
    }
  }
}
