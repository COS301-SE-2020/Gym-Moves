using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using GymMovesFunctionApp.Models;
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
        public static async System.Threading.Tasks.Task RunAsync([TimerTrigger("0 */1 * * * *")]TimerInfo myTimer, ILogger log)
        {
            log.LogInformation($"{PROGRESS_PREFIX}Getting Connection String...");
            string connString = Environment.GetEnvironmentVariable("MainDatabase");

            log.LogInformation($"{PROGRESS_PREFIX}Attempting Database Connection...");
            using (SqlConnection connection = new SqlConnection(connString)) {
                if (connection != null) {
                    log.LogInformation("Connected");
                    connection.Open();

                    string date = DateTime.Today.ToString();
                    log.LogInformation(date);

                    string query = "SELECT Heading, Body, GymIdForeignKey FROM dbo.Notifications WHERE Date='" + date + "' ORDER BY GymIdForeignKey ASC;";

                    SqlCommand command = new SqlCommand(query, connection);

                    SqlDataReader reader = command.ExecuteReader();

                    List<NotificationModel> notificationList = new List<NotificationModel>();

                    while (reader.Read()) {
                        NotificationModel newModel = new NotificationModel();

                        newModel.GymId = (int)reader["GymIdForeignKey"];
                        newModel.Heading = reader["Heading"].ToString();
                        newModel.Body = reader["Body"].ToString();

                        notificationList.Add(newModel);
                    }

                    int prevGymId = -1;

                    foreach (NotificationModel model in notificationList) {
                        if (prevGymId == -1 || prevGymId != model.GymId) {
                            prevGymId = model.GymId;
                        }


                    }
                } else {
                    log.LogError($"{ERROR_PREFIX}Database Connection Failed!!!");
                }
            }

            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
        }
    }
}
