using System;
using Api.Domain.DTOs.Uf;

namespace Api.Domain.DTOs.Municipio
{
  public class MunicipioDtoCompleto
  {
    public Guid Id { get; set; }
    public string Nome { get; set; }
    public int CodIBGE { get; set; }
    public Guid UfId { get; set; }
    public UfDto Uf { get; set; }
  }
}
