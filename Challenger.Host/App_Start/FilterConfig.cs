using System.Web;
using System.Web.Mvc;
using Challenger.Filters;

namespace Challenger
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
