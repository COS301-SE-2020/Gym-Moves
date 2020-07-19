/*
File Name:
    ClassMappers.cs

Author:
    Longji

Date Created:
    28/06/2020

Update History:
--------------------------------------------------------------------------------
Date          |    Author      |     Changes
--------------------------------------------------------------------------------


Functional Description:
    This file implements a mapper for gym classes.

List of Classes:
    - ClassMappers

 */

using GymMovesWebAPI.Data.Models.DatabaseModels;
using GymMovesWebAPI.Data.Models.RequestModels;
using GymMovesWebAPI.Data.Models.ResponseModels;
using Microsoft.CodeAnalysis.Differencing;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;

namespace GymMovesWebAPI.Data.Mappers {
    public static class ClassMappers {
        public static GymClassResponse classModelToReducedModel(GymClasses source) {
            GymClassResponse target = new GymClassResponse();

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

        public static GymClassResponse[] classModelToReducedModel(GymClasses[] sources) {
            GymClassResponse[] targets = new GymClassResponse[sources.Length];

            for (int i = 0; i < sources.Length; i++) {
                targets[i] = classModelToReducedModel(sources[i]);
            }

            return targets;
        }

        public static GymClasses reducedClassToClassModel(GymClassResponse source) {
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

        public static GymClasses[] reducedClassToClassModel(GymClassResponse[] sources) {
            GymClasses[] targets = new GymClasses[sources.Length];

            for (int i = 0; i < sources.Length; i++) {
                targets[i] = reducedClassToClassModel(sources[i]);
            }

            return targets;
        }

        public static void editRequestToGymClassModel(EditGymClassRequest source, GymClasses target) {
            target.ClassId = source.ClassId;
            target.InstructorUsername = source.InstructorUsername;
            target.Name = source.Name;
            target.Description = source.Description;
            target.Day = source.Day;
            target.StartTime = source.StartTime;
            target.EndTime = source.EndTime;
            target.MaxCapacity = source.MaxCapacity;
            target.Cancelled = source.Cancelled;
        }

        public static EditGymClassRequest classToClassRequestModel(GymClasses source) {
            EditGymClassRequest target = new EditGymClassRequest();

            target.ClassId = source.ClassId;
            target.InstructorUsername = source.InstructorUsername;
            target.Name = source.Name;
            target.Description = source.Description;
            target.Day = source.Day;
            target.StartTime = source.StartTime;
            target.EndTime = source.EndTime;
            target.MaxCapacity = source.MaxCapacity;
            target.Cancelled = source.Cancelled;

            return target;
        }
    }
}
