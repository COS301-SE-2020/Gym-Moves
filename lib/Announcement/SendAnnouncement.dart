/*
File Name:
  SendAnnouncement.dart

Author:
  Raeesa

Date Created:
  15/06/2020

Update History:
--------------------------------------------------------------------------------
Date          |    Author      |     Changes
--------------------------------------------------------------------------------
24/06/2020    |    Danel       |    Fixed input and scrolling
--------------------------------------------------------------------------------
28/06/2020    |    Danel       |    Added date picker
--------------------------------------------------------------------------------
01/07/2020    |    Tia         |    Added request to send announcement
02/07/2020    |    Ayanda         |    Added request to send announcement to firebase console
--------------------------------------------------------------------------------
05/08/2020    |    Raeesa      |    Fixed UI
01/08/2020    |    Ayanda         |    Fixed Push Notifications
--------------------------------------------------------------------------------

Functional Description:
  This file implements the the UI for managers to be able to send announcements.
  It also sends the announcement to the database to be stored.
 */

import 'package:flutter/material.dart';
import 'package:flutter_svg/flutter_svg.dart';
import 'package:flutter_datetime_picker/flutter_datetime_picker.dart';
import 'package:gym_moves/NavigationBar.dart';
import 'package:http/http.dart' as http;
import 'package:firebase_messaging/firebase_messaging.dart';
import 'dart:convert';
import 'dart:async';
import 'package:shared_preferences/shared_preferences.dart';

/*
Class Name:
  SendAnnouncement

Purpose:
  This class is used to call the class that builds the UI, it also ensures that
  the screen keeps the state of the text fields and does not clear them when the
  keyboard closes.
 */
class SendAnnouncement extends StatefulWidget {
  const SendAnnouncement({Key key}) : super(key: key);

  @override
  SendAnnouncementState createState() => SendAnnouncementState();
}

/*
Class Name:
  SendAnnouncementState

Purpose:
  This class is used to build the UI to allow managers to send announcements,
   and also handles what happens with the information that gets inputted.
 */
class SendAnnouncementState extends State<SendAnnouncement> {
  String headingOfAnnouncement, detailsOfAnnouncement;

  String serverToken = "AAAAHKLq9MA:APA91bEArbWAhsvd6sWybfGe4d-RXUH0zWNvhoa4yUyFIOfeDPFcSZ9Grlfs7nPc5KGVAgYpDsCZxIJBk8i8cXYTf5ITKZsA-TVLF2q9-nAsP8CbEBYnlTjDa6b6EKRhX8FrAbpuY11o";
  final FirebaseMessaging firebaseMessaging = FirebaseMessaging();
  final announcementFormKey = GlobalKey<FormState>();

  String day = (new DateTime.now().day).toString();
  String month = (new DateTime.now().month).toString();
  String year = (new DateTime.now().year).toString();

  final headingHolder = TextEditingController();
  final detailsHolder = TextEditingController();

