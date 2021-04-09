using Api.Domain.DTOs.Cep;
using Api.Domain.Interfaces.Services.Cep;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Api.Application.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class CepsController : ControllerBase
  {
    private readonly ICepService _service;
    public CepsController(ICepService service)
    {
      _service = service;
    }

    [Authorize("Bearer")]
    [HttpGet]
    [Route("{id}", Name = "GetCepWithId")]
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

    [AllowAnonymous]
    [HttpGet]
    [Route("byCep/{cep}")]
    public async Task<ActionResult> Get(string cep)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState); //400 bad request - solicitacao inválida
      }
      try
      {
        var result = await _service.Get(cep);
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
    public async Task<ActionResult> Post([FromBody] CepDtoCreate dtoCreate)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState); //400 bad request - solicitacao inválida
      }
      try
      {
        var result = await _service.Post(dtoCreate);
        if (result != null)
          return Created(new Uri(Url.Link("GetCepWithId", new { id = result.Id })), result);
        return BadRequest();
      }
      catch (ArgumentException e)
      {
        return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
      }
    }

    [Authorize("Bearer")]
    [HttpPut]
    public async Task<ActionResult> Put([FromBody] CepDtoUpdate dtoUpdate)
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