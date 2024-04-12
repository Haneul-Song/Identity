//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore.Migrations;
//using Identity.Models;

//namespace Identity.Components
//{
//    public class NavigationMenuViewComponent : ViewComponent
//    {
//        private IProductRepository repository;

//        public NavigationMenuViewComponent(IProductRepository repo)
//        {
//            repository = repo;
//        }

//        //public IViewComponentResult Invoke()
//        //{
//        //    ViewBag.SelectedCategory = RouteData?.Values["category"];
//        //    return View(repository.Products
//        //        .Join(
//        //        .Select(x => x.category_ID)
//        //        .Distinct()
//        //        .OrderBy(x => x));
//        //}



//        public IViewComponentResult Invoke()
//        {
//            ViewBag.SelectedCategory = RouteData?.Values["category"];

//            // Assuming `repository.Categories` is your other table
//            // and `product.CategoryId` matches `category.Id`
//            var productsWithCategories = repository.Products
//                .Join(repository.ProductCategories, // The inner sequence to join with
//                      product => product.product_ID, // Outer key selector
//                      productCategories => productCategories.product_ID, // Inner key selector
//                      (product, productCategories) => new { Product = product, productCategories = productCategories }) // Result selector
//                .Select(x => x.category_ID)
//                .Distinct()
//                .OrderBy(x => x));


//                .Select(joinedItem => new {
//                    // You can select the properties you need, for example:
//                    product_ID = joinedItem.Product.Id,
//                    ProductName = joinedItem.Product.Name,
//                    CategoryName = joinedItem.Category.Name
//                    // Add more properties as needed
//                })
//                .Distinct()
//                .OrderBy(item => item.CategoryName); // Assuming you want to order by CategoryName

//            return View(productsWithCategories);
//        }

//    }
//}
