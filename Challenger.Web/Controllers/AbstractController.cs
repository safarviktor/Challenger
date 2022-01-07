using System.Web.Mvc;
using Challenger.DataAccess;
using Challenger.Models;
using Microsoft.AspNet.Identity;

namespace Challenger.Web.Controllers
{
    public abstract class AbstractController : Controller
    {
        public UserSettingsModel UserSettings;

        protected AbstractController()
        {
            UserSettings = new UserSettingsRepository().GetUserSettingsModel(System.Web.HttpContext.Current.User.Identity.GetUserId());
            if (UserSettings != null)
            {
                ViewBag.UserSettingsSalutation = UserSettings.Salutation;
            }
        }
    }
}