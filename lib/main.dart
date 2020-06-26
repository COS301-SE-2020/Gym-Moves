import 'package:flutter/material.dart';

import 'package:gym_moves/GymClass/ClassDetails.dart';

import 'Announcement/SendAnnouncement.dart';

import 'Rating/ManagerViewClassRatings.dart';
import 'Rating/ManagerViewInstructorRatings.dart';
import 'Rating/InstructorViewMyRatings.dart';

import 'User/LogIn.dart';
import 'User/SignUp.dart';
import 'User/ViewMyProfile.dart';
import 'package:gym_moves/User/ManagerPages.dart';
import 'package:gym_moves/User/InstructorPages.dart';

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
      home:(InstructorPages()),
    );
  }
}

