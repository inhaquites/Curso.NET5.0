using Api.Domain.DTOs.Cep;
using Api.Domain.DTOs.Municipio;
using Api.Domain.DTOs.Uf;
using Api.Domain.DTOs.User;
using Api.Domain.Models;
using AutoMapper;

namespace Api.CrossCutting.Mappings
{
  public class DtoToModelProfile : Profile
  {
    public DtoToModelProfile()
    {
      #region Usuario
      CreateMap<UserModel, UserDTO>().ReverseMap();
      CreateMap<UserModel, UserDTOCreate>().ReverseMap();
      CreateMap<UserModel, UserDTOUpdate>().ReverseMap();
      #endregion

      #region UF
      CreateMap<UfModel, UfDto>().ReverseMap();
      #endregion

      #region Municipio
      CreateMap<MunicipioModel, MunicipioDto>().ReverseMap();
      CreateMap<MunicipioModel, MunicipioDtoCreate>().ReverseMap();
      CreateMap<MunicipioModel, MunicipioDtoUpdate>().ReverseMap();
      #endregion

      #region Cep
      CreateMap<CepModel, CepDto>().ReverseMap();
      CreateMap<CepModel, CepDtoCreate>().ReverseMap();
      CreateMap<CepModel, CepDtoUpdate>().ReverseMap();
      #endregion


    }

  }
}
