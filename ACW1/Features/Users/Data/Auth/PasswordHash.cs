using System.Security.Cryptography;
using System.Text;

namespace ACW1.Features.Users.Data.Auth;

public class PasswordHash
{
    public string HashPassword(string password)
    {

        var inputBytes = Encoding.UTF8.GetBytes(password);
        var hashBytes = SHA256.HashData(inputBytes);

        var sb = new StringBuilder();
        foreach (var b in hashBytes)
        {
            sb.Append(b.ToString("x2"));
        }

        return sb.ToString();
    }
}
