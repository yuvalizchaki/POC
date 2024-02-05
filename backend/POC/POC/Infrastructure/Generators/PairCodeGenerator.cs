namespace POC.Infrastructure.Generators;
using System.Security.Cryptography;
using System.Text;

public class PairCodeGenerator
{
    private const string Chars = "0123456789";

    public async Task<string> GenerateAsync(int length = 6)
    {
        return await Task.Run(() =>
        {
            using (RNGCryptoServiceProvider cryptoProvider = new RNGCryptoServiceProvider())
            {
                var data = new byte[length];
                cryptoProvider.GetBytes(data);

                StringBuilder codeBuilder = new StringBuilder(length);
                foreach (var b in data)
                {
                    codeBuilder.Append(Chars[b % (Chars.Length)]);
                }
                return codeBuilder.ToString();
            }
        });
    }
}

// ---------- Option2: ---------------
// public class CodeGenerator
// {
//     private Random random = new Random();
//     
//     private const string Chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
//
//     public async Task<string> GenerateAsync(int length)
//     {
//         return await Task.Run(() =>
//         {
//             return new string(Enumerable.Repeat(Chars, length)
//                 .Select(s => s[random.Next(s.Length)]).ToArray());
//         });
//     }
// }
