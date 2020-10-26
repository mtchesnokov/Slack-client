using System.Collections.Generic;
using System.Linq;
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
      private readonly ClientSettings _clientSettings;
      private readonly ICrudChannelService _crudChannelService;
      private readonly ICrudMemberService _crudMemberService;
      private readonly ICrudChannelMemberService _crudChannelMemberService;

      #region ctor

      internal SlackClient(ClientSettings clientSettings,
         ICrudChannelService crudChannelService,
         ICrudMemberService crudMemberService,
         ICrudChannelMemberService crudChannelMemberService)
      {
         _clientSettings = clientSettings;
         _crudChannelService = crudChannelService;
         _crudMemberService = crudMemberService;
         _crudChannelMemberService = crudChannelMemberService;
      }

      public SlackClient(ClientSettings clientSettings) : this(clientSettings,
         new CrudChannelService(),
         new CrudMemberService(),
         new CrudChannelMemberService())
      {
      }

      #endregion

      public Task<IEnumerable<SlackChannel>> GetAllChannels()
      {
         return _crudChannelService.GetAll(_clientSettings.Token);
      }

      public Task<SlackChannel> CreateChannel(string channelName)
      {
         return _crudChannelService.Create(_clientSettings.Token, channelName);
      }

      public Task<SlackChannel> GetChannelByName(string channelName)
      {
         return _crudChannelService.GetByName(_clientSettings.Token, channelName);
      }

      public Task CreateChannelIfNotExists(string channelName)
      {
         return _crudChannelService.CreateIfNotExists(_clientSettings.Token, channelName);
      }

      public Task<IEnumerable<SlackMember>> GetAllTeamMembers()
      {
         return _crudMemberService.GetAll(_clientSettings.Token);
      }

      public async Task<IEnumerable<SlackMember>> GetChannelMembers(SlackChannel slackChannel)
      {
         var members = (await GetAllTeamMembers()).ToDictionary(x => x.Id, z => z);
         var memberIds = await _crudChannelMemberService.ListChannelMemberIds(_clientSettings.Token, slackChannel.Id);

         var result = new List<SlackMember>(memberIds.Count());

         foreach (var memberId in memberIds)
         {
            result.Add(members[memberId]);
         }

         return result;
      }
   }
}