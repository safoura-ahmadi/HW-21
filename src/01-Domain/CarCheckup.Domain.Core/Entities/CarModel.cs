using System.ComponentModel.DataAnnotations;

namespace CarCheckup.Domain.Core.Entities;

public class CarModel
{
    #region Properties
    public int Id { get; set; }
    [Required(ErrorMessage ="واردن کردن نام مدل خودرو الزامی است")]
    public required string Name { get; set; }
    #endregion

    #region Navigation
    public List<Car> Cars { get; set; } = [];
    #endregion
}
