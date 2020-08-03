using System;
using System.Data.SqlClient;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace GymMovesFunctionApp
{
    public static class NotificationsSender
    {
        private static readonly string PROGRESS_PREFIX = "[PROGRESS] - ";
        private static readonly string ERROR_PREFIX = "[ERROR] - ";

        [FunctionName("NotificationsFunction")]
        public static void Run([TimerTrigger("0 */1 * * * *")]TimerInfo myTimer, ILogger log)
        {
            log.LogInformation($"{PROGRESS_PREFIX}Getting Connection String...");
            string connString = Environment.GetEnvironmentVariable("MainDatabase");

            log.LogInformation($"{PROGRESS_PREFIX}Attempting Database Connection...");
            using (SqlConnection connection = new SqlConnection(connString)) {
                if (connection != null) {
                    /* Get All Notifications */
                } else {
                    log.LogError($"{ERROR_PREFIX}Database Connection Failed!!!");
                }
            }

            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
        }
    }
}
