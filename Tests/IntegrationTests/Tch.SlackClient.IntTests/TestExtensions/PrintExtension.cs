using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Tch.SlackClient.IntTests.TestExtensions
{
   internal static class ToDictionaryExtension
   {
      private static List<string> GeneralExceptionPropertyNames
      {
         get
         {
            return typeof(Exception).GetProperties(BindingFlags.Instance | BindingFlags.Public)
               .Select(x => x.Name).ToList();
         }
      }

      private static JsonSerializerSettings JsonSettings => new JsonSerializerSettings
      {
         ReferenceLoopHandling = ReferenceLoopHandling.Ignore
      };

      /// <summary>
      ///    Converts exception to dictionary which can them be serialized to error log/response
      /// </summary>
      public static Dictionary<string, string> ToDictionary(this Exception exception)
      {
         var result = new Dictionary<string, string>
         {
            {"ErrorType", exception.GetType().FullName},
            {"Message", exception.Message}
         };

         if (exception.GetType() != typeof(Exception))
         {
            AddAdditionalData(result, exception);
         }

         return result;
      }

      private static void AddAdditionalData(IDictionary<string, string> result, Exception exception)
      {
         var exceptionProperties2Log = GetExceptionProperties2Log(exception);

         foreach (var prop in exceptionProperties2Log)
         {
            try
            {
               var value = prop.GetValue(exception);

               string keyValue;

               if (value is string)
               {
                  keyValue = (string)value;
               }
               else
               {
                  keyValue = JsonConvert.SerializeObject(value, JsonSettings);
               }

               var key = prop.Name;

               if (result.ContainsKey(key))
               {
                  continue;
               }

               result.Add(key, keyValue);
            }
            catch (Exception e)
            {
               Debug.WriteLine(e.Message);
               //if we cannot serialize exception property leave it. continue with processing
            }
         }
      }

      private static IEnumerable<PropertyInfo> GetExceptionProperties2Log(Exception exception)
      {
         return exception.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public)
            .Where(p => !GeneralExceptionPropertyNames.Contains(p.Name))
            .Where(p => p.PropertyType != typeof(Task))
            .Where(p => p.PropertyType != typeof(CancellationToken));
      }
   }

   public static class PrintExtension
   {
      public static void Print(this object o)
      {
         var obj2print = o;

         if (o is Exception) //printing exception to console is ugly. we print only good stuff
         {
            obj2print = ((Exception)o).ToDictionary();
         }

         var settings = new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore };
         var text = JsonConvert.SerializeObject(obj2print, Formatting.Indented, settings);
         Console.WriteLine(text);
      }
   }
}