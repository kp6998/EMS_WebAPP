using System.Collections.Generic;
using EMS_WebAPI.Model;
using EMS_WebAPP.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using static EMS_WebAPI.Model.RQRS;

namespace EMS_WebAPP.Pages
{
    public class VoterListModel : PageModel
    {
        public List<RQRS.Voter> Voters { get; set; }
        public bool isAdmin { get; set; }

        public void OnGet()
        {
            var user = HttpContext.Session.GetInt32("User");
            if (user == null)
            {
                RedirectToPage("/Login");
            }
            else
            {
                isAdmin = user == 0 ? true : false;
            }
            RQRS.ResponseData response = APIHandler.APICallMethod("", Contrains.getVotersList, "GET");
            Voters = JsonConvert.DeserializeObject<List<RQRS.Voter>>(response.strResponse);
        }
        public IActionResult OnPostApproveVoter([FromBody] RQRS.VoterId req)
        {
            RQRS.ResponseData response = APIHandler.APICallMethod(req, Contrains.approveVoter, "POST");
            return new JsonResult(new { status = response.strStatus, message = response.strResponse });
        }
    }
}
