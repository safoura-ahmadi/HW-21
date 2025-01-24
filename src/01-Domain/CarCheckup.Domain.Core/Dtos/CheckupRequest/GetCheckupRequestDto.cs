using CarCheckup.Domain.Core.Enums.Car;
using CarCheckup.Domain.Core.Enums.checkupRequest;

namespace CarCheckup.Domain.Core.Dtos.CheckupRequest;

public class GetCheckupRequestDto
{
    public int Id { get; set; }
    public string OwnerMeliCode { get; set; } = null!;
    public string Plate { get; set; } = null!;
    public string ModelName { get; set; } = null!;
    public CarCompanyEnum Company { get; set; }
    public DateOnly TimeToDone { get; set; }
    public CheckUpRequestStatusEnum Status { get; set; }

}
