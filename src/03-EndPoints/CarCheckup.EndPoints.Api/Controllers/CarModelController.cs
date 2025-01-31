using CarCheckup.Domain.Core.Contarcts.AppService;
using CarCheckup.Domain.Core.Entities;
using CarCheckup.Domain.Core.Entities.Configs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace CarCheckup.EndPoints.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CarModelController(ICarModelAppService carModelAppService) : ControllerBase
{
    [HttpPost("create")]
    public async Task< IActionResult> Create(string name, [FromHeader] string apikey,CancellationToken cancellationToken)
    {
        if (!string.IsNullOrEmpty(name))
        {
            var result = await carModelAppService.Create(name, cancellationToken);
            return Ok(result.Message);
        }
        return BadRequest("Model Name Can Not be Empty or Null");

    }
    [HttpGet("get-all")]
    public async Task< IActionResult> GetAll([FromHeader] string apiKey,CancellationToken cancellationToken)
    {
        var item =  await carModelAppService.GetAll(cancellationToken);
        if (item.Count == 0 || item == null)
            return NoContent();
        return Ok(item);
    }
    [HttpGet("get-byId")]
    public async Task< IActionResult> Get(int id, [FromHeader] string apiKey,CancellationToken cancellationToken)
    {
        var model = await carModelAppService.GetById(id,cancellationToken);
        if (model is null)
            return NotFound();
        return Ok(model);
    }
    [HttpPatch("update")]
    public async Task< IActionResult> Update(int id, string name, [FromHeader] string apiKey,CancellationToken cancellationToken)
    {

        var result  = await carModelAppService.UpdateName(id, name,cancellationToken);
        if (result.IsSuccess)
            return Ok(result.Message);
        else
            return BadRequest(result.Message);
    }
    [HttpDelete("delete")]
    public async Task< IActionResult> Delete([FromQuery] int id, [FromHeader] string apiKey,CancellationToken cancellationToken)
    {
        var result = await carModelAppService.Delete(id,cancellationToken);
        if (result.IsSuccess)
            return Ok(result.Message);
        return BadRequest(result.Message);

    }

}
