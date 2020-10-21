using System.Collections.Generic;
using System.Threading.Tasks;
using Tch.Nuget.SlackClient.Domain.Objects;

namespace Tch.Nuget.SlackClient.Interfaces
{
   public interface ISlackClient
   {
      Task<IEnumerable<SlackChannel>> GetPublicChannels();

      Task<SlackChannel> CreateChannel(string channelName);
      Task<IEnumerable<SlackMember>> GetTeamMembers();
   }
}