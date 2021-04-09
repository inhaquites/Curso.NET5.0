using Api.Domain.DTOs.Uf;
using Api.Domain.Interfaces.Services.Uf;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Api.Application.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class UfsController : ControllerBase
  {
    private readonly IUfService _service;

    public UfsController(IUfService service)
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
    [Route("{id}")]
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

  }
}
