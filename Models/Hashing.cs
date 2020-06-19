using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace doan_qldkonline.Models    
{
    public class Hashing
    {
        //private static string GetRandomSalt()
        //{
        //    return BCrypt.Net.BCrypt.GenerateSalt(12);
        //}

        //public static string HashPassword(string matkhau)
        //{
        //    return BCrypt.Net.BCrypt.HashPassword(matkhau, GetRandomSalt());
        //}

        //public static bool ValidatePassword(string matkhau, string correctHash)
        //{
        //    return BCrypt.Net.BCrypt.Verify(matkhau, correctHash);
        //}
        public static string Hash(string value)
        {
            return Convert.ToBase64String(
                System.Security.Cryptography.SHA256.Create()
                .ComputeHash(Encoding.UTF8.GetBytes(value))
                );
        }
    }
}