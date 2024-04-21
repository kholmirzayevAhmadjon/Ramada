namespace Ramada.Service.Helpers;

using BCrypt.Net;

public static class PasswordHasher
{
    public static string Hash(string password)
    {
        var salt = BCrypt.GenerateSalt(12);
        return BCrypt.HashPassword(password, salt);
    }

    public static bool Verify(string password, string passwordHash)
    {
        return BCrypt.Verify(password, passwordHash);
    }
}
