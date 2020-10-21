using NUnit.Framework;
using Tch.Nuget.SlackClient.Domain.Exceptions;
using Tch.SlackClient.IntTests.TestExtensions;

namespace Tch.SlackClient.IntTests.UseCases.CreateChannel
{
   public class UnhappyTests : IntegrationTestBase
   {
      [Test]
      public void Channel_Already_Exists()
      {
         //arrange
         var channelName = "dummy-channel";

         //act+assert
         var e = Assert.ThrowsAsync<SlackChannelAlreadyExists>(() => SUT.CreateChannel(channelName));

         //print
         e.Print();
      }

      [Test]
      public void Bad_Channel_Name()
      {
         //arrange
         var channelName = "ABCD";

         //act+assert
         var e = Assert.ThrowsAsync<SlackClientException>(() => SUT.CreateChannel(channelName));

         //print
         e.Print();
      }
   }
}