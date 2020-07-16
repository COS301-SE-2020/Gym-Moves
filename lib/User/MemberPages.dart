/*
File Name
  MemberPages.dart
Author:
  Danel
Date Created
  27/06/2020
Update History:
--------------------------------------------------------------------------------
| Name               | Date              | Changes                             |
--------------------------------------------------------------------------------
Functional Description:
  This file contains the MemberPages class that handles building the UI for
  the Welcome page and the menu. It also implements the scroll screen the user
  will first encounter. It also shows the announcements to users.
Classes in the File:
- MemberPages
- MemberPagesState
 */

import 'package:flutter/material.dart';
import 'package:flutter_svg/flutter_svg.dart';
import 'package:auto_size_text/auto_size_text.dart';

import 'package:gym_moves/Dashboard/ViewPredictions.dart';
import 'package:gym_moves/GymClass/ViewMyClassesMember.dart';
import 'package:gym_moves/User/ViewMyProfile.dart';
import 'package:gym_moves/GymClass/ViewAllClassesMember.dart';

import 'package:shared_preferences/shared_preferences.dart';

/*
Class Name:
  MemberPages
Purpose:
  This class creates the class that will build the page. It allows for dynamic
  adding of elements onto the welcome page.
 */
class MemberPages extends StatefulWidget {
  const MemberPages({Key key}) : super(key: key);

  @override
  MemberPagesState createState() => MemberPagesState();
}

/*
Class Name:
  MemberPagesState
Purpose:
  This class builds the UI for the scrollable welcome and menu page for members.
  It also ensures all the menu options are functional and that the UI is
  responsive to the screen size. The welcome page will also show the amount of
  people at the gym and the name of the user. This class will also handles this.
  It also ensures announcements are shown on the welcome page.
 */
class MemberPagesState extends State<MemberPages> {
  MemberPagesState({Key key});

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

