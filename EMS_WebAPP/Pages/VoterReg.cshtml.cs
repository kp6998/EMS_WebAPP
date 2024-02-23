using EMS_WebAPI.Model;
using EMS_WebAPP.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace EMS_WebAPP.Pages
{
    public class VoterRegModel : PageModel
    {
        public void OnGet()
        {
        }
        public IActionResult OnPostVoterReg([FromBody] RQRS.Voter req)
        {
            req.voterId = 0;
            req.isApproved = false;
            RQRS.ResponseData response = APIHandler.APICallMethod(req, Contrains.registerVoter, "POST");
            return new JsonResult(new { status = response.strStatus, message = response.strResponse });
        }

    }
}
