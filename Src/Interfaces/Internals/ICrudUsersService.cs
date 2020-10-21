using System.Collections.Generic;
using System.Threading.Tasks;
using Tch.Nuget.SlackClient.Domain.Objects;

namespace Tch.Nuget.SlackClient.Interfaces.Internals
{
   /// <summary>
   /// This is handy service to work with team member collection
   /// </summary>
   internal interface ICrudUsersService
   {
      Task<IEnumerable<SlackMember>> GetTeamMembers(string token);
   }
}