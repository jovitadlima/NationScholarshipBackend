using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Models
{
    public class ScholarshipDbContext : DbContext
    {
        public ScholarshipDbContext(DbContextOptions<ScholarshipDbContext> options): base(options) 
        {
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Institute> Institutes { get; set; }
        public DbSet<ScholarshipApplication> ScholarshipApplications { get; set; }
        public DbSet<Ministry> Ministries { get; set; }
        public DbSet<NodalOfficer> NodalOfficers { get; set; }
        public DbSet<ScholarshipScheme> ScholarshipSchemes { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Seed();

            builder.Entity<Institute>().HasIndex(p => new { p.DiseCode, p.InstituteCode }).IsUnique();

            builder.Entity<Student>().HasIndex(p => new { p.AadharNumber, p.PhoneNo, p.BankAccountNumber }).IsUnique();
        }
    }
}
