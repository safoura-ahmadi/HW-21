using CarCheckup.Domain.Core.Dtos;
using CarCheckup.Domain.Core.Entities;
using CarCheckup.Domain.Core.Enums.Car;

namespace CarCheckup.Domain.Core.Contarcts.AppService;

public interface ICheckupRequestAppService
{
    Result Create(int carId, DateOnly timeToDone);
    DateOnly DetermineTimetoDone(CarCompanyEnum carCompany);
    string MarkAsCompleted(int id);
    List<GetCheckupRequestDto> GeByCarModel(int modelId);
    List<GetCheckupRequestDto> GetByDate(DateOnly timeToDone);
    bool SetRequestsToIncompleted();
}
