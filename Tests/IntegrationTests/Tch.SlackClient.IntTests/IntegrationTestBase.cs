using NUnit.Framework;
using System.Configuration;
using Tch.Nuget.SlackClient.Configuration;
using Tch.Nuget.SlackClient.Interfaces;

namespace Tch.SlackClient.IntTests
{
   [TestFixture]
   public abstract class IntegrationTestBase
   {
      public ISlackClient SUT { get; private set; }

      [SetUp]
      public void SetUp()
      {
         var token = ConfigurationManager.AppSettings["userToken"];
         var clientSettings = new ClientSettings(token);
         SUT = new Nuget.SlackClient.Services.SlackClient(clientSettings);
      }
   }
}