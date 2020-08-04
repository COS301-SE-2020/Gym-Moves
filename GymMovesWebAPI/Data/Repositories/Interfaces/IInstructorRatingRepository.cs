/*
File Name:
    IInstructorRatingRepository.cs

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
    - Provides the interface to be implemented by the repository.

List of Classes:
    - IInstructorRatingRepository
*/


using GymMovesWebAPI.Data.Models.DatabaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GymMovesWebAPI.Data.Repositories.Interfaces {
    public interface IInstructorRatingRepository {
        public Task<bool> addRating(InstructorRating rating);
        public Task<bool> rateClass(InstructorRating rating);
        public Task<InstructorRating> getRating(string username);
    }
}
