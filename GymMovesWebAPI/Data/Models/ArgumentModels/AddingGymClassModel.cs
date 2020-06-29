using GymMovesWebAPI.Data.Models.ReducedModels;

namespace GymMovesWebAPI.Data.Models.ArgumentModels {
    public class AddingGymClassModel {
        /* Who made the request */
        public string Username { get; set; }
        public GymClassReducedModel NewClass { get; set; }
    }
}
