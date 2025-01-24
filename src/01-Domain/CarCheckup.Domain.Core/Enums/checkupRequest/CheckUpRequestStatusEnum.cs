using System.ComponentModel.DataAnnotations;

namespace CarCheckup.Domain.Core.Enums.checkupRequest;

public enum CheckUpRequestStatusEnum
{
    [Display(Name = "در انتظار")]
    Pending = 1,
    [Display(Name = "تایید شده")]
    Accepted,
    [Display(Name = "رد شده")]
    Rejected

}
