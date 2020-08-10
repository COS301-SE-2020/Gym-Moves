/*
File Name:
    ApplicationStatusEnum.cs

Author:
    Longji

Date Created:
    11/08/2020

Update History:
--------------------------------------------------------------------------------
Date          |    Author      |     Changes
--------------------------------------------------------------------------------


Functional Description:
    
          
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GymMovesWebAPI.Data.Enums {
    public enum ApplicationStatus {
        Pending,
        Reviewed,
        Approved,
        Rejected
    }
}
