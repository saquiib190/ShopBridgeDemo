using ShopBridgeInventory.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;


namespace ShopBridgeInventory.Controllers
{
    public class ProductInventoriesApiController : ApiController
    {
        #region Fields

        private readonly IProductInventoryRepository _productInventoryRepository = new ProductInventoryRepository();

        #endregion

        #region Methods

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/ProductInventories/Get")]
        public async Task<IEnumerable<ProductInventory>> Get()
        {
            return await _productInventoryRepository.GetProductInventories();
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/ProductInventories/Create")]
        public async Task CreateAsync([FromBody]ProductInventory productInventory)
        {
            if (ModelState.IsValid)
            {
                await _productInventoryRepository.Add(productInventory);
            }
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/ProductInventories/Details/{id}")]
        public async Task<ProductInventory> Details(string id)
        {
            var result = await _productInventoryRepository.GetProductInventory(id);
            return result;
        }

        [System.Web.Http.HttpPut]
        [System.Web.Http.Route("api/ProductInventories/Edit")]
        public async Task EditAsync([FromBody]ProductInventory productInventory)
        {
            if (ModelState.IsValid)
            {
                await _productInventoryRepository.Update(productInventory);
            }
        }

        [System.Web.Http.HttpDelete]
        [System.Web.Http.Route("api/ProductInventories/Delete/{id}")]
        public async Task DeleteConfirmedAsync(string id)
        {
            await _productInventoryRepository.Delete(id);
        }
        #endregion
    }
}