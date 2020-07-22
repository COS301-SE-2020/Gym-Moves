import 'package:flutter/material.dart';
import 'package:gym_moves/User/LogIn.dart';
import 'package:gym_moves/Announcement/SendAnnouncement.dart';
import 'dart:async';
import 'package:firebase_messaging/firebase_messaging.dart';
import 'package:http/http.dart' as http;
import 'package:flutter/material.dart';
import 'dart:convert';

void main() {
  runApp(MyAppExample());
}

class Details {
  final String body;
  final String title;

  Details({this.body, this.title});

  factory Details.fromJson(Map<String, dynamic> json) {
    return Details(
        body: json['body'],
        title: json['title'],
    );
  }
}

Future<dynamic> myBackgroundMessageHandler(Map<String, dynamic> message) {
  if (message.containsKey('data')) {
    // Handle data message
    final dynamic data = message['data'];
  }

  if (message.containsKey('notification')) {
    // Handle notification message
    final dynamic notification = message['notification'];
  }
  // Or do other work.
}

class MyAppExample extends StatefulWidget {
  @override
  MyApp createState() => MyApp();
}

class MyApp extends State<MyAppExample> {
  final FirebaseMessaging _firebaseMessaging = FirebaseMessaging();
   String mytitle = "";
   String mybody = "";

/*
Class Name:
  DecodeNotification

Purpose:
  This method deserialises the response received from the api.
 */
   String DecodeNotification(message)
  {
    Details res = Details.fromJson(json.decode(message.body));
     mytitle = res.title;
      mybody = res.body;
      _alertDialog(mytitle, mybody);

  }

/*
Class Name:
  _alertDialog

Purpose:
  This method shows the message as a notification on the user's screen.
 */
  _alertDialog(text1, text2 ) async {
    return showDialog<void>(
      //context: context,
      barrierDismissible: false,
      builder: (BuildContext context) {
        return AlertDialog(
          title: Text(text1),
          content: Text(text2),
          actions: <Widget>[
            FlatButton(
              child: Text(
                'Ok',
                style: TextStyle(color: Color(0xff513369)),
              ),
              onPressed: () {
                Navigator.of(context).pop();
              },
            ),
          ],
        );
      },
    );
  }

  @override
  void initState() {
    super.initState();
    _firebaseMessaging.configure(
      onMessage: (Map<String, dynamic> message) async {
        print("onMessage: $message");
        DecodeNotification(message);
      },
      onLaunch: (Map<String, dynamic> message) async {
        print("onLaunch: $message");
        DecodeNotification(message);
      },
      onResume: (Map<String, dynamic> message) async {
        print("onResume: $message");
        DecodeNotification(message);
      },
    );
  }

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

