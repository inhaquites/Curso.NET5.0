using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Api.Domain.DTOs.Cep;
using Api.Domain.DTOs.Municipio;
using Newtonsoft.Json;
using Xunit;

namespace Api.Integration.Test.Cep
{
  public class QuandoRequisitarCep : BaseIntegration
  {
    [Fact]
    public async Task E_Possivel_Realizar_Crud_Cep()
    {
      await AdicionarToken();

      var municipioDto = new MunicipioDtoCreate
      {
        Nome = "Guaíba",
        CodIBGE = 123456,
        UfId = new Guid("88970a32-3a2a-4a95-8a18-2087b65f59d1")
      };

      //Insere Municipio pra usar o ID Gerado no cadastro do CEP
      var response = await PostJsonAsync(municipioDto, $"{hostApi}Municipios", client);
      var postResult = await response.Content.ReadAsStringAsync();
      var registroPostMunicipio = JsonConvert.DeserializeObject<MunicipioDtoCreateResult>(postResult);
      Assert.Equal(HttpStatusCode.Created, response.StatusCode);
      Assert.Equal("Guaíba", registroPostMunicipio.Nome);
      Assert.Equal(123456, registroPostMunicipio.CodIBGE);
      Assert.True(registroPostMunicipio.Id != default(Guid));

      var cepDto = new CepDtoCreate
      {
        Cep = "92714-630",
        Logradouro = "Rua Leopoldo Rassier",
        Numero = "242",
        MunicipioId = registroPostMunicipio.Id
      };

      //Post
      response = await PostJsonAsync(cepDto, $"{hostApi}Ceps", client);
      postResult = await response.Content.ReadAsStringAsync();
      var registroPost = JsonConvert.DeserializeObject<CepDtoCreateResult>(postResult);
      Assert.Equal(HttpStatusCode.Created, response.StatusCode);
      Assert.Equal("92714-630", registroPost.Cep);
      Assert.Equal("Rua Leopoldo Rassier", registroPost.Logradouro);
      Assert.True(registroPost.Id != default(Guid));

      //Put
      var cepDtoAtualizado = new CepDtoUpdate
      {
        Id = registroPost.Id,
        Cep = "92714-630",
        Logradouro = "Rua Leopoldo Rassier (casa de esquina)",
        Numero = "242",
        MunicipioId = registroPostMunicipio.Id
      };

      var stringContent = new StringContent(JsonConvert.SerializeObject(cepDtoAtualizado),
                              Encoding.UTF8, "application/json");

      response = await client.PutAsync($"{hostApi}Ceps", stringContent);
      var jsonResult = await response.Content.ReadAsStringAsync();
      var registroAtualizado = JsonConvert.DeserializeObject<CepDtoUpdateResult>(jsonResult);

      Assert.Equal(HttpStatusCode.OK, response.StatusCode);
      Assert.Equal("Rua Leopoldo Rassier (casa de esquina)", registroAtualizado.Logradouro);
      Assert.Equal(registroPost.Id, registroAtualizado.Id);

      //Get por ID
      response = await client.GetAsync($"{hostApi}Ceps/{registroAtualizado.Id}");
      Assert.Equal(HttpStatusCode.OK, response.StatusCode);
      jsonResult = await response.Content.ReadAsStringAsync();
      var registroSelecionado = JsonConvert.DeserializeObject<CepDto>(jsonResult);
      Assert.NotNull(registroSelecionado);
      Assert.Equal(registroSelecionado.Id, registroAtualizado.Id);
      Assert.Equal(registroSelecionado.Cep, registroAtualizado.Cep);
      Assert.Equal(registroSelecionado.Logradouro, registroAtualizado.Logradouro);

      //Get por Cep
      response = await client.GetAsync($"{hostApi}Ceps/byCep/{registroAtualizado.Cep}");
      Assert.Equal(HttpStatusCode.OK, response.StatusCode);
      jsonResult = await response.Content.ReadAsStringAsync();
      registroSelecionado = JsonConvert.DeserializeObject<CepDto>(jsonResult);
      Assert.NotNull(registroSelecionado);
      Assert.Equal(registroSelecionado.Cep, registroAtualizado.Cep);
      Assert.Equal(registroSelecionado.Logradouro, registroAtualizado.Logradouro);

      //Delete
      response = await client.DeleteAsync($"{hostApi}Ceps/{registroSelecionado.Id}");
      Assert.Equal(HttpStatusCode.OK, response.StatusCode);

      //Get ID depois do Delete
      response = await client.GetAsync($"{hostApi}Ceps/{registroSelecionado.Id}");
      Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);

    }
  }
}
