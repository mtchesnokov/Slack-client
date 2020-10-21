using System.Threading.Tasks;
using NUnit.Framework;
using Tch.SlackClient.IntTests.TestExtensions;

namespace Tch.SlackClient.IntTests.UseCases.CreateChannel
{
   public class HappyTests : IntegrationTestBase
   {
      [Test]
      public async Task Happy_Case()
      {
         //arrange
         var channelName = "dummy-channel";

         //act
         var result = await SUT.CreateChannel(channelName);

         //print
         result.Print();
      }
   }
}