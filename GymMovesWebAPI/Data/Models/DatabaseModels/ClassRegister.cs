using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GymMovesWebAPI.Models.DatabaseModels {
    public class ClassRegister {
        public string StudentUsernameForeignKey { get; set; }
        [ForeignKey("StudentUsernameForeignKey")]
        public Users Student { get; set; }

        public int ClassIdForeignKey { get; set; }
        [ForeignKey("ClassIdForeignKey")]
        public GymClasses Class { get; set; }
    }
}
