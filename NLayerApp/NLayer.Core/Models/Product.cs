namespace NLayer.Core.Models
{
    //[Table("Products")]
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public int Stock { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        // [ForeignKey("Category_Id")]  eğer CategoryId'yi Category_Id şeklinde tanımlasaydık, Ef_Core bunu foreignkey olarak göremicekti ve bizim bu şekilde foreignkey olduğunu belirtmemiz gerekirdi
        public Category Category { get; set; }
        public ProductFeature ProductFeature { get; set; }
    }
}
