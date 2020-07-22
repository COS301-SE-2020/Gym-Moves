/*
File Name
  InstructorPages.dart

Author:
  Danel

Date Created
  13/06/2020

Update History:
--------------------------------------------------------------------------------
| Name               | Date              | Changes                             |
--------------------------------------------------------------------------------


Functional Description:
  This file contains the InstructorPages class that handles building the UI for
  the Welcome page and the menu. It also implements the scroll screen the user
  will first encounter.

Classes in the File:
- InstructorPages
- InstructorPagesState
 */

import 'package:flutter/material.dart';
import 'package:flutter_svg/flutter_svg.dart';
import 'package:auto_size_text/auto_size_text.dart';
import 'package:flutter_swiper/flutter_swiper.dart';
import 'package:shared_preferences/shared_preferences.dart';
import 'package:gym_moves/GymClass/InstructorViewClasses.dart';
import 'package:gym_moves/User/ViewMyProfile.dart';
import 'dart:convert';
import 'package:http/http.dart';

/*
Class Name:
  InstructorPages

Purpose:
  This class creates the class that will build the page. It allows for dynamic
  adding of elements onto the welcome page.
 */
class InstructorPages extends StatefulWidget {
  const InstructorPages({
    Key key,
  }) : super(key: key);

  @override
  InstructorPagesState createState() => InstructorPagesState();
}

/*
Class Name:
  InstructorPagesState

Purpose:
  This class builds the UI for the scrollable welcome and menu page for
  instructors. It also ensures all the menu options are functional and that the
  UI is responsive to the screen size. The welcome page will also show the
  amount of people at the gym and the name of the user. This class will also
  handles this.
 */
class InstructorPagesState extends State<InstructorPages> {
  InstructorPagesState({Key key});

  static final controller = PageController(
    initialPage: 1,
  );

  /* This will store the name of the person.*/
  String name = "";

  /* This will store the number of people that are currently at the gym.*/
  String numberOfPeople = "";

  /* This will store the gym name.*/
  String gymName = "";

  Future local;
  Future announcements;

  List<Announcement> announcementsList = [];

  @override
  void initState() {
    announcements = _getAnnouncementsFromDB();
    local = _getDetails();
    super.initState();
  }

  _getDetails() async {
    SharedPreferences prefs = await SharedPreferences.getInstance();
    name = prefs.get("name");
    gymName = prefs.get("gymName");
  }

