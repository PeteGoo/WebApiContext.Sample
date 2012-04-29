using System.Collections.Generic;
using System.Diagnostics;
using Linq2Rest.Provider;
using Newtonsoft.Json;

namespace PeteGoo.WebApi.Client.Infrastructure {
    public class JsonNetSerializerFactory : ISerializerFactory {
        public ISerializer<T> Create<T>() {
            return new JsonNetSerializer<T>();
        }

        public class JsonNetSerializer<T> : ISerializer<T> {
            public T Deserialize(string input) {
                return JsonConvert.DeserializeObject<T>(input);
            }

            public IList<T> DeserializeList(string input) {
                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();
                IList<T> results = JsonConvert.DeserializeObject<IList<T>>(input);
                stopwatch.Stop();
                return results;
            }
        }
    }

}