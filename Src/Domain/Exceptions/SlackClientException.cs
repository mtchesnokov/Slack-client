using System;
using System.Net;

namespace Tch.Nuget.SlackClient.Domain.Exceptions
{
   public class SlackClientException : Exception
   {
      public HttpStatusCode ResponseStatusCode { get; set; }

      public string ResponseReasonPhrase { get; set; }

      public SlackClientException(string message) : base(message)
      {
      }
   }
}