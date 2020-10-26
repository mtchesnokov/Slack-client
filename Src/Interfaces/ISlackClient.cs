using System.Collections.Generic;
using System.Threading.Tasks;
using Tch.Nuget.SlackClient.Domain.Objects;

namespace Tch.Nuget.SlackClient.Interfaces
{
   public interface ISlackClient
   {
      /// <summary>
      /// Get all members in your workspace including bot users
      /// </summary>
      /// <returns></returns>
      Task<IEnumerable<SlackMember>> GetAllTeamMembers();

      /// <summary>
      /// Get all public channels in your workspace
      /// </summary>
      /// <returns></returns>
      Task<IEnumerable<SlackChannel>> GetAllChannels();

      /// <summary>
      /// Create new public channel in your workspace
      /// </summary>
      /// <param name="channelName"></param>
      /// <returns></returns>
      Task<SlackChannel> CreateChannel(string channelName);

      /// <summary>
      /// Get channel by name
      /// </summary>
      /// <param name="channelName"></param>
      /// <returns></returns>
      Task<SlackChannel> GetChannelByName(string channelName);

      /// <summary>
      /// Create new public channel in your workspace. If chanel already exists, no exception thrown
      /// </summary>
      /// <param name="channelName"></param>
      /// <returns></returns>
      Task CreateChannelIfNotExists(string channelName);


      /// <summary>
      /// Get members of given channel
      /// </summary>
      /// <param name="slackChannel"></param>
      /// <returns></returns>
      Task<IEnumerable<SlackMember>> GetChannelMembers(SlackChannel slackChannel);
   }
}