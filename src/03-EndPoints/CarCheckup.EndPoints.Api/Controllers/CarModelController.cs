using CarCheckup.Domain.Core.Contarcts.AppService;
using CarCheckup.Domain.Core.Entities;
using CarCheckup.Domain.Core.Entities.Configs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace CarCheckup.EndPoints.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CarModelController(ICarModelAppService carModelAppService,SiteSettings siteSettings) : ControllerBase
{
    [HttpPost("Create")]
    public string? Create(string name, [FromHeader] string apikey)
    {
        if (apikey == siteSettings.ApiKey)
        {
            return carModelAppService.Create(name).Message;
        }
        return "api key not found";
    }
    [HttpGet("GetAll")]
    public List<CarModel> GetAll([FromHeader]string apiKey)
    {
        if (apiKey == siteSettings.ApiKey)
        {
            return carModelAppService.GetAll();
        }
        return [];
    }
    [HttpGet("Get")]
    public CarModel? Get(int id, [FromHeader] string apiKey)
    {
        if (apiKey == siteSettings.ApiKey)
        {

            return carModelAppService.GetById(id);
        }
        return null;
    }
    [HttpPatch("Update")]
    public bool Update([FromQuery] int id, [FromQuery] string name, [FromHeader] string apiKey)
    {
        if (apiKey == siteSettings.ApiKey)
        {
            return carModelAppService.UpdateName(id, name).IsSuccess;
        }
        return false;
    }
    [HttpDelete("Delete")]
    public bool Delete([FromQuery] int id, [FromHeader] string apiKey)
    {
        if (apiKey == siteSettings.ApiKey)
        {
            return carModelAppService.Delete(id).IsSuccess;
        }
        return false;
    }
  
}
