/*
File Name:
    NotificationSettingsRepository.cs

Author:
    Danel

Date Created:
    29/06/2020

Update History:
--------------------------------------------------------------------------------
Date          |    Author      |     Changes
--------------------------------------------------------------------------------


Functional Description:
    This file implements the interface for the notification settings.

List of Classes:
    - NotificationSettingsRepository

 */

using GymMovesWebAPI.Data.DatabaseContexts.MainDatabaseContext;
using GymMovesWebAPI.Data.Models.DatabaseModels;
using GymMovesWebAPI.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace GymMovesWebAPI.Data.Repositories.Implementations{

    /*
    Class Name:
        NotificationSettingsRepository

    Purpose:
        This class handles the database interactions with the notification
        settings table.
    */
    public class NotificationSettingsRepository : INotificationSettingsRepository {
        
        private readonly MainDatabaseContext context;

        /*
        Method Name:
            NotificationSettingsRepository
        Purpose:
            This method instantiates the database context.
        */
        public NotificationSettingsRepository(MainDatabaseContext context) {
            this.context = context;
        }

        /*
        Method Name:
            addUser
        Purpose:
            Adds a new user to the notification settings table.
        */
        public async Task<bool> addUser(NotificationSettings user){
            
            context.Add(user);
           
            return (await context.SaveChangesAsync()) > 0;
        }


        /*
        Method Name:
            changeSetting
        Purpose:
            This changes the settings of the notifications.
        */
        public async Task<bool> changeSetting(string username, bool email, bool sms, bool push) {

            var user = context.NotificationSettings.First(a => a.UsernameForeignKey == username);

            user.Sms = sms;
            user.PushNotifications = push;
            user.Email = email;

            return (await context.SaveChangesAsync()) > 0;
        }

        public async Task<NotificationSettings> getSettingsOfUser(string username)
        {
            IQueryable<NotificationSettings> query = context.NotificationSettings;
            query = query.Where(p => p.UsernameForeignKey == username);

            return await query.FirstOrDefaultAsync();
        }

    }
}
