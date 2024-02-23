using EMS_WebAPI.Model;
using EMS_WebAPP.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using static EMS_WebAPI.Model.RQRS;

namespace EMS_WebAPP.Pages
{
    public class ResultModel : PageModel
    {
        public List<VoteCount> voteCounts {  get; set; }
        public void OnGet()
        {
            RQRS.ResponseData response = APIHandler.APICallMethod("", Contrains.getElectionResults, "GET");
            voteCounts = JsonConvert.DeserializeObject<List<RQRS.VoteCount>>(response.strResponse);
        }
    }
}
