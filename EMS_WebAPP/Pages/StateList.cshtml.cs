using EMS_WebAPI.Model;
using EMS_WebAPP.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using static EMS_WebAPI.Model.RQRS;

namespace EMS_WebAPP.Pages
{
    public class StateListModel : PageModel
    {
        public List<RQRS.State> States { get; set; }
        public void OnGet()
        {
            RQRS.ResponseData response = APIHandler.APICallMethod("", Contrains.getMPSeatsList, "GET");
            States = JsonConvert.DeserializeObject<List<RQRS.State>>(response.strResponse);
        }

        public IActionResult OnPostUpdateSeats([FromBody]RQRS.State req)
        {
            RQRS.ResponseData response = APIHandler.APICallMethod(req, Contrains.manageMPSeats, "POST");
            return new JsonResult(new { status = response.strStatus, message = response.strResponse });
        }

    }
}
