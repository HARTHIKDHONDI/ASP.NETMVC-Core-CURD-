using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace Basic_model
{
    public class SetSessionGlobally:ActionFilterAttribute

    {
        public override void OnActionExecuting(ActionExecutingContext filtercontext)
        {
            var value = filtercontext.HttpContext.Session.GetString("Loginpersonsname");
            if (value == null) {
                filtercontext.Result =
                     new RedirectToRouteResult(
                         new RouteValueDictionary
                         {
                            {"controller","Register" },
                            {"action","Login" }

                         }
                         );
            }
        }
    }
}
