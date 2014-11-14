namespace Template.Authentication.PasswordHashing
{
    public interface IPasswordHashing
    {
        string GetSalt();
        string Hash(string plainText, string salt);
        bool Verify(string plainText, string cryptText, string salt);
    }
}