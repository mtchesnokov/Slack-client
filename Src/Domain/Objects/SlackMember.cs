namespace Tch.Nuget.SlackClient.Domain.Objects
{
   public class SlackMember
   {
      public string Id { get; set; }

      public string Name { get; set; }

      public string Real_Name { get; set; }

      public bool Is_Bot { get; set; }

      public bool Is_Owner { get; set; }
   }
}