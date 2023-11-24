

using MedicalOffice.Shared.Entities;

namespace MedicalOffice.Shared.DTO;

public class TimesReserveDto
{
    public long Id { get; set; }
    public long DaysReserveId { get; set; }
    public TimeSpan FromTime { get; set; }
    public TimeSpan ToTime { get; set; }

}
