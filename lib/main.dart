import 'package:flutter/material.dart';
import 'package:gym_moves/GymClass/ClassDetails.dart';
import 'package:gym_moves/User/NFC.dart';
import 'package:gym_moves/User/ViewMyProfile.dart';
import 'GymClass/BookClass.dart';
import 'User/LogIn.dart';
import 'Announcement/SendAnnouncement.dart';
import 'package:gym_moves/GymClass/EditClassesInstructor.dart';
import 'package:gym_moves/Rating/ViewMyRatingsInstructor.dart';
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
      home:LogIn(),
    );
  }
}

