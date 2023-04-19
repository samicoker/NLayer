﻿namespace NLayer.Core.Models
{
    public abstract class BaseEntity
    {
        //[Key] Id'nin ismini Id değil de farklı birşey verseydik Ef_Core bunu primarykey olarak algılayamazdı ve o zaman başına böyle [Key] attributesi koymamız gerekirdi
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
