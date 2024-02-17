using System.ComponentModel.DataAnnotations;
namespace MedicalOffice.Shared.Entities;

public class Reservation
{
    public long Id { get; set; }
    public long TimesReserveId { get; set; }
    public long UserId { get; set; }
    public StatusEnum Status { get; set; }
    public ReserveTypeEnum ReserveType { get; set; }

    public TimesReserve? TimesReserve { get; set; }
    public User? User { get; set; }

}
public enum StatusEnum
{
    [Display(Name = "رزرو شده")]
    Reserved = 1,

    [Display(Name = "کنسل شده")]
    Cancelled = 2,

   [Display(Name = " در حال بررسی")]
    Pending = 3
}

public enum ReserveTypeEnum
{
    [Display(Name = " ویزیت")]
    Visit = 1,

    [Display(Name = "ترمیم ")]
    Restoration = 2,

    [Display(Name = "عصب کشی")]
    RootCanal = 3
}

