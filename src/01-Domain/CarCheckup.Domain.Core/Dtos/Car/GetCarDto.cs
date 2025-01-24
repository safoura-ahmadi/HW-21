using CarCheckup.Domain.Core.Enums.Car;

namespace CarCheckup.Domain.Core.Dtos.Car;

public class GetCarDto
{
    public DateOnly GenerationYear { get; set; }
    public CarCompanyEnum Company { get; set; }
}
