using System.Security.Cryptography;

namespace SistemaQuinielaMundialistasV2.Services;

public class PasswordService
{
    private const int Iterations = 100_000;
    public string Hash(string password)
    {
        byte[] salt = RandomNumberGenerator.GetBytes(16);
        byte[] hash = Rfc2898DeriveBytes.Pbkdf2(password, salt, Iterations, HashAlgorithmName.SHA256, 32);
        return $"PBKDF2${Iterations}${Convert.ToBase64String(salt)}${Convert.ToBase64String(hash)}";
    }
    public bool Verify(string password, string stored)
    {
        if (!stored.StartsWith("PBKDF2$", StringComparison.Ordinal))
            return password == stored; // compatibilidad temporal con datos de V1
        string[] parts = stored.Split('$');
        if (parts.Length != 4 || !int.TryParse(parts[1], out int iterations)) return false;
        byte[] salt = Convert.FromBase64String(parts[2]);
        byte[] expected = Convert.FromBase64String(parts[3]);
        byte[] actual = Rfc2898DeriveBytes.Pbkdf2(password, salt, iterations, HashAlgorithmName.SHA256, expected.Length);
        return CryptographicOperations.FixedTimeEquals(actual, expected);
    }
}
