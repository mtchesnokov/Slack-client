using System.Net.Http;

namespace Tch.Nuget.SlackClient.Interfaces.Helpers
{
   /// <summary>
   /// Handy help service to serialize objects to json content
   /// </summary>
   internal interface IHttpContentService
   {
      HttpContent ToHttpContent(object obj);
   }
}