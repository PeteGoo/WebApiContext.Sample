using System;
using System.Collections.Generic;
using System.Linq;
using Linq2Rest.Implementations;
using Linq2Rest.Provider;
using PeteGoo.WebApi.Client.Infrastructure;
using PeteGoo.WebApi.Common;

namespace PeteGoo.WebApi.Client {
    public class PeopleContext {
        private readonly RestContext<Person> restContext;

        public PeopleContext(Uri uri, Format format) {
            restContext = new RestContext<Person>(GetRestClient(uri, format), GetSerializerFactory(format));
        }

        public enum Format {
            Pox,
            Json
        }

        public static IRestClient GetRestClient(Uri uri, Format format) {
            switch (format) {
                case Format.Pox:
                    return new XmlRestClient(uri);
                case Format.Json:
                    return new JsonRestClient(uri);
                default:
                    throw new NotImplementedException();
            }
        }

        public static ISerializerFactory GetSerializerFactory(Format format) {
            switch (format) {
                case Format.Pox:
                    return new XmlSerializerFactory(knownTypes);
                case Format.Json:
                    return new JsonNetSerializerFactory();

                default:
                    throw new NotImplementedException();
            }
        }

        private static readonly IEnumerable<Type> knownTypes = new[] {
            typeof (Person)
        };

        public IQueryable<Person> People {
            get { return restContext.Query; }
        }
    
    }
}