using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Api.Domain.DTOs.Municipio;
using Newtonsoft.Json;
using Xunit;

namespace Api.Integration.Test.Municipio
{
  public class QuandoRequisitarMunicipio : BaseIntegration
  {
    [Fact]
    public async Task E_Possivel_Realizar_Crud_Municipio()
    {
      await AdicionarToken();

      var municipioDto = new MunicipioDtoCreate
      {
        Nome = "Guaíba",
        CodIBGE = 123456,
        UfId = new Guid("88970a32-3a2a-4a95-8a18-2087b65f59d1")
      };

      //Post
      var response = await PostJsonAsync(municipioDto, $"{hostApi}municipios", client);
      var postResult = await response.Content.ReadAsStringAsync();
      var registroPost = JsonConvert.DeserializeObject<MunicipioDtoCreateResult>(postResult);
      Assert.Equal(HttpStatusCode.Created, response.StatusCode);
      Assert.Equal("Guaíba", registroPost.Nome);
      Assert.Equal(123456, registroPost.CodIBGE);
      Assert.True(registroPost.Id != default(Guid));

      //GetAll
      response = await client.GetAsync($"{hostApi}municipios");
      var jsonResult = await response.Content.ReadAsStringAsync();
      var listaFromJson = JsonConvert.DeserializeObject<IEnumerable<MunicipioDto>>(jsonResult);
      Assert.NotNull(listaFromJson);
      Assert.True(listaFromJson.Count() > 0);
      Assert.True(listaFromJson.Where(x => x.Id == registroPost.Id).Count() == 1);

      //Put
      var municipioDtoAtualizado = new MunicipioDtoUpdate
      {
        Id = registroPost.Id,
        Nome = "Porto Alegre",
        CodIBGE = 654321,
        UfId = new Guid("88970a32-3a2a-4a95-8a18-2087b65f59d1")
      };

      var stringContent = new StringContent(JsonConvert.SerializeObject(municipioDtoAtualizado),
                              Encoding.UTF8, "application/json");

      response = await client.PutAsync($"{hostApi}municipios", stringContent);
      jsonResult = await response.Content.ReadAsStringAsync();
      var registroAtualizado = JsonConvert.DeserializeObject<MunicipioDtoUpdateResult>(jsonResult);

      Assert.Equal(HttpStatusCode.OK, response.StatusCode);
      Assert.Equal("Porto Alegre", registroAtualizado.Nome);
      Assert.Equal(654321, registroAtualizado.CodIBGE);

      //Get Id
      response = await client.GetAsync($"{hostApi}municipios/{registroAtualizado.Id}");
      Assert.Equal(HttpStatusCode.OK, response.StatusCode);
      jsonResult = await response.Content.ReadAsStringAsync();
      var registroSelecionado = JsonConvert.DeserializeObject<MunicipioDto>(jsonResult);
      Assert.NotNull(registroSelecionado);
      Assert.Equal(registroSelecionado.Id, registroAtualizado.Id);
      Assert.Equal(registroSelecionado.Nome, registroAtualizado.Nome);
      Assert.Equal(registroSelecionado.CodIBGE, registroAtualizado.CodIBGE);

      //Get CompleteById/Id
      response = await client.GetAsync($"{hostApi}municipios/CompleteById/{registroAtualizado.Id}");
      Assert.Equal(HttpStatusCode.OK, response.StatusCode);
      jsonResult = await response.Content.ReadAsStringAsync();
      var registroSelecionadoCompletoById = JsonConvert.DeserializeObject<MunicipioDtoCompleto>(jsonResult);
      Assert.NotNull(registroSelecionadoCompletoById);
      Assert.Equal(registroSelecionadoCompletoById.Nome, registroAtualizado.Nome);
      Assert.Equal(registroSelecionadoCompletoById.CodIBGE, registroAtualizado.CodIBGE);
      Assert.NotNull(registroSelecionadoCompletoById.Uf);
      Assert.Equal("Rio Grande do Sul", registroSelecionadoCompletoById.Uf.Nome);
      Assert.Equal("RS", registroSelecionadoCompletoById.Uf.Sigla);


      //Get CompleteByIBGE/Id      
      response = await client.GetAsync($"{hostApi}municipios/CompleteByIBGE/{registroAtualizado.CodIBGE}");
      Assert.Equal(HttpStatusCode.OK, response.StatusCode);
      jsonResult = await response.Content.ReadAsStringAsync();
      var registroSelecionadoCompletoByIBGE = JsonConvert.DeserializeObject<MunicipioDtoCompleto>(jsonResult);
      Assert.NotNull(registroSelecionadoCompletoByIBGE);
      Assert.Equal(registroSelecionadoCompletoByIBGE.Nome, registroAtualizado.Nome);
      Assert.Equal(registroSelecionadoCompletoByIBGE.CodIBGE, registroAtualizado.CodIBGE);
      Assert.NotNull(registroSelecionadoCompletoByIBGE.Uf);
      Assert.Equal("Rio Grande do Sul", registroSelecionadoCompletoByIBGE.Uf.Nome);
      Assert.Equal("RS", registroSelecionadoCompletoByIBGE.Uf.Sigla);

      //Delete
      response = await client.DeleteAsync($"{hostApi}municipios/{registroSelecionado.Id}");
      Assert.Equal(HttpStatusCode.OK, response.StatusCode);

      //Get ID depois do Delete
      response = await client.GetAsync($"{hostApi}municipios/{registroSelecionado.Id}");
      Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);

    }

  }
}
