using EMS_WebAPI.Model;
using EMS_WebAPP.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using static EMS_WebAPI.Model.RQRS;

namespace EMS_WebAPP.Pages
{
    public class VotingSystemModel : PageModel
    {
        public List<RQRS.State> States { get; set; }
        public bool isVoted { get; set; }
        public void OnGet()
        {
            Vote req = new Vote();
            req.voterId = 11;
            req.voteId = 0;
            req.candidateId = 0;
            RQRS.ResponseData response = APIHandler.APICallMethod(req, Contrains.checkAlreadyVoted, "POST");
            isVoted = response.strResponse == "[]" ? false : true;
            if(!isVoted)
            {
                response = APIHandler.APICallMethod("", Contrains.getMPSeatsList, "GET");
                States = JsonConvert.DeserializeObject<List<RQRS.State>>(response.strResponse);
            }
        }
        public IActionResult OnGetVotingSystem()
        {
            RQRS.ResponseData response = APIHandler.APICallMethod("", Contrains.getVotingSystem, "GET");
            return new JsonResult(new { status = response.strStatus, message = response.strResponse });
        }
        public IActionResult OnPostVote([FromBody] RQRS.Vote req)
        {
            req.voterId = 1;
            req.voteId = 0;
            RQRS.ResponseData response = APIHandler.APICallMethod(req, Contrains.vote, "POST");
            return new JsonResult(new { status = response.strStatus, message = response.strResponse });
        }
    }
}
