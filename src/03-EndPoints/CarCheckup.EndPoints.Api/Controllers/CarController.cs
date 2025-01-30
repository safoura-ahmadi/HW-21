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
        [HttpPost("create")]
        public IActionResult Create([FromBody] CarDto car)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (car is null)
            {
                return BadRequest("Car Data is Required");
            }

            var carId = carAppService.GetCarId(car.Plate);
            if (carId == 0)
            {
                carId = carAppService.Create(car);
                if (carId == 0)
                    return BadRequest("you Entered Invalid ModelId");
            }
            return CreatedAtAction(nameof(Get), new { id = carId }, carId);
        }
        [HttpGet("get")]
        public IActionResult Get(int id)
        {
            var car = carAppService.Get(id);
            if (car is null)
                return NotFound();
            return Ok(car);
        }
    }
}
