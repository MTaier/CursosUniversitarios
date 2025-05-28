using CursosUniversitarios.Shared.Data.Models;
using CursosUniversitarios_Console;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CursosUniversitarios.Shared.Data.DB
{
    public class CursosUniversitariosContext : IdentityDbContext<AccessUser, AccessRole, int>
    {
        public DbSet<Course> Course { get; set; }
        public DbSet<Subject> Subject { get; set; }
        public DbSet<Professor> Professor { get; set; }
        private string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=CursosUniversitarios_DB_V1;Integrated Security=True;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString).UseLazyLoadingProxies();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Course>().HasMany(c => c.Professors).WithMany(p => p.Courses);
        }
    }
}
