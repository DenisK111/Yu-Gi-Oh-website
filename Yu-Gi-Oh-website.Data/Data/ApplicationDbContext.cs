using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Yu_Gi_Oh_website.Models;

namespace Yu_Gi_Oh_website.Web.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
          : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            //builder.Entity<CustomAttributeData>().HasNoKey();
        }
        public DbSet<Card> Cards { get; set; } = null!;
        public DbSet<CardImage> CardImages { get; set; } = null!;

      

       
    }
}