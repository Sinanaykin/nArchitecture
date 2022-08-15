using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Contexts
{
    public class BaseDbContext : DbContext
    {
        protected IConfiguration Configuration { get; set; }
        public DbSet<Brand> Brands { get; set; }
       

        public BaseDbContext(DbContextOptions dbContextOptions, IConfiguration configuration) : base(dbContextOptions)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //if (!optionsBuilder.IsConfigured)
            //    base.OnConfiguring(
            //        optionsBuilder.UseSqlServer(Configuration.GetConnectionString("SomeConnectionString")));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)//Oluşturulma esnasında
        {
            modelBuilder.Entity<Brand>(a =>
            {
                a.ToTable("Brands").HasKey(k => k.Id);//Brand nesnesi db Brands karşılık gelsin
                a.Property(p => p.Id).HasColumnName("Id");//kolonları bunlar olsun
                a.Property(p => p.Name).HasColumnName("Name");//kolonları bunlar olsun
            });



            Brand[] brandEntitySeeds = { new(1, "BMW"), new(2, "Mercedes") };//Migration yapınca test datası olustursun bize sürekli sıfırlanmasın bunu getirsin default
            modelBuilder.Entity<Brand>().HasData(brandEntitySeeds);

           
        }
    }
}
