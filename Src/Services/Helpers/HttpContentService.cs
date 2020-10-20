using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Tch.Nuget.SlackClient.Interfaces.Helpers;

namespace Tch.Nuget.SlackClient.Services.Helpers
{
   internal class HttpContentService : IHttpContentService
   {
      public HttpContent ToHttpContent(object obj)
      {
         var settings = new JsonSerializerSettings
         {
            ContractResolver = new CamelCasePropertyNamesContractResolver(),
            NullValueHandling = NullValueHandling.Ignore
         };

         var serializeObject = JsonConvert.SerializeObject(obj, Formatting.None, settings);
         var buffer = Encoding.UTF8.GetBytes(serializeObject);
         var byteContent = new ByteArrayContent(buffer);
         byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
         return byteContent;
      }
   }
}