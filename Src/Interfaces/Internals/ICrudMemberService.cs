using System.Collections.Generic;
using System.Threading.Tasks;
using Tch.Nuget.SlackClient.Domain.Objects;

namespace Tch.Nuget.SlackClient.Interfaces.Internals
{
   /// <summary>
   /// This interface represents service to CRUD team members
   /// </summary>
   internal interface ICrudMemberService
   {
      Task<IEnumerable<SlackMember>> GetAll(string token);
   }
}