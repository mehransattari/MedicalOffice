
using System.Reflection.Emit;

namespace MedicalOffice.Shared.Entities;

public class Role
{
    public long Id { get; set; }
    public string FaCaption { get; set; }
    public string EnCaption { get; set; }

    public ICollection<User> Users { get; set; }
}



