using System.Data.Objects;
using System.Linq;
using System.Web;
using System.Web.Http;
using PeteGoo.WebApi.Web.Models;

namespace PeteGoo.WebApi.Web.Controllers
{
    /// <summary>
    /// Api Controller exposing Northwind Customers and supporting expands
    /// </summary>
    public class NorthwindController : ApiController
    {
        private readonly NorthwindEntities northwindEntities = new NorthwindEntities();

        public NorthwindController() {
            northwindEntities.ContextOptions.LazyLoadingEnabled = false;
        }

        public IQueryable<Customer> GetCustomers() {
            return northwindEntities.Customers.ProcessExpands();
        }   
    }

    public static class ObjectQueryExtensions {
        public static ObjectQuery<T> ProcessExpands<T>(this ObjectSet<T> source) where T : class {
            string expandsQueryString = HttpContext.Current.Request.QueryString["$expand"];
            if(string.IsNullOrWhiteSpace(expandsQueryString)) {
                return source;
            }

            ObjectQuery<T> query = source;

            expandsQueryString.Split(',').Select(s => s.Trim()).ToList().ForEach(
                expand => {
                    query = query.Include(expand.Replace("/", "."));
                });

            return query;
        }
    }
}
