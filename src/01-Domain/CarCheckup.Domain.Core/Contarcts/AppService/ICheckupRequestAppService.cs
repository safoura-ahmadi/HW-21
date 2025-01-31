using CarCheckup.Domain.Core.Dtos.CheckupRequest;
using CarCheckup.Domain.Core.Entities;
using CarCheckup.Domain.Core.Enums.Car;

namespace CarCheckup.Domain.Core.Contarcts.AppService;

public interface ICheckupRequestAppService
{
    Task<Result> Create(int carId, CancellationToken cancellationToken);
   Task< DateOnly> DetermineTimetoDone(CarCompanyEnum carCompany,CancellationToken cancellationToken);
    Task<Result> MarkAsAccepted(int id, CancellationToken cancellationToken);
    Task<Result> MarkAsRejected(int id, CancellationToken cancellationToken);
    Task<List<GetCheckupRequestDto>> GeByCarModel(int modelId, CancellationToken cancellationToken);
    Task<List<GetCheckupRequestDto>> GetByDate(DateOnly timeToDone, CancellationToken cancellationToken);
    Task<List<GetCheckupRequestDto>> GettAll(CancellationToken cancellationToken);
    Task<GetCheckupRequestDto?> GetByCarId(int id, CancellationToken cancellationToken);
    public string ConvertDateToPersion(DateOnly date);
    Task<bool> SetRequestsToIncompleted(CancellationToken cancellationToken);
}
