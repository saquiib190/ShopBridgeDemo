using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace ShopBridgeInventory.Models
{
    public class ProductInventoryRepository : IProductInventoryRepository
    {
        private readonly SqlDbContext db = new SqlDbContext();

        
        public async Task Add(ProductInventory productInventory)
        {
            productInventory.ProductId = Guid.NewGuid().ToString();
            db.ProductInventories.Add(productInventory);
            try
            {
                await db.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task Delete(string id)
        {
            try
            {
                ProductInventory productInventory = await db.ProductInventories.FindAsync(id);
                db.ProductInventories.Remove(productInventory);
                await db.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task<IEnumerable<ProductInventory>> GetProductInventories()
        {
            try
            {
                var productInventories = await db.ProductInventories.ToListAsync();
                return productInventories.AsQueryable();
            }
            catch
            {
                throw;
            }
        }

        public async Task<ProductInventory> GetProductInventory(string id)
        {
            try
            {
                ProductInventory productInventory = await db.ProductInventories.FindAsync(id);
                if (productInventory == null)
                {
                    return null;
                }
                return productInventory;
            }
            catch
            {
                throw;
            }
        }

        public async Task Update(ProductInventory productInventory)
        {
            try
            {
                db.Entry(productInventory).State = EntityState.Modified;
                await db.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        private bool ProductInventoryExists(string id)
        {
            return db.ProductInventories.Count(pi => pi.ProductId == id) > 0;
        }
    }
}