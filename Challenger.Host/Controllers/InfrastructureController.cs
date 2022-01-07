using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using Challenger.Models;

namespace Challenger.Controllers
{
    [System.Web.Mvc.Route("/api/infrastructure")]
    public class InfrastructureController : ApiController
    {
        [System.Web.Http.HttpGet]
        [System.Web.Mvc.Route("/challengetypes")]
        public ActionResult ChallengeTypes()
        {
            var model = Enum.GetValues(typeof(ChallengeType)).Cast<ChallengeType>();
            return new JsonResult() { Data = model, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
    }
}
