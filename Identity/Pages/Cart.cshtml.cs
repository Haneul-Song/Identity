using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace Identity.Pages
{
    public class CartModel : PageModel
    {
        public CartModel? Cart { get; set; }
        public void OnGet()
        {
        }
    }
}
