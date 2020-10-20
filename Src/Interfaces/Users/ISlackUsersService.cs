using System.Collections.Generic;
using System.Threading.Tasks;
using Tch.Nuget.SlackClient.Domain.Objects;

namespace Tch.Nuget.SlackClient.Interfaces.Users
{
   /// <summary>
   /// This is handy service to work with team member collection
   /// </summary>
   internal interface ISlackUsersService
   {
      Task<IEnumerable<SlackMember>> GetTeamMembers(string token);
   }
}