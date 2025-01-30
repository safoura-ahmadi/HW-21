using CarCheckup.Domain.Core.Dtos.CheckupRequest;
using CarCheckup.Domain.Core.Entities;
using CarCheckup.Domain.Core.Enums.Car;

namespace CarCheckup.Domain.Core.Contarcts.AppService;

public interface ICheckupRequestAppService
{
    Result Create(int carId);
    DateOnly DetermineTimetoDone(CarCompanyEnum carCompany);
    Result MarkAsAccepted(int id);
    Result MarkAsRejected(int id);
    List<GetCheckupRequestDto> GeByCarModel(int modelId);
    List<GetCheckupRequestDto> GetByDate(DateOnly timeToDone);
    List<GetCheckupRequestDto> GettAll();
    GetCheckupRequestDto? GetByCarId(int id);
    public string ConvertDateToPersion(DateOnly date);
    bool SetRequestsToIncompleted();
}
