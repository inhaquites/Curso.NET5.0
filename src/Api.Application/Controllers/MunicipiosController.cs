using Api.Domain.DTOs.Municipio;
using Api.Domain.Interfaces.Services.Municipio;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Api.Application.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class MunicipiosController : ControllerBase
  {
    private readonly IMunicipioService _service;

    public MunicipiosController(IMunicipioService service)
    {
      _service = service;
    }

    [Authorize("Bearer")]
    [HttpGet]
    public async Task<ActionResult> GetAll()
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState); //400 bad request - solicitacao inválida
      }
      try
      {
        return Ok(await _service.GetAll());
      }
      catch (ArgumentException e)
      {
        return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
      }
    }

    [Authorize("Bearer")]
    [HttpGet]
    [Route("{id}", Name = "GetMunicipioWithId")]
    public async Task<ActionResult> Get(Guid id)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState); //400 bad request - solicitacao inválida
      }
      try
      {
        var result = await _service.Get(id);
        if (result == null)
          return NotFound();
        return Ok(result);
      }
      catch (ArgumentException e)
      {
        return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
      }
    }

    /// <summary>
    /// Busca Obj completo por ID
    /// </summary>
    /// <returns>Busca Municipio completo por ID</returns>
    [Authorize("Bearer")]
    [HttpGet]
    [Route("CompleteById/{id}")]
    public async Task<ActionResult> GetCompleteById(Guid id)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState); //400 bad request - solicitacao inválida
      }
      try
      {
        var result = await _service.GetCompleteById(id);
        if (result == null)
          return NotFound();
        return Ok(result);
      }
      catch (ArgumentException e)
      {
        return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
      }
    }


    /// <summary>
    /// retorna municipio pelo codigo do IBGE
    /// </summary>
    /// <param name="codIBGE">Código do IBGE</param>
    /// <returns>Busca Municipio completo por Código do IBGE</returns>
    [Authorize("Bearer")]
    [HttpGet]
    [Route("CompleteByIBGE/{codIBGE}")]
    public async Task<ActionResult> GetCompleteByIBGE(int codIBGE)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState); //400 bad request - solicitacao inválida
      }
      try
      {
        var result = await _service.GetCompleteByIBGE(codIBGE);
        if (result == null)
          return NotFound();
        return Ok(result);
      }
      catch (ArgumentException e)
      {
        return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
      }
    }

    [Authorize("Bearer")]
    [HttpPost]
    public async Task<ActionResult> Post([FromBody] MunicipioDtoCreate dtoCreate)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState); //400 bad request - solicitacao inválida
      }
      try
      {
        var result = await _service.Post(dtoCreate);
        if (result != null)
          return Created(new Uri(Url.Link("GetMunicipioWithId", new { id = result.Id })), result);
        return BadRequest();
      }
      catch (ArgumentException e)
      {
        return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
      }
    }

    [Authorize("Bearer")]
    [HttpPut]
    public async Task<ActionResult> Put([FromBody] MunicipioDtoUpdate dtoUpdate)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState); //400 bad request - solicitacao inválida
      }
      try
      {
        var result = await _service.Put(dtoUpdate);
        if (result != null)
          return Ok(result);
        return BadRequest();
      }
      catch (ArgumentException e)
      {
        return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
      }
    }

    [Authorize("Bearer")]
    [HttpDelete]
    [Route("{id}")]
    public async Task<ActionResult> Delete(Guid id)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState); //400 bad request - solicitacao inválida
      }
      try
      {
        return Ok(await _service.Delete(id));
      }
      catch (ArgumentException e)
      {
        return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
      }
    }


  }
}
