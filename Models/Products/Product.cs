using KOG.ECommerce.Models.Brands;
using KOG.ECommerce.Models.CartProducts;
using KOG.ECommerce.Models.Categories;
using KOG.ECommerce.Models.OrderItems;
using KOG.ECommerce.Models.DiscountProducts;
using KOG.ECommerce.Models.WishlistProducts;
using System.ComponentModel.DataAnnotations.Schema;
using KOG.ECommerce.Common.Interfaces;
using KOG.ECommerce.Models.Companies;
using KOG.ECommerce.Models.Enums;

namespace KOG.ECommerce.Models.Products
{
    [Table("Product", Schema = "Products")]
    public class Product:BaseModel, ISelectableListItem
    {
       public string Name {  get; set; }
       public string Description {  get; set; }
       [ForeignKey("Category")]
       public string CategoryId { get; set; }
       public Category Category { get; set; }
       public List<string> Tags { get; set; }
       public string Model {  get; set; }
       public double  Price { get; set; }
       public double Tax { get; set; }
       [ForeignKey("Brand")]
       public string BrandId {  get; set; }
       public Brand Brand { get; set; }
        [ForeignKey("Company")]
        public string CompanyId { get; set; }
        public Company Company { get; set; }
       public int MinimumQuantity {  get; set; }
        public int Quantity { get; set; }
        public bool IsActivePoint {  get; set; }
        public int NumberOfPoints { get; set; }
        public int MaximumQuantity { get; set; }
       public double Length {  get; set; }
       public double Width {  get; set; }
       public double Height { get; set; }
       public double Liter {  get; set; }
       public DateTime AvailableDate { get; set; }
        public string SpecificationMetrix {  get; set; }
        public string Data { get; set; }
        public bool FeaturedProduct {  get; set; }
        public bool IsActive { get; set; }
        public Grade Grade { get; set; }
        public ICollection<CartProduct> CartProducts { get; set; }
        public ICollection<WishlistProduct> WishlistProducts { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }  
        public ICollection<DiscountProduct> DiscountProducts { get; set; }
    }
}
