using KOG.ECommerce.Common.Interfaces;
using KOG.ECommerce.Models.Products;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
namespace KOG.ECommerce.Models.Categories
{
    [Table("Category", Schema = "Categories")]
    public class Category : BaseModel, ISelectableListItem
    {
        public string Name { get; set; }
        public string Description { get; set; }
        [ForeignKey("ParentCategory")]
        public string? ParentCategoryId { get; set; }
        public Category ParentCategory { get; set; }
        public List<Category> Subcategories { get; set; }
        public List<string> Tags { get; set; }
        public List<string>SEO { get; set; }
        public bool IsActive {  get; set; } 
        public List<Product>products { get; set; }
    }
}
