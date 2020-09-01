/*
File Name
  Welcome.dart
Author:
  Danel
Date Created
  06/08/2020
Update History:
--------------------------------------------------------------------------------
| Name               | Date              | Changes                             |
--------------------------------------------------------------------------------
Raeesa                 08/08/2020          Added number of people in gym in real
                                            time, using firebase
 --------------------------------------------------------------------------------
Functional Description:

Classes in the File:
-
 */

import 'package:flutter/material.dart';
import 'package:auto_size_text/auto_size_text.dart';
import 'dart:convert';
import 'package:firebase_database/firebase_database.dart';
import 'package:shared_preferences/shared_preferences.dart';
import 'package:http/http.dart';

/*
Class Name:
  ManagerPages
Purpose:
  This class creates the class that will build the page. It allows for dynamic
  adding of elements onto the welcome page.
 */
class Welcome extends StatefulWidget {
  const Welcome({
    Key key,
  }) : super(key: key);

  @override
  WelcomeState createState() => WelcomeState();
}


/* This creates a database reference.*/
FirebaseDatabase database = new FirebaseDatabase();
DatabaseReference _userRef=database.reference().child('users');
/*
Class Name:
  ManagerPagesState
Purpose:
  This class builds the UI for the scrollable welcome and menu page for managers.
  It also ensures all the menu options are functional and that the UI is
  responsive to the screen size. The welcome page will also show the amount of
  people at the gym and the name of the user. This class will also handles this.
 */
class WelcomeState extends State<Welcome> {
  WelcomeState({Key key});

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

  /*
   Method Name: numUsers
   Purpose:
    This method fetches the number of people from the firebase database in real time
   */
  void numUsers() async{
    SharedPreferences prefs = await SharedPreferences.getInstance();
    int gymid = prefs.get('gymId');
    String gym = gymid.toString();
    String gymID = "gym" + gym;
    _userRef.child("uizCT8uR8oWSKgOIiVYy/count/" + gymID)
        .onValue.listen((event) {
      var snapshot = event.snapshot;
      setState(() {
        if(snapshot.value!=null)
        numberOfPeople = snapshot.value.toString();
        else
          numberOfPeople="0";
      });
    });
  }


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
    numUsers();
    return new FutureBuilder(
        future: local,
        builder: (BuildContext context, AsyncSnapshot snapshot) {
          if (snapshot.connectionState == ConnectionState.done) {
            return Scaffold(
                backgroundColor: const Color(0xffffffff),
                body: new Column(children: <Widget>[
                  Container(
                    alignment: Alignment.center,
                    width: media.size.width * 0.8,
                    height: 0.4 * media.size.height,
                    decoration: BoxDecoration(
                      image: DecorationImage(
                        image:
                        const AssetImage('assets/MenuPicture.png'),
                        fit: BoxFit.fill,
                      ),
                    ),
                  ),
                  Container(
                      width: media.size.width,
                      child: AutoSizeText('Welcome $name!',
                          style: TextStyle(
                            fontFamily: 'Lastwaerk',
                            fontSize: 42,
                            color: const Color(0xff3e3e3e),
                          ),
                          maxLines: 1,
                          textAlign: TextAlign.center)),
                  Container(
                      padding: EdgeInsets.all(10),
                      width: media.size.width,
                      child: Text(
                        'Number of people at $gymName:',
                        style: TextStyle(
                          fontFamily: 'Roboto',
                          fontSize: media.size.width * 0.05,
                          color: const Color(0xff3e3e3e),
                        ),
                        textAlign: TextAlign.center,
                      )),
                  Container(
                      width: media.size.width,
                      child: Text(
                        numberOfPeople,
                        style: TextStyle(
                          fontFamily: 'Digital',
                          fontSize: media.size.width * 0.2,
                          color: const Color(0xff7341E6),
                        ),
                        textAlign: TextAlign.center,
                      ))
                ])
            );
          } else {
            return Scaffold(
                backgroundColor: const Color(0xff513369),
                body: new Column(children: <Widget>[
                  Stack(children: <Widget>[
                    Container(
                      width: media.size.width,
                      height: media.size.height,
                      decoration: BoxDecoration(
                        image: DecorationImage(
                          image:
                          const AssetImage('assets/MenuPicture.png'),
                          fit: BoxFit.fill,
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
                                textAlign: TextAlign.center))),
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
                  ])
                ]));
          }
        });
  }

}