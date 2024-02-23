using EMS_WebAPI.Model;
using EMS_WebAPP.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using static EMS_WebAPI.Model.RQRS;

namespace EMS_WebAPP.Pages
{
    public class PartiesListModel : PageModel
    {
        public List<Party> Parties { get; set; }
        public void OnGet()
        {
            RQRS.ResponseData response = APIHandler.APICallMethod("", Contrains.getPartiesList, "GET");
            Parties = JsonConvert.DeserializeObject<List<Party>>(response.strResponse);
        }
        public IActionResult OnPostPartyReg([FromBody] RQRS.Party req)
        {
            RQRS.ResponseData response = APIHandler.APICallMethod(req, Contrains.registerParty, "POST");
            return new JsonResult(new { status = response.strStatus, message = response.strResponse });
        }
    }
}
