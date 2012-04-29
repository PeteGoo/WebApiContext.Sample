using System;
using System.Collections.Specialized;
using System.Data.Objects;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Http.Filters;

namespace PeteGoo.WebApi.Web.Infrastructure {

    /// <summary>
    /// Adds $expand support for entity framework entities exposed over ASP.Net Web API
    /// </summary>
    /// <remarks>
    /// Remember to add it to the Filters for your configuration
    /// </remarks>
    public class EFExpandActionFilter : ActionFilterAttribute {
        /// <summary>
        /// Called when the action is executed.
        /// </summary>
        /// <param name="actionExecutedContext">The action executed context.</param>
        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext) {
            NameValueCollection querystringParams = HttpUtility.ParseQueryString(actionExecutedContext.Request.RequestUri.Query);
            string expandsQueryString = querystringParams["$expand"];

            if (string.IsNullOrWhiteSpace(expandsQueryString)) {
                return;
            }

            object responseObject;

            actionExecutedContext.Result.TryGetObjectValue(out responseObject);

            ObjectQuery query = responseObject as ObjectQuery;
            if (query == null) {
                return;
            }

            MethodInfo info = query.GetType().GetMethod("Include", new Type[] {typeof (string)});

            expandsQueryString.Split(',').Select(s => s.Trim()).ToList().ForEach(
                expand => {
                    query = info.Invoke(query, new object[]{expand}) as ObjectQuery;
                });

            actionExecutedContext.Result.TrySetObjectValue<object>(query);
        }
    }
}