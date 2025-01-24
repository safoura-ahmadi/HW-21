using CarCheckup.Domain.Core.Entities.validationAtrribute;
using CarCheckup.Domain.Core.Enums.Car;
using System.ComponentModel.DataAnnotations;

namespace CarCheckup.Domain.Core.Entities;

public class Car
{
    #region Properties
    public int Id { get; set; }
    [Required(ErrorMessage = "پلاک ماشین نمی‌تواند خالی باشد")]
    [RegularExpression(@"^\d{2}[آ-ی]\d{3}-\d{2}$", ErrorMessage = "پلاک باید در فرمت استاندارد پلاک ماشین ایرانی باشد مثلاً 12ب345-67")]
    public required string Plate { get; set; }

    [Required(ErrorMessage = "سال تولید خودرو نمی‌تواند خالی باشد.")]
    public DateOnly GenerationYear { get; set; }

    [Required(ErrorMessage = "کد ملی نمی‌تواند خالی باشد")]
    [RegularExpression(@"^\d{10}$", ErrorMessage = "کد ملی باید دقیقا 10 رقم باشد")]
    public required string OwnerMeliCode { get; set; }

    [Required(ErrorMessage = "شماره موبایل نمی‌تواند خالی باشد")]
    [MobileValidation(ErrorMessage ="شماره موبایل مالک معتبر نیست")]
    public required string OwnerMobile { get; set; }

    [Required(ErrorMessage = "مدل خودرو نمی‌تواند خالی باشد")]
    public int ModelId { get; set; }

    [Required(ErrorMessage = "شرکت خودرو نمی‌تواند خالی باشد")]
    public CarCompanyEnum Company { get; set; }
    #endregion

    #region Navigation
    public CarModel? Model { get; set; }
    public List<CheckupRequest> CheckupRequests { get; set; } = [];
    public List<RejectedCheckupRequest> RejectedCheckupRequests { get; set; } = [];
    #endregion
}
