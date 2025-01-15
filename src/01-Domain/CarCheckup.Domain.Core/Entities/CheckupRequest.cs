using CarCheckup.Domain.Core.Enums.checkupRequest;

namespace CarCheckup.Domain.Core.Entities;

public class CheckupRequest
{
    #region Properties
    public int Id { get; set; }
    public DateOnly TimeToDone { get; set; }
    public CheckUpRequestStatusEnum Status { get; set; }
    public int CarId { get; set; }
    #endregion

    #region Navigation
    public Car? Car { get; set; }
    #endregion
}
