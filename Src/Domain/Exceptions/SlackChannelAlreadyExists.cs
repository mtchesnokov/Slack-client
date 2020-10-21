namespace Tch.Nuget.SlackClient.Domain.Exceptions
{
   public class SlackChannelAlreadyExists : SlackClientException
   {
      public string ChannelName { get; set; }

      public SlackChannelAlreadyExists() : base("Channel already exists")
      {
      }
   }
}