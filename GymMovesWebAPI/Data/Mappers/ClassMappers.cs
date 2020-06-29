using GymMovesWebAPI.Data.Models.ReducedModels;
using GymMovesWebAPI.Models.DatabaseModels;

namespace GymMovesWebAPI.Data.Mappers {
    public static class ClassMappers {
        public static GymClassReducedModel classModelToReducedModel(GymClasses source) {
            GymClassReducedModel target = new GymClassReducedModel();

            target.ClassId = source.ClassId;
            target.GymId = source.GymIdForeignKey;
            target.Instructor = source.InstructorUsername;
            target.Name = source.Name;
            target.Description = source.Description;
            target.Day = source.Day;
            target.StartTime = source.StartTime;
            target.EndTime = source.EndTime;
            target.MaxCapacity = source.MaxCapacity;
            target.CurrentStudents = source.CurrentStudents;
            target.Cancelled = source.Cancelled;

            return target;
        }

        public static GymClassReducedModel[] classModelToReducedModel(GymClasses[] sources) {
            GymClassReducedModel[] targets = new GymClassReducedModel[sources.Length];

            for (int i = 0; i < sources.Length; i++) {
                targets[i] = classModelToReducedModel(sources[i]);
            }

            return targets;
        }

        public static GymClasses reducedClassToClassModel(GymClassReducedModel source) {
            GymClasses target = new GymClasses();

            target.ClassId = source.ClassId;
            target.GymIdForeignKey = source.GymId;
            target.InstructorUsername = source.Instructor;
            target.Name = source.Name;
            target.Description = source.Description;
            target.Day = source.Day;
            target.StartTime = source.StartTime;
            target.EndTime = source.EndTime;
            target.MaxCapacity = source.MaxCapacity;
            target.CurrentStudents = source.CurrentStudents;
            target.Cancelled = source.Cancelled;

            return target;
        }

        public static GymClasses[] reducedClassToClassModel(GymClassReducedModel[] sources) {
            GymClasses[] targets = new GymClasses[sources.Length];

            for (int i = 0; i < sources.Length; i++) {
                targets[i] = reducedClassToClassModel(sources[i]);
            }

            return targets;
        }
    }
}
