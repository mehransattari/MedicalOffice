
namespace MedicalOffice.Shared.Helper;

public class TokenData
{
    public string Token { get; set; }
    public DateTime? Expiration { get; set; }
    public bool Status { get; set; }
    public string  Message { get; set; }
}