/*
File Name:
  Announcements.dart

Author:
  Danel

Date Created:
  05/08/2020

Update History:
--------------------------------------------------------------------------------
Date          |    Author      |     Changes
--------------------------------------------------------------------------------


Functional Description:
  This file implements the the UI for people to see announcements from their gym.

Classes:
- Announcements
- AnnouncementsState
 */

import 'dart:convert';
import 'package:flutter/material.dart';
import 'package:http/http.dart';
import 'package:shared_preferences/shared_preferences.dart';

import 'package:gym_moves/Announcement/SendAnnouncement.dart';

/*
Class Name:
  Announcements

Purpose:
  This class is used to call the class that builds the UI.
 */
class Announcements extends StatefulWidget {
  const Announcements({Key key}) : super(key: key);

  @override
  AnnouncementsState createState() => AnnouncementsState();
}

/*
Class Name:
  AnnouncementsState

Purpose:
  This class builds the UI.
 */
class AnnouncementsState extends State<Announcements> {

  Future announcements;
  List<Announcement> announcementsList = [];

  int userType;

  @override
  void initState() {
    announcements = _getAnnouncementsFromDB();
    super.initState();
  }

  @override
  Widget build(BuildContext context) {
    MediaQueryData media = MediaQuery.of(context);

    return Scaffold(
        backgroundColor: const Color(0xffffffff),
        body: Column(
            children: <Widget>[
              Stack(
                  children: <Widget>[
                    Container(
                        width: media.size.width,
                        height: 0.3 * media.size.height,
                        decoration: BoxDecoration(
                            image: DecorationImage(
                                image: const AssetImage('assets/SendAnnouncementPicture.png'),
                                fit: BoxFit.fill
                            )
                        )
                    ),
                    Container(
                      width: media.size.width,
                      height: 0.32 * media.size.height,
                      alignment: Alignment.bottomCenter,
                      child: Text(
                        'Announcements',
                        style: TextStyle(
                          fontFamily: 'Lastwaerk',
                          fontSize: 0.08 * media.size.width,
                          color: const Color(0xFF3E3E3E),
                        ),
                        textAlign: TextAlign.right,
                      ),
                    ),
                  ]
              ),
              SizedBox(height: 0.01 * media.size.height),
              Expanded(
                  child: new FutureBuilder(
                    future: announcements,
                    builder: (BuildContext context, AsyncSnapshot snapshot) {
                      if (snapshot.connectionState == ConnectionState.done) {
                        return getAnnouncements(media);
                      } else {
                        return Text(
                          'Busy getting your announcements.',
                          style: TextStyle(
                            fontFamily: 'Roboto',
                            fontSize: media.size.width * 0.05,
                            color: const Color(0xFF3E3E3E),
                          ),
                          textAlign: TextAlign.center,
                        );
                      }},
                  )
              )
            ]
        )
    );
  }

  /*
   Method Name:
    getAnnouncements

   Purpose:
    This method returns a listview that as all the announcements.

   */
  Widget getAnnouncements(media) {
    List<Widget> displayedAnnouncements = [];

    if (announcementsList.length == 0) {
      return Center(
          child: Container(
              height: 1 / 10 * media.size.height,
              width: media.size.width,
              child: Text(
                'There are currently no recent announcements.',
                style: TextStyle(
                  fontFamily: 'Roboto',
                  fontSize: media.size.width * 0.05,
                  color: const Color(0xFF3E3E3E),
                ),
                textAlign: TextAlign.center,
              )
          )
      );
    } else {
      displayedAnnouncements.add(
          userType == 2
          ? Row(
              mainAxisAlignment: MainAxisAlignment.end,
              children: <Widget>[
                Container(
                    width: 0.2 * media.size.width,
                    child: FlatButton(
                        shape: RoundedRectangleBorder(
                            borderRadius: BorderRadius.circular(10.0)
                        ),
                        color: const Color(0xff7341E6),
                        onPressed: () {
                          Navigator.push(
                            context,
                            MaterialPageRoute(
                                builder: (context) => SendAnnouncement()
                            ),
                          );},
                        textColor: Colors.white,
                        padding: const EdgeInsets.all(0.0),
                        child: Container(
                            padding: const EdgeInsets.all(10.0),
                            child: Text(
                              'New',
                              style: TextStyle(
                                  fontSize: 0.04 * media.size.width,
                                  fontFamily: 'Roboto'
                              )
                            )
                        )
                    )
                )
            ]
          ) : SizedBox());

      displayedAnnouncements.insert(1, SizedBox(height: 0.04 * media.size.height));

      for (Announcement announce in announcementsList) {
        displayedAnnouncements.insert(1,
            Row(
                mainAxisAlignment: MainAxisAlignment.center,
                children: <Widget>[
                  Stack(
                      children: <Widget>[
                        Container(
                          padding: EdgeInsets.all(0.02 * media.size.height),
                          width: 0.8 * media.size.width,
                          decoration: BoxDecoration(
                            borderRadius: BorderRadius.circular(19.0),
                            color: const Color(0xff7341E6).withOpacity(0.03),
                            border: Border.all(
                                width: 1.0,
                                color: const Color(0xff7341E6).withOpacity(0.15)),
                          ),
                          child: Text.rich(
                              TextSpan(children: [
                                TextSpan(
                                    text: announce.heading,
                                    style: TextStyle(fontWeight: FontWeight.w800)
                                ),
                                TextSpan(text: "\n\n\n"),
                                TextSpan(text: announce.body)
                              ]),
                              style: TextStyle(
                                  color: Color(0xFF3E3E3E),
                                  fontSize: 0.035 * media.size.width
                              )
                          ),
                        ),
                        Transform.translate(
                            offset: Offset(0.6 * 0.9 * media.size.width,
                                0.3 * 0.15 * media.size.height),
                            child: SizedBox(
                                width: 0.7 * media.size.width,
                                child: Text(
                                    announce.date.day.toString() + "/" +
                                        announce.date.month.toString() + "/" +
                                        announce.date.year.toString(),
                                    style: TextStyle(
                                        color: Color(0xFF3E3E3E),
                                        fontSize: 0.027 * media.size.width
                                    )
                                )
                            )
                        ),
                      ]
                  )
                ]
            )
        );

        displayedAnnouncements.insert(1, SizedBox(height: 0.04 * media.size.height));
      }
    }

    return ListView(
        padding: const EdgeInsets.all(15), children: displayedAnnouncements
    );
  }

  /*
   Method Name:
    _getAnnouncementsFromDB

   Purpose:
    This method gets all announcements from the database.

   */
  _getAnnouncementsFromDB() async {
    SharedPreferences prefs = await SharedPreferences.getInstance();
    String gymId = prefs.get("gymId").toString();

    userType = prefs.get("type");

    String url =
        'https://gymmoveswebapi.azurewebsites.net/api/notifications/getAllAnnouncements?gymID=$gymId';

    Response response = await get(url);

    if (response.statusCode == 200) {
      List<dynamic> announcementsJson = json.decode(response.body);

      for (int i = 0; i < announcementsJson.length; i++) {
        announcementsList.add(Announcement.fromJson(announcementsJson[i]));
      }
    }
  }
}

/*
Class Name:
  Announcement

Purpose:
  Structure of an announcement.

*/
class Announcement {
  final String heading;
  final String body;
  final DateTime date;

  Announcement({this.heading, this.body, this.date});

  factory Announcement.fromJson(Map<dynamic, dynamic> json) {
    return Announcement(
        heading: json['heading'],
        body: json['body'],
        date: DateTime.parse(json['date']));
  }
}
