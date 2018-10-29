using b = BCrypt.Net;

namespace BusinessLogic.Helpers
{
    public class Hashing
    {
        private static string GetRandomSalt()
        {
            return b.BCrypt.GenerateSalt(12);
        }
        public static string HashPassword(string password)
        {
            return b.BCrypt.HashPassword(password, GetRandomSalt());
        }
        public static bool ValidatePassword(string password, string correctHash)
        {
            return b.BCrypt.Verify(password, correctHash);
        }
    }
}
