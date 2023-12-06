using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ShopApp.Business.Abstract;
using ShopApp.Model;
using ShopApp.Web.Models;
using System.Diagnostics;

namespace ShopApp.Web.Controllers
{
    public class HomeController : Controller
    {
        private IProductService _productService;
       
        public HomeController(IProductService productService)
        {
            _productService = productService;
        }
        public IActionResult Index( int page = 1)
        {

            const int pageSize = 6;
            return View(new ProductListModel()
            {
                Products = _productService.GetProductsByCategory(null, page, pageSize),
                PagingInfo = new PagingInfo()
                {
                    CurrentPage = page,
                    ItemsPerPage = pageSize,
                    TotalItems = _productService.GetAll().Count,
                    CurrentCategory = null
                }
            });
        }
    }
}