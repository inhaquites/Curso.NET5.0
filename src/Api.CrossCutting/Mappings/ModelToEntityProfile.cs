using Api.Domain.Entities;
using Api.Domain.Models;
using AutoMapper;

namespace Api.CrossCutting.Mappings
{
  public class ModelToEntityProfile : Profile
  {
    public ModelToEntityProfile()
    {
      //Usuario
      CreateMap<UserEntity, UserModel>().ReverseMap();

      //UF
      CreateMap<UfEntity, UfModel>().ReverseMap();

      //Municipio
      CreateMap<MunicipioEntity, MunicipioModel>().ReverseMap();

      //Cep
      CreateMap<CepEntity, CepModel>().ReverseMap();
    }
  }
}
