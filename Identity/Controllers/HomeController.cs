using Identity.Models;
using Identity.Models.ViewModels;
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
        public IActionResult Home()
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
        // [Authorize]
        //[Authorize(Roles = "Manager")]
        //public async Task<IActionResult> Index()
        //{
        //    AppUser user = await userManager.GetUserAsync(HttpContext.User);
        //    //  string message = "Hello " + user.UserName;
        //    // return View((object)message);
        //    return View("Home");
        //}

        public ViewResult Index(string? category, int productPage = 1)
           => View(new ProductsListViewModel
           {
               Products = _repo.Products
                    .Where(p => category == null
                        || p.Category == category)
                   .OrderBy(p => p.ProductID)
                   .Skip((productPage - 1) * PageSize)
                   .Take(PageSize),
               PagingInfo = new PagingInfo
               {
                   CurrentPage = productPage,
                   ItemsPerPage = PageSize,
                   TotalItems = category == null
                        ? _repo.Products.Count()
                        : _repo.Products.Where(e =>
                            e.Category == category).Count()
               },
               CurrentCategory = category
           });

        public async Task<IActionResult> Privacy()
        {
            return View(Privacy);
        }
    }
}