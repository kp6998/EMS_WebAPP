using EMS_WebAPI.Model;
using EMS_WebAPP.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace EMS_WebAPP.Pages
{
    public class LoginModel : PageModel
    {
        public IActionResult OnGet()
        {
            var user = HttpContext.Session.GetInt32("User");
            if (user != null)
            {
                return RedirectToPage("/Home");
            }
            return Page();
        }
        public IActionResult OnPostVoterLogin([FromBody] RQRS.VoterId req)
        {
            RQRS.ResponseData response = APIHandler.APICallMethod(req, Contrains.voterLogin, "POST");
            if(response.strResponse != "[]")
            {
                HttpContext.Session.SetInt32("User", req.voterId);
            }
            else
            {
                response.strStatus = "00";
            }
            return new JsonResult(new { status = response.strStatus, message = response.strResponse });
        }

        public IActionResult OnPostAdminLogin([FromBody] RQRS.Cred req)
        {
            RQRS.ResponseData response = new RQRS.ResponseData();
            if(req.username.ToUpper() == "ADMIN" && req.password.ToUpper() == "ADMIN")
            {
                response.strStatus = "01";
                response.strResponse = "Logged in successfully";
                HttpContext.Session.SetInt32("User", 0);
            }
            else
            {
                response.strStatus = "00";
                response.strResponse = "Incorrect credentials";
            }
            return new JsonResult(new { status = response.strStatus, message = response.strResponse });
        }
    }
}
