using Microsoft.EntityFrameworkCore;
using NLayer.Core.Models;
using System.Reflection;

namespace NLayer.Repository
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            //var p = new Product() { ProductFeature = new ProductFeature() { Color = "red", Height = 15,Width = 30 } };
        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductFeature> ProductFeatures { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            // burada tüm entitylerde CreatedDate ve UpdatedDate'yi otomatik setleyeceğiz

            //foreach (var item in ChangeTracker.Entries())
            //{
            //    if (item.Entity is BaseEntity entityReference) // tüm database entitylerimiz BaseEntityden türediği için 
            //    { // BaseEntity'den türeyen classları aldık

            //        switch (item.State)
            //        {
            //                case EntityState.Added:
            //                {
            //                    entityReference.CreatedDate = DateTime.Now;
            //                    break;
            //                }
            //                case EntityState.Modified:
            //                {
            //                    Entry(entityReference).Property(x => x.CreatedDate).IsModified = false;

            //                    entityReference.UpdatedDate = DateTime.Now;
            //                    break;
            //                }
            //        }
            //    }
            //    // aynı işlemleri savechanges için de yapacağız
            //}
            UpdateChangeTracker();
            return base.SaveChangesAsync(cancellationToken);
        }

        public override int SaveChanges()
        {
            //foreach (var item in ChangeTracker.Entries())
            //{
            //    if (item.Entity is BaseEntity entityReference) // tüm database entitylerimiz BaseEntityden türediği için 
            //    { // BaseEntity'den türeyen classları aldık

            //        switch (item.State)
            //        {
            //            case EntityState.Added:
            //                {
            //                    Entry(entityReference).Property(x => x.UpdatedDate).IsModified = false;
            //                    entityReference.CreatedDate = DateTime.Now;
            //                    break;
            //                }
            //            case EntityState.Modified:
            //                {
            //                    Entry(entityReference).Property(x => x.CreatedDate).IsModified = false;
            //                    entityReference.UpdatedDate = DateTime.Now;
            //                    break;
            //                }
            //        }
            //    }

            //}
            UpdateChangeTracker();
            return base.SaveChanges();
        }

        public void UpdateChangeTracker()
        {
            foreach (var item in ChangeTracker.Entries())
            {
                if (item.Entity is BaseEntity entityReference) // tüm database entitylerimiz BaseEntityden türediği için 
                { // BaseEntity'den türeyen classları aldık

                    switch (item.State)
                    {
                        case EntityState.Added:
                            {
                                Entry(entityReference).Property(x => x.UpdatedDate).IsModified = false;
                                entityReference.CreatedDate = DateTime.Now;
                                break;
                            }
                        case EntityState.Modified:
                            {
                                Entry(entityReference).Property(x => x.CreatedDate).IsModified = false;
                                entityReference.UpdatedDate = DateTime.Now;
                                break;
                            }
                    }
                }

            }
        }
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
