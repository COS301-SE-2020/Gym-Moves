using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using GymMovesFunctionApp.Mailer;
using GymMovesFunctionApp.Models;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace GymMovesFunctionApp {
    public static class NotificationsSender {
        private static readonly string PROGRESS_PREFIX = "[PROGRESS] - ";
        private static readonly string ERROR_PREFIX = "[ERROR] - ";

        [FunctionName("NotificationsFunction")]
        public static async System.Threading.Tasks.Task RunAsync([TimerTrigger("0 */1 * * * *")] TimerInfo myTimer, ILogger log) {
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

                    reader.Close();

                    int prevGymId = -1;
                    List<MemberModel> memberList = new List<MemberModel>();

                    foreach (NotificationModel notification in notificationList) {
                        if (prevGymId == -1 || prevGymId != notification.GymId) {
                            log.LogInformation("Retrieving members...");
                            prevGymId = notification.GymId;

                            string memberQuery = "SELECT Username, Name, Surname, dbo.Users.Email, dbo.NotificationSettings.Email AS ReceiveEmail, dbo.NotificationSettings.PushNotifications FROM dbo.Users JOIN dbo.NotificationSettings ON Username = UsernameForeignKey WHERE GymIdForeignKey=" + notification.GymId + ";";

                            SqlCommand memberCommand = new SqlCommand(memberQuery, connection);

                            SqlDataReader memberReader = memberCommand.ExecuteReader();

                            memberList.Clear();

                            while (memberReader.Read()) {
                                MemberModel newModel = new MemberModel();

                                newModel.Username = memberReader["Username"].ToString();
                                newModel.Name = memberReader["Name"].ToString();
                                newModel.Surname = memberReader["Surname"].ToString();
                                newModel.Email = memberReader["Email"].ToString();
                                newModel.EmailEnabled = (bool)memberReader["ReceiveEmail"];
                                newModel.PushEnabled = (bool)memberReader["PushNotifications"];

                                memberList.Add(newModel);
                            }

                            memberReader.Close();
                        }

                        foreach (MemberModel member in memberList) {
                            sendEmail(member, notification, log);
                        }
                    }
                } else {
                    log.LogError($"{ERROR_PREFIX}Database Connection Failed!!!");
                }
            }

            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
        }

        public static async Task sendEmail(MemberModel member, NotificationModel notification, ILogger log) {
            await Mailer.Mailer.sendEmailAsync(member.Email, notification.Heading, notification.Body, member.Name, member.Surname, log);
        }
    }
}