  /*
   Method Name: build

   Purpose:
    This method builds the UI for the screen. It calls the necessary
    function in order to display the dynamic information the user needs
    to see.
   */
  @override
  Widget build(BuildContext context) {
    MediaQueryData media = MediaQuery.of(context);

    return new FutureBuilder(
        future: local,
        builder: (BuildContext context, AsyncSnapshot snapshot) {
          if (snapshot.connectionState == ConnectionState.done) {
            return Scaffold(
                backgroundColor: const Color(0xff513369),
                body: new Swiper.children(
                    autoplay: false,
                    index: 1,
                    pagination: new SwiperPagination(
                        margin: new EdgeInsets.fromLTRB(0, 0, 0, 30),
                        builder: new DotSwiperPaginationBuilder(
                            color: Colors.white30,
                            activeColor: Colors.white,
                            size: 0.025 * media.size.width,
                            activeSize: 0.035 * media.size.width)),
                    children: <Widget>[
                      Column(children: <Widget>[
                        Stack(children: <Widget>[
                          Transform.translate(
                              offset: Offset(0.0, -0.035 * media.size.height),
                              child: Container(
                                  width: media.size.width,
                                  height: 0.13 * media.size.height,
                                  decoration: BoxDecoration(
                                      image: DecorationImage(
                                        image: const AssetImage(
                                            'assets/Banner.jpg'),
                                        fit: BoxFit.fill,
                                        colorFilter: new ColorFilter.mode(
                                            Colors.black.withOpacity(0.52),
                                            BlendMode.dstIn),
                                      ),
                                      boxShadow: [
                                        BoxShadow(
                                          color: const Color(0x46000000),
                                          offset: Offset(0, 3),
                                          blurRadius: 6,
                                        )
                                      ]))),
                          Container(
                            width: media.size.width,
                            child: Text(
                              'Announcements',
                              style: TextStyle(
                                fontFamily: 'FreestyleScript',
                                fontSize: 0.13 * media.size.width,
                                color: const Color(0xffffffff),
                              ),
                              textAlign: TextAlign.right,
                            ),
                          ),
                        ]),
                        Expanded(
                            child: new FutureBuilder(
                          future: announcements,
                          builder:
                              (BuildContext context, AsyncSnapshot snapshot) {
                            if (snapshot.connectionState ==
                                ConnectionState.done) {
                              return getAnnouncements(media);
                            } else {
                              return Text(
                                'Busy getting your announcements.',
                                style: TextStyle(
                                  fontFamily: 'Roboto',
                                  fontSize: media.size.width * 0.05,
                                  color: const Color(0xffffffff),
                                  shadows: [
                                    Shadow(
                                      color: const Color(0xbd000000),
                                      offset: Offset(0, 3),
                                      blurRadius: 6,
                                    ),
                                  ],
                                ),
                                textAlign: TextAlign.center,
                              );
                            }
                          },
                        ))
                      ]),
                      Stack(children: <Widget>[
                        Container(
                          width: media.size.width,
                          height: media.size.height,
                          decoration: BoxDecoration(
                            image: DecorationImage(
                              image:
                                  const AssetImage('assets/LeftSidePool.png'),
                              fit: BoxFit.fill,
                              colorFilter: new ColorFilter.mode(
                                  Colors.black.withOpacity(1.0),
                                  BlendMode.dstIn),
                            ),
                          ),
                        ),
                        Transform.translate(
                            offset: Offset(0.0, 0.4 * media.size.height),
                            child: Container(
                                height: 1 / 5 * media.size.height,
                                width: media.size.width,
                                child: AutoSizeText(
                                  'Welcome $name!',
                                  style: TextStyle(
                                    fontFamily: 'Roboto',
                                    fontSize: media.size.width * 0.1,
                                    color: const Color(0xffffffff),
                                    shadows: [
                                      Shadow(
                                        color: const Color(0xbd000000),
                                        offset: Offset(0, 3),
                                        blurRadius: 6,
                                      ),
                                    ],
                                  ),
                                  maxLines: 1,
                                  textAlign: TextAlign.center,
                                ))),
                        Transform.translate(
                            offset: Offset(0.0, 0.5 * media.size.height),
                            child: Container(
                                height: 1 / 10 * media.size.height,
                                width: media.size.width,
                                child: Text(
                                  'Number of people at $gymName:',
                                  style: TextStyle(
                                    fontFamily: 'Roboto',
                                    fontSize: media.size.width * 0.05,
                                    color: const Color(0xffffffff),
                                    shadows: [
                                      Shadow(
                                        color: const Color(0xbd000000),
                                        offset: Offset(0, 3),
                                        blurRadius: 6,
                                      ),
                                    ],
                                  ),
                                  textAlign: TextAlign.center,
                                ))),
                        Transform.translate(
                            offset: Offset(0.0, 0.56 * media.size.height),
                            child: Container(
                                height: 1 / 10 * media.size.height,
                                width: media.size.width,
                                child: Text(
                                  numberOfPeople,
                                  style: TextStyle(
                                    fontFamily: 'Roboto',
                                    fontSize: media.size.width * 0.05,
                                    color: const Color(0xffffffff),
                                    shadows: [
                                      Shadow(
                                        color: const Color(0xbd000000),
                                        offset: Offset(0, 3),
                                        blurRadius: 6,
                                      ),
                                    ],
                                  ),
                                  textAlign: TextAlign.center,
                                )))
                      ]),
                      Stack(children: <Widget>[
                        Container(
                          width: media.size.width,
                          height: media.size.height,
                          decoration: BoxDecoration(
                            image: DecorationImage(
                              image:
                                  const AssetImage('assets/RightSidePool.png'),
                              fit: BoxFit.fill,
                              colorFilter: new ColorFilter.mode(
                                  Colors.black.withOpacity(1.0),
                                  BlendMode.dstIn),
                            ),
                          ),
                        ),
                        Transform.translate(
                            offset: Offset(0.1 * media.size.width,
                                2.3 / 6 * media.size.height),
                            child: getMenuContainers(0.8, 0.1, media)),
                        Transform.translate(
                            offset: Offset(0.1 * media.size.width,
                                3.3 / 6 * media.size.height),
                            child: getMenuContainers(0.8, 0.1, media)),
                        Transform.translate(
                            offset: Offset(0.15 * media.size.width,
                                2.5 / 6 * media.size.height),
                            child: getMenuOptionText('View my classes', media)),
                        Transform.translate(
                            offset: Offset(0.15 * media.size.width,
                                3.5 / 6 * media.size.height),
                            child: getMenuOptionText('View my profile', media)),
                        Transform.translate(
                            offset: Offset(0.8 * media.size.width,
                                2.5 / 6 * media.size.height),
                            child:
                                getArrow(0.06, media, InstructorViewClasses())),
                        Transform.translate(
                            offset: Offset(0.8 * media.size.width,
                                3.5 / 6 * media.size.height),
                            child: getArrow(0.06, media, ViewMyProfile()))
                      ])
                    ]));
          } else {
            return Scaffold(
                backgroundColor: const Color(0xff513369),
                body: new Swiper.children(
                    autoplay: false,
                    index: 1,
                    pagination: new SwiperPagination(
                        margin: new EdgeInsets.fromLTRB(0, 0, 0, 30),
                        builder: new DotSwiperPaginationBuilder(
                            color: Colors.white30,
                            activeColor: Colors.white,
                            size: 0.025 * media.size.width,
                            activeSize: 0.035 * media.size.width)),
                    children: <Widget>[
                      Column(),
                      Stack(children: <Widget>[
                        Container(
                          width: media.size.width,
                          height: media.size.height,
                          decoration: BoxDecoration(
                            image: DecorationImage(
                              image:
                                  const AssetImage('assets/LeftSidePool.png'),
                              fit: BoxFit.fill,
                              colorFilter: new ColorFilter.mode(
                                  Colors.black.withOpacity(1.0),
                                  BlendMode.dstIn),
                            ),
                          ),
                        ),
                        Transform.translate(
                            offset: Offset(0.0, 0.4 * media.size.height),
                            child: Container(
                                height: 1 / 5 * media.size.height,
                                width: media.size.width,
                                child: AutoSizeText(
                                  'Welcome $name!',
                                  style: TextStyle(
                                    fontFamily: 'Roboto',
                                    fontSize: media.size.width * 0.1,
                                    color: const Color(0xffffffff),
                                    shadows: [
                                      Shadow(
                                        color: const Color(0xbd000000),
                                        offset: Offset(0, 3),
                                        blurRadius: 6,
                                      ),
                                    ],
                                  ),
                                  maxLines: 1,
                                  textAlign: TextAlign.center,
                                ))),
                        Transform.translate(
                            offset: Offset(0.0, 0.5 * media.size.height),
                            child: Container(
                                height: 1 / 10 * media.size.height,
                                width: media.size.width,
                                child: Text(
                                  'Number of people at $gymName:',
                                  style: TextStyle(
                                    fontFamily: 'Roboto',
                                    fontSize: media.size.width * 0.05,
                                    color: const Color(0xffffffff),
                                    shadows: [
                                      Shadow(
                                        color: const Color(0xbd000000),
                                        offset: Offset(0, 3),
                                        blurRadius: 6,
                                      ),
                                    ],
                                  ),
                                  textAlign: TextAlign.center,
                                ))),
                        Transform.translate(
                            offset: Offset(0.0, 0.56 * media.size.height),
                            child: Container(
                                height: 1 / 10 * media.size.height,
                                width: media.size.width,
                                child: Text(
                                  numberOfPeople,
                                  style: TextStyle(
                                    fontFamily: 'Roboto',
                                    fontSize: media.size.width * 0.05,
                                    color: const Color(0xffffffff),
                                    shadows: [
                                      Shadow(
                                        color: const Color(0xbd000000),
                                        offset: Offset(0, 3),
                                        blurRadius: 6,
                                      ),
                                    ],
                                  ),
                                  textAlign: TextAlign.center,
                                )))
                      ]),
                      Stack(children: <Widget>[
                        Container(
                          width: media.size.width,
                          height: media.size.height,
                          decoration: BoxDecoration(
                            image: DecorationImage(
                              image:
                                  const AssetImage('assets/RightSidePool.png'),
                              fit: BoxFit.fill,
                              colorFilter: new ColorFilter.mode(
                                  Colors.black.withOpacity(1.0),
                                  BlendMode.dstIn),
                            ),
                          ),
                        ),
                        Transform.translate(
                            offset: Offset(0.1 * media.size.width,
                                2.3 / 6 * media.size.height),
                            child: getMenuContainers(0.8, 0.1, media)),
                        Transform.translate(
                            offset: Offset(0.1 * media.size.width,
                                3.3 / 6 * media.size.height),
                            child: getMenuContainers(0.8, 0.1, media)),
                        Transform.translate(
                            offset: Offset(0.15 * media.size.width,
                                2.5 / 6 * media.size.height),
                            child: getMenuOptionText('View my classes', media)),
                        Transform.translate(
                            offset: Offset(0.15 * media.size.width,
                                3.5 / 6 * media.size.height),
                            child: getMenuOptionText('View my profile', media)),
                        Transform.translate(
                            offset: Offset(0.8 * media.size.width,
                                2.5 / 6 * media.size.height),
                            child:
                                getArrow(0.06, media, InstructorViewClasses())),
                        Transform.translate(
                            offset: Offset(0.8 * media.size.width,
                                3.5 / 6 * media.size.height),
                            child: getArrow(0.06, media, ViewMyProfile()))
                      ])
                    ]));
          }
        });
  }

