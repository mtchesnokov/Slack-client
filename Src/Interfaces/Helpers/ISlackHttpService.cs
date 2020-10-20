using System.Net.Http;
using System.Threading.Tasks;
using Tch.Nuget.SlackClient.Domain.Helpers;

namespace Tch.Nuget.SlackClient.Interfaces.Helpers
{
   /// <summary>
   /// This interface represents low level client service to send http requests to slack
   /// </summary>
   internal interface ISlackHttpService
   {
      Task<TSlackHttpResponse> Send<TSlackHttpResponse>(HttpMethod method, string relativeUrl, string token, HttpContent content = null)
         where TSlackHttpResponse : SlackHttpResponseBase;

      Task<TSlackHttpResponse> SendMultipart<TSlackHttpResponse>(HttpMethod method, string relativeUrl, string token, HttpContent[] httpContents)
         where TSlackHttpResponse : SlackHttpResponseBase;
   }
}