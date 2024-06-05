using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ReFound.Models;
using Microsoft.AspNetCore.Identity;

namespace ReFound.Models
{
    public class ReFoundDbContext : IdentityDbContext
    {
        public ReFoundDbContext(DbContextOptions<ReFoundDbContext> options): base(options){}

        public DbSet<Object> Objects { get; set; }
        public DbSet<Cart> Cart { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
         => options.UseSqlite(@"Data Source=Models/database.db");
    }
}
