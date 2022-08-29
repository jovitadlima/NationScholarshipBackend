using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace Backend.Models
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Ministry>().HasData(
                new Ministry
                {
                    MinistryId = 1,
                    Name = "Ministry",
                    MinistryEmail = "ministry@nsp.com",
                    PasswordHash = CreatePasswordHash("mpassword"),
                    PasswordSalt = CreatePasswordSalt("mpassword")
                }
            );

            modelBuilder.Entity<NodalOfficer>().HasData(
                new NodalOfficer
                {
                    OfficerId = 1,
                    OfficerName = "Officer",
                    OfficerEmail = "officer@nsp.com",
                    PasswordHash = CreatePasswordHash("opassword"),
                    PasswordSalt = CreatePasswordSalt("0password")
                }
            );
        }

        private static byte[] CreatePasswordHash(string password)
        {
            byte[] passwordHash;
            using (var hmac = new HMACSHA512())
            {
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return passwordHash;
            }
        }

        private static byte[] CreatePasswordSalt(string password)
        {
            byte[] passwordSalt;
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                return passwordSalt;
            }
        }
    }
}
