using Identity.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Controllers
{
    public class HomeController : Controller
    {
        private IProductRepository _repo;

        private UserManager<AppUser> userManager;
        public HomeController(UserManager<AppUser> userMgr, IProductRepository temp)
        {
            userManager = userMgr;
            _repo = temp;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Product()
        {
            var productData = _repo.Products;
            return View(productData);
        }

        public IActionResult Order()
        {
            var orderData = _repo.Orders;
            return View(orderData);
        }

        public IActionResult Customer()
        {
            var customerData = _repo.Customers;
            return View(customerData);
        }
        public IActionResult LineItem()
        {
            var lineItemData = _repo.LineItems;
            return View(lineItemData);
        }
        public IActionResult Category()
        {
            var categoryData = _repo.Categories;
            return View(categoryData);
        }
        public IActionResult ProductCategory()
        {
            var productCategoryData = _repo.ProductCategories;
            return View(productCategoryData);
        }
        public IActionResult ProductRecommendation()
        {
            var productRecommendationData = _repo.ProductRecommendations;
            return View(productRecommendationData);
        }
        public IActionResult ContentRecommendation()
        {
            var contentRecommendationData = _repo.ContentRecommendations;
            return View(contentRecommendationData);
        }
        public IActionResult Checkout()
        {
            return View();
        }
        [Authorize]
        //[Authorize(Roles = "Manager")]
        public async Task<IActionResult> Login()
        {
            AppUser user = await userManager.GetUserAsync(HttpContext.User);
            string message = "Hello " + user.UserName;
            return View((object)message);
        }

        public async Task<IActionResult> Privacy()
        {
            return View(Privacy);
        }
    }
}