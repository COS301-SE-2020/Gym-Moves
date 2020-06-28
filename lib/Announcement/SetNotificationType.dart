/*
File Name:
  SetNotificationType.dart

Author:
  Danel

Date Created:
  28/06/2020

Update History:
--------------------------------------------------------------------------------
Date          |    Author      |     Changes
--------------------------------------------------------------------------------

Functional Description:
  This file implements the screen that will allow members to decide how they
  want to be notified of announcements and cancelled classes.

List of Classes:
 - SetNotificationType
 - SetNotificationTypeState
 - Notifications
 */

import 'dart:convert';

import 'package:flutter/material.dart';
import 'package:flutter_svg/flutter_svg.dart';
import 'package:http/http.dart' as http;

/*
Class Name:
  SetNotificationType

Purpose:
  This class instantiates the state of the class that builds the UI. It ensures
  that the screen keeps state if the user makes any changes with the switches.
 */

class SetNotificationType extends StatefulWidget {
  const SetNotificationType({Key key}) : super(key: key);

  @override
  SetNotificationTypeState createState() => SetNotificationTypeState();
}

/*
Class Name:
  SetNotificationTypeState

Purpose:
  This class builds the UI for members to be able to decide what type of
  notifications they want. It let's users decide between on and off switches.
 */
class SetNotificationTypeState extends State<SetNotificationType> {
  /* These make sure the switches are in the correct position. We need to get
     these values from the API.  */
  bool pushNotifications = true;
  bool emailNotifications = false;
  bool smsNotifications = false;

  /*Url of API*/
  String url = "";

  /*This will be the users username.*/
  String username = "";

  /*
   Method Name:
    build

   Purpose:
     This method builds the UI of the screen. It also builds the switches.
   */
  @override
  Widget build(BuildContext context) {
    MediaQueryData media = MediaQuery.of(context);

    getNotificationValues();

    return Scaffold(
        backgroundColor: const Color(0xff513369),
        body: Column(children: <Widget>[
          Stack(children: <Widget>[
            Container(
                width: media.size.width,
                height: media.size.height * 0.4,
                decoration: BoxDecoration(
                    image: DecorationImage(
                      image: const AssetImage('assets/images/bicycles.jpg'),
                      fit: BoxFit.fill,
                      colorFilter: new ColorFilter.mode(
                          Color(0xff513369).withOpacity(0.6), BlendMode.dstIn),
                    ),
                    boxShadow: [
                      BoxShadow(
                        color: const Color(0x46000000),
                        offset: Offset(0, 3),
                        blurRadius: 6,
                      )
                    ])),
            Transform.translate(
                offset:
                    Offset(0.04 * media.size.width, 0.05 * media.size.height),
                child: GestureDetector(
                  onTap: () {
                    Navigator.pop(context);
                  },
                  child: SvgPicture.string(backButton,
                      allowDrawingOutsideViewBox: true,
                      width: 0.07 * media.size.width),
                ))
          ]),
          SizedBox(
            height: 0.04 * media.size.height,
          ),
          Container(
              padding: EdgeInsets.all(0.02 * media.size.width),
              width: 0.8 * media.size.width,
              child: SwitchListTile(
                title: Text('Push notifications',
                    style: TextStyle(
                        color: Colors.white,
                        fontSize: 0.05 * media.size.width)),
                value: pushNotifications,
                onChanged: (bool value) {
                  setState(() async{
                    await sendToDatabase("push", value);
                  });
                },
                activeColor: Colors.white,
              ),
              decoration: BoxDecoration(
                  color: Color(0x26ffffff),
                  borderRadius: BorderRadius.all(Radius.circular(15.0)))),
          SizedBox(
            height: 0.04 * media.size.height,
          ),
          Container(
              padding: EdgeInsets.all(0.02 * media.size.width),
              width: 0.8 * media.size.width,
              child: SwitchListTile(
                title: Text('Email notifications',
                    style: TextStyle(
                        color: Colors.white,
                        fontSize: 0.05 * media.size.width)),
                value: emailNotifications,
                onChanged: (bool value) {
                  setState(() async {
                    emailNotifications = await sendToDatabase("email", value);
                  });
                },
                activeColor: Colors.white,
              ),
              decoration: BoxDecoration(
                  color: Color(0x26ffffff),
                  borderRadius: BorderRadius.all(Radius.circular(15.0)))),
          SizedBox(
            height: 0.04 * media.size.height,
          ),
          Container(
              padding: EdgeInsets.all(0.02 * media.size.width),
              width: 0.8 * media.size.width,
              child: SwitchListTile(
                title: Text('SMS notifications',
                    style: TextStyle(
                        color: Colors.white,
                        fontSize: 0.05 * media.size.width)),
                value: smsNotifications,
                onChanged: (bool value) {
                  setState(() async {
                    smsNotifications = await sendToDatabase("sms", value);
                  });
                },
                activeColor: Colors.white,
              ),
              decoration: BoxDecoration(
                  color: Color(0x26ffffff),
                  borderRadius: BorderRadius.all(Radius.circular(15.0)))),
        ]));
  }