  /*
   Method Name:
    build

   Purpose:
    This method builds the UI and also calls the necessary functions that
    process the data that was entered.
   */
  @override
  Widget build(BuildContext context) {
    MediaQueryData media = MediaQuery.of(context);

    final headingField = Container(
        alignment: Alignment.centerLeft,
        width: 0.7 * media.size.width,
        height: 0.085 * media.size.height,
        child: TextField(
            controller: headingHolder,
            cursorColor: Color(0xff787878),
            obscureText: false,
            style: TextStyle(
                color: Color(0xff787878),
                fontSize: 0.04 * media.size.width
            ),
            decoration: InputDecoration(
                filled: true,
                fillColor: Colors.white,
                labelText: 'Heading',
                contentPadding: const EdgeInsets.all(20.0),
                labelStyle: new TextStyle(
                    color: Color(0xff787878)
                ),
                enabledBorder: OutlineInputBorder(
                    borderRadius: BorderRadius.all(
                        Radius.circular(15.0)
                    ),
                    borderSide: new BorderSide(
                        color: Color(0xff787878)
                    )
                ),
                focusedBorder: OutlineInputBorder(
                    borderSide: new BorderSide(
                        color: Color(0xff787878)
                    ),
                    borderRadius: BorderRadius.circular(15.0)
                )
            ),
            onChanged: (value) {
              setState(() {
                headingOfAnnouncement = value;
              });
            })
    );

    final detailField = Container(
        padding: EdgeInsets.all(0.01 * media.size.width),
        alignment: Alignment.topLeft,
        width: 0.7 * media.size.width,
        height: 0.3 * media.size.height,
        child: TextField(
            controller: detailsHolder,
            cursorColor: Color(0xff787878),
            obscureText: false,
            key: Key('detailsField'),
            style: TextStyle(
                color: Color(0xff787878),
                fontSize: 0.04 * media.size.width
            ),
            minLines: 1,
            maxLines: 9,
            decoration: InputDecoration(
                filled: true,
                fillColor: Colors.white,
                labelText: 'Details',
                contentPadding: const EdgeInsets.all(20.0),
                labelStyle: new TextStyle(
                    color: Color(0xff787878)
                ),
                enabledBorder: OutlineInputBorder(
                    borderRadius: BorderRadius.all(Radius.circular(15.0)),
                    borderSide: new BorderSide(color: Color(0xff787878))
                ),
                focusedBorder: OutlineInputBorder(
                    borderSide: new BorderSide(
                        color: Color(0xff787878)
                    ),
                    borderRadius: BorderRadius.circular(15.0)
                )
            ),
            onChanged: (value) {
              setState(() {
                detailsOfAnnouncement = value;
              });
            })
    );

    final dateField = Container(
        width: 0.7 * media.size.width,
        height: 0.085 * media.size.height,
        decoration: BoxDecoration(
            borderRadius: BorderRadius.all(Radius.circular(15.0)),
            border: Border.all(color: Color(0xff787878))
        ),
        child: FlatButton(
          shape:
              RoundedRectangleBorder(borderRadius: BorderRadius.circular(19.0)),
          onPressed: () {
            DatePicker.showDatePicker(context,
                theme: DatePickerTheme(
                  containerHeight: media.size.height * 0.35,
                ),
                showTitleActions: true,
                minTime: DateTime(new DateTime.now().year,
                    new DateTime.now().month, new DateTime.now().day
                ),
                maxTime: DateTime(
                    new DateTime.now().year + 1,
                    new DateTime.now().month,
                    new DateTime.now().day), onConfirm: (date) {
              year = date.year.toString();
              month = date.month.toString();
              day = date.day.toString();
              setState(() {});
            }, currentTime: DateTime.now());
          },
          child: Container(
            alignment: Alignment.center,
            width: 0.63 * media.size.width,
            child: Row(
              mainAxisAlignment: MainAxisAlignment.spaceBetween,
              children: <Widget>[
                Row(
                  children: <Widget>[
                    Container(
                      child: Row(
                        children: <Widget>[
                          Icon(
                              Icons.date_range,
                              size: 0.04 * media.size.width,
                              color: Color(0xff787878)
                          ),
                          Text(
                            "     Send Date:   $day - $month - $year ",
                            style: TextStyle(
                                color: Color(0xff787878),
                                fontSize: 0.04 * media.size.width
                            ),
                          ),
                        ],
                      ),
                    )
                  ],
                )
              ],
            ),
          ),
          color: Colors.white,
        )
    );

    return Scaffold(
        backgroundColor: const Color(0xffffffff),
        body: ListView(
            children: <Widget>[
              Stack(
                  children: <Widget>[
                    Transform.translate(
                        offset: Offset(0.05 * media.size.width, 0.0),
                        child: Container(
                            width: 0.6 * media.size.width,
                            height: 0.19 * media.size.height,
                            decoration: BoxDecoration(
                                image: DecorationImage(
                                    image: const AssetImage('assets/SendAnnouncementPicture.png'),
                                    fit: BoxFit.fill
                                )
                            )
                        )
                    ),
                    Transform.translate(
                        offset: Offset(0.0, 0.04 * media.size.height),
                        child: Transform.translate(
                            offset: Offset(
                                0.05 * media.size.width, -0.02 * media.size.height),
                            child: GestureDetector(
                                onTap: () {
                                  Navigator.pop(context);
                                  Navigator.pop(context);
                                  Navigator.push(
                                    context,
                                    MaterialPageRoute(builder: (context) => NavigationBar(index: 1)),
                                  );},
                                child: SvgPicture.string(backArrow,
                                    allowDrawingOutsideViewBox: true,
                                    width: 0.06 * media.size.width,
                                    color: Color(0xff7341E6)
                                )
                            )
                        )
                    ),
                    Container(
                        alignment: Alignment.bottomRight,
                        width: media.size.width,
                        height: 0.25 * media.size.height,
                        padding: EdgeInsets.all(0.05 * media.size.width),
                        child: Text(
                          'New Announcement',
                          style: TextStyle(
                            fontFamily: 'Lastwaerk',
                            fontSize: 0.07 * media.size.width,
                            color: const Color(0xFF3E3E3E),
                          ),
                          textAlign: TextAlign.right,
                        )
                    )
                  ]
              ),
          SizedBox(height: 0.06 * media.size.height),
          Center(child: dateField),
          SizedBox(height: 0.05 * media.size.height),
          Form(
              key: announcementFormKey,
              child: Column(
                  children: <Widget>[
                    headingField,
                    SizedBox(height: 0.05 * media.size.height),
                    detailField
                  ]
              )
          ),
          SizedBox(height: 0.05 * media.size.height),
          Center(
              child: SizedBox(
                  width: 0.4 * media.size.width,
                  child: FlatButton(
                      shape: RoundedRectangleBorder(
                          borderRadius: BorderRadius.circular(10.0)
                      ),
                      color: const Color(0xff7341E6),
                      onPressed: () {
                        sendValuesToNotify();
                      },
                      textColor: Colors.white,
                      padding: const EdgeInsets.all(0.0),
                      child: Container(
                          padding: const EdgeInsets.all(10.0),
                          child: Text(
                            'Send',
                            style: TextStyle(
                                fontSize: 0.05 * media.size.width,
                                fontFamily: 'Roboto'),
                          )
                      )
                  )
              )
          ),
          SizedBox(height: 0.05 * media.size.height)
        ])
    );
  }
/*
Class Name:
  sendPushNotification

Purpose:
  This method sends a post request to the api which then forwards the message as a notification
  to the user.
 */

