using CarCheckup.Domain.Core.Contarcts.AppService;
using CarCheckup.Domain.Core.Dtos.Car;
using CarCheckup.Domain.Core.Dtos.CheckupRequest;
using CarCheckup.Domain.Core.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace CarCheckup.EndPoints.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CheckupRequest : ControllerBase
    {
        //[HttpPost(Name ="CreateRequestByCar")]
        //public GetCheckupRequestDto? CreateWithCar([FromBody] CarDto car)
        //{
        //    var id = carAppService.GetCarId(car.Plate);
        //    if (id == 0)
        //    {
        //        id = carAppService.Create(car);
        //    }
        //    checkupRequestAppService.Create(id);
        //    return checkupRequestAppService.GetByCarId(id);
        //}
        private readonly ICheckupRequestAppService _checkupRequestAppService;
        public  CheckupRequest(ICheckupRequestAppService checkupRequestAppService)
        {
            _checkupRequestAppService = checkupRequestAppService;
            //_checkupRequestAppService.SetRequestsToIncompleted();
        }
        [HttpPost("create")]
        public async Task< IActionResult> Create(int carId,CancellationToken cancellationToken)
        {
            var result = await _checkupRequestAppService.Create(carId, cancellationToken);
            if (result.IsSuccess)
                return Ok(new { message = result.Message, checkupRequest =await _checkupRequestAppService.GetByCarId(carId, cancellationToken) });
            return BadRequest(result.Message);

        }
        [HttpGet(("get"))]
        public async Task< IActionResult> GetByCarId(int carId,CancellationToken cancellationToken)
        {
            var item = _checkupRequestAppService.GetByCarId(carId,cancellationToken);
            if (item == null)
                return NotFound();
            return Ok(item);
        }
    }
}
