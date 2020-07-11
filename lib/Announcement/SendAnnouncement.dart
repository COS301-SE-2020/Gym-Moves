/*
File Name:
  SendAnnouncement.dart

Author:
  Raeesa

Date Created:
  15/06/2020

Update History:
--------------------------------------------------------------------------------
| Name          |    Date             |     Changes
--------------------------------------------------------------------------------
| Danel         |      24/06/2020     |    Fixed input and scrolling
--------------------------------------------------------------------------------
| Danel         |      28/06/2020     |    Added date picker
--------------------------------------------------------------------------------
|Tia            |     04/07/2020      | Added Announcement API call function

Functional Description:
  This file implements the the UI for managers to be able to send announcements.
  It also sends the announcement to the database to be stored.
 */

import 'package:flutter/material.dart';
import 'package:flutter_svg/flutter_svg.dart';
import 'package:flutter_datetime_picker/flutter_datetime_picker.dart';
import 'package:http/http.dart' as http;
import 'dart:convert';
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
  String _headingOfAnnouncement = "";
  String _detailsOfAnnouncement = "";

  final announcementFormKey = GlobalKey<FormState>();

  String day = (new DateTime.now().day).toString();
  String month = (new DateTime.now().month).toString();
  String year = (new DateTime.now().year).toString();

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

    final headingField = Material(
        shadowColor: Colors.black,
        elevation: 15,
        child: Container(
            alignment: Alignment.centerLeft,
            width: 0.7 * media.size.width,
            height: 0.085 * media.size.height,
            child: TextField(
                cursorColor: Colors.black45,
                obscureText: false,
                style: TextStyle(
                    color: Colors.black54, fontSize: 0.04 * media.size.width),
                decoration: InputDecoration(
                    filled: true,
                    fillColor: Colors.white,
                    labelText: 'Heading',
                    contentPadding: const EdgeInsets.all(15.0),
                    border: InputBorder.none,
                    labelStyle: new TextStyle(color: Colors.black54),
                    enabledBorder: OutlineInputBorder(
                        borderRadius: BorderRadius.all(Radius.circular(19.0)),
                        borderSide: BorderSide.none),
                    focusedBorder: OutlineInputBorder(
                        borderSide: BorderSide.none,
                        borderRadius: BorderRadius.circular(19.0))),
                onChanged: (value) {
                  setState(() {
                    _headingOfAnnouncement = value;
                  });
                })),
        borderRadius: BorderRadius.all(Radius.circular(19.0)),
        color: Colors.white);

    final detailField = Material(
        shadowColor: Colors.black,
        elevation: 15,
        child: Container(
            padding: EdgeInsets.all(0.01 * media.size.width),
            alignment: Alignment.topLeft,
            width: 0.7 * media.size.width,
            height: 0.3 * media.size.height,
            child: TextField(
                cursorColor: Colors.black45,
                obscureText: false,
                style: TextStyle(
                    color: Colors.black54, fontSize: 0.04 * media.size.width),
                maxLines: 9,
                minLines: 1,
                decoration: InputDecoration(
                    filled: true,
                    fillColor: Colors.white,
                    labelText: 'Details',
                    border: InputBorder.none,
                    labelStyle: new TextStyle(color: Colors.black54),
                    enabledBorder: OutlineInputBorder(
                        borderRadius: BorderRadius.all(Radius.circular(19.0)),
                        borderSide: BorderSide.none),
                    focusedBorder: OutlineInputBorder(
                        borderSide: BorderSide.none,
                        borderRadius: BorderRadius.circular(19.0))),
                onChanged: (value) {
                  setState(() {
                    _detailsOfAnnouncement = value;
                  });
                })),
        borderRadius: BorderRadius.all(Radius.circular(19.0)),
        color: Colors.white);

    final dateField = Material(
        shadowColor: Colors.black,
        elevation: 15,
        child: Container(
            width: 0.7 * media.size.width,
            height: 0.085 * media.size.height,
            child: FlatButton(
              shape: RoundedRectangleBorder(
                  borderRadius: BorderRadius.circular(19.0)),
              onPressed: () {
                DatePicker.showDatePicker(context,
                    theme: DatePickerTheme(
                      containerHeight: media.size.height * 0.35,
                    ),
                    showTitleActions: true,
                    minTime: DateTime(new DateTime.now().year,
                        new DateTime.now().month, new DateTime.now().day),
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
                height: 0.075 * media.size.height,
                child: Row(
                  mainAxisAlignment: MainAxisAlignment.spaceBetween,
                  children: <Widget>[
                    Row(
                      children: <Widget>[
                        Container(
                          child: Row(
                            children: <Widget>[
                              Icon(Icons.date_range,
                                  size: 0.04 * media.size.width,
                                  color: Colors.black54),
                              Text(
                                " $day - $month - $year",
                                style: TextStyle(
                                    color: Colors.black54,
                                    fontSize: 0.04 * media.size.width),
                              ),
                            ],
                          ),
                        )
                      ],
                    ),
                    Text(
                      "  Change",
                      style: TextStyle(
                          color: Colors.black54,
                          fontSize: 0.04 * media.size.width),
                    ),
                  ],
                ),
              ),
              color: Colors.white,
            )),
        borderRadius: BorderRadius.all(Radius.circular(19.0)),
        color: Colors.transparent);

    return Scaffold(
        backgroundColor: const Color(0xff513369),
        body: ListView(children: <Widget>[
          Stack(children: <Widget>[
            Transform.translate(
                offset: Offset(0.0, -0.035 * media.size.height),
                child: Container(
                    width: media.size.width,
                    height: 0.13 * media.size.height,
                    decoration: BoxDecoration(
                        image: DecorationImage(
                          image: const AssetImage('assets/Banner.jpg'),
                          fit: BoxFit.fill,
                          colorFilter: new ColorFilter.mode(
                            Colors.black.withOpacity(0.52),
                            BlendMode.dstIn,
                          ),
                        ),
                        boxShadow: [
                          BoxShadow(
                            color: const Color(0x46000000),
                            offset: Offset(0, 3),
                            blurRadius: 6,
                          )
                        ]))),
            Transform.translate(
                offset: Offset(0.0, 0.04 * media.size.height),
                child: Transform.translate(
                    offset: Offset(
                        0.05 * media.size.width, -0.02 * media.size.height),
                    child: GestureDetector(
                        onTap: () {
                          Navigator.pop(context);
                        },
                        child: SvgPicture.string(backArrow,
                            allowDrawingOutsideViewBox: true,
                            width: 0.06 * media.size.width)))),
            Container(
                alignment: Alignment.centerRight,
                width: media.size.width,
                height: 0.09 * media.size.height,
                padding: EdgeInsets.all(0.01 * media.size.width),
                child: Text(
                  'New Announcement',
                  style: TextStyle(
                    fontFamily: 'FreestyleScript',
                    fontSize: 0.1 * media.size.width,
                    color: const Color(0xFFFFFFFF),
                    shadows: [
                      Shadow(
                        color: const Color(0xbd000000),
                        offset: Offset(0, 3),
                        blurRadius: 6,
                      ),
                    ],
                  ),
                  textAlign: TextAlign.right,
                ))
          ]),
          SizedBox(height: 0.04 * media.size.height),
          Center(child: dateField),
          SizedBox(height: 0.05 * media.size.height),
          Form(
              key: announcementFormKey,
              child: Column(children: <Widget>[
                headingField,
                SizedBox(height: 0.05 * media.size.height),
                detailField
              ])),
          SizedBox(height: 0.05 * media.size.height),
          Center(
              child: SizedBox(
                  width: 0.2 * media.size.width,
                  child: FlatButton(
                    shape: RoundedRectangleBorder(
                        borderRadius: BorderRadius.circular(10.0)),
                    color: const Color(0xffffffff).withOpacity(0.3),
                    onPressed: () {
                      sendValuesToNotify(
                          _headingOfAnnouncement, _detailsOfAnnouncement);
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
                      ),
                    ),
                  ))),
          SizedBox(height: 0.05 * media.size.height)
        ]));
  }

  /*
  Method Name:
    sendValuesToNotify

  Purpose:
    This method is called when the send button is pressed. It tells the API to
    send this announcement as a notification to the members.
*/
  void sendValuesToNotify(String heading, String details) async {
    final prefs = await SharedPreferences.getInstance();

    final int gymId = prefs.getInt('gymId');

    final http.Response response = await http.post(
      'https://gymmoveswebapi.azurewebsites.net/api/sendNotification',
      headers: <String, String>{'Content-Type': 'application/json'},
      body: jsonEncode({
        'gymId': gymId,
        'heading': heading,
        'body': details,
        'announcementDay': day,
        'announcementMonth': month,
        'announcementYear': year
      }),
    );
    String title = "";
    String message = "";
    if (response.statusCode == 200) {
      title = "Success!";
      message = response.body;
    } else {
      title = "Oh no!";
      message = "Could not send announcement. Please try again later.";
    }

    Widget okButton =
        FlatButton(child: Text("OK"), onPressed: () => Navigator.pop(context));

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

const String backArrow =
    '<svg viewBox="28.2 38.0 31.4 27.9" ><path transform="matrix(-1.0, 0.0, 0.0, -1.0, 65.61, 71.93)" d="M 21.68118286132813 6 L 18.91737365722656 8.460894584655762 L 29.85499572753906 18.21720886230469 L 6 18.21720886230469 L 6 21.70783996582031 L 29.85499572753906 21.70783996582031 L 18.91737365722656 31.46415710449219 L 21.68118286132813 33.925048828125 L 37.36236572265625 19.9625244140625 L 21.68118286132813 6 Z" fill="#fcfbfc" stroke="none" stroke-width="1" stroke-miterlimit="4" stroke-linecap="butt" /></svg>';
