using System.Security.Cryptography;
using System.Text;

namespace SierraDB.Crypto;

public static class Crypto
{
    public static string Password(string? password)
    {
        if(password == null) 
            throw new ArgumentNullException("Senha nula");

        var senhaBytes = Encoding.ASCII.GetBytes(password);
        var criptoPassBytes = MD5.Create().ComputeHash(senhaBytes);
        var criptoPass = Encoding.ASCII.GetString(criptoPassBytes);
        return criptoPass;
    }
}
