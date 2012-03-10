using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PeteGoo.WebApi.Client {
    class Program {
        static void Main(string[] args) {
            PrintQuery(PeopleContext.Format.Pox);
            PrintQuery(PeopleContext.Format.Json);

            Console.ReadKey(true);
        }

        private static void PrintQuery(PeopleContext.Format wireFormat) {
            Console.WriteLine();
            Console.WriteLine("*** Querying in {0} format ***", wireFormat);
            Console.WriteLine();

            // Perform Query
            new PeopleContext(new Uri("http://ipv4.fiddler:14061/api/People"), wireFormat)
                .People
                .Where(model => model.Id == 1)
                .Where(model => model.FirstName.StartsWith("Pe"))
                .ToList()
                .ForEach(item => {
                    Console.WriteLine("-----------------------------------");
                    Console.WriteLine("Id:{0}", item.Id);
                    Console.WriteLine("First Name:{0}", item.FirstName);
                    Console.WriteLine("Last Name:{0}", item.LastName);
                });
            Console.WriteLine("-----------------------------------");
        }
    }
}
