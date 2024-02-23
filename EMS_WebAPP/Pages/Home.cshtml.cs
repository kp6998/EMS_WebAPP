using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EMS_WebAPP.Pages
{
    public class HomeModel : PageModel
    {
        public bool isAdmin {  get; set; }
        public void OnGet()
        {
            var user = HttpContext.Session.GetInt32("User");
            if(user == null)
            {
                RedirectToPage("/Login");
            }
            else
            {
                isAdmin = user == 0 ? true : false;
            }
        }
        public void OnGetLogout()
        {
            HttpContext.Session.Remove("User");

        }
    }
}
