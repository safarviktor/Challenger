using System;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Mvc;
using Challenger.DataAccess;
using Challenger.Models;

namespace Challenger.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods:"*")]
    [System.Web.Mvc.Route("/api/challenges")]
    public class ChallengesController : System.Web.Http.ApiController
    {
        private readonly DataContext _dataContext = new DataContext();

        [System.Web.Http.HttpGet]
        [System.Web.Mvc.Route("/list")]
        public ActionResult List()
        {
            var model = _dataContext.GetChallengeOverviews();
            return new JsonResult() { Data = model, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        [System.Web.Http.HttpGet]
        [System.Web.Mvc.Route("/details/{id}")]
        public ActionResult Details(int id)
        {
            var model = _dataContext.GetChallengeDetails(id);
            return new JsonResult() { Data = model, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }


        [System.Web.Http.HttpPost]
        [System.Web.Mvc.Route("/create")]
        public ActionResult Create([FromBody] ChallengeOverviewModel model)
        {
            var newChallenge = _dataContext.AddNewChallenge(model.Name, model.Type);
            return new JsonResult() { Data = newChallenge, JsonRequestBehavior = JsonRequestBehavior.DenyGet };
        }
    }
}
