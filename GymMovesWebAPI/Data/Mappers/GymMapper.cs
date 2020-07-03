/*
File Name:
    GymMapper.cs

Author:
    Longji

Date Created:
    02/07/2020

Update History:
--------------------------------------------------------------------------------
Date          |    Author      |     Changes
--------------------------------------------------------------------------------
02/07/2020      Longji          Added getAllGyms function definition


Functional Description:
    

List of Classes:
    - GymMapper
*/

using GymMovesWebAPI.Data.Models.DatabaseModels;
using GymMovesWebAPI.Data.Models.GymModels;

namespace GymMovesWebAPI.Data.Mappers {
    public static class GymMapper {
        public static GymModel mapToGymModel(Gym source) {
            GymModel target = new GymModel();

            target.GymId = source.GymId;
            target.GymName = source.GymName;
            target.GymBranch = source.GymBranch;

            return target;
        }

        public static Gym mapToGym(GymModel source) {
            Gym target = new Gym();

            target.GymId = source.GymId;
            target.GymName = source.GymName;
            target.GymBranch = source.GymBranch;

            return target;
        }

        public static GymModel[] mapToGymModel(Gym[] source) {
            GymModel[] targets = new GymModel[source.Length];

            for (int i = 0; i < source.Length; i++) {
                targets[i] = mapToGymModel(source[i]);
            }

            return targets;
        }

        public static Gym[] mapToGym(GymModel[] source) {
            Gym[] targets = new Gym[source.Length];

            for (int i = 0; i < source.Length; i++) {
                targets[i] = mapToGym(source[i]);
            }

            return targets;
        }
    }
}
