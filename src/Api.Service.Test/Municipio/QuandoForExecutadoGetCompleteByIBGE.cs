using System;
using System.Threading.Tasks;
using Api.Domain.DTOs.Municipio;
using Api.Domain.Interfaces.Services.Municipio;
using Moq;
using Xunit;

namespace Api.Service.Test.Municipio
{
  public class QuandoForExecutadoGetCompleteByIBGE : MunicipioTestes
  {
    private IMunicipioService _service;
    private Mock<IMunicipioService> _serviceMock;

    [Fact(DisplayName = "É possível executar o método GET Complete by IBGE")]
    public async Task E_Possivel_Executar_Metodo_Get_Complete_By_IBGE()
    {
      _serviceMock = new Mock<IMunicipioService>();
      _serviceMock.Setup(m => m.GetCompleteByIBGE(CodigoIBGEMunicipio)).ReturnsAsync(municipioDtoCompleto);
      _service = _serviceMock.Object;

      var result = await _service.GetCompleteByIBGE(CodigoIBGEMunicipio);
      Assert.NotNull(result);
      Assert.True(result.Id == IdMunicipio);
      Assert.Equal(NomeMunicipio, result.Nome);
      Assert.Equal(CodigoIBGEMunicipio, result.CodIBGE);
      Assert.NotNull(result.Uf);
      



    }
  }
}
