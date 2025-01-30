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
        public CheckupRequest(ICheckupRequestAppService checkupRequestAppService)
        {
            _checkupRequestAppService = checkupRequestAppService;
            _checkupRequestAppService.SetRequestsToIncompleted();
        }
        [HttpPost("create")]
        public IActionResult Create(int carId)
        {
            var result = _checkupRequestAppService.Create(carId);
            if (result.IsSuccess)
                return Ok(new { message = result.Message, checkupRequest = _checkupRequestAppService.GetByCarId(carId) });
            return BadRequest(result.Message);

        }
        [HttpGet(("get"))]
        public IActionResult GetByCarId(int carId)
        {
            var item = _checkupRequestAppService.GetByCarId(carId);
            if (item == null)
                return NotFound();
            return Ok(item);
        }
    }
}
