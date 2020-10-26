namespace Tch.Nuget.SlackClient.Domain.Exceptions
{
   public class SlackChannelAlreadyExistsException : SlackClientException
   {
      public string ChannelName { get; set; }

      public SlackChannelAlreadyExistsException() : base("Channel already exists")
      {
      }
   }
}