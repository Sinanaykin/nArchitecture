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
        public DbSet<Model> Models { get; set; }    
       

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
            modelBuilder.Entity<Brand>(a =>//BUrayı vermesek de olur ama kurumsal mimari açısından bunu da yazmak daha iyi 
            {
                a.ToTable("Brands").HasKey(k => k.Id);//Brand nesnesi db Brands karşılık gelsin
                a.Property(p => p.Id).HasColumnName("Id");//kolonları bunlar olsun
                a.Property(p => p.Name).HasColumnName("Name");//kolonları bunlar olsun

                a.HasMany(p => p.Models);//Brand in de birden fazla Modeli var diyoruz
            });

            modelBuilder.Entity<Model>(a =>
            {
                a.ToTable("Models").HasKey(k => k.Id);
                a.Property(p => p.Id).HasColumnName("Id");
                a.Property(p => p.BrandId).HasColumnName("BrandId");
                a.Property(p => p.Name).HasColumnName("Name");
                a.Property(p => p.DailyPrice).HasColumnName("DailyPrice");
                a.Property(p => p.ImageUrl).HasColumnName("ImageUrl");

                a.HasOne(p => p.Brand);//Bir tane Brand i var dicez
            });




            Brand[] brandEntitySeeds = { new(1, "BMW"), new(2, "Mercedes") };//Migration yapınca test datası olustursun bize sürekli sıfırlanmasın bunu getirsin default
            modelBuilder.Entity<Brand>().HasData(brandEntitySeeds);



            Model[] modelEntitySeeds = { new(1,"Series 4",1500,"",1) , new(2, "Series 3", 1200, "", 2), new(3, "Series 5", 1000, "", 1) };//Migration yapınca test datası olustursun bize sürekli sıfırlanmasın bunu getirsin default
            modelBuilder.Entity<Model>().HasData(modelEntitySeeds);

        }
    }
}
