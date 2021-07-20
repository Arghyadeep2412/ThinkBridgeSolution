using System;
using System.Collections.Generic;

#nullable disable

namespace ThinkBridgeSolu.Models.DBModels
{
    public partial class ProductCategory
    {
        public ProductCategory()
        {
            Products = new HashSet<Product>();
        }

        public int CategoryId { get; set; }
        public int ParentCategoryId { get; set; }
        public string CategoryName { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
