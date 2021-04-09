using System;
using System.ComponentModel.DataAnnotations;

namespace Api.Domain.DTOs.Cep
{
  public class CepDtoCreate
  {
    [Required(ErrorMessage = "CEP é campo Obrigatório")]
    [MaxLength(10)]
    public string Cep { get; set; }

    [Required(ErrorMessage = "Logradouro é campo Obrigatório")]
    [MaxLength(60)]
    public string Logradouro { get; set; }

    public string Numero { get; set; }

    [Required(ErrorMessage = "Município é campo Obrigatório")]
    public Guid MunicipioId { get; set; }
  }
}
