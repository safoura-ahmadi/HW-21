using CarCheckup.Domain.Core.Enums.Car;
using CarCheckup.Domain.Core.Enums.checkupRequest;

namespace CarCheckup.Domain.Core.Dtos;

public class GetCheckupRequestDto
{
    public int Id { get; set; }
    public required string OwnerMeliCode { get; set; }
    public required string ModelName { get; set; }
    public CarCompanyEnum Company { get; set; }
    public DateOnly TimeToDone { get; set; }
    public CheckUpRequestStatusEnum Status { get; set; }

}
