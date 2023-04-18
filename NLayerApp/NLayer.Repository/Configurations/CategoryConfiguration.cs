using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NLayer.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Repository.Configurations
{
    internal class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(x => x.Id); // id alanı primary key olsun
            builder.Property(x=>x.Id).UseIdentityColumn(); // burada istersek parametre olarak kaçtan başlayıp kaçar kaçar artacağını verebiliriz. hiçbirşey vermezsek default olarak 1,1  dir.
            builder.Property<string>(x => x.Name).IsRequired().HasMaxLength(50); // name alanı zorunlu ve max 50 karakterli olsun

            builder.ToTable("Categories"); // tablonun ismi Categories olsun. bunu vermezsek default olarak AppDbContextteki tanımlanan DbSetin ismini verir
        }
    }
}
