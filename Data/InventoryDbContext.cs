using Inventory_Manager.Models;
using InventoryManger.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace InventoryManger.Data
{
    public class InventoryDbContext:IdentityDbContext
    {
        public InventoryDbContext(DbContextOptions options):base (options)
        {

        }
        public DbSet<Unit> Units { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ProductGroup> ProductGroups { get; set; }
        public DbSet<ProductProfile> ProductProfiles { get; set; }
        public DbSet<Product> Grossary { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
    }
}
