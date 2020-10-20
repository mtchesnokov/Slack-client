﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Tch.Nuget.SlackClient.Domain.Objects;

namespace Tch.Nuget.SlackClient.Interfaces.Channels
{
   /// <summary>
   ///    This is handy service to manage Slack channels
   /// </summary>
   internal interface ICrudChannelService
   {
      Task<IEnumerable<SlackChannel>> ListPublicChannels(string token);

      Task<SlackChannel> CreateChannel(string token, string channelName);
   }
}