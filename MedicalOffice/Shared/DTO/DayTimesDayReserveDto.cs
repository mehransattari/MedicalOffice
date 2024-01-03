

namespace MedicalOffice.Shared.DTO
{
    public class DayTimesDayReserveDto
    {
        public long Id { get; set; }

        public DateTime Day { get; set; }

        public List<TimesReserveDto> TimesReserveDtos { get; set; }
    }
}
