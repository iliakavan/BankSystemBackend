using System.Security.Cryptography;
using System.Text;

namespace BankSystem.Application.Common.GenratingUnique;


public static class UniqueString
{
    public static string GetUniqueKey(int size)
    {
        byte[] data = new byte[4 * size];
        var chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890".ToCharArray();
        using (var crypto = RandomNumberGenerator.Create())
        {
            crypto.GetBytes(data);
        }
        StringBuilder result = new(size);
        for (int i = 0; i < size; i++)
        {
            var rnd = BitConverter.ToUInt32(data, i * 4);
            var idx = rnd % chars.Length;

            result.Append(chars[idx]);
        }

        return result.ToString();
    }

}

