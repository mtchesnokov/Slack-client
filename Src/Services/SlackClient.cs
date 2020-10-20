using System.Collections.Generic;
using System.Threading.Tasks;
using Tch.Nuget.SlackClient.Configuration;
using Tch.Nuget.SlackClient.Domain.Objects;
using Tch.Nuget.SlackClient.Interfaces;
using Tch.Nuget.SlackClient.Interfaces.Channels;
using Tch.Nuget.SlackClient.Services.Channels;

namespace Tch.Nuget.SlackClient.Services
{
   public class SlackClient : ISlackClient
   {
      private readonly ICrudChannelService _crudChannelService;
      private readonly ClientSettings _clientSettings;

      #region ctor

      internal SlackClient(ClientSettings clientSettings, ICrudChannelService crudChannelService)
      {
         _clientSettings = clientSettings;
         _crudChannelService = crudChannelService;
      }

      public SlackClient(ClientSettings clientSettings) : this(clientSettings, new CrudChannelService())
      {
      }

      #endregion

      public Task<IEnumerable<SlackChannel>> ListPublicChannels()
      {
         return _crudChannelService.ListPublicChannels(_clientSettings.Token);
      }

      public Task<SlackChannel> CreateChannel(string channelName)
      {
         return _crudChannelService.CreateChannel(_clientSettings.Token, channelName);
      }
   }
}