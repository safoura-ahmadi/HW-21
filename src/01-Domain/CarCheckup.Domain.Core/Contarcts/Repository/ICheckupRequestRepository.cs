using CarCheckup.Domain.Core.Dtos.CheckupRequest;
using CarCheckup.Domain.Core.Entities;
using CarCheckup.Domain.Core.Enums.Car;

namespace CarCheckup.Domain.Core.Contarcts.Repository;

public interface ICheckupRequestRepository
{
    //Create
    Task<bool> Create(CheckupRequest checkupRequest, CancellationToken cancellationToken);

    //Read
    Task<List<GetCheckupRequestDto>> GeByCarModel(int modelId, CancellationToken cancellationToken);
    Task<List<GetCheckupRequestDto>> GetByDate(DateOnly timeToDone, CancellationToken cancellationToken);
    Task<List<GetCheckupRequestDto>> GettAll(CancellationToken cancellationToken);
    Task<GetCheckupRequestDto?> GetByCarId(int id, CancellationToken cancellationToken);
    Task<int> GetDailyCount(DateOnly date, CancellationToken cancellationToken);

    //Update
    Task<bool> MarkAsAccepted(int id, CancellationToken cancellationToken);
    Task<bool> MarkAsRejected(int id, CancellationToken cancellationToken);
    Task<DateOnly?> GetLastCheckupDate(CarCompanyEnum carCompany, CancellationToken cancellationToken);
    Task<DateOnly> GetLastCheckupDateByCar(int carId, CancellationToken cancellationToken);
    Task<bool> SetRequestsToIncompleted(CancellationToken cancellationToken);

}
