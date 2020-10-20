using System;

namespace Tch.Nuget.SlackClient.Configuration
{
   public class ClientSettings
   {
      public string Token { get; }

      #region ctor

      public ClientSettings(string token)
      {
         if (string.IsNullOrEmpty(token))
         {
            throw new ArgumentNullException(nameof(token), "token cannot be null or empty string");
         }

         Token = token;
      }

      #endregion
   }
}