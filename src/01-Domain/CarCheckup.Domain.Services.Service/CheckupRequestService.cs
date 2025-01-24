using CarCheckup.Domain.Core.Contarcts.Repository;
using CarCheckup.Domain.Core.Contarcts.Service;
using CarCheckup.Domain.Core.Dtos.CheckupRequest;
using CarCheckup.Domain.Core.Entities;
using CarCheckup.Domain.Core.Enums.Car;

namespace CarCheckup.Domain.Services.Service;

public class CheckupRequestService(ICheckupRequestRepository checkupRequestRepository) : ICheckupRequestService
{
    private readonly ICheckupRequestRepository _checkupRequestRepository = checkupRequestRepository;
    public bool Create(CheckupRequest checkupRequest)
    {
        return _checkupRequestRepository.Create(checkupRequest);
    }

    public List<GetCheckupRequestDto> GeByCarModel(int modelId)
    {
        return _checkupRequestRepository.GeByCarModel(modelId);
    }

    public List<GetCheckupRequestDto> GetByDate(DateOnly timeToDone)
    {
        return _checkupRequestRepository.GetByDate(timeToDone);
    }

    public bool MarkAsAccepted(int id)
    {
        return _checkupRequestRepository.MarkAsAccepted(id);
    }

    public int GetDailyCount(DateOnly date)
    {
       return _checkupRequestRepository.GetDailyCount(date);
    }

    public DateOnly? GetLastCheckupDate(CarCompanyEnum carCompany)
    {
        return _checkupRequestRepository.GetLastCheckupDate(carCompany);
    }

    public DateOnly GetLastCheckupDateByCar(int carId)
    {
        return _checkupRequestRepository.GetLastCheckupDateByCar(carId);
    }

    public bool SetRequestsToIncompleted()
    {
        return _checkupRequestRepository.SetRequestsToIncompleted();
    }

    public List<GetCheckupRequestDto> GettAll()
    {
        return _checkupRequestRepository.GettAll();
    }

    public bool MarkAsRejected(int id)
    {
        return _checkupRequestRepository.MarkAsRejected(id);
    }

    public GetCheckupRequestDto? GetByCarId(int id)
    {
        return _checkupRequestRepository.GetByCarId(id);
    }
}
