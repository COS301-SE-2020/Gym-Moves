using GymMovesWebAPI.Data.Models.DatabaseModels;
using System.Threading.Tasks;

namespace GymMovesWebAPI.Data.Repositories.Interfaces {
    public interface INotificationsRepository {
        public Task<bool> addNotification(Notifications notification);
        public Task<Notifications[]> getGymNotifications(int gymId);
    }
}
