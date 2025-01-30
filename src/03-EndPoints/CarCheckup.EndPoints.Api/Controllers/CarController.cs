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
        public async Task< IActionResult> Create([FromBody] CarDto car,CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (car is null)
            {
                return BadRequest("Car Data is Required");
            }

            var carId = await carAppService.GetCarId(car.Plate,cancellationToken);
            if (carId == 0)
            {
                carId = await carAppService.Create(car, cancellationToken);
                if (carId == 0)
                    return BadRequest("you Entered Invalid ModelId");
            }
            return CreatedAtAction(nameof(Get), new { id = carId }, carId);
        }
        [HttpGet("get")]
        public async Task< IActionResult> Get(int id,CancellationToken cancellationToken)
        {
            var car = await carAppService.Get(id, cancellationToken);
            if (car is null)
                return NotFound();
            return Ok(car);
        }
    }
}
