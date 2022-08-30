using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace Backend.Models
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            List<byte[]> ministryPassword = CreatePasswordHash("mpassword");
            modelBuilder.Entity<Ministry>().HasData(
                new Ministry
                {
                    MinistryId = 1,
                    Name = "Ministry",
                    MinistryEmail = "ministry@nsp.com",
                    PasswordHash = ministryPassword[1],
                    PasswordSalt = ministryPassword[0]
                }
            );

            List<byte[]> officerPassword = CreatePasswordHash("opassword");
            modelBuilder.Entity<NodalOfficer>().HasData(
                new NodalOfficer
                {
                    OfficerId = 1,
                    OfficerName = "Officer",
                    OfficerEmail = "officer@nsp.com",
                    PasswordHash = officerPassword[1],
                    PasswordSalt = officerPassword[0]
                }
            );
        }

        private static List<byte[]> CreatePasswordHash(string password)
        {
            using (var hmac = new HMACSHA512())
            {
                List<byte[]> list = new List<byte[]>();
                byte[] passwordSalt = hmac.Key;
                list.Add(passwordSalt);
                byte[] passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                list.Add(passwordHash);
                return list;
            }
        }
    }
}
