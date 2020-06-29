using System.ComponentModel;

namespace GymMovesWebAPI.Data.Models.ReducedModels {
    public class GymClassReducedModel {
        public int ClassId { get; set; }
        public int GymId { get; set; }
        public string Instructor { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Day { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public int MaxCapacity { get; set; }
        public int CurrentStudents { get; set; }
        [DefaultValue(false)]
        public bool Cancelled { get; set; }
    }
}
