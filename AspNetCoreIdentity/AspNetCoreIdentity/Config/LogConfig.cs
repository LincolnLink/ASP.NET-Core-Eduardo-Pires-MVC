using KissLog;
using KissLog.AspNetCore;
using KissLog.CloudListeners.Auth;
using KissLog.CloudListeners.RequestLogsListener;
using Microsoft.Extensions.Configuration;
using System;
using System.Diagnostics;
using System.Text;

namespace AspNetCoreIdentity.Config
{
    public class LogConfig
    {
        public static void ConfigureKissLog(IOptionsBuilder options, IConfiguration Configuration)
        {
            // optional KissLog configuration
            options.Options
                .AppendExceptionDetails((Exception ex) =>
                {
                    StringBuilder sb = new StringBuilder();

                    if (ex is System.NullReferenceException nullRefException)
                    {
                        sb.AppendLine("Important: check for null references");
                    }

                    return sb.ToString();
                });

            // KissLog internal logs
            options.InternalLog = (message) =>
            {
                Debug.WriteLine(message);
            };

            RegisterKissLogListeners(options, Configuration);
        }


        public static void RegisterKissLogListeners(IOptionsBuilder options, IConfiguration Configuration)
        {
            // multiple listeners can be registered using options.Listeners.Add() method
 
            // register KissLog.net cloud listener
            options.Listeners.Add(new RequestLogsApiListener(new Application(
                Configuration["KissLog.OrganizationId"],    //  "16bf8aa9-d986-44fc-815a-4a82a8012261"
                Configuration["KissLog.ApplicationId"])     //  "7a7704e7-1a95-42dc-ba20-62249b91a261"
            )
            {
                ApiUrl = Configuration["KissLog.ApiUrl"]    //  "https://api.kisslog.net"
            });
        }
    }
}
