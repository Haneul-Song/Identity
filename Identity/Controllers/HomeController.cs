using Identity.Models;
using Identity.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

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

        public IActionResult Product(int pageNum)
        {
            int pageSize = 10;

            var blah = new ProductsListViewModel
            {
                Products = _repo.Products
                    .OrderBy(x => x.product_ID)
                    .Skip((pageNum - 1) * pageSize)
                    .Take(pageSize),

                PaginationInfo = new PaginationInfo
                {
                    CurrentPage = pageNum,
                    ItemsPerPage = pageSize,
                    TotalItems = _repo.Products.Count()
                }
            };

            return View(blah);
        }

        public IActionResult Order(int pageNum)
        {
            int pageSize = 100;

            var blah = new OrdersListViewModel
            {
                Orders = _repo.Orders
                    .OrderBy(x => x.date)
                    .Skip((pageNum - 1) * pageSize)
                    .Take(pageSize),

                PaginationInfo = new PaginationInfo
                {
                    CurrentPage = pageNum,
                    ItemsPerPage = pageSize,
                    TotalItems = _repo.Orders.Count()
                }
            };

            return View(blah);
        }

        public IActionResult Customer(int pageNum)
        {
            int pageSize = 100;

            var blah = new CustomersListViewModel
            {
                Customers = _repo.Customers
                    .OrderBy(x => x.customer_ID)
                    .Skip((pageNum - 1) * pageSize)
                    .Take(pageSize),

                PaginationInfo = new PaginationInfo
                {
                    CurrentPage = pageNum,
                    ItemsPerPage = pageSize,
                    TotalItems = _repo.Customers.Count()
                }
            };

            return View(blah);
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
        public async Task<IActionResult> Index()
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