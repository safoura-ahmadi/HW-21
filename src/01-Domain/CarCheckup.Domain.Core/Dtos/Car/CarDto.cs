using CarCheckup.Domain.Core.Entities.validationAtrribute;
using CarCheckup.Domain.Core.Enums.Car;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarCheckup.Domain.Core.Dtos.Car;

public class CarDto
{

    [Required(ErrorMessage = "پلاک ماشین نمی‌تواند خالی باشد")]
    [RegularExpression(@"^\d{2}[آ-ی]\d{3}-\d{2}$", ErrorMessage = "پلاک باید در فرمت استاندارد پلاک ماشین ایرانی باشد مثلاً 12ب345-67")]
    public required string Plate { get; set; }

    [Required(ErrorMessage = "سال تولید خودرو نمی‌تواند خالی باشد.")]
    [Range(1340, 1403, ErrorMessage = "سال شمسی باید بین 1340 تا 1403 باشد.")]
    public int ShamsiYear { get; set; }

    [Required(ErrorMessage = "کد ملی نمی‌تواند خالی باشد")]
    [RegularExpression(@"^\d{10}$", ErrorMessage = "کد ملی باید دقیقا 10 رقم باشد")]
    public required string OwnerMeliCode { get; set; }

    [Required(ErrorMessage = "شماره موبایل نمی‌تواند خالی باشد")]
    [MobileValidation(ErrorMessage = "شماره موبایل مالک معتبر نیست")]
    public required string OwnerMobile { get; set; }

    [Required(ErrorMessage = "مدل خودرو نمی‌تواند خالی باشد")]
    public int ModelId { get; set; }

    [Required(ErrorMessage = "شرکت خودرو نمی‌تواند خالی باشد")]
    public CarCompanyEnum Company { get; set; }
}
