import 'package:flutter/material.dart';

import 'package:gym_moves/Announcement/SendAnnouncement.dart';

import 'package:gym_moves/Rating/ManagerViewClassRatings.dart';
import 'package:gym_moves/Rating/ManagerViewInstructorRatings.dart';
import 'package:gym_moves/Rating/InstructorViewMyRatings.dart';

import 'package:gym_moves/User/LogIn.dart';
import 'package:gym_moves/User/SignUp.dart';
import 'package:gym_moves/User/ViewMyProfile.dart';
import 'package:gym_moves/User/ManagerPages.dart';
import 'package:gym_moves/User/InstructorPages.dart';
import 'package:gym_moves/User/MemberPages.dart';

import 'package:gym_moves/GymClass/ClassDetails.dart';
import 'package:gym_moves/GymClass/EditClassesInstructor.dart';
import 'package:gym_moves/GymClass/EditClassesManager.dart';
import 'package:gym_moves/GymClass/EditClass.dart';
import 'package:gym_moves/GymClass/AddAClass.dart';
import 'package:gym_moves/GymClass/ViewAllClassesMember.dart';
import 'package:gym_moves/GymClass/ViewMyClassesMember.dart';

void main() {
  runApp(MyApp());
}

class MyApp extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      title: 'Gym Moves',
      theme: ThemeData(

        visualDensity: VisualDensity.adaptivePlatformDensity,
      ),
      home:(InstructorViewMyRatings()),
    );
  }
}

