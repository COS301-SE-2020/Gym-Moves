/*
File Name:
    GymController.cs

Author:
    Longji

Date Created:
    02/07/2020

Update History:
--------------------------------------------------------------------------------
Date          |    Author      |     Changes
--------------------------------------------------------------------------------
02/07/2020      Longji          Create the GymModel data model

Functional Description:
    

List of Classes:
    - GymModel
*/

namespace GymMovesWebAPI.Data.Models.GymModels {
    public class GymModel {
        public int GymId { get; set; }
        public string GymName { get; set; }
        public string GymBranch { get; set; }
    }
}
