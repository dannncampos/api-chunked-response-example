using Chunked.Models;
using Microsoft.AspNet.WebApi.Extensions.Compression.Server.Attributes;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Chunked.Controllers
{
    public class ChunkedController : ApiController
    {

        [Route("chunked_stream_house")]
        [HttpGet]
        [Compression(Enabled = false)]
        public HttpResponseMessage GetChunkedMessageStreamHouse()
        {
            var response = ActionContext.Request.CreateResponse(HttpStatusCode.OK);
            response.Headers.TransferEncodingChunked = true;

            var list = GetHousesOnSale();

            response.Content =
                new PushStreamContent((stream, content, context) =>
                {
                    using (var sw = new StreamWriter(stream))
                    using (var jsonWriter = new JsonTextWriter(sw))
                    {
                        jsonWriter.WriteStartArray();
                        {
                            foreach (var item in list)
                            {
                                var jObject = JObject.FromObject(item);
                                jObject.WriteTo(jsonWriter);
                            }
                        }

                        jsonWriter.WriteEndArray();
                    }
                },
                    "application/json");

            return response;
        }


        private IEnumerable<House> GetHousesOnSale()
        {
            var houses = Enumerable.Range(0, 500000);

            foreach (var house in houses)
            {
                yield return new House
                {

                    OldId = house,
                    EntityType = "House",
                    RefNumber = "123",
                    Bedrooms = 1,
                    Bathrooms = 1,
                    Rooms = 1,
                    ParkingSpaces = 1,
                };
            }
        }

    }
}
