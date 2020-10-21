using System.Collections.Generic;
using System.Threading.Tasks;
using Tch.Nuget.SlackClient.Configuration;
using Tch.Nuget.SlackClient.Domain.Objects;
using Tch.Nuget.SlackClient.Interfaces;
using Tch.Nuget.SlackClient.Interfaces.Internals;
using Tch.Nuget.SlackClient.Services.Internals;

namespace Tch.Nuget.SlackClient.Services
{
   public class SlackClient : ISlackClient
   {
      private readonly ICrudChannelService _crudChannelService;
      private readonly ClientSettings _clientSettings;
      private readonly ICrudUsersService _crudUsersService;

      #region ctor

      internal SlackClient(ClientSettings clientSettings, ICrudChannelService crudChannelService, ICrudUsersService crudUsersService)
      {
         _clientSettings = clientSettings;
         _crudChannelService = crudChannelService;
         _crudUsersService = crudUsersService;
      }

      public SlackClient(ClientSettings clientSettings) : this(clientSettings, new CrudChannelService(), new CrudUsersService())
      {
      }

      #endregion

      public Task<IEnumerable<SlackChannel>> GetPublicChannels()
      {
         return _crudChannelService.GetPublicChannels(_clientSettings.Token);
      }

      public Task<SlackChannel> CreateChannel(string channelName)
      {
         return _crudChannelService.CreateChannel(_clientSettings.Token, channelName);
      }

      public Task<IEnumerable<SlackMember>> GetTeamMembers()
      {
         return _crudUsersService.GetTeamMembers(_clientSettings.Token);
      }
   }
}