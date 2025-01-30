using CarCheckup.Domain.Core.Contarcts.Repository;
using CarCheckup.Domain.Core.Contarcts.Service;
using CarCheckup.Domain.Core.Dtos.CheckupRequest;
using CarCheckup.Domain.Core.Entities;
using CarCheckup.Domain.Core.Enums.Car;

namespace CarCheckup.Domain.Services.Service;

public class CheckupRequestService(ICheckupRequestRepository checkupRequestRepository) : ICheckupRequestService
{
    private readonly ICheckupRequestRepository _checkupRequestRepository = checkupRequestRepository;
    public async Task<bool> Create(CheckupRequest checkupRequest, CancellationToken cancellationToken)
    {
        return await _checkupRequestRepository.Create(checkupRequest, cancellationToken);
    }

    public async Task<List<GetCheckupRequestDto>> GeByCarModel(int modelId, CancellationToken cancellationToken)
    {
        return await _checkupRequestRepository.GeByCarModel(modelId, cancellationToken);
    }

    public async Task<List<GetCheckupRequestDto>> GetByDate(DateOnly timeToDone, CancellationToken cancellationToken)
    {
        return await _checkupRequestRepository.GetByDate(timeToDone, cancellationToken);
    }

    public async Task<bool> MarkAsAccepted(int id, CancellationToken cancellationToken)
    {
        return await _checkupRequestRepository.MarkAsAccepted(id, cancellationToken);
    }

    public async Task<int> GetDailyCount(DateOnly date, CancellationToken cancellationToken)
    {
        return await _checkupRequestRepository.GetDailyCount(date, cancellationToken);
    }

    public async Task<DateOnly?> GetLastCheckupDate(CarCompanyEnum carCompany, CancellationToken cancellationToken)
    {
        return await _checkupRequestRepository.GetLastCheckupDate(carCompany, cancellationToken);
    }

    public async Task<DateOnly> GetLastCheckupDateByCar(int carId, CancellationToken cancellationToken)
    {
        return await _checkupRequestRepository.GetLastCheckupDateByCar(carId, cancellationToken);
    }

    public async Task<bool> SetRequestsToIncompleted(CancellationToken cancellationToken)
    {
        return await _checkupRequestRepository.SetRequestsToIncompleted(cancellationToken);
    }

    public async Task<List<GetCheckupRequestDto>> GettAll(CancellationToken cancellationToken)
    {
        return await _checkupRequestRepository.GettAll(cancellationToken);
    }

    public async Task<bool> MarkAsRejected(int id, CancellationToken cancellationToken)
    {
        return await _checkupRequestRepository.MarkAsRejected(id, cancellationToken);
    }

    public async Task<GetCheckupRequestDto?> GetByCarId(int id, CancellationToken cancellationToken)
    {
        return await _checkupRequestRepository.GetByCarId(id, cancellationToken);
    }
}
