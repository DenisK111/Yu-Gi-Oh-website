using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using System.Reflection;
using Yu_Gi_Oh_website.Models;
using Yu_Gi_Oh_website.Models.CardCatalogue.Models;

namespace Yu_Gi_Oh_website.Web.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
          : base(options)
        {

        }

        public ApplicationDbContext()
        {

        }



        public DbSet<Card> Cards { get; set; } = null!;
        public DbSet<CardImage> CardImages { get; set; } = null!;

        public DbSet<CardType> Types { get; set; } = null!;

        public DbSet<Race> Races { get; set; } = null!;

        public DbSet<CardAttribute> CardAttributes { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                    .Add(new JsonConfigurationSource { Path = "appsettings.json" })
                          .Build();

                var connectionString = configuration.GetConnectionString("DefaultConnection");
                optionsBuilder.UseSqlServer(connectionString);
            }
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Card>()
                .HasIndex(b => b.Name);
            base.OnModelCreating(builder);
            //builder.Entity<CustomAttributeData>().HasNoKey();
        }




    }
}