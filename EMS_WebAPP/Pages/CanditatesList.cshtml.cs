using EMS_WebAPI.Model;
using EMS_WebAPP.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using static EMS_WebAPI.Model.RQRS;

namespace EMS_WebAPP.Pages
{
    public class CanditatesListModel : PageModel
    {
        public List<Candidate> Candidates { get; set; }
        public void OnGet()
        {
            RQRS.ResponseData response = APIHandler.APICallMethod("", Contrains.getCandidateList, "GET");
            Candidates = JsonConvert.DeserializeObject<List<Candidate>>(response.strResponse);
        }
        public IActionResult OnPostCanditateReg([FromBody] RQRS.Party req)
        {
            RQRS.ResponseData response = APIHandler.APICallMethod(req, Contrains.registerCandidate, "POST");
            return new JsonResult(new { status = response.strStatus, message = response.strResponse });
        }
    }
}
