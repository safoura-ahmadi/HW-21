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
    public class CheckupRequest(ICarAppService carAppService, ICheckupRequestAppService checkupRequestAppService) : ControllerBase
    {
        [HttpPost("Create")]
        public GetCheckupRequestDto? CreateWithCar([FromBody] CarDto car)
        {
            var id = carAppService.GetCarId(car.Plate);
            if (id == 0)
            {
                id = carAppService.Create(car);
            }
            checkupRequestAppService.Create(id);
            return checkupRequestAppService.GetByCarId(id);
        }
        public Result Create(int carId)
        {
            var result = checkupRequestAppService.Create(carId);
            return result;
        }
        public GetCheckupRequestDto? GetByCarId(int carId)
        {
            return checkupRequestAppService.GetByCarId(carId);
        }
    }
}
