using System.Threading.Tasks;
using NUnit.Framework;
using Tch.SlackClient.IntTests.TestExtensions;

namespace Tch.SlackClient.IntTests.UseCases.GetPublicChannels
{
   public class HappyTests : IntegrationTestBase
   {
      [Test]
      public async Task Happy_Case()
      {
         //act
         var result = await SUT.ListPublicChannels();

         //print
         result.Print();
      }
   }
}