  /*
   Method Name:
    sendToDatabase

   Purpose:
     This method updates the changed notification to the database.
   */
  Future<bool> sendToDatabase(String setting, bool value) async {
     final http.Response response = await http.post(
       url,
      headers: <String, String>{
        'Content-Type': 'application/json; charset=UTF-8',
      },
      body: jsonEncode(<String, String>{
        setting: value.toString(),
         }),
     );

     if(response.statusCode != 200){
    _errorDialogue(
        "We could not update your information. Please try again later.");
     }

    return !value;
  }

  /*
   Method Name:
    getNotificationValues

   Purpose:
     This method gets what the members current notification preferences are.
   */
  getNotificationValues() async {
    final http.Response response = await http.post(
      url,
      headers: <String, String>{
        'Content-Type': 'application/json; charset=UTF-8',
      },
      body: jsonEncode(<String, String>{
        'username': username,
      }),
    );

    if (response.statusCode != 200) {
      _errorDialogue(
          "We could not get any of your information. Please try again later.");
      Navigator.pop(context);
    } else {
      Notifications notifications =
          Notifications.fromJson(json.decode(response.body));

      smsNotifications = notifications.sms;
      pushNotifications = notifications.push;
      emailNotifications = notifications.email;
    }
  }

  /*
   Method Name:
    _errorDialogue

   Purpose:
     This method shows a dialogue if there was an error with getting or sending
     information from or to the database.
   */
  _errorDialogue(text) async {
    return showDialog<void>(
      context: context,
      barrierDismissible: false,
      builder: (BuildContext context) {
        return AlertDialog(
          title: Text('Oh no!'),
          content: Text(text),
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
}


/*
Class Name:
  Notifications

Purpose:
  This class helps with the return of the notifications and their results
  from the database.
 */
class Notifications {
  final bool push;
  final bool email;
  final bool sms;

  Notifications({this.push, this.email, this.sms});

  factory Notifications.fromJson(Map<String, dynamic> json) {
    return Notifications(
      push: json['push'],
      email: json['email'],
      sms: json['sms'],
    );
  }
}

const String backButton =
    '<svg viewBox="28.2 38.0 31.4 27.9" ><path transform="matrix(-1.0, 0.0, 0.0, -1.0, 65.61, 71.93)" d="M 21.68118286132813 6 L 18.91737365722656 8.460894584655762 L 29.85499572753906 18.21720886230469 L 6 18.21720886230469 L 6 21.70783996582031 L 29.85499572753906 21.70783996582031 L 18.91737365722656 31.46415710449219 L 21.68118286132813 33.925048828125 L 37.36236572265625 19.9625244140625 L 21.68118286132813 6 Z" fill="#fcfbfc" stroke="none" stroke-width="1" stroke-miterlimit="4" stroke-linecap="butt" /></svg>';
