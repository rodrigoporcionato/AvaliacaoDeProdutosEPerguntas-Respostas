using System;
using System.Collections.Generic;

namespace BetaViews.Core.Framework
{
   public static class LogFile
   {

       public static string EnableDebug {

           get {

               return !string.IsNullOrEmpty(System.Configuration.ConfigurationManager.AppSettings["EnableDebug"]) ? System.Configuration.ConfigurationManager.AppSettings["EnableDebug"] : "";
           }
       }

       public static string DebugPath { 
           get 
           {
                return !string.IsNullOrEmpty(System.Configuration.ConfigurationManager.AppSettings["DebugPath"]) ? System.Configuration.ConfigurationManager.AppSettings["DebugPath"] : "";
            } 
       }

      
       public  static void WriteLog(string outputMessage, string eventLocation)
       {
           

           var eventLog = new
           {
               date = DateTime.Now,
               EventLocation = eventLocation,
               DescriptionLog = outputMessage
           };
           var output = new List<string>() { string.Format("Data:{0}{1}Local do evento:{2}{3}Descrição do erro:{4}", 
               eventLog.date,  Environment.NewLine,
               eventLog.EventLocation, Environment.NewLine,
               eventLog.DescriptionLog) };

           var debugEnable = EnableDebug.Equals("true") ? true : false;
           
           if (debugEnable)
               System.IO.File.AppendAllLines(DebugPath, output);
       
       }

   }
}