  /*
   Method Name:
    getArrow

   Purpose:
    This method returns a front facing arrow that is used to navigate.
   */
  Widget getArrow(width, media, nextPage) {
    return GestureDetector(
        onTap: () {
          Navigator.push(
            context,
            MaterialPageRoute(builder: (context) => nextPage),
          );
        },
        child: SvgPicture.string(frontArrow,
            width: width * media.size.width, allowDrawingOutsideViewBox: true));
  }

  /*
   Method Name:
    getMenuOptionText

   Purpose:
    This method returns the text of the options.
   */
  Widget getMenuOptionText(text, media) {
    return Text(
      text,
      style: TextStyle(
        fontFamily: 'Roboto',
        fontSize: 0.053 * media.size.width,
        color: const Color(0xfffcfbfc),
      ),
      textAlign: TextAlign.left,
    );
  }

  /*
   Method Name:
    getMenuContainers

   Purpose:
    This method returns the containers that hold the menu options.
   */
  Widget getMenuContainers(width, height, media) {
    return Container(
      width: width * media.size.width,
      height: height * media.size.height,
      decoration: BoxDecoration(
        borderRadius: BorderRadius.circular(19.0),
        color: const Color(0x30ffffff),
        border: Border.all(width: 1.0, color: const Color(0x30707070)),
      ),
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
                  color: const Color(0xffffffff),
                  shadows: [
                    Shadow(
                      color: const Color(0xbd000000),
                      offset: Offset(0, 3),
                      blurRadius: 6,
                    ),
                  ],
                ),
                textAlign: TextAlign.center,
              )));
    } else {
      for (Announcement announce in announcementsList) {
        displayedAnnouncements.insert(0,
            Row(mainAxisAlignment: MainAxisAlignment.center, children: <Widget>[
          Stack(children: <Widget>[
            Container(
              padding: EdgeInsets.all(0.015 * media.size.height),
              width: 0.7 * media.size.width,
              decoration: BoxDecoration(
                borderRadius: BorderRadius.circular(19.0),
                color: const Color(0x26ffffff),
                border: Border.all(width: 1.0, color: const Color(0x26707070)),
              ),
              child: Text(announce.heading + "\n\n\n" + announce.body,
                  style: TextStyle(
                      color: Colors.white, fontSize: 0.035 * media.size.width)),
            ),
            Transform.translate(
                offset: Offset(0.6 * 0.9 * media.size.width,
                    0.3 * 0.15 * media.size.height),
                child: SizedBox(
                    width: 0.7 * media.size.width,
                    child: Text(
                        announce.date.day.toString() +
                            "/" +
                            announce.date.month.toString() +
                            "/" +
                            announce.date.year.toString(),
                        style: TextStyle(
                            color: Colors.white,
                            fontSize: 0.025 * media.size.width)))),
          ])
        ]));

        displayedAnnouncements.insert(0,SizedBox(height: 0.025 * media.size.height));
      }
    }

    return ListView(
        padding: const EdgeInsets.all(15), children: displayedAnnouncements);
  }

  _getAnnouncementsFromDB() async {
    SharedPreferences prefs = await SharedPreferences.getInstance();
    String gymId = prefs.get("gymId").toString();

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

const String frontArrow =
    '<svg viewBox="288.2 250.0 21.8 18.5" ><path transform="translate(282.25, 244.0)" d="M 16.87643432617188 6 L 14.95946311950684 7.633152008056641 L 22.54577445983887 14.10784912109375 L 6 14.10784912109375 L 6 16.42437744140625 L 22.54577445983887 16.42437744140625 L 14.95946216583252 22.89907455444336 L 16.87643432617188 24.5322265625 L 27.75286865234375 15.26611328125 L 16.87643432617188 6 Z" fill="#fcfbfc" stroke="none" stroke-width="1" stroke-miterlimit="4" stroke-linecap="butt" /></svg>';
