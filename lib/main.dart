import 'package:flutter/material.dart';
import 'package:gym_moves/GymClass/ManagerViewClasses.dart';
import 'package:gym_moves/User/LogIn.dart';
import 'package:gym_moves/User/ViewMyProfile.dart';


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
      home:(LogIn()),
    );
  }
}