  Future<Map<String, dynamic>> sendPushNotification() async {
    await firebaseMessaging.requestNotificationPermissions(
      const IosNotificationSettings(sound: true, badge: true, alert: true, provisional: false),
    );
    final prefs = await SharedPreferences.getInstance();
    final int gymId = prefs.getInt('gymId');

    await http.post(
      'https://fcm.googleapis.com/fcm/send',
      headers: <String, String>{
        'Content-Type': 'application/json',
        'Authorization': 'key=$serverToken',
      },
      body: jsonEncode(
        <String, dynamic>{
          'notification': <String, dynamic>{
            'body': headingOfAnnouncement,
            'title': detailsOfAnnouncement,
            'day': day,
            'month': month,
            'year': year,
            'gymid': gymId
          },
          'priority': 'high',
          'data': <String, dynamic>{
            'click_action': 'FLUTTER_NOTIFICATION_CLICK',
            'id': '1',
            'status': 'done'
          },
          'to': await firebaseMessaging.getToken(),
        },
      ),
    );

    final Completer<Map<String, dynamic>> completer =
    Completer<Map<String, dynamic>>();

    firebaseMessaging.configure(
      onMessage: (Map<String, dynamic> message) async {
        completer.complete(message);
      },
    );
    return completer.future;
  }
  /*
  Method Name:
    sendValuesToNotify

  Purpose:
    This method is called when the send button is pressed. It tells the API to
    send this announcement as a notification to the members.
*/
  void sendValuesToNotify() async {
    final prefs = await SharedPreferences.getInstance();
    sendPushNotification();
    final int gymId = prefs.getInt('gymId');

    final http.Response response = await http.post(
      'https://gymmoveswebapi.azurewebsites.net/api/notifications/sendNotification',
      headers: <String, String>{'Content-Type': 'application/json'},
      body: jsonEncode({
        'gymId': gymId,
        'heading': headingOfAnnouncement,
        'body': detailsOfAnnouncement,
        'announcementDay': day,
        'announcementMonth': month,
        'announcementYear': year
      }),
    );

    String title = "";
    String message = "";

    if (response.statusCode == 200) {
      title = "Success!";
      message = Message.fromJson(jsonDecode(response.body)).message;

      detailsHolder.clear();
      headingHolder.clear();
    } else {
      title = "Oh no!";
      message = "Could not send announcement. Please try again later.";
    }

    Widget okButton = FlatButton(
        child: Text(
          "Ok",
          style: TextStyle(color: Color(0xff7341E6)),
        ),
        onPressed: () => Navigator.pop(context)
    );

    AlertDialog alert = AlertDialog(
      title: Text(title),
      content: Text(message),
      actions: [
        okButton,
      ],
    );

    showDialog(
      context: context,
      builder: (BuildContext context) {
        return alert;
      },
    );
  }
}

/*
Class Name:
  Message

Purpose:
  Structure of the message from server.
 */
class Message {
  final String message;

  Message({this.message});

  factory Message.fromJson(Map<String, dynamic> json) {
    return Message(message: json['message']);
  }
}

const String backArrow =
    '<svg viewBox="28.2 38.0 31.4 27.9" ><path transform="matrix(-1.0, 0.0, 0.0, -1.0, 65.61, 71.93)" d="M 21.68118286132813 6 L 18.91737365722656 8.460894584655762 L 29.85499572753906 18.21720886230469 L 6 18.21720886230469 L 6 21.70783996582031 L 29.85499572753906 21.70783996582031 L 18.91737365722656 31.46415710449219 L 21.68118286132813 33.925048828125 L 37.36236572265625 19.9625244140625 L 21.68118286132813 6 Z" fill="#fcfbfc" stroke="none" stroke-width="1" stroke-miterlimit="4" stroke-linecap="butt" /></svg>';
