using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Tch.Nuget.SlackClient.Domain.Helpers;
using Tch.Nuget.SlackClient.Interfaces.Helpers;
using Tch.Nuget.SlackClient.Interfaces.Internals;
using Tch.Nuget.SlackClient.Services.Helpers;

namespace Tch.Nuget.SlackClient.Services.Internals
{
   internal class CrudChannelMemberService : ICrudChannelMemberService
   {
      public class SlackChannelResponse : SlackHttpResponseBase
      {
         public string[] Members { get; set; }
      }

      private readonly ISlackHttpService _httpService;
      private readonly IHttpContentService _httpContentService;

      #region ctor

      public CrudChannelMemberService() : this(new SlackHttpService(), new HttpContentService())
      {
      }

      protected CrudChannelMemberService(ISlackHttpService httpService, IHttpContentService httpContentService)
      {
         _httpContentService = httpContentService ?? throw new ArgumentNullException(nameof(httpContentService));
         _httpService = httpService ?? throw new ArgumentNullException(nameof(httpService));
      }

      #endregion

      public async Task<IEnumerable<string>> ListChannelMemberIds(string token, string channelId)
      {
         try
         {
            var response = await _httpService.Send<SlackChannelResponse>(HttpMethod.Post, $"/conversations.members?channel={channelId}", token);
            return response.Members;
         }
         catch
         {
            return Enumerable.Empty<string>();
         }
      }

      public async Task InviteMembers(string token, string channelId, IEnumerable<string> memberIds)
      {
         if (!memberIds.Any())
         {
            return;
         }

         var payload = new
         {
            channel = channelId,
            users = memberIds
         };

         var httpContent = _httpContentService.ToHttpContent(payload);

         await _httpService.Send<SlackChannelResponse>(HttpMethod.Post, "/conversations.invite", token, httpContent);
      }
   }
}