  @override
  void initState() {
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
                body: PageView(controller: controller, children: <Widget>[
                  Column(),
                  Stack(children: <Widget>[
                    Container(
                      width: media.size.width,
                      height: media.size.height,
                      decoration: BoxDecoration(
                        image: DecorationImage(
                          image: const AssetImage('assets/LeftSidePool.png'),
                          fit: BoxFit.fill,
                          colorFilter: new ColorFilter.mode(
                              Colors.black.withOpacity(1.0), BlendMode.dstIn
                          ),
                        ),
                      ),
                    ),
                    Transform.translate(
                        offset: Offset(0.0, 0.4 * media.size.height),
                        child: Container(
                            height: 1 / 5 * media.size.height,
                            width: media.size.width,
                            child: AutoSizeText('Welcome $name!',
                                style: TextStyle(
                                    fontFamily: 'Roboto',
                                    fontSize: 42,
                                    color: const Color(0xffffffff),
                                    shadows: [
                                      Shadow(
                                        color: const Color(0xbd000000),
                                        offset: Offset(0, 3),
                                        blurRadius: 6,
                                      )
                                    ]),
                                maxLines: 1,
                                textAlign: TextAlign.center)
                        )
                    ),
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
                            )
                        )
                    ),
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
                            )
                        )
                    )
                  ]
                  ),
                  Stack(children: <Widget>[
                    Container(
                      width: media.size.width,
                      height: media.size.height,
                      decoration: BoxDecoration(
                        image: DecorationImage(
                          image: const AssetImage('assets/RightSidePool.png'),
                          fit: BoxFit.fill,
                          colorFilter: new ColorFilter.mode(
                              Colors.black.withOpacity(1.0), BlendMode.dstIn),
                        ),
                      ),
                    ),
                    Transform.translate(
                        offset:
                        Offset(0.1 * media.size.width, 1.8 / 6 * media.size.height),
                        child: getMenuContainers(0.8, 0.1, media)),
                    Transform.translate(
                        offset:
                        Offset(0.1 * media.size.width, 2.8 / 6 * media.size.height),
                        child: getMenuContainers(0.8, 0.1, media)),
                    Transform.translate(
                        offset:
                        Offset(0.1 * media.size.width, 3.8 / 6 * media.size.height),
                        child: getMenuContainers(0.8, 0.1, media)),
                    Transform.translate(
                        offset:
                        Offset(0.15 * media.size.width, 2 / 6 * media.size.height),
                        child: getMenuOptionText('View classes', media)),
                    Transform.translate(
                        offset:
                        Offset(0.15 * media.size.width, 3 / 6 * media.size.height),
                        child: getMenuOptionText('View predictions', media)),
                    Transform.translate(
                        offset:
                        Offset(0.15 * media.size.width, 4 / 6 * media.size.height),
                        child: getMenuOptionText('View my profile', media)),
                    Transform.translate(
                        offset:
                        Offset(0.8 * media.size.width, 2 / 6 * media.size.height),
                        child: getArrow(0.05, media, ViewAllClassesMember())),
                    Transform.translate(
                        offset:
                        Offset(0.8 * media.size.width, 3 / 6 * media.size.height),
                        child: getArrow(0.05, media, ViewPredictions())),
                    Transform.translate(
                        offset:
                        Offset(0.8 * media.size.width, 4 / 6 * media.size.height),
                        child: getArrow(0.05, media, ViewMyProfile())),
                  ])
                ]));
          } else {
            return Scaffold(
                backgroundColor: const Color(0xff513369),
                body: PageView(controller: controller, children: <Widget>[
                  Column(),
                  Stack(children: <Widget>[
                    Container(
                      width: media.size.width,
                      height: media.size.height,
                      decoration: BoxDecoration(
                        image: DecorationImage(
                          image: const AssetImage('assets/LeftSidePool.png'),
                          fit: BoxFit.fill,
                          colorFilter: new ColorFilter.mode(
                              Colors.black.withOpacity(1.0), BlendMode.dstIn
                          ),
                        ),
                      ),
                    ),
                    Transform.translate(
                        offset: Offset(0.0, 0.4 * media.size.height),
                        child: Container(
                            height: 1 / 5 * media.size.height,
                            width: media.size.width,
                            child: AutoSizeText('Welcome $name!',
                                style: TextStyle(
                                    fontFamily: 'Roboto',
                                    fontSize: 42,
                                    color: const Color(0xffffffff),
                                    shadows: [
                                      Shadow(
                                        color: const Color(0xbd000000),
                                        offset: Offset(0, 3),
                                        blurRadius: 6,
                                      )
                                    ]),
                                maxLines: 1,
                                textAlign: TextAlign.center)
                        )
                    ),
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
                            )
                        )
                    ),
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
                            )
                        )
                    )
                  ]
                  ),
                  Stack(children: <Widget>[
                    Container(
                      width: media.size.width,
                      height: media.size.height,
                      decoration: BoxDecoration(
                        image: DecorationImage(
                          image: const AssetImage('assets/RightSidePool.png'),
                          fit: BoxFit.fill,
                          colorFilter: new ColorFilter.mode(
                              Colors.black.withOpacity(1.0), BlendMode.dstIn),
                        ),
                      ),
                    ),
                    Transform.translate(
                        offset:
                        Offset(0.1 * media.size.width, 1.8 / 6 * media.size.height),
                        child: getMenuContainers(0.8, 0.1, media)),
                    Transform.translate(
                        offset:
                        Offset(0.1 * media.size.width, 2.8 / 6 * media.size.height),
                        child: getMenuContainers(0.8, 0.1, media)),
                    Transform.translate(
                        offset:
                        Offset(0.1 * media.size.width, 3.8 / 6 * media.size.height),
                        child: getMenuContainers(0.8, 0.1, media)),
                    Transform.translate(
                        offset:
                        Offset(0.15 * media.size.width, 2 / 6 * media.size.height),
                        child: getMenuOptionText('View classes', media)),
                    Transform.translate(
                        offset:
                        Offset(0.15 * media.size.width, 3 / 6 * media.size.height),
                        child: getMenuOptionText('View predictions', media)),
                    Transform.translate(
                        offset:
                        Offset(0.15 * media.size.width, 4 / 6 * media.size.height),
                        child: getMenuOptionText('View my profile', media)),
                    Transform.translate(
                        offset:
                        Offset(0.8 * media.size.width, 2 / 6 * media.size.height),
                        child: getArrow(0.05, media, ViewMyClassesMember())),
                    Transform.translate(
                        offset:
                        Offset(0.8 * media.size.width, 3 / 6 * media.size.height),
                        child: getArrow(0.05, media, ViewPredictions())),
                    Transform.translate(
                        offset:
                        Offset(0.8 * media.size.width, 4 / 6 * media.size.height),
                        child: getArrow(0.05, media, ViewMyProfile())),
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
    getAnnouncements
   Purpose:
    This method returns a listview that as all the announcements.
   Extra:
    There is one hard coded announcement block. This will change once
    implemented properly.
   */
  ListView getAnnouncements(media) {
    return ListView(children: <Widget>[
      Center(
        child: Container(
          width: 0.8 * media.size.width,
          height: 0.25 * media.size.height,
          decoration: BoxDecoration(
            borderRadius: BorderRadius.circular(19.0),
            color: const Color(0xffffffff).withOpacity(0.1),
            border: Border.all(width: 1.0, color: const Color(0x30707070)),
          ),
        ),
      )
    ]);
  }
}

const String frontArrow =
    '<svg viewBox="288.2 250.0 21.8 18.5" ><path transform="translate(282.25, 244.0)" d="M 16.87643432617188 6 L 14.95946311950684 7.633152008056641 L 22.54577445983887 14.10784912109375 L 6 14.10784912109375 L 6 16.42437744140625 L 22.54577445983887 16.42437744140625 L 14.95946216583252 22.89907455444336 L 16.87643432617188 24.5322265625 L 27.75286865234375 15.26611328125 L 16.87643432617188 6 Z" fill="#fcfbfc" stroke="none" stroke-width="1" stroke-miterlimit="4" stroke-linecap="butt" /></svg>';
