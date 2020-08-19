/*
File Name:
    IGymApplicationRepository.cs

Author:
    Longji

Date Created:
    10/08/2020

Update History:
--------------------------------------------------------------------------------
Date          |    Author      |     Changes
--------------------------------------------------------------------------------
10/08/2020    |  Longji        | Created the interface
--------------------------------------------------------------------------------


Functional Description:
    

List of Classes:
    - IGymApplicationRepository
*/

using GymMovesWebAPI.Data.Models.DatabaseModels;
using GymMovesWebAPI.Migrations;
using System.Threading.Tasks;

namespace GymMovesWebAPI.Data.Repositories.Interfaces {
    public interface IGymApplicationRepository {
        public Task<bool> addApplication(GymApplications application);
        public Task<bool> removeApplication(GymApplications application);
        public Task<GymApplications[]> getApplication(string gymName, string gymBranch = "");
        public Task<GymApplications[]> getAllApplications();
        public Task<bool> updateApplication(GymApplications application);
    }
}
