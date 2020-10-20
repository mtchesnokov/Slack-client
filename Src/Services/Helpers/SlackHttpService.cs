using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Tch.Nuget.SlackClient.Data;
using Tch.Nuget.SlackClient.Domain.Exceptions;
using Tch.Nuget.SlackClient.Domain.Helpers;
using Tch.Nuget.SlackClient.Interfaces.Helpers;

namespace Tch.Nuget.SlackClient.Services.Helpers
{
   internal class SlackHttpService : ISlackHttpService
   {
      public async Task<TSlackHttpResponse> Send<TSlackHttpResponse>(HttpMethod method, string relativeUrl, string token, HttpContent content = null)
         where TSlackHttpResponse : SlackHttpResponseBase
      {
         var serviceUrl = $"{Constants.SlackBaseUrl}{relativeUrl}";

         using (var httpClient = new HttpClient())
         {
            var httpRequestMessage = new HttpRequestMessage(method, serviceUrl);

            if (content != null)
            {
               httpRequestMessage.Content = content;
            }

            httpRequestMessage.Headers.Add("Authorization", $"Bearer {token}");

            var httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);

            if (!httpResponseMessage.IsSuccessStatusCode)
            {
               throw new SlackClientException("Sending message to Slack failed")
               {
                  ResponseStatusCode = httpResponseMessage.StatusCode,
                  ResponseReasonPhrase = httpResponseMessage.ReasonPhrase
               };
            }

            var responseBody = await httpResponseMessage.Content.ReadAsStringAsync();
            var response = JsonConvert.DeserializeObject<TSlackHttpResponse>(responseBody);
            return response;
         }
      }

      public async Task<TSlackHttpResponse> SendMultipart<TSlackHttpResponse>(HttpMethod method, string relativeUrl, string token, HttpContent[] httpContents)
         where TSlackHttpResponse : SlackHttpResponseBase
      {
         var serviceUrl = $"{Constants.SlackBaseUrl}{relativeUrl}";

         using (var httpClient = new HttpClient())
         {
            var multipartContent = new MultipartFormDataContent();

            foreach (var httpContent in httpContents)
            {
               multipartContent.Add(httpContent);
            }

            var httpRequestMessage = new HttpRequestMessage(method, serviceUrl) {Content = multipartContent};

            httpRequestMessage.Headers.Add("Authorization", $"Bearer {token}");

            var httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);

            if (!httpResponseMessage.IsSuccessStatusCode)
            {
               throw new SlackClientException("Sending message to Slack failed")
               {
                  ResponseStatusCode = httpResponseMessage.StatusCode,
                  ResponseReasonPhrase = httpResponseMessage.ReasonPhrase
               };
            }

            var responseBody = await httpResponseMessage.Content.ReadAsStringAsync();
            var response = JsonConvert.DeserializeObject<TSlackHttpResponse>(responseBody);
            return response;
         }
      }
   }
}