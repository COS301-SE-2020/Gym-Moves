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

import 'package:auto_size_text/auto_size_text.dart';
import 'package:flutter/material.dart';
import 'package:flutter_svg/flutter_svg.dart';
import 'package:http/http.dart' as http;
import 'package:shared_preferences/shared_preferences.dart';

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
  bool pushNotifications = false;
  bool pushNotificationsChanged = false;
  bool emailNotifications = false;
  bool emailNotificationsChanged = false;

  String url = "https://gymmoveswebapi.azurewebsites.net/api/notifications/";
  String username = "";
  Future loadSettingsFuture;

  @override
  void initState() {
    super.initState();
    loadSettingsFuture = getNotificationValues();
  }

  /*
   Method Name:
    build

   Purpose:
     This method builds the UI of the screen. It also builds the switches.
   */
  @override
  Widget build(BuildContext context) {
    MediaQueryData media = MediaQuery.of(context);

    return new FutureBuilder(
        future: loadSettingsFuture,
        builder: (BuildContext context, AsyncSnapshot snapshot) {
          if (snapshot.connectionState == ConnectionState.done) {
            return Scaffold(
                backgroundColor: const Color(0xffffffff),
                body: Column(
                    children: <Widget>[
                      Stack(
                          children: <Widget>[
                            Container(
                                width: media.size.width,
                                height: media.size.height * 0.4,
                                decoration: BoxDecoration(
                                    image: DecorationImage(
                                      image: const AssetImage(
                                          'assets/notifications.png'),
                                      fit: BoxFit.fill,
                                    ),
                                    
                                )
                            ),
                            Transform.translate(
                                offset: Offset(
                                    0.04 * media.size.width, 0.05 *
                                    media.size.height
                                ),
                                child: GestureDetector(
                                    onTap: () {
                                      Navigator.pop(context);
                                      },
                                    child: SvgPicture.string(
                                        backArrow,
                                        allowDrawingOutsideViewBox: true,
                                        width: 0.06 * media.size.width
                                    )
                                )
                            ),
                            Container(
                      width: media.size.width,
                      height: 0.4 * media.size.height,
                      child: Center(
                          child: Container(
                              width: 0.48 * media.size.width,
                              height: 0.4 * 0.65 * media.size.height,
                              child:     Transform.translate(
                      offset: Offset(0.3 * 0.82 * media.size.width,
                          0.08 * 0.55 * media.size.height
                      ),
                      child:Center(
                                  child: AutoSizeText(
                                    "Notification \n Settings",
                                    style: TextStyle(
                                      fontFamily: 'Last',
                                      fontSize: media.size.width * 0.5,
                                      color: const Color(0xff3E3E3E),
                                      ),
                                    maxLines: 2,
                                    textAlign: TextAlign.left
                                  )
                              ))
                          )
                      )
                  )
                          ]
                      ),
                      SizedBox(
                        height: 0.1 * media.size.height,
                      ),
                      getPush(media),
                      SizedBox(
                        height: 0.04 * media.size.height,
                      ),
                      getEmail(media),
                      SizedBox(
                        height: 0.04 * media.size.height,
                      ),
                      Container(
                          padding: EdgeInsets.all(0.02 * media.size.width),
                          child: FlatButton(
                            shape: RoundedRectangleBorder(
                                borderRadius: BorderRadius.circular(10.0)
                            ),
                            color: const Color(0xff7341E6),
                            onPressed: () {
                              changeSettings();
                              },
                            textColor: Colors.white,
                            padding: const EdgeInsets.all(0.0),
                            child: Container(
                              width : 0.4 * media.size.width,
                              padding: const EdgeInsets.all(10.0),
                              child: Text(
                                'Submit',
                                style: TextStyle(
                                    fontSize: 0.04 * media.size.width,
                                    fontFamily: 'Roboto',
                                ),
                                textAlign: TextAlign.center ,
                              )
                            )
                          )
                      )
                    ]
                )
            );
          }
          else {
            return Scaffold(
                backgroundColor: const Color(0xff513369),
                body: Column(
                    children: <Widget>[
                      Stack(
                          children: <Widget>[
                            Container(
                              width: media.size.width,
                              height: media.size.height * 0.4,
                              decoration: BoxDecoration(
                                  image: DecorationImage(
                                    image: const AssetImage(
                                        'assets/notifications.png'
                                    ),
                                    fit: BoxFit.fill,
                                  ),
                              ),
                            ),
                            Container(
                                alignment: Alignment.center,
                                width: media.size.width,
                                height: media.size.height * 0.4,
                                child:Text('Loading your settings..',
                                    style: TextStyle(
                                        color: Colors.white, fontSize: 0.05 *
                                        media.size.width
                                    )
                                )
                            ),
                            Transform.translate(
                                offset: Offset(
                                    0.04 * media.size.width, 0.05 *
                                    media.size.height
                                ),
                                child: GestureDetector(
                                  onTap: () {
                                    Navigator.pop(context);
                                    },
                                  child: SvgPicture.string(
                                      backArrow,
                                      allowDrawingOutsideViewBox: true,
                                      width: 0.06 * media.size.width
                                  )
                                )
                            ),
                            
                          ]
                      ),
                      SizedBox(
                        height: 0.04 * media.size.height,
                      ),
                      getPush(media),
                      SizedBox(
                        height: 0.04 * media.size.height,
                      ),
                      getEmail(media),
                      SizedBox(
                        height: 0.04 * media.size.height,
                      ),
                      Container(
                          padding: EdgeInsets.all(0.02 * media.size.width),
                          child: FlatButton(
                            shape: RoundedRectangleBorder(
                                borderRadius: BorderRadius.circular(10.0)
                            ),
                            color: const Color(0xff7341E6),
                            onPressed: () {
                              changeSettings();
                              },
                            textColor: const Color(0xff787878),
                            padding: const EdgeInsets.all(0.0),
                            child: Container(
                              padding: const EdgeInsets.all(10.0),
                              child: Text(
                                'Submit',
                                style: TextStyle(
                                    fontSize: 0.04 * media.size.width,
                                    fontFamily: 'Roboto'
                                )
                              )
                            )
                          )
                      )
                    ]
                )
            );
          }}
        );
  }

  Widget getPush(MediaQueryData media) {
    return Container(
        padding: EdgeInsets.all(0.02 * media.size.width),
        width: 0.8 * media.size.width,
        child: SwitchListTile(
          title: Text(
              'Push notifications',
              style: TextStyle(
                  color: const Color(0xff3E3E3E), fontSize: 0.05 * media.size.width
              )
          ),
          value: pushNotifications,
          onChanged: (bool value) {
            setState(() {
              pushNotifications = value;
              pushNotificationsChanged = !pushNotificationsChanged;
            });
          },
          activeColor: const Color(0xff7341E6),
        ),
        decoration: BoxDecoration(
            color: Color(0x267341E6),
            borderRadius: BorderRadius.all(Radius.circular(15.0)
            )
        )
    );
  }

  Widget getEmail(MediaQueryData media) {
    return Container(
        padding: EdgeInsets.all(0.02 * media.size.width),
        width: 0.8 * media.size.width,
        child: SwitchListTile(
          title: Text(
              'Email notifications',
              style: TextStyle(
                  color: const Color(0xff3E3E3E), fontSize: 0.05 * media.size.width
              )
          ),
          value: emailNotifications,
          onChanged: (bool value) {
            setState(() {
              emailNotifications = value;
              emailNotificationsChanged = !emailNotificationsChanged;
            });
          },
          activeColor: const Color(0xff7341E6),
        ),
        decoration: BoxDecoration(
            color: Color(0x267341E6),
            borderRadius: BorderRadius.all(Radius.circular(15.0))
        )
    );
  }


  /*
   Method Name:
    changeSettings

   Purpose:
     This method updates the changed notification to the database.
   */
  changeSettings() async {
    final http.Response response = await http.post(
      url + "changeNotificationSettings",
      headers: <String, String>{
        'Content-Type': 'application/json',
      },
      body: jsonEncode({
        "username": username,
        "push": pushNotifications,
        "email": emailNotifications,
      }),
    );

    if (response.statusCode != 200) {
      _showDialogue("Oh no!",
          "We could not update your information. Please try again later.");

      if (pushNotificationsChanged) {
        pushNotifications = !pushNotifications;
      }

      if (emailNotificationsChanged) {
        emailNotifications = !emailNotifications;
      }

    } else {
      _showDialogue("Great news!", "Your settings were updated successfully.");
    }
  }

  /*
   Method Name:
    getNotificationValues

   Purpose:
     This method gets what the members current notification preferences are.
   */
  getNotificationValues() async {
    SharedPreferences prefs = await SharedPreferences.getInstance();
    username = prefs.get("username");

    final http.Response response = await http.get(
      url + "getNotificationSettings?username="  + username);

    if (response.statusCode != 200) {
      _showDialogue("Oh no!", response.body);
    } else {
      Notifications notifications =
          Notifications.fromJson(json.decode(response.body));

      pushNotifications = notifications.push;
      emailNotifications = notifications.email;
    }
  }

  /*
   Method Name:
    _showDialogue

   Purpose:
     This method shows a dialogue.dynamic.
     */
  _showDialogue(heading, body) async {
    return showDialog<void>(
      context: context,
      barrierDismissible: false,
      builder: (BuildContext context) {
        return AlertDialog(
          title: Text(heading),
          content: Text(body),
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

  Notifications({this.push, this.email});

  factory Notifications.fromJson(Map<String, dynamic> json) {
    return Notifications(
      push: json['push'],
      email: json['email']
    );
  }
}

const String backArrow =
    '<svg viewBox="28.2 38.0 31.4 27.9" ><path transform="matrix(-1.0, 0.0, 0.0, -1.0, 65.61, 71.93)" d="M 21.68118286132813 6 L 18.91737365722656 8.460894584655762 L 29.85499572753906 18.21720886230469 L 6 18.21720886230469 L 6 21.70783996582031 L 29.85499572753906 21.70783996582031 L 18.91737365722656 31.46415710449219 L 21.68118286132813 33.925048828125 L 37.36236572265625 19.9625244140625 L 21.68118286132813 6 Z" fill="#513369" stroke="none" stroke-width="1" stroke-miterlimit="4" stroke-linecap="butt" /></svg>';
