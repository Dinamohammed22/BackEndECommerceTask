using KOG.ECommerce.Helpers;
using KOG.ECommerce.Models.Advertisements;
using KOG.ECommerce.Models.BlackListTokens;
using KOG.ECommerce.Models.Brands;
using KOG.ECommerce.Models.CartProducts;
using KOG.ECommerce.Models.Carts;
using KOG.ECommerce.Models.Categories;
using KOG.ECommerce.Models.Cities;
using KOG.ECommerce.Models.Classifications;
using KOG.ECommerce.Models.ClientGroups;
using KOG.ECommerce.Models.Clients;
using KOG.ECommerce.Models.Companies;
using KOG.ECommerce.Models.CompanyGovernorates;
using KOG.ECommerce.Models.Coupons;
using KOG.ECommerce.Models.Emails;
using KOG.ECommerce.Models.Governorates;
using KOG.ECommerce.Models.Medias;
using KOG.ECommerce.Models.ModuleFeatures;
using KOG.ECommerce.Models.Modules;
using KOG.ECommerce.Models.Notifications;
using KOG.ECommerce.Models.OrderItems;
using KOG.ECommerce.Models.Orders;
using KOG.ECommerce.Models.Pages;
using KOG.ECommerce.Models.Products;
using KOG.ECommerce.Models.RoleFeatures;
using KOG.ECommerce.Models.RoleModules;
using KOG.ECommerce.Models.ShippingAddresses;
using KOG.ECommerce.Models.Users;
using KOG.ECommerce.Models.WishlistProducts;
using KOG.ECommerce.Models.Wishlists;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Diagnostics;
using System.Text.Json;

namespace KOG.ECommerce.Data;

public class Entities : DbContext
{
    public Entities()
    {
        ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
    }

    public DbSet<Company> Companies { get; set; }
    public DbSet<Governorate> Governorates { get; set; }
    public DbSet<City> Cities { get; set; }
    public DbSet<Classification> Classifications { get; set; }
    public DbSet<Brand> Brands { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Media> Medias { get; set; }
    public DbSet<Client> Clients { get; set; }
    public DbSet<RoleFeature> RoleFeatures { get; set; }
    public DbSet<Page> Pages { get; set; }
    public DbSet<Module> Modules { get; set; }
    public DbSet<RoleModule> RoleModules { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<ModuleFeature> ModuleFeatures { get; set; }
    public DbSet<ClientGroup> ClientGroups { get; set; }
    public DbSet<Email>Emails { get; set; }
    public DbSet<CartProduct> CartProducts { get; set; }
    public DbSet<Cart>Carts { get; set; }
    public DbSet<WishlistProduct> WishlistProducts { get; set; }
    public DbSet<Wishlist> Wishlists { get; set; }
    public DbSet<ShippingAddress> ShippingAddresses { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Coupon> Coupons { get; set; }
    public DbSet<Advertisement> Advertisements { get; set; }
    public DbSet<BlackListToken> blackListTokens { get; set; }
    public DbSet<CompanyGovernorate> CompanyGovernorates { get; set; }
    public DbSet<NotificationMessage> NotificationMessages { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(ConfigurationHelper.GetConnectionString())
            .LogTo(log => Debug.WriteLine(log), Microsoft.Extensions.Logging.LogLevel.Information)
            .EnableSensitiveDataLogging()
            .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
            .ConfigureWarnings(w => w.Ignore(SqlServerEventId.SavepointsDisabledBecauseOfMARS));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // modelBuilder.Entity<Instructor>().ToTable("Instructor", schema: "HR");
        modelBuilder.Entity<Category>()
        .Property(c => c.Tags)
        .HasConversion(
            v => JsonSerializer.Serialize(v, (JsonSerializerOptions)null),
            v => JsonSerializer.Deserialize<List<string>>(v, (JsonSerializerOptions)null));
        modelBuilder.Entity<Category>()
       .Property(c => c.SEO)
       .HasConversion(
           v => JsonSerializer.Serialize(v, (JsonSerializerOptions)null),
           v => JsonSerializer.Deserialize<List<string>>(v, (JsonSerializerOptions)null));
        modelBuilder.Entity<Product>()
        .Property(c => c.Tags)
        .HasConversion(
            v => JsonSerializer.Serialize(v, (JsonSerializerOptions)null),
            v => JsonSerializer.Deserialize<List<string>>(v, (JsonSerializerOptions)null));
        modelBuilder.Entity<Brand>()
       .Property(c => c.Tags)
       .HasConversion(
           v => JsonSerializer.Serialize(v, (JsonSerializerOptions)null),
           v => JsonSerializer.Deserialize<List<string>>(v, (JsonSerializerOptions)null));
        modelBuilder.Entity<Order>()
     .HasIndex(rf => new { rf.OrderNumber })
     .IsUnique();
        modelBuilder.Entity<RoleFeature>()
          .HasIndex(rf => new { rf.RoleId, rf.Features });
        modelBuilder.Entity<RoleModule>()
          .HasIndex(rf => new { rf.RoleId, rf.ModuleId })
          .IsUnique();
        modelBuilder.Entity<ModuleFeature>()
         .HasIndex(rf => new { rf.ModuleId, rf.Features })
         .IsUnique();
        modelBuilder.Entity<CartProduct>()
        .HasIndex(rf => new { rf.CartId, rf.ProductId })
        .IsUnique(false);
     //   modelBuilder.Entity<Client>()
     //.HasIndex(rf => new { rf.NationalNumber })
     //.IsUnique();
    }

}
