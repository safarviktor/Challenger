using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Challenger.DataAccess;
using Challenger.Models;

namespace Challenger.Controllers
{
    public class HomeController : Controller
    {
        //private readonly DataContext _dataContext = new DataContext();

        //[HttpGet]
        //public ActionResult Index()
        //{
        //    var model = _dataContext.GetChallengeOverviews();
        //    return Json(new { model }, JsonRequestBehavior.AllowGet);
        //}

        //[HttpGet]
        //public ActionResult Details(int id)
        //{
        //    var model = _dataContext.GetChallengeDetails(id);

        //    return Json(new { model }, JsonRequestBehavior.AllowGet);
        //}

        //[HttpGet]
        //public ActionResult Create(int id)
        //{
        //    var model = _dataContext.GetSetForNewTracking(id);
        //    return Json(new { model }, JsonRequestBehavior.AllowGet);
        //}

        //[HttpPost]
        //public ActionResult Create(FormCollection collection)
        //{
        //    try
        //    {
        //        var challengeId = Convert.ToInt32(collection[nameof(TrackSetModel.ChallengeId)]);

        //        _dataContext.AddNewSet(new TrackSetModel()
        //        {
        //            ChallengeId = challengeId,
        //            Repetitions = Convert.ToInt32(collection[nameof(TrackSetModel.Repetitions)]),
        //            Date = Convert.ToDateTime(collection[nameof(TrackSetModel.Date)]).Date,
        //        });

        //        return RedirectToAction("Details", new { id = challengeId });
        //    }
        //    catch
        //    {
        //        return RedirectToAction("Index");
        //    }
        //}
    }
}
