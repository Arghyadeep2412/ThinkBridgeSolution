using System;
using System.Collections.Generic;

#nullable disable

namespace ThinkBridgeSolu.Models.DBModels
{
    public partial class Product
    {
        public int ProductId { get; set; }
        public int CategoryId { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public decimal? ProductPrice { get; set; }
        public string PriceCurrency { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual ProductCategory Category { get; set; }
    }
}
