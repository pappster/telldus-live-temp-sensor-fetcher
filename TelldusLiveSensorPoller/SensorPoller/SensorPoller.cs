using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Wolfberry.TelldusLive;

namespace SensorPoller
{
    public static class SensorPoller
    {
        /// <summary>
        /// Läs av sensorvärden med hjälp av timer.
        /// TimerTrigger-värden: {second} {minute} {hour} {day} {month} {day-of-week}
        /// </summary>
        /// <param name="myTimer"></param>
        /// <param name="log"></param>
        /// <returns></returns>
        [FunctionName("checkTemp")]
        public static async Task RunAsync([TimerTrigger("%TimeTrigger%")] TimerInfo myTimer, ILogger log)
        {
            // Mer information om Telldus Live-biblioteket finns på: https://github.com/wolfberry-ab/telldus-live-dotnet

            var emailSettings = GetEmailSettings();
            var sensorName = Environment.GetEnvironmentVariable("SensorName");

            if (sensorName == null)
            {
                log.LogError("Environment variable SensorName is not set");
                return;
            }

            var telldusClient = CreateTelldusLiveClient();

            try
            {
                var response = await telldusClient.Sensors.GetSensorsAsync(false, true);
                var sensor = response.Sensors.FirstOrDefault(x => sensorName.Equals(x.Name));

                if (sensor == null)
                {
                    log.LogError($"Sensor with name {sensorName} was not found");
                }

                log.LogInformation($"Got sensor: {JsonConvert.SerializeObject(sensor)}");
                Mailer.SendMail(emailSettings, JsonConvert.SerializeObject(sensor));
            }
            catch (Exception e)
            {
                log.LogError(e, $"Failed to request sensor data from Telldus Live API");
                return;
            }

            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
        }

        private static ITelldusLiveClient CreateTelldusLiveClient()
        {
            var consumerKey = Environment.GetEnvironmentVariable("ConsumerKey");
            var consumerKeySecret = Environment.GetEnvironmentVariable("ConsumerKeySecret");
            var accessToken = Environment.GetEnvironmentVariable("AccessToken");
            var accessTokenSecret = Environment.GetEnvironmentVariable("AccessTokenSecret");

            var client = new TelldusLiveClient(
                    consumerKey, consumerKeySecret, accessToken, accessTokenSecret);

            return client;
        }

        private static EmailSettings GetEmailSettings()
        {
            var settings = new EmailSettings
            {
                SmtpServer = Environment.GetEnvironmentVariable("SmtpServer"),
                SenderEmail = Environment.GetEnvironmentVariable("SenderEmail"),
                SenderPassword = Environment.GetEnvironmentVariable("SenderPassword"),
                ReceiverEmail = Environment.GetEnvironmentVariable("ReceiverEmail")
            };

            return settings;
        }
    }
}
