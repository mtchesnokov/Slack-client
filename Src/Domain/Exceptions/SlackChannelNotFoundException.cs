namespace Tch.Nuget.SlackClient.Domain.Exceptions
{
   public class SlackChannelNotFoundException : SlackClientException
   {
      public string ChannelName { get; set; }

      public SlackChannelNotFoundException() : base("Channel has not been found")
      {
      }
   }
}