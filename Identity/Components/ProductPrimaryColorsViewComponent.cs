using Identity.Models;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Components
{
    public class ProductPrimaryColorsViewComponent : ViewComponent
    {
        private IProductRepository _productRepo;

        public ProductPrimaryColorsViewComponent(IProductRepository temp)
        {
            _productRepo = temp;
        }

        public IViewComponentResult Invoke()
        {
            var productPrimaryColors = _productRepo.Products
                .Select(x => x.primary_color)
                .Distinct()
                .OrderBy(x => x);

            return View(productPrimaryColors);
        }
    }
}
