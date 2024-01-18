

namespace MedicalOffice.Shared.Entities;

public class Reservation
{
    public long Id { get; set; }
    public long TimesReserveId { get; set; }
    public long UserId { get; set; }

    public TimesReserve? TimesReserve { get; set; }
    public User? User { get; set; }

}
