using System;
using System.IO;
using System.Net;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace PeteGoo.WebApi.Web.Infrastructure {
    public class JsonNetFormatter : MediaTypeFormatter {
        private readonly JsonSerializerSettings jsonSerializerSettings;
        private UTF8Encoding utf8Encoding;

        public JsonNetFormatter(JsonSerializerSettings jsonSerializerSettings) {
            this.jsonSerializerSettings = jsonSerializerSettings ?? new JsonSerializerSettings();

            // Fill out the mediatype and encoding we support
            SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/json"));
            utf8Encoding = new UTF8Encoding(false, true);
            SupportedEncodings.Add(utf8Encoding);
        }

        public override bool CanReadType(Type type) {
            return true;
        }

        public override bool CanWriteType(Type type) {
            return true;
        }

        public override Task<object> ReadFromStreamAsync(Type type, Stream stream, HttpContentHeaders contentHeaders, IFormatterLogger logger) {
            // Create a serializer
            JsonSerializer serializer = JsonSerializer.Create(jsonSerializerSettings);

            // Create task reading the content
            return Task.Factory.StartNew(() => {
                using (StreamReader streamReader = new StreamReader(stream, utf8Encoding)) {
                    using (JsonTextReader jsonTextReader = new JsonTextReader(streamReader)) {
                        return serializer.Deserialize(jsonTextReader, type);
                    }
                }
            });
        }

        public override Task WriteToStreamAsync(Type type, object value, Stream stream, HttpContentHeaders contentHeaders, TransportContext transportContext) {
            // Create a serializer
            JsonSerializer serializer = JsonSerializer.Create(jsonSerializerSettings);

            // Create task writing the serialized content
            return Task.Factory.StartNew(() => {
                using (JsonTextWriter jsonTextWriter = new JsonTextWriter(new StreamWriter(stream, utf8Encoding)) { CloseOutput = false }) {
                    serializer.Serialize(jsonTextWriter, value);
                    jsonTextWriter.Flush();
                }
            });
        }
    }
}