using System.ComponentModel.DataAnnotations;

namespace CarCheckup.Domain.Core.Enums.Car;

public enum CarCompanyEnum
{
    [Display(Name = "ایرانخودرو")]
    IranKhodro = 1,
    [Display(Name = "سایپا")]
    Saipa
}
