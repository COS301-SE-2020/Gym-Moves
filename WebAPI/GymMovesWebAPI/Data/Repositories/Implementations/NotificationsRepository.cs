using GymMovesWebAPI.Data.DatabaseContexts.MainDatabaseContext;
using GymMovesWebAPI.Data.Repositories.Interfaces;
using GymMovesWebAPI.Data.Models.DatabaseModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GymMovesWebAPI.Data.Repositories.Implementations {
    public class NotificationsRepository : INotificationsRepository {
        private readonly MainDatabaseContext context;

        public NotificationsRepository(MainDatabaseContext context) {
            this.context = context;
        }

        public async Task<bool> addNotification(Notifications notification) {
            context.Add(notification);
            return (await context.SaveChangesAsync()) > 0;
        }

        public async Task<Notifications[]> getGymNotifications(int gymId) {
            IQueryable<Notifications> query = context.Notifications;
            query = query.Where(p => p.GymIdForeignKey == gymId);

            return await query.ToArrayAsync();
        }
    }
}
