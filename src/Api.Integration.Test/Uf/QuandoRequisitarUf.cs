using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Api.Domain.DTOs.Uf;
using Newtonsoft.Json;
using Xunit;

namespace Api.Integration.Test.Uf
{
  public class QuandoRequisitarUf : BaseIntegration
  {
    [Fact]
    public async Task E_Possivel_Realizar_Crud_Uf()
    {
      await AdicionarToken();

      //Get All
      response = await client.GetAsync($"{hostApi}Ufs");
      Assert.Equal(HttpStatusCode.OK, response.StatusCode);

      var jsonResult = await response.Content.ReadAsStringAsync();
      var listaFromJson = JsonConvert.DeserializeObject<IEnumerable<UfDto>>(jsonResult);
      Assert.NotNull(listaFromJson);
      Assert.True(listaFromJson.Count() == 27);
      Assert.True(listaFromJson.Where(x => x.Sigla == "RS").Count() == 1);

      //get
      var id = listaFromJson.Where(x => x.Sigla == "RS").FirstOrDefault().Id;
      response = await client.GetAsync($"{hostApi}Ufs/{id}");
      Assert.Equal(HttpStatusCode.OK, response.StatusCode);
      jsonResult = await response.Content.ReadAsStringAsync();

      var registroSelecionado = JsonConvert.DeserializeObject<UfDto>(jsonResult);
      Assert.NotNull(registroSelecionado);
      Assert.Equal("Rio Grande do Sul", registroSelecionado.Nome);
      Assert.Equal("RS", registroSelecionado.Sigla);
    }

  }
}
