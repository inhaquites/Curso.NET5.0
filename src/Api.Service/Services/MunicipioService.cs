using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Domain.DTOs.Municipio;
using Api.Domain.Entities;
using Api.Domain.Interfaces.Services.Municipio;
using Api.Domain.Models;
using Api.Domain.Repository;
using AutoMapper;

namespace Api.Service.Services
{
  public class MunicipioService : IMunicipioService
  {
    private readonly IMunicipioRepository _repository;
    private readonly IMapper _mapper;

    public MunicipioService(IMunicipioRepository repository, IMapper mapper)
    {
      _repository = repository;
      _mapper = mapper;
    }

    public async Task<MunicipioDto> Get(Guid Id)
    {
      var entity = await _repository.SelectAsync(Id);
      return _mapper.Map<MunicipioDto>(entity);
    }
    public async Task<MunicipioDtoCompleto> GetCompleteById(Guid Id)
    {
      var entity = await _repository.GetCompleteById(Id);
      return _mapper.Map<MunicipioDtoCompleto>(entity);
    }
    public async Task<MunicipioDtoCompleto> GetCompleteByIBGE(int codIBGE)
    {
      var entity = await _repository.GetCompleteByIBGE(codIBGE);
      return _mapper.Map<MunicipioDtoCompleto>(entity);
    }

    public async Task<IEnumerable<MunicipioDto>> GetAll()
    {
      var listentity = await _repository.SelectAsync();
      return _mapper.Map<IEnumerable<MunicipioDto>>(listentity.OrderBy(m => m.Nome));
    }

    public async Task<MunicipioDtoCreateResult> Post(MunicipioDtoCreate municipio)
    {
      var model = _mapper.Map<MunicipioModel>(municipio);
      var entity = _mapper.Map<MunicipioEntity>(model);
      var result = await _repository.InsertAsync(entity);

      return _mapper.Map<MunicipioDtoCreateResult>(result);
    }

    public async Task<MunicipioDtoUpdateResult> Put(MunicipioDtoUpdate municipio)
    {
      var model = _mapper.Map<MunicipioModel>(municipio);
      var entity = _mapper.Map<MunicipioEntity>(model);
      var result = await _repository.UpdateAsync(entity);

      return _mapper.Map<MunicipioDtoUpdateResult>(result);
    }

    public async Task<bool> Delete(Guid Id)
    {
      return await _repository.DeleteAsync(Id);
    }
  }
}
