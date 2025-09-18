using KOG.ECommerce.Common.Interfaces;
using KOG.ECommerce.Models.Categories;
using KOG.ECommerce.Models.Products;
using System.ComponentModel.DataAnnotations.Schema;

namespace KOG.ECommerce.Models.Brands
{
    [Table("Brand", Schema = "Brands")]
    public class Brand : BaseModel, ISelectableListItem
    {
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public List<string> Tags { get; set; }
        public List<Product> Products { get; set; }
    }
}
