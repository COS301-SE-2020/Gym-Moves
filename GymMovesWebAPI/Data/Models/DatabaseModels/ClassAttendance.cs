/*
File Name:
    ClassAttendance.cs

Author:
    Longji

Date Created:
    15/08/2020

Update History:
--------------------------------------------------------------------------------
Date          |    Author      |     Changes
--------------------------------------------------------------------------------

Functional Description:
    

List of Classes:
    - ClassAttendance
*/

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GymMovesWebAPI.Data.Models.DatabaseModels {
    public class ClassAttendance {
        public int ClassId { get; set; }
        [ForeignKey("ClassId")]
        public GymClasses Class { get; set; }
        public DateTime Date { get; set; }
        public int Capacity { get; set; }
        public int NumberOfStudents { get; set; }
    }
}
