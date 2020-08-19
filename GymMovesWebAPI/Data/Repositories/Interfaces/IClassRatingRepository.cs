/*
File Name:
    IClassRatingRepository.cs

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
    - IClassRatingRepository
*/

using GymMovesWebAPI.Data.Models.DatabaseModels;
using System.Threading.Tasks;

namespace GymMovesWebAPI.Data.Repositories.Interfaces
{
    public interface IClassRatingRepository {
        public Task<bool> addRating(ClassRating rating);
        public Task<bool> rateClass(ClassRating rating);
        public Task<ClassRating> getRating(int classid);
    }
}
