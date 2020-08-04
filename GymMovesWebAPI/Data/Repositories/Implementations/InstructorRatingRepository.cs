/*
File Name:
    InstructorRatingRepository.cs

Author:
    Longji

Date Created:
    04/08/2020

Update History:
--------------------------------------------------------------------------------
Date          |    Author      |     Changes
--------------------------------------------------------------------------------
04/08/2020    |  Longji        |  Created the interface
--------------------------------------------------------------------------------

Functional Description:
    - Implements the interface defined by IInstructorRatingRepository. The functions
      implemented perform the necessary interactions with the database to update
      ratings for instructors.

List of Classes:
    - InstructorRatingRepository
*/


using GymMovesWebAPI.Data.DatabaseContexts.MainDatabaseContext;
using GymMovesWebAPI.Data.Models.DatabaseModels;
using GymMovesWebAPI.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GymMovesWebAPI.Data.Repositories.Implementations {
    public class InstructorRatingRepository : IInstructorRatingRepository {
        private readonly MainDatabaseContext context;

        public InstructorRatingRepository(MainDatabaseContext context) {
            this.context = context;
        }

        public async Task<bool> addRating(InstructorRating rating) {
            context.Add(rating);
            return (await context.SaveChangesAsync()) > 0;
        }

        public async Task<InstructorRating> getRating(string username) {
            IQueryable<InstructorRating> query = context.InstructorRatings;
            query = query.Where(p => p.InstructorUsernameForeignKey == username);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<bool> rateClass(InstructorRating rating) {
            context.Update(rating);
            return (await context.SaveChangesAsync()) > 0;
        }
    }
}
