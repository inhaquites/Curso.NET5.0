using System;
using System.Linq;
using System.Threading.Tasks;
using Api.Data.Context;
using Api.Data.Implementations;
using Api.Domain.Entities;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Api.Data.Test.Uf
{
  public class UfGets : BaseTest, IClassFixture<DbTeste>
  {
    private ServiceProvider _serviceProvider;
    public UfGets(DbTeste dbTeste)
    {
      _serviceProvider = dbTeste.ServiceProvider;
    }

    [Fact(DisplayName = "Gets de UF")]
    [Trait("GETs", "UfEntity")]
    public async Task E_Possivel_Realizar_Gets_UF()
    {
      using (var context = _serviceProvider.GetService<MyContext>())
      {
        UfImplementation _repositorio = new UfImplementation(context);
        UfEntity _entity = new UfEntity
        {
          Id = new Guid("88970a32-3a2a-4a95-8a18-2087b65f59d1"),
          Sigla = "RS",
          Nome = "Rio Grande do Sul"
        };

        var _registroExiste = await _repositorio.ExistAsync(_entity.Id);
        Assert.True(_registroExiste);

        var _registroSelecionado = await _repositorio.SelectAsync(_entity.Id);
        Assert.NotNull(_registroSelecionado);
        Assert.Equal(_entity.Sigla, _registroSelecionado.Sigla);
        Assert.Equal(_entity.Nome, _registroSelecionado.Nome);
        Assert.Equal(_entity.Id, _registroSelecionado.Id);

        var _todosRegistros = await _repositorio.SelectAsync();
        Assert.NotNull(_todosRegistros);
        Assert.True(_todosRegistros.Count() == 27);


      }
    }


  }
}
