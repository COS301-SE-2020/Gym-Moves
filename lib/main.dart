import 'package:flutter/material.dart';
import 'package:gym_moves/Rating/ManagerViewClassRatings.dart';
import 'package:gym_moves/User/LogIn.dart';
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
      home:(ManagerViewClassRatings()),
    );
  }
}

