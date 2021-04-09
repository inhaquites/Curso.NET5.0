using Api.Domain.DTOs.Cep;
using Api.Domain.DTOs.Municipio;
using Api.Domain.DTOs.Uf;
using Api.Domain.DTOs.User;
using Api.Domain.Entities;
using AutoMapper;

namespace Api.CrossCutting.Mappings
{
  public class EntityToDtoProfile : Profile
  {
    public EntityToDtoProfile()
    {
      #region Usuario
      CreateMap<UserDTO, UserEntity>().ReverseMap();
      CreateMap<UserDTOCreateResult, UserEntity>().ReverseMap();
      CreateMap<UserDTOUpdateResult, UserEntity>().ReverseMap();
      #endregion

      #region UF
      CreateMap<UfDto, UfEntity>().ReverseMap();
      #endregion

      #region Municipio
      CreateMap<MunicipioDto, MunicipioEntity>().ReverseMap();
      CreateMap<MunicipioDtoCompleto, MunicipioEntity>().ReverseMap();
      CreateMap<MunicipioDtoCreateResult, MunicipioEntity>().ReverseMap();
      CreateMap<MunicipioDtoUpdateResult, MunicipioEntity>().ReverseMap();
      #endregion

      #region Cep
      CreateMap<CepDto, CepEntity>().ReverseMap();
      CreateMap<CepDtoCreateResult, CepEntity>().ReverseMap();
      CreateMap<CepDtoUpdateResult, CepEntity>().ReverseMap();
      #endregion




    }

  }
}
