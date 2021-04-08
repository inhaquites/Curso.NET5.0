using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Domain.DTOs.Uf;
using Api.Domain.Interfaces.Services.Uf;
using Api.Domain.Repository;
using AutoMapper;

namespace Api.Service.Services
{
  public class UfService : IUfService
  {
    private readonly IUfRepository _repository;
    private readonly IMapper _mapper;

    public UfService(IUfRepository repository, IMapper mapper)
    {
      _repository = repository;
      _mapper = mapper;
    }

    public async Task<UfDto> Get(Guid Id)
    {
      var entity = await _repository.SelectAsync(Id);
      return _mapper.Map<UfDto>(entity);
    }

    public async Task<IEnumerable<UfDto>> GetAll()
    {
      var listEntity = await _repository.SelectAsync();
      return _mapper.Map<IEnumerable<UfDto>>(listEntity.OrderBy(u => u.Nome));
    }
  }
}
