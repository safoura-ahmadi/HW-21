using CarCheckup.Domain.Core.Enums.checkupRequest;

namespace CarCheckup.Domain.Core.Entities;

public class RejectedCheckupRequest
{
    #region Properties
    public int Id { get; set; }
    public int CarId { get; set; }
    #endregion
    #region navigation
    public Car? Car { get; set; }
    #endregion
}
