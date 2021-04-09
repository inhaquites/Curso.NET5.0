using System;
using System.Collections.Generic;
using System.Linq;
using Api.Domain.DTOs.Uf;
using Api.Domain.Entities;
using Api.Domain.Models;
using Xunit;

namespace Api.Service.Test.AutoMapper
{
  public class UfMapper : BaseTestService
  {
    [Fact(DisplayName = "É possivel Mapear os Modelos de UF")]
    public void E_Possivel_Mapear_os_Modelos_de_UF()
    {
      var model = new UfModel
      {
        Id = Guid.NewGuid(),
        Nome = Faker.Address.UsState(),
        Sigla = Faker.Address.UsState().Substring(1, 3),
        CreateAt = DateTime.UtcNow,
        UpdateAt = DateTime.UtcNow
      };

      var listaEntity = new List<UfEntity>();
      for (int i = 0; i < 5; i++)
      {
        var item = new UfEntity
        {
          Id = Guid.NewGuid(),
          Nome = Faker.Address.UsState(),
          Sigla = Faker.Address.UsState().Substring(1, 3),
          CreateAt = DateTime.UtcNow,
          UpdateAt = DateTime.UtcNow
        };
        listaEntity.Add(item);
      }

      //model to entity
      var entity = Mapper.Map<UfEntity>(model);
      Assert.Equal(entity.Id, model.Id);
      Assert.Equal(entity.Nome, model.Nome);
      Assert.Equal(entity.Sigla, model.Sigla);
      Assert.Equal(entity.CreateAt, model.CreateAt);
      Assert.Equal(entity.UpdateAt, model.UpdateAt);

      //entity to Dto
      var ufDto = Mapper.Map<UfDto>(entity);
      Assert.Equal(ufDto.Id, entity.Id);
      Assert.Equal(ufDto.Nome, entity.Nome);
      Assert.Equal(ufDto.Sigla, entity.Sigla);

      var listaDto = Mapper.Map<List<UfDto>>(listaEntity);
      Assert.True(listaDto.Count() == listaEntity.Count());
      for (int i = 0; i < listaDto.Count(); i++)
      {
        Assert.Equal(listaDto[i].Id, listaEntity[i].Id);
        Assert.Equal(listaDto[i].Nome, listaEntity[i].Nome);
        Assert.Equal(listaDto[i].Sigla, listaEntity[i].Sigla);
      }

      //Dto to model
      var ufModel = Mapper.Map<UfDto>(model);
      Assert.Equal(ufModel.Id, model.Id);
      Assert.Equal(ufModel.Nome, model.Nome);
      Assert.Equal(ufModel.Sigla, model.Sigla);
    }
  }
}
