using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Tch.Nuget.SlackClient.Configuration;
using Tch.Nuget.SlackClient.Domain;
using Tch.Nuget.SlackClient.Interfaces.Infra;

namespace Tch.Nuget.SlackClient.Services.Infra
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
               throw new ApplicationException($"Sending message to Slack failed. Error code {httpResponseMessage.StatusCode} returned");
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
               throw new ApplicationException($"Sending message to Slack failed. Error code {httpResponseMessage.StatusCode} returned");
            }

            var responseBody = await httpResponseMessage.Content.ReadAsStringAsync();

            var response = JsonConvert.DeserializeObject<TSlackHttpResponse>(responseBody);

            return response;
         }
      }
   }
}