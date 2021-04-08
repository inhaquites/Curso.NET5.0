using System;
using System.Collections.Generic;
using System.Linq;
using Api.Domain.DTOs.Cep;
using Api.Domain.Entities;
using Api.Domain.Models;
using Xunit;

namespace Api.Service.Test.AutoMapper
{
  public class CepMapper : BaseTestService
  {
    [Fact(DisplayName = "É possivel Mapear os Modelos de Cep")]
    public void E_Possivel_Mapear_os_Modelos_de_Cep()
    {

      var model = new CepModel
      {
        Id = Guid.NewGuid(),
        Cep = Faker.RandomNumber.Next(1, 10000).ToString(),
        Logradouro = Faker.Address.StreetName(),
        Numero = "",
        CreateAt = DateTime.UtcNow,
        UpdateAt = DateTime.UtcNow,
        MunicipioId = Guid.NewGuid()
      };

      var listaEntity = new List<CepEntity>();
      for (int i = 0; i < 5; i++)
      {
        var item = new CepEntity
        {
          Id = Guid.NewGuid(),
          Cep = Faker.RandomNumber.Next(1, 10000).ToString(),
          Logradouro = Faker.Address.StreetName(),
          Numero = Faker.RandomNumber.Next(1, 10000).ToString(),
          CreateAt = DateTime.UtcNow,
          UpdateAt = DateTime.UtcNow,
          MunicipioId = Guid.NewGuid(),
          Municipio = new MunicipioEntity
          {
            Id = Guid.NewGuid(),
            Nome = Faker.Address.City(),
            CodIBGE = Faker.RandomNumber.Next(1000000, 9999999),
            UfId = Guid.NewGuid(),
            Uf = new UfEntity
            {
              Id = Guid.NewGuid(),
              Nome = Faker.Address.UsState(),
              Sigla = Faker.Address.UsState().Substring(1, 3)
            }
          }
        };
        listaEntity.Add(item);
      }

      //Model to Entity
      var entity = Mapper.Map<CepEntity>(model);
      Assert.Equal(entity.Id, model.Id);
      Assert.Equal(entity.Logradouro, model.Logradouro);
      Assert.Equal(entity.Numero, model.Numero);
      Assert.Equal(entity.Cep, model.Cep);
      Assert.Equal(entity.CreateAt, model.CreateAt);
      Assert.Equal(entity.UpdateAt, model.UpdateAt);
      Assert.Equal(entity.MunicipioId, model.MunicipioId);

      //Entity to Dto
      var cepDto = Mapper.Map<CepDto>(entity);
      Assert.Equal(cepDto.Id, entity.Id);
      Assert.Equal(cepDto.Logradouro, entity.Logradouro);
      Assert.Equal(cepDto.Numero, entity.Numero);
      Assert.Equal(cepDto.Cep, entity.Cep);
      Assert.Equal(cepDto.MunicipioId, entity.MunicipioId);

      var cepDtoCompleto = Mapper.Map<CepDto>(listaEntity.FirstOrDefault());
      Assert.Equal(cepDtoCompleto.Id, listaEntity.FirstOrDefault().Id);
      Assert.Equal(cepDtoCompleto.Logradouro, listaEntity.FirstOrDefault().Logradouro);
      Assert.Equal(cepDtoCompleto.Numero, listaEntity.FirstOrDefault().Numero);
      Assert.Equal(cepDtoCompleto.Cep, listaEntity.FirstOrDefault().Cep);
      Assert.Equal(cepDtoCompleto.MunicipioId, listaEntity.FirstOrDefault().MunicipioId);
      Assert.NotNull(cepDtoCompleto.Municipio);
      Assert.NotNull(cepDtoCompleto.Municipio.Uf);

      var listaDto = Mapper.Map<List<CepDto>>(listaEntity);
      Assert.True(listaDto.Count() == listaEntity.Count());
      for (int i = 0; i < listaDto.Count(); i++)
      {
        Assert.Equal(listaDto[i].Id, listaEntity[i].Id);
        Assert.Equal(listaDto[i].Logradouro, listaEntity[i].Logradouro);
        Assert.Equal(listaDto[i].Numero, listaEntity[i].Numero);
        Assert.Equal(listaDto[i].Cep, listaEntity[i].Cep);
        Assert.Equal(listaDto[i].MunicipioId, listaEntity[i].MunicipioId);
      }

      var cepDtoCreateResult = Mapper.Map<CepDtoCreateResult>(entity);
      Assert.Equal(cepDtoCreateResult.Id, entity.Id);
      Assert.Equal(cepDtoCreateResult.Logradouro, entity.Logradouro);
      Assert.Equal(cepDtoCreateResult.Numero, entity.Numero);
      Assert.Equal(cepDtoCreateResult.Cep, entity.Cep);
      Assert.Equal(cepDtoCreateResult.CreateAt, entity.CreateAt);
      Assert.Equal(cepDtoCreateResult.MunicipioId, entity.MunicipioId);

      var cepDtoUpdateResult = Mapper.Map<CepDtoUpdateResult>(entity);
      Assert.Equal(cepDtoUpdateResult.Id, entity.Id);
      Assert.Equal(cepDtoUpdateResult.Logradouro, entity.Logradouro);
      Assert.Equal(cepDtoUpdateResult.Numero, entity.Numero);
      Assert.Equal(cepDtoUpdateResult.Cep, entity.Cep);
      Assert.Equal(cepDtoUpdateResult.UpdateAt, entity.UpdateAt);
      Assert.Equal(cepDtoUpdateResult.MunicipioId, entity.MunicipioId);


      //Dto to Model
      cepDto.Numero = "";
      var cepModel = Mapper.Map<CepModel>(cepDto);
      Assert.Equal(cepModel.Id, cepDto.Id);
      Assert.Equal(cepModel.Logradouro, cepDto.Logradouro);
      Assert.Equal("S/N", cepModel.Numero);
      Assert.Equal(cepModel.Cep, cepDto.Cep);
      Assert.Equal(cepModel.MunicipioId, cepDto.MunicipioId);

      var cepCreate = Mapper.Map<CepDtoCreate>(cepModel);
      Assert.Equal(cepCreate.Logradouro, cepModel.Logradouro);
      Assert.Equal(cepCreate.Numero, cepModel.Numero);
      Assert.Equal(cepCreate.Cep, cepModel.Cep);
      Assert.Equal(cepCreate.MunicipioId, cepModel.MunicipioId);

      var cepUpdate = Mapper.Map<CepDtoUpdate>(cepModel);
      Assert.Equal(cepUpdate.Id, cepModel.Id);
      Assert.Equal(cepUpdate.Logradouro, cepModel.Logradouro);
      Assert.Equal(cepUpdate.Numero, cepModel.Numero);
      Assert.Equal(cepUpdate.Cep, cepModel.Cep);
      Assert.Equal(cepUpdate.MunicipioId, cepModel.MunicipioId);

    }
  }
}
