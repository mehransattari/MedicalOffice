
using System.ComponentModel.DataAnnotations;

namespace MedicalOffice.Shared.DTO
{
    public class ReserveDto
    {
      
        //UserInfo
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Mobile { get; set; }

        public string? Password { get; set; }

        public string NationalCode { get; set; }

        public long RoleId { get; set; }

        //Reserve
        public long TimesReserveId { get; set; }
        public long UserId { get; set; }
    }
}
