import 'package:flutter/material.dart';
import 'package:gymmoves/GymClass/ClassDetails.dart';
import 'package:gymmoves/User/NFC.dart';
import 'package:gymmoves/User/ViewMyProfile.dart';
import 'GymClass/BookClass.dart';
import 'User/LogIn.dart';
import 'Announcement/SendAnnouncement.dart';
import 'package:gymmoves/GymClass/ViewMyClassesMember.dart';
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

