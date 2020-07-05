/*
File Name:
    INotificationSettingsRepository.cs

Author:
    Danel

Date Created:
    29/06/2020

Update History:
--------------------------------------------------------------------------------
Date          |    Author      |     Changes
--------------------------------------------------------------------------------


Functional Description:
    This file implements interface for the notification setting repository.

List of Interfaces:
    - INotificationSettingsRepository

 */

using GymMovesWebAPI.Data.Models.DatabaseModels;
using System.Threading.Tasks;

namespace GymMovesWebAPI.Data.Repositories.Interfaces {

    /*
    Interface Name:
        INotificationSettingsRepository

    Purpose:
        This class is just the interface for the repo.
    */
    public interface INotificationSettingsRepository {
        public Task<bool> addUser(NotificationSettings user);
        public Task<bool> changeSetting(string username, bool email, bool sms, bool push);
        public Task<NotificationSettings> getSettingsOfUser(string username);
    }
}
