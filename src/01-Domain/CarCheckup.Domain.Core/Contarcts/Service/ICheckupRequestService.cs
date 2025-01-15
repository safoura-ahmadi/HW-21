using CarCheckup.Domain.Core.Dtos;
using CarCheckup.Domain.Core.Entities;
using CarCheckup.Domain.Core.Enums.Car;

namespace CarCheckup.Domain.Core.Contarcts.Service;

public interface ICheckupRequestService
{
    bool Create(CheckupRequest checkupRequest);

    //Read
    List<GetCheckupRequestDto> GeByCarModel(int modelId);
    List<GetCheckupRequestDto> GetByDate(DateOnly timeToDone);
    int GetDailyCount(DateOnly date);

    //Update
    bool MarkAsCompleted(int id);
    DateOnly? GetLastCheckupDate(CarCompanyEnum carCompany);
    public DateOnly GetLastCheckupDateByCar(int carId);

    bool SetRequestsToIncompleted();
}
