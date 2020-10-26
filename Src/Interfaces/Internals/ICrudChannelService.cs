using System.Collections.Generic;
using System.Threading.Tasks;
using Tch.Nuget.SlackClient.Domain.Objects;

namespace Tch.Nuget.SlackClient.Interfaces.Internals
{
   /// <summary>
   ///    This interface represents service to CRUD slack channels
   /// </summary>
   internal interface ICrudChannelService
   {
      Task<IEnumerable<SlackChannel>> GetAll(string token);

      Task<SlackChannel> Create(string token, string channelName);

      Task CreateIfNotExists(string token, string channelName);

      Task<SlackChannel> GetByName(string token, string channelName);
   }
}