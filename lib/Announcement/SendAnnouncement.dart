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

Functional Description:
  This file implements the SendAnnouncementState class. It creates the UI for
  Managers to be able to send announcements; as well as implements the actual
  functionality of sending the announcements, notifying members of the
  announcements, and storing the new data in the database. It also implements
  the SendAnnouncement class which calls the other class to be built.
 */

import 'package:flutter/material.dart';
import 'package:flutter_svg/flutter_svg.dart';
import 'package:http/http.dart' as http;
import 'dart:convert';
import 'package:fluttertoast/fluttertoast.dart';

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
  input.
 */

class SendAnnouncementState extends State<SendAnnouncement> {
  String _headingOfAnnouncement = "";
  String _detailsAnnouncement = "";

  final announcementFormKey = GlobalKey<FormState>();

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
            width: 0.7 * media.size.width,
            height: 0.08 * media.size.height,
            child: TextField(
                cursorColor: Colors.black45,
                obscureText: false,
                style: TextStyle(
                    color: Colors.black54,
                    fontSize: 0.04 * media.size.width
                ),
                decoration: InputDecoration(
                    filled: true,
                    fillColor: Colors.white,
                    labelText: 'Heading',
                    contentPadding: const EdgeInsets.all(15.0),
                    border: InputBorder.none,
                    labelStyle: new TextStyle(color: Colors.black54),
                    enabledBorder: OutlineInputBorder(
                        borderRadius: BorderRadius.all(Radius.circular(19.0)),
                        borderSide: BorderSide.none
                    ),
                    focusedBorder: OutlineInputBorder(
                        borderSide: BorderSide.none,
                        borderRadius: BorderRadius.circular(19.0)
                    )
                ),
                onChanged: (value) {
                  setState(() {
                    _headingOfAnnouncement = value;
                  });
                })),
        borderRadius: BorderRadius.all(Radius.circular(19.0)),
        color: Colors.transparent);

    final detailField = Material(
        shadowColor: Colors.black,
        elevation: 15,
        child: Container(
            width: 0.7 * media.size.width,
            height: 0.3 * media.size.height,
            child: TextField(
                cursorColor: Colors.black45,
                obscureText: false,
                style: TextStyle(
                    color: Colors.black54, fontSize: 0.04 * media.size.width),
                maxLines: 9,
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
                    _detailsAnnouncement = value;
                  });
                })),
        borderRadius: BorderRadius.all(Radius.circular(19.0)),
        color: Colors.transparent);

    return Scaffold(
        backgroundColor: const Color(0xff513369),
        body: ListView(children: <Widget>[
          Stack(children: <Widget>[
            Transform.translate(
              offset: Offset(0.0, -0.033 * media.size.height),
              child: Container(
                width: media.size.width,
                height: media.size.height * 0.35,
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
                    ),
                  ],
                ),
              ),
            ),
            Transform.translate(
                offset:
                    Offset(0.05 * media.size.width, 0.01 * media.size.height),
                child: GestureDetector(
                  onTap: () {
                    Navigator.pop(context);
                  },
                  child: SvgPicture.string(backArrow,
                      allowDrawingOutsideViewBox: true,
                      height: 0.05 * media.size.height,
                      width: 0.07 * media.size.width),
                )),
            Transform.translate(
                offset: Offset(0.0, -0.033 * media.size.height),
                child: SizedBox(
                    width: media.size.width,
                    height: media.size.height * 0.35,
                    child: Center(
                      child: Text(
                        'New Announcement',
                        style: TextStyle(
                          fontFamily: 'FreestyleScript',
                          fontSize: 0.16 * media.size.width,
                          color: const Color(0xFFFFFFFF),
                          shadows: [
                            Shadow(
                              color: const Color(0xbd000000),
                              offset: Offset(0, 3),
                              blurRadius: 6,
                            ),
                          ],
                        ),
                        textAlign: TextAlign.center,
                      ),
                    ))),
          ]),
          SizedBox(height: 0.04 * media.size.height),
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
                  child: RaisedButton(
                    shape: RoundedRectangleBorder(
                        borderRadius: BorderRadius.circular(10.0)),
                    color: const Color(0xffffffff).withOpacity(0.3),
                    onPressed: () {
                      sendValuesToNotify(
                          _headingOfAnnouncement, _detailsAnnouncement);
                      sendValuesToDatabase(
                          _headingOfAnnouncement, _detailsAnnouncement);
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
                  )))
        ]));
  }
}

/*
  Method Name:
    sendValuesToNotify

  Purpose:
    This method is called when the send button is pressed. It tells the API to
    send this announcement as a notification to the members.
*/
void sendValuesToNotify(String heading, String details) async {
  final http.Response response = await http.post(
    'https://jsonplaceholder.typicode.com/',
    headers: <String, String>{
      'Content-Type': 'application/json',
    },
    body: jsonEncode(<String, String>{'heading': heading, 'body': details}),
  );

  if (response.statusCode == 201) {
    //return Album.fromJson(json.decode(response.body));
  } 
  else {
    Fluttertoast.showToast(
        msg: "Could not send announcement. Try again later.",
        toastLength: Toast.LENGTH_SHORT,
        gravity: ToastGravity.CENTER,
        timeInSecForIosWeb: 1,
        backgroundColor: Colors.grey,
        textColor: Colors.black,
        fontSize: 16.0);
  }
}

/*
  Method Name:
    sendValuesToDatabase

  Purpose:
    This method is called when the send button is pressed. It sends the values
    to the database to be stored.
*/

sendValuesToDatabase(_heading, _details) async {
  var date = new DateTime.now();

  var year = date.year;
  var month = date.month;
  var day = date.day;

  final http.Response response = await http.post(
    'https://jsonplaceholder.typicode.com/',
    headers: <String, String>{
      'Content-Type': 'application/json',
    },
    body: jsonEncode(<String, String>{
      'heading': _heading,
      'body': _details,
      'announcementDay': day.toString(),
      'announcementYear': year.toString(),
      'announcementMonth': month.toString(),
    }),
  );

  if (response.statusCode == 201) {
    //return Album.fromJson(json.decode(response.body));
  } 
  else {
    Fluttertoast.showToast(
        msg: "Could not store announcement. Try again later.",
        toastLength: Toast.LENGTH_SHORT,
        gravity: ToastGravity.CENTER,
        timeInSecForIosWeb: 1,
        backgroundColor: Colors.grey,
        textColor: Colors.black,
        fontSize: 16.0);
  }
}

const String backArrow =
    '<svg viewBox="28.2 38.0 31.4 27.9" ><path transform="matrix(-1.0, 0.0, 0.0, -1.0, 65.61, 71.93)" d="M 21.68118286132813 6 L 18.91737365722656 8.460894584655762 L 29.85499572753906 18.21720886230469 L 6 18.21720886230469 L 6 21.70783996582031 L 29.85499572753906 21.70783996582031 L 18.91737365722656 31.46415710449219 L 21.68118286132813 33.925048828125 L 37.36236572265625 19.9625244140625 L 21.68118286132813 6 Z" fill="#fcfbfc" stroke="none" stroke-width="1" stroke-miterlimit="4" stroke-linecap="butt" /></svg>';
