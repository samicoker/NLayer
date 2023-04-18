using Microsoft.EntityFrameworkCore;
using NLayer.Core.Models;
using NLayer.Repository.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Repository
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {
            //var p = new Product() { ProductFeature = new ProductFeature() { Color = "red", Height = 15,Width = 30 } };
        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductFeature> ProductFeatures { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) // entityler ile ilgili ayar yapabilmek için
        { // bu metotu override ettik ama bu ayarları configurations dosyasındaki classlarla yapmak, bestpractiseye daha uygun

            // modelBuilder.Entity<Category>().HasKey(c => c.Id); // burada primarykey alanı Id olsun dedik ama bu ayarları configurations dosyasındaki classlarla yapmak, bestpractiseye daha uygun

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly()); // bu metot, IEntityTypeConfiguration dan türemiş tüm classları tarar ve yazmış olduğumuz configurationları yapar

            //modelBuilder.ApplyConfiguration(new ProductConfiguration()); //istersek her bir configuration için böyle tek tek configuration yapabiliriz

            modelBuilder.Entity<ProductFeature>().HasData(
                new ProductFeature { Id = 1, Color = "Kırmızı", Height = 100, Width = 200, ProductId = 1 },
                new ProductFeature { Id = 2, Color = "Mavi", Height = 300, Width = 500, ProductId = 2 }
                );

            base.OnModelCreating(modelBuilder);
        }
    }
}
