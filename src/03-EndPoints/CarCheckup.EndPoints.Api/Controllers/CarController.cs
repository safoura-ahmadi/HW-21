using CarCheckup.Domain.Core.Contarcts.AppService;
using CarCheckup.Domain.Core.Dtos.Car;
using CarCheckup.Domain.Core.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarCheckup.EndPoints.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarController(ICarAppService carAppService) : ControllerBase
    {
        public int Create(CarDto car)
        {
            return carAppService.Create(car);
        }
        public CarDto? Get(int id)
        {
            var item = carAppService.Get(id);
            return item;
        }
    }
}
