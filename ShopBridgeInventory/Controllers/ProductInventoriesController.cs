using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ShopBridgeInventory.Models;

namespace ShopBridgeInventory.Controllers
{
    public class ProductInventoriesController : Controller
    {

        readonly string apiBaseAddress = ConfigurationManager.AppSettings["apiBaseAddress"];

        // GET: ProductInventories
        public async Task<ActionResult> Index()
        {
            IEnumerable<ProductInventory> productInventories = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiBaseAddress);

                var result = await client.GetAsync("productInventories/get");

                if (result.IsSuccessStatusCode)
                {
                    productInventories = await result.Content.ReadAsAsync<IList<ProductInventory>>();
                }
                else
                {
                    productInventories = Enumerable.Empty<ProductInventory>();
                    ModelState.AddModelError(string.Empty, "Server error try after some time.");
                }
            }
            return View(productInventories);
        }

        // GET: ProductInventories/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ProductInventory productInventory = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiBaseAddress);

                var result = await client.GetAsync($"productInventories/details/{id}");

                if (result.IsSuccessStatusCode)
                {
                    productInventory = await result.Content.ReadAsAsync<ProductInventory>();
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Server error try after some time.");
                }
            }

            if (productInventory == null)
            {
                return HttpNotFound();
            }
            return View(productInventory);
        }


        // GET: ProductInventories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProductInventories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ProductName,ProductDescription,ProductPrice")] ProductInventory productInventory)
        {
            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(apiBaseAddress);

                    var response = await client.PostAsJsonAsync("productInventories/Create", productInventory);
                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Server error try after some time.");
                    }
                }
            }
            return View(productInventory);
        }

        // GET: ProductInventories/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductInventory productInventory = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiBaseAddress);

                var result = await client.GetAsync($"productInventories/details/{id}");

                if (result.IsSuccessStatusCode)
                {
                    productInventory = await result.Content.ReadAsAsync<ProductInventory>();
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Server error try after some time.");
                }
            }
            if (productInventory == null)
            {
                return HttpNotFound();
            }
            return View(productInventory);
        }

        // POST: ProductInventories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ProductId,ProductName,ProductDescription,ProductPrice")] ProductInventory productInventory)
        {
            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(apiBaseAddress);
                    var response = await client.PutAsJsonAsync("productInventories/edit", productInventory);
                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Server error try after some time.");
                    }
                }
                return RedirectToAction("Index");
            }
            return View(productInventory);
        }

        // GET: ProductInventories/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductInventory productInventory = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiBaseAddress);

                var result = await client.GetAsync($"productInventories/details/{id}");

                if (result.IsSuccessStatusCode)
                {
                    productInventory = await result.Content.ReadAsAsync<ProductInventory>();
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Server error try after some time.");
                }
            }

            if (productInventory == null)
            {
                return HttpNotFound();
            }
            return View(productInventory);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiBaseAddress);

                var response = await client.DeleteAsync($"productInventories/delete/{id}");
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                    ModelState.AddModelError(string.Empty, "Server error try after some time.");
            }
            return View();
        }
    }
}
