import 'package:flutter/material.dart';
import 'User/SignUp.dart';
import 'User/ForgotPassword.dart';
import 'Announcement/SendAnnouncement.dart';
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
      home:SignUp(),
    );
  }
}

