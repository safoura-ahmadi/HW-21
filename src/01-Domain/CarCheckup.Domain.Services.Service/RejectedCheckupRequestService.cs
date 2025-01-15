using CarCheckup.Domain.Core.Contarcts.Repository;
using CarCheckup.Domain.Core.Contarcts.Service;

namespace CarCheckup.Domain.Services.Service;

public class RejectedCheckupRequestService(IRejectedCheckupRequestRepository rejectedCheckupRequestRepository) : IRejectedCheckupRequestService
{
    private readonly IRejectedCheckupRequestRepository _rejectedCheckupRequestRepository = rejectedCheckupRequestRepository;
    public void Create(int carId)
    {
        _rejectedCheckupRequestRepository.Create(carId);
    }
}
