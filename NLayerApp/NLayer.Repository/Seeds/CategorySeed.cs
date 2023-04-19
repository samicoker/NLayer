using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NLayer.Core.Models;

namespace NLayer.Repository.Seeds
{
    internal class CategorySeed : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder) // program ilk çalıştığında veritabanına bu datalar eklensin istedik
        {
            builder.HasData(
                new Category { Id = 1, Name = "Kalemler" },
                new Category { Id = 2, Name = "Kİtaplar" },
                new Category { Id = 3, Name = "Defterler" });
        }
    }
}
