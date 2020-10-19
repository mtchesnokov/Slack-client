namespace Tch.Nuget.SlackClient.Domain
{
   internal abstract class SlackHttpResponseBase
   {
      public bool Ok { get; set; }

      public string Error { get; set; }
   }
}