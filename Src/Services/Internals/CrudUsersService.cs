using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Tch.Nuget.SlackClient.Domain.Helpers;
using Tch.Nuget.SlackClient.Domain.Objects;
using Tch.Nuget.SlackClient.Interfaces.Helpers;
using Tch.Nuget.SlackClient.Interfaces.Internals;
using Tch.Nuget.SlackClient.Services.Helpers;

namespace Tch.Nuget.SlackClient.Services.Internals
{
   internal class CrudUsersService : ICrudUsersService
   {
      class SlackUserResponse : SlackHttpResponseBase
      {
         public SlackMember[] Members { get; set; }
      }

      private readonly ISlackHttpService _httpService;

      #region ctor

      public CrudUsersService() : this(new SlackHttpService())
      {
      }

      protected CrudUsersService(ISlackHttpService httpService)
      {
         _httpService = httpService ?? throw new ArgumentNullException(nameof(httpService));
      }

      #endregion

      public async Task<IEnumerable<SlackMember>> GetTeamMembers(string token)
      {
         try
         {
            var response = await _httpService.Send<SlackUserResponse>(HttpMethod.Get, "/users.list", token);
            return response.Members.Where(u => !u.Is_Bot && u.Is_Owner);
         }
         catch
         {
            return Enumerable.Empty<SlackMember>();
         }
      }
   }
}