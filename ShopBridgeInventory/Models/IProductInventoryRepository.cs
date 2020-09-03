using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopBridgeInventory.Models
{
    public interface IProductInventoryRepository
    {
        Task Add(ProductInventory productInventory);
        Task Update(ProductInventory productInventory);
        Task Delete(string id);
        Task<ProductInventory> GetProductInventory(string id);
        Task<IEnumerable<ProductInventory>> GetProductInventories();
    }
}
