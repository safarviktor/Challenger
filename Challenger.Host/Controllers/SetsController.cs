using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Mvc;
using Challenger.DataAccess;
using Challenger.Models;

namespace Challenger.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [System.Web.Mvc.Route("/api/sets")]
    public class SetsController : ApiController
    {
        private readonly DataContext _dataContext = new DataContext();

        [System.Web.Http.HttpGet]
        [System.Web.Mvc.Route("/forchallenge/{id}")]
        public ActionResult ForChallenge(int id)
        {
            var model = _dataContext.GetSetForNewTracking(id);
            return new JsonResult() { Data = model, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        [System.Web.Http.HttpGet]
        [System.Web.Mvc.Route("/details/{id}")]
        public ActionResult Details(int id)
        {
            var model = _dataContext.GetSetForNewTracking(id);
            return new JsonResult() { Data = model, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        [System.Web.Http.HttpPost]
        [System.Web.Mvc.Route("/create")]
        public ActionResult Create([FromBody] TrackSetModel newSet)
        {
            var set = _dataContext.AddNewSet(newSet);
            return new JsonResult() { Data = set, JsonRequestBehavior = JsonRequestBehavior.DenyGet };
        }
    }
}
