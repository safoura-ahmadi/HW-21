namespace CarCheckup.Domain.Core.Entities;

public class CarModel
{
    #region Properties
    public int Id { get; set; }
    public required string Name { get; set; }
    #endregion

    #region Navigation
    public List<Car> Cars { get; set; } = [];
    #endregion
}
