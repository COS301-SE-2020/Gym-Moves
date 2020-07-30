/*
File Name:
    ClassRatingRepository.cs

Author:
    Longji

Date Created:
    30/07/2020

Update History:
--------------------------------------------------------------------------------
Date          |    Author      |     Changes
--------------------------------------------------------------------------------


Functional Description:
    

List of Classes:
    - ClassRatingRepository
*/


using GymMovesWebAPI.Data.DatabaseContexts.MainDatabaseContext;
using GymMovesWebAPI.Data.Models.DatabaseModels;
using GymMovesWebAPI.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GymMovesWebAPI.Data.Repositories.Implementations
{
    public class ClassRatingRepository : IClassRatingRepository {
        private readonly MainDatabaseContext context;

        public ClassRatingRepository(MainDatabaseContext context) {
            this.context = context;
        }

        public async Task<bool> addRating(ClassRating rating) {
            context.Add(rating);
            return (await context.SaveChangesAsync()) > 0;
        }

        public async Task<ClassRating> getRating(int classid) {
            IQueryable<ClassRating> query = context.ClassRatings;
            query = query.Where(p => p.ClassIdForeignKey == classid);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<bool> rateClass(ClassRating rating) {
            context.Update(rating);
            return (await context.SaveChangesAsync()) > 0;
        }
    }
}
