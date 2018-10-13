using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace twilioIntegration
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new  ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional:true, reloadOnChange:true )
            .AddJsonFile($"appsettings.local.json", optional: true) // for storing actual information
            ;
            IConfigurationRoot configuration = builder.Build();
            string Account = (configuration.GetSection("TwilioSettings:Account").Value);
             string Token = (configuration.GetSection("TwilioSettings:Token").Value);
            string username = (configuration.GetSection("TwilioSettings:Username").Value);
            string password = (configuration.GetSection("TwilioSettings:Password").Value);
             string phoneNumber = (configuration.GetSection("TwilioSettings:DefaultNumber").Value);
           TwilioClient.Init(Account,Token);
            var from =  new Twilio.Types.PhoneNumber(phoneNumber);
            var to = new PhoneNumber("+19725239489");
            string MessageBody = "Join Earth's mightiest heroes. Like Kevin Bacon.";

            var message = MessageResource.Create(body: "Join Earth's mightiest heroes. Like Kevin Bacon.",from: from,to: to );
            Console.WriteLine($"{message.Sid}  - {message.Status}");
            var call = CallResource.Create(to, from,url: new Uri("http://demo.twilio.com/docs/voice.xml"));
            Console.WriteLine($"{call.Sid}  - {call.Status}");
        }
    }
}
