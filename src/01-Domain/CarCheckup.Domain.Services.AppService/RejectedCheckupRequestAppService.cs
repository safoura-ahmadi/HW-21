

using CarCheckup.Domain.Core.Contarcts.AppService;
using CarCheckup.Domain.Core.Contarcts.Service;

namespace CarCheckup.Domain.Services.AppService;

public class RejectedCheckupRequestAppServic(IRejectedCheckupRequestService rejectedCheckupRequestService) : IRejectedCheckupRequestAppService
{
    private readonly IRejectedCheckupRequestService _rejectedCheckupRequestService = rejectedCheckupRequestService;
    public void Create(int carId)
    {
       _rejectedCheckupRequestService.Create(carId);
    }
}
