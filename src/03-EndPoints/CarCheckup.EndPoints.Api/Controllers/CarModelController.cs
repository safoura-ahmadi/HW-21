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
    public IActionResult Create(string name, [FromHeader] string apikey)
    {
        if (!string.IsNullOrEmpty(name))
        {
            var result = carModelAppService.Create(name).Message;
            return Ok(result);
        }
        return BadRequest("Model Name Can Not be Empty or Null");

    }
    [HttpGet("get-all")]
    public IActionResult GetAll([FromHeader] string apiKey)
    {
        var item = carModelAppService.GetAll();
        if (item.Count == 0 || item == null)
            return NoContent();
        return Ok(item);
    }
    [HttpGet("get-byId")]
    public IActionResult Get(int id, [FromHeader] string apiKey)
    {
        var model = carModelAppService.GetById(id);
        if (model is null)
            return NotFound();
        return Ok(model);
    }
    [HttpPatch("update")]
    public IActionResult Update(int id, string name, [FromHeader] string apiKey)
    {

        var result = carModelAppService.UpdateName(id, name);
        if (result.IsSuccess)
            return Ok(result.Message);
        else
            return BadRequest(result.Message);
    }
    [HttpDelete("delete")]
    public IActionResult Delete([FromQuery] int id, [FromHeader] string apiKey)
    {
        var result = carModelAppService.Delete(id);
        if (result.IsSuccess)
            return Ok(result.Message);
        return BadRequest(result.Message);

    }

}
