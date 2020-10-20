namespace Tch.Nuget.SlackClient.Domain.Objects
{
   internal class SlackMember
   {
      public string Id { get; set; }

      public string Name { get; set; }

      public bool Is_Bot { get; set; }

      public bool Is_Owner { get; set; }
   }
}