using System.Collections.Generic;
using System.Threading.Tasks;

namespace Tch.Nuget.SlackClient.Interfaces.Internals
{
   /// <summary>
   ///    This interface represents service to CRUD channel members
   /// </summary>
   internal interface ICrudChannelMemberService
   {
      Task<IEnumerable<string>> ListChannelMemberIds(string token, string channelId);

      Task InviteMembers(string token, string channelId, IEnumerable<string> memberIds);
   }
}