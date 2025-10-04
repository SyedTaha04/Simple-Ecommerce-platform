using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Identity;
using WebApplication3.Models;

namespace WebApplication3.ExtraClasses;

public class PasswordHashing
{
    private const int SaltSize = 16;
    private const int  HashSize = 32;
    private const int Iterations = 10000;
    
    private readonly HashAlgorithmName Algorithm = HashAlgorithmName.SHA512;

    public string HashPassword(string password)
    {
        byte[] salt = RandomNumberGenerator.GetBytes(SaltSize);
        byte[] hash = Rfc2898DeriveBytes.Pbkdf2(password, salt, Iterations, Algorithm, HashSize);
        return $"{Convert.ToHexString(hash)}-{Convert.ToHexString(salt)}";

    }
    public bool Verify(string password, string hashedPassword)
    {
        string [] parts  =  hashedPassword.Split("-");
        byte [] hash = Convert.FromHexString(parts[0]);
        byte [] salt = Convert.FromHexString(parts[1]);
        byte [] inputHash = Rfc2898DeriveBytes.Pbkdf2(password, salt, Iterations, Algorithm,  HashSize);
        return CryptographicOperations.FixedTimeEquals(inputHash, hash);
    }

    public string BuiltInHashImplementation(SignUpPage signUpPage)
    {
        PasswordHasher<SignUpPage> hasherObject = new PasswordHasher<SignUpPage>();
        string HashedPassword = hasherObject.HashPassword(signUpPage, signUpPage.Password);
        return HashedPassword;
    }

    public bool VerifyHashedPassword(SignUpPage signUpPage, string dbHashedPassword)
    {
        PasswordHasher<SignUpPage> hasherObject = new PasswordHasher<SignUpPage>();

        PasswordVerificationResult VerifiedHashedPassword = hasherObject.VerifyHashedPassword(signUpPage, dbHashedPassword,  signUpPage.Password  );
        if (VerifiedHashedPassword == PasswordVerificationResult.Success)
        {
            return true;
        }
        return false;
    }

    
    
}