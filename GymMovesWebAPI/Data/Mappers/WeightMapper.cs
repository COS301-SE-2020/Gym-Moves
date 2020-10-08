using GymMovesWebAPI.Data.Models.DatabaseModels;
using GymMovesWebAPI.Data.Models.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GymMovesWebAPI.Data.Mappers {
    public static class WeightMapper {
        public static WeightResponse minimize(WeightData source) {
            WeightResponse target = new WeightResponse();

            target.Date = source.Date;
            target.Height = source.Height;
            target.Weight = source.Weight;

            return target;
        }
    }
}
