using System.Threading.Tasks;
using NUnit.Framework;
using Tch.SlackClient.IntTests.TestExtensions;

namespace Tch.SlackClient.IntTests.UseCases.GetTeamMembers
{
   public class HappyTests : IntegrationTestBase
   {
      [Test]
      public async Task Happy_Case()
      {
         //act
         var result = await SUT.GetTeamMembers();

         //print
         result.Print();
      }
   }
}