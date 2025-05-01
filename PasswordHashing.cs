using Microsoft.Extensions.Configuration.EnvironmentVariables;
using System.Security.Cryptography;
using System.Text;
namespace WebApp4310HTML
{
    public class PasswordHashing
    {
        public static string GetPasswordHash(string rawPassword)
        {
            string ret;
            var sha256 = SHA256.Create();
            var passwordBytes = Encoding.UTF8.GetBytes(rawPassword);
            byte[] passwordHash = sha256.ComputeHash(passwordBytes);

            //WHATEVER YOU WANT BUT IT MUST PRODUCE THE SAME RESULT FROM THE SAME HASED
            //PASSWORD BYTES
            return BitConverter.ToString(passwordHash); 
        }
        
    }
}
