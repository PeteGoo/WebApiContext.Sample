using System.Linq;
using System.Web.Http;
using PeteGoo.WebApi.Common;


namespace PeteGoo.WebApi.Web.Controllers {
    public class PeopleController : ApiController {
        public IQueryable<Person> GetModels() {

            return people.AsQueryable();
        }

        private readonly Person[] people = new Person[] {
            new Person {
                Id = 1,
                FirstName = "Peter",
                LastName = "Goodman"
            }, 
            new Person {
                Id = 2,
                FirstName = "Peter",
                LastName = "Skeeter"
            }, 
            new Person {
                Id = 3,
                FirstName = "Tony",
                LastName = "Stark"
            }, 
            new Person {
                Id = 4,
                FirstName = "Frank",
                LastName = "Grimes"
            }, 
            new Person {
                Id = 5,
                FirstName = "Doug",
                LastName = "Kettle"
            }, 
            new Person {
                Id = 6,
                FirstName = "Finbar",
                LastName = "Coole"
            }, 
        };
    }

}