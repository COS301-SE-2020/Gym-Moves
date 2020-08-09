/*
File Name:
  MemberClassDetails.dart

Author:
  Raeesa

Date Created:
  07/07/2020

Update History:
--------------------------------------------------------------------------------
Date          |    Author      |     Changes
--------------------------------------------------------------------------------
07/07/2020    |  Raeesa        |    Users can book and unbook classes
--------------------------------------------------------------------------------
12/07/2020    |  Raeesa        |    added class details
--------------------------------------------------------------------------------
09/08/2020    |  Tia           |    added ratings for intstuctor
--------------------------------------------------------------------------------

Functional Description:
  This file implements the BookClassState class. It creates the UI for users
  to be able to see details of a class. It also implements the SendAnnouncement
  class which calls the other class to be built.

Classes in the File:
- BookClass
- BookClassState
 */

import 'dart:convert';

import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';
import 'package:flutter/painting.dart';
import 'package:flutter_svg/flutter_svg.dart';
import 'package:auto_size_text/auto_size_text.dart';
import 'package:http/http.dart';
import 'package:shared_preferences/shared_preferences.dart';
import 'package:http/http.dart' as http;
import 'package:smooth_star_rating/smooth_star_rating.dart';

import 'package:smooth_star_rating/smooth_star_rating.dart';
import 'package:gym_moves/GymClass/MemberViewMyClasses.dart';

/*
Class Name:
  MemberClassDetails

Purpose:
  This class is used to create the BookClassState that builds the UI for
  viewing classes. It allows for widgets to be dynamically added as well.

 */
class MemberClassDetails extends StatefulWidget {
  final String instructor;
  final String classN;
  final String classD;
  final String classT;
  final int availableSpots;
  final String description;
  final int id;

  MemberClassDetails(
      {Key key,
        this.instructor = "",
        this.classN = "",
        this.classD = "",
        this.classT = "",
        this.availableSpots = 0,
        this.description = "",
        this.id = 0})
      : super(key: key);

  @override
  MemberClassDetailsState createState() => MemberClassDetailsState();
}

/*
Class Name:
  MemberClassDetailsState

Purpose:
  This class is used to create the UI for users to see the classes. It also
  handles the request to the API to get the details of a class to display.
 */

class MemberClassDetailsState extends State<MemberClassDetails> {
  MemberClassDetailsState({Key key});

  @override
  void initState() {
    getBookClass();
    _getUserClassStatus();
    super.initState();
    ratingRes = getRating();
  }

  String instructorName = '';
  String className = "";
  String classDay = "";
  String classTime = "";
  String classAvailableSpots = "";
  String classDescription = "";
  int classID;
  double iRating = 0;
  Future<String> ratingRes;
  int numStars = 0;
  double rating = 0.0;
  double cRating =4.5;
  String sRating = "(4.5)";
  String book = "Loading.."
      ".";

  /*
   Method Name:
    build

   Purpose:
    This method builds the UI and also calls the necessary functions that make
    a request to the API to get the class details.
   */
  @override
  Widget build(BuildContext context) {
    MediaQueryData media = MediaQuery.of(context);

    return Scaffold(
        backgroundColor: const Color(0xffffffff),
        body: ListView(children: <Widget>[
          Column(
            children: <Widget>[
              Stack(children: <Widget>[
                Transform.translate(
                    offset: Offset(0.0, -0.033 * media.size.height),
                    child: Container(
                      width: media.size.width,
                      height: media.size.height * 0.4,
                      decoration: BoxDecoration(
                        image: DecorationImage(
                          image: const AssetImage(
                              'assets/ClassDetailsPicture.png'),
                          fit: BoxFit.fill,
                          colorFilter: new ColorFilter.mode(
                              Colors.white.withOpacity(0.6), BlendMode.dstIn),
                        ),
                      ),
                    )),
                Transform.translate(
                    offset: Offset(0.0, -0.033 * media.size.height),
                    child: SizedBox(
                        width: media.size.width,
                        height: media.size.height * 0.4,
                        child: Center(
                            child: Container(
                                width: 0.55 * media.size.width,
                                height: 0.31 * media.size.height,
                                child: Center(
                                    child: AutoSizeText(
                                  className,
                                  style: TextStyle(
                                      fontFamily: 'Last',
                                      fontSize: 0.10 * media.size.width,
                                      color: const Color(0xff3E3E3E),
                                      shadows: [
                                        Shadow(
                                          color: const Color(0x38000000),
                                          offset: Offset(0, 3),
                                          blurRadius: 6,
                                        )
                                      ]),
                                  textAlign: TextAlign.center,
                                  maxLines: 3,
                                )))))),
                                Transform.translate(
                  offset: Offset(0.0, 0.1 * media.size.height),
                  child: SizedBox(
                      width: media.size.width,
                      height: media.size.height * 0.4,
                      child: Center(
                          child: Container(
                              width: 0.55 * media.size.width,
                              height: 0.31 * media.size.height,
                              child: Center(
                                  child: AutoSizeText(
                                    "" + cRating.toString() +"☆",
                                    style: TextStyle(
                                        fontFamily: 'Last',
                                        fontSize: 0.05 * media.size.width,
                                        color: const Color(0xff3E3E3E),
                                        shadows: [
                                          Shadow(
                                            color: const Color(0x38000000),
                                            offset: Offset(0, 3),
                                            blurRadius: 6,
                                          )
                                        ]),
                                    textAlign: TextAlign.center,
                                    maxLines: 3,
                                  ))

                          ))
                  )),
                Transform.translate(
                    offset: Offset(
                        0.04 * media.size.width, 0.02 * media.size.height),
                    child: GestureDetector(
                        onTap: () {
                          Navigator.pop(context);
                          Navigator.pop(context);
                          Navigator.push(
                              context,
                              MaterialPageRoute(
                                  builder: (context) => MemberViewMyClasses()));
                        },
                        child: SvgPicture.string(backArrow,
                            allowDrawingOutsideViewBox: true,
                            width: 0.06 * media.size.width))),
                Transform.translate(
                    offset:
                        Offset(0.2 * media.size.width, 0.6 * media.size.height),
                    child: SvgPicture.string(dumbbell,
                        width: 0.95 * media.size.width * 0.7,
                        height: 0.6 * media.size.height * 0.7,
                        allowDrawingOutsideViewBox: true))
              ]),
                
             Row(  
                children: [ Container(
                padding:
                    EdgeInsets.fromLTRB(0.05 * media.size.width, 0.0, 0.0, 0.0),
                child: Text(
                  'Instructor: ',
                  style: TextStyle(
                      fontFamily: 'Roboto',
                      fontSize: 0.045 * media.size.width,
                      color: const Color(0xff3E3E3E),
                      fontWeight: FontWeight.w800),
                  textAlign: TextAlign.left,
                ),
                alignment: Alignment.centerLeft,
              ),
              Container(
                  padding: EdgeInsets.fromLTRB(0.4 * media.size.width,
                      0.01 * media.size.height, 0.0, 0.0),
                  child: FlatButton(
                  shape: RoundedRectangleBorder(
                      borderRadius: BorderRadius.circular(10.0)),
                  color: const Color(0xff7341E6).withOpacity(0.9),
                  onPressed: () {
                    showRatingDialog();
                  },
                  textColor: Colors.black,
                  padding: const EdgeInsets.all(0.0),
                  child: Container(
                    padding: const EdgeInsets.all(10.0),
                    child: Text(
                      'Rate Instructor',
                      style: TextStyle(
                          fontSize: 0.035 * media.size.width,
                          fontFamily: 'Roboto',
                          color: Colors.white),
                    ),
                  ),
                ))]),

              FutureBuilder<String>(
                  future: ratingRes,
                  builder: (context, snapshot) {
                    if (snapshot.hasData) {
                      return Container(
                          padding: EdgeInsets.fromLTRB(0.0 * media.size.width,
                              0.01 * media.size.height, 0.0* media.size.width, 0.0),
                          child: SmoothStarRating(
                            rating: iRating.roundToDouble(),
                            isReadOnly: true,
                            size: 30,
                            color: Colors.amberAccent,
                            borderColor: Colors.amberAccent,
                            filledIconData: Icons.star,
                            halfFilledIconData: Icons.star_half,
                            defaultIconData: Icons.star_border,
                            starCount: 5,
                            allowHalfRating: true,
                            spacing: 2.0,
                            onRated: (value) {},
                          ));
                    } else if (snapshot.hasError) {
                      return Text("${snapshot.error}");
                    }
                    // By default, show a loading spinner.
                    return Center(
                        child: CircularProgressIndicator(
                      backgroundColor: Colors.white,
                    ));
                  },
                ),
              Container(
                padding: EdgeInsets.fromLTRB(0.05 * media.size.width,
                    0.0 * media.size.height, 0.0, 0.0),
                child: Text(
                  'Day: ',
                  style: TextStyle(
                      fontFamily: 'Roboto',
                      fontSize: 0.045 * media.size.width,
                      color: const Color(0xff3E3E3E),
                      fontWeight: FontWeight.w800),
                  textAlign: TextAlign.left,
                ),
                alignment: Alignment.centerLeft,
              ),
              Container(
                padding: EdgeInsets.fromLTRB(
                    0.1 * media.size.width, 0.01 * media.size.height, 0.0, 0.0),
                child: Text(
                  classDay,
                  style: TextStyle(
                    fontFamily: 'Roboto',
                    fontSize: 0.04 * media.size.width,
                    color: const Color(0xff3E3E3E),
                  ),
                  textAlign: TextAlign.left,
                ),
                alignment: Alignment.centerLeft,
              ),
              Container(
                padding: EdgeInsets.fromLTRB(0.05 * media.size.width,
                    0.05 * media.size.height, 0.0, 0.0),
                child: Text(
                  'Time: ',
                  style: TextStyle(
                      fontFamily: 'Roboto',
                      fontSize: 0.045 * media.size.width,
                      color: const Color(0xff3E3E3E),
                      fontWeight: FontWeight.w800),
                  textAlign: TextAlign.left,
                ),
                alignment: Alignment.centerLeft,
              ),

              Container(
                padding: EdgeInsets.fromLTRB(
                    0.1 * media.size.width, 0.01 * media.size.height, 0.0, 0.0),
                child: Text(
                  classTime,
                  style: TextStyle(
                    fontFamily: 'Roboto',
                    fontSize: 0.04 * media.size.width,
                    color: const Color(0xff3E3E3E),
                  ),
                  textAlign: TextAlign.left,
                ),
                alignment: Alignment.centerLeft,
              ),
              Container(
                padding: EdgeInsets.fromLTRB(0.05 * media.size.width,
                    0.05 * media.size.height, 0.0, 0.0),
                child: Text(
                  'Available Spots: ',
                  style: TextStyle(
                      fontFamily: 'Roboto',
                      fontSize: 0.045 * media.size.width,
                      color: const Color(0xff3E3E3E),
                      fontWeight: FontWeight.w800),
                  textAlign: TextAlign.left,
                ),
                alignment: Alignment.centerLeft,
              ),
              Container(
                padding: EdgeInsets.fromLTRB(
                    0.1 * media.size.width, 0.01 * media.size.height, 0.0, 0.0),
                child: Text(
                  classAvailableSpots,
                  style: TextStyle(
                    fontFamily: 'Roboto',
                    fontSize: 0.04 * media.size.width,
                    color: const Color(0xff3E3E3E),
                  ),
                  textAlign: TextAlign.left,
                ),
                alignment: Alignment.centerLeft,
              ),
              Container(
                padding: EdgeInsets.fromLTRB(0.05 * media.size.width,
                    0.05 * media.size.height, 0.0, 0.0),
                child: Text(
                  'Description: ',
                  style: TextStyle(
                      fontFamily: 'Roboto',
                      fontSize: 0.045 * media.size.width,
                      color: const Color(0xff3E3E3E),
                      fontWeight: FontWeight.w800),
                  textAlign: TextAlign.left,
                ),
                alignment: Alignment.centerLeft,
              ),
              Container(
                padding: EdgeInsets.fromLTRB(
                    0.1 * media.size.width, 0.01 * media.size.height, 0.0, 0.03 * media.size.height),
                child: Text(
                  classDescription,
                  style: TextStyle(
                    fontFamily: 'Roboto',
                    fontSize: 0.04 * media.size.width,
                    color: const Color(0xff3E3E3E),
                  ),
                  textAlign: TextAlign.left,
                ),
                alignment: Alignment.centerLeft,
              ),
              
            Container(
              padding: EdgeInsets.fromLTRB(
                  0.05 * media.size.width, 0.0 * media.size.height, 0.0, 0.0),
              child: Text(
                'Rate this class: ',
                style: TextStyle(
                  fontFamily: 'Roboto',
                    fontSize: 0.045 * media.size.width,
                    color:  const Color(0xff3E3E3E),
                    fontWeight: FontWeight.w800
                ),
                textAlign: TextAlign.left,
              ),
              alignment: Alignment.centerLeft,
            ),

            //Row(children: <Widget>[container, container])
            Row(children: <Widget>[Container(
              padding: EdgeInsets.fromLTRB(
                  0.05 * media.size.width, 0.01 * media.size.height, 0.0, 0.0),
              child: SmoothStarRating(
                rating: rating,
                allowHalfRating: false,
                isReadOnly: false,
                size: 30,
                filledIconData: Icons.star,
                halfFilledIconData: Icons.star_half,
                defaultIconData: Icons.star_border,
                starCount: 5,
                color: Colors.amberAccent,
                borderColor: Colors.amberAccent,
                spacing: 2.0,
                onRated: (value) {
                  rating = value;
                  //sendRating();
                  // print("rating value dd -> ${value.truncate()}");
                },
              ),
              alignment: Alignment.centerLeft,
            ),
            Container(
              //  alignment: Alignment(0.9, 0.5),
                padding: EdgeInsets.fromLTRB(
                   0.18 * media.size.width, 0.00 * media.size.height, 0.0, 0.0),
                child: SizedBox(

                    width: 0.25 * media.size.width,
                    child: FlatButton(
                      shape: RoundedRectangleBorder(
                          borderRadius: BorderRadius.circular(10.0)),
                      color: const Color(0xff7341E6).withOpacity(0.9),
                      onPressed: () {
                        sendRating();
                      },
                      textColor: Colors.white,
                      padding: const EdgeInsets.all(0.0),
                      child: Container(
                        padding: const EdgeInsets.all(10.0),
                        child: Text(
                          'Rate',
                          style: TextStyle(
                              fontSize: 0.038 * media.size.width,
                              fontFamily: 'Roboto'),
                        ),
                      ),
                    )))]),
            Container(
                alignment: Alignment.bottomCenter,
                padding:
                    EdgeInsets.fromLTRB(0.02 * media.size.width, 0.05 * media.size.width, 0.02 * media.size.width, 0.0),
                child: FlatButton(
                  shape: RoundedRectangleBorder(
                      borderRadius: BorderRadius.circular(10.0)),
                  color: const Color(0xff7341E6).withOpacity(0.9),
                  onPressed: () {
                    sendValuesToDatabase();
                  },
                  textColor: Colors.black,
                  padding: const EdgeInsets.all(0.0),
                  child: Container(
                    padding: const EdgeInsets.all(10.0),
                    height: 0.06*media.size.height,
                    child: Text(
                      book,
                      style: TextStyle(
                          fontSize: 0.045 * media.size.width,
                          fontFamily: 'Roboto',
                          color: Colors.white),
                    ),
                  ),
                ),
              ),
            SizedBox(
              height: 0.02 * media.size.height,
            )
        ])]));
  }



sendRating() async {
  String url = 'https://gymmoveswebapi.azurewebsites.net/api/ratings/class';

  SharedPreferences prefs = await SharedPreferences.getInstance();
  String username = prefs.get("username");

  int RT = rating.floor();
  final http.Response response = await http.post(
    url,
    headers: <String, String>{'Content-type': 'application/json'},
    body: jsonEncode({
      "username": username,
      "classId": classID,
      "rating": RT
    }),
  );

  int status = response.statusCode;

  if (status == 200) {
    setState(() {
      _showAlertDialog(
          "Thank you for rating this class, your feedback helps us improve your experience.",
          "Success!");
    });
  } else {
    _showAlertDialog(
        "There's been a problem on our side. Please try again in a few minutes.",
        "Unsuccessful!");
  }
}
  /*
   Method Name:
    getRating

   Purpose:
    This method will send the request to the api to get the instructor's rating.

   */
  Future<String> getRating() async {
    String url =
        'https://gymmoveswebapi.azurewebsites.net/api/ratings/instructor?instructor=$instructorName';
    Response response = await get(url);

    if (response.statusCode == 200) {
      getResponse res = getResponse.fromJson(json.decode(response.body));
      int temp = ((res.ratingSum / res.ratingCount) * 10).truncate();
      iRating = (temp) / 10;
      return response.body;
    } else {
      throw Exception('Failed to retrieve user data. Please try again later');
    }
  }

  /*
   Method Name:
    getBookClass

   Purpose:
    This method will get the details of the class.

   */
  void getBookClass() {
    className = this.widget.classN;
    classAvailableSpots = this.widget.availableSpots.toString();
    instructorName = this.widget.instructor;
    classDescription = this.widget.description;
    classTime = this.widget.classT;
    classDay = this.widget.classD;
    classID = this.widget.id;
    sRating = "(" + cRating.toString() + ")";
  }

  /*
   Method Name:
    getStarsForInstructor

   Purpose:
    This method will get the rating for the specific instructor for the class
    and show the correct stars.

   Extra:
    Rating is currently hardcoded. This will be changed.
   */

  List<Widget> getStarsForInstructor(MediaQueryData media) {
    List<Widget> stars = [];

    int full = 2;
    int half = 1;
    int empty = 2;

    for (int i = full; i > 0; i--) {
      stars.add(SvgPicture.string(
        fullStar,
        height: 0.02 * media.size.height,
        width: 0.02 * media.size.width,
        allowDrawingOutsideViewBox: true,
      ));
    }

    for (int i = half; i > 0; i--) {
      stars.add(SvgPicture.string(
        halfStar,
        height: 0.02 * media.size.height,
        width: 0.02 * media.size.width,
        allowDrawingOutsideViewBox: true,
      ));
    }

    for (int i = empty; i > 0; i--) {
      stars.add(SvgPicture.string(
        emptyStar,
        height: 0.02 * media.size.height,
        width: 0.02 * media.size.width,
        allowDrawingOutsideViewBox: true,
      ));
    }

    return stars;
  }

  /*
   Method Name:
    getStarsForClass

   Purpose:
    This method will get the rating for the specific class and show the
    correct stars.

   Extra: Rating is currently hardcoded. This will be changed.
   */
  List<Widget> getStarsForClass(MediaQueryData media) {
    List<Widget> stars = [];

    int full = 3;
    int half = 1;
    int empty = 1;

    for (int i = full; i > 0; i--) {
      stars.add(SvgPicture.string(
        fullStar,
        height: 0.02 * media.size.height,
        width: 0.02 * media.size.width,
        allowDrawingOutsideViewBox: true,
      ));
    }

    for (int i = half; i > 0; i--) {
      stars.add(SvgPicture.string(
        halfStar,
        height: 0.02 * media.size.height,
        width: 0.02 * media.size.width,
        allowDrawingOutsideViewBox: true,
      ));
    }

    for (int i = empty; i > 0; i--) {
      stars.add(SvgPicture.string(
        emptyStar,
        height: 0.02 * media.size.height,
        width: 0.02 * media.size.width,
        allowDrawingOutsideViewBox: true,
      ));
    }

    return stars;
  }

  sendValuesToDatabase() async {
    /*Get user name from local storage */
    SharedPreferences prefs = await SharedPreferences.getInstance();
    String username = prefs.get("username");

    if (book == "Book Class") {
      String url =
          'https://gymmoveswebapi.azurewebsites.net/api/classes/signup';
      final http.Response response = await http.post(
        url,
        headers: <String, String>{'Content-type': 'application/json'},
        body: jsonEncode({
          "Username": username,
          "ClassId": classID,
        }),
      );

      if (response.statusCode == 200) {
        setState(() {
          book = "Unbook Class";
          classAvailableSpots = (int.parse(classAvailableSpots) - 1).toString();
          _showAlertDialog(
              "Success!", "You've successfully enrolled for this class.");
        });
      } else {
        _showAlertDialog("Oh no!", response.body);
      }
    } else if (book == "Unbook Class") {
      String url =
          'https://gymmoveswebapi.azurewebsites.net/api/classes/deregister';

      final http.Response response = await http.post(
        url,
        headers: <String, String>{'Content-type': 'application/json'},
        body: jsonEncode({
          "Username": username,
          "ClassId": classID,
        }),
      );

      if (response.statusCode == 200) {
        setState(() {
          book = "Book Class";
          classAvailableSpots = (int.parse(classAvailableSpots) + 1).toString();
          _showAlertDialog(
              "Success!", "You've successfully un-enrolled for this class");
        });
      } else {
        _showAlertDialog("Oh no!", response.body);
      }
    }
  }

  void showRatingDialog() {
    // set up the button
    Widget okButton = FlatButton(
      child: Text("Ok", style: TextStyle(color: Color(0xff513369))),
      onPressed: () {
        sendRating();
        Navigator.pop(context);
      },
    );

    AlertDialog alert = AlertDialog(
      title: Text('Rate $instructorName'),
      content: SmoothStarRating(
        rating: 0,
        isReadOnly: false,
        size: 40,
        filledIconData: Icons.star,
        halfFilledIconData: Icons.star_half,
        defaultIconData: Icons.star_border,
        starCount: 5,
        color: Colors.amberAccent,
        borderColor: Colors.amberAccent,
        allowHalfRating: false,
        spacing: 2.0,
        onRated: (value) {
          numStars = value.truncate();
          print("rating value -> $value");
        },
      ),
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

  void _showAlertDialog(String heading, String body) {
    // set up the button
    Widget okButton = FlatButton(
      child: Text("Ok", style: TextStyle(color: Color(0xff513369))),
      onPressed: () {
        Navigator.pop(context);
      },
    );

    AlertDialog alert = AlertDialog(
      title: Text(heading),
      content: Text(body),
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

  /*
   Method Name:
    _getUserClassStatus

   Purpose:
    This function checks to see if the user is signed up for the class, and sets
    their state accordingly, so see if they should be able to book or unbook for
    a class.
   */
  _getUserClassStatus() async {
    classID = this.widget.id;
    String classes = classID.toString();
    SharedPreferences prefs = await SharedPreferences.getInstance();
    String username = prefs.get("username");

    String user = "username=" + username;
    String userClass = "&classid=" + classes;
    String url =
        'https://gymmoveswebapi.azurewebsites.net/api/classes/userclass?' +
            user +
            userClass;

    Response response = await get(url);

    if (response.statusCode == 200 && response.body == "true") {
      setState(() {
        book = "Unbook Class";
      });
    } else if (response.statusCode == 200) {
      setState(() {
        book = "Book Class";
      });
    } else {
      _showAlertDialog("Oh no!", response.body);
    }
  }
}

class getResponse {
  final int ratingCount;
  final String instructor;
  final int ratingSum;

  getResponse({this.ratingCount, this.instructor, this.ratingSum});

  factory getResponse.fromJson(Map<String, dynamic> json) {
    return getResponse(
        instructor: json['instructor'],
        ratingCount: json['ratingCount'],
        ratingSum: json['ratingSum']);
  }
}

const String backArrow =
    '<svg viewBox="28.2 38.0 31.4 27.9" ><path transform="matrix(-1.0, 0.0, 0.0, -1.0, 65.61, 71.93)" d="M 21.68118286132813 6 L 18.91737365722656 8.460894584655762 L 29.85499572753906 18.21720886230469 L 6 18.21720886230469 L 6 21.70783996582031 L 29.85499572753906 21.70783996582031 L 18.91737365722656 31.46415710449219 L 21.68118286132813 33.925048828125 L 37.36236572265625 19.9625244140625 L 21.68118286132813 6 Z" fill="#7341E6" stroke="none" stroke-width="1" stroke-miterlimit="4" stroke-linecap="butt" /></svg>';
const String dumbbell =
    '<svg viewBox="105.7 62.6 148.0 151.2" ><path transform="translate(104.73, 61.61)" d="M 52.92843627929688 140.3509826660156 C 54.10280227661133 141.6522674560547 54.0328369140625 143.6774139404297 52.77159881591797 144.8908081054688 L 45.95822906494141 151.4024047851563 C 44.69138336181641 152.6053619384766 42.71061325073242 152.5321807861328 41.53205490112305 151.2389068603516 L 1.853297233581543 107.5332946777344 C 0.6797584295272827 106.2311325073242 0.7511200904846191 104.2059478759766 2.0132737159729 102.9934539794922 L 8.836055755615234 96.49147796630859 C 10.1022424697876 95.28672790527344 12.08445072174072 95.35995483398438 13.26223087310791 96.65498352050781 L 52.92843627929688 140.3509826660156 Z M 103.5581436157227 58.11441802978516 C 104.731689453125 59.41657638549805 104.6603317260742 61.44176483154297 103.3981552124023 62.65425872802734 L 60.51661682128906 103.5961990356445 C 59.24977874755859 104.7991409301758 57.26900863647461 104.7259674072266 56.09044647216797 103.4326858520508 L 45.85470962524414 92.14398956298828 C 44.68115997314453 90.84183502197266 44.75253295898438 88.816650390625 46.01469421386719 87.60414886474609 L 88.88682556152344 46.66221237182617 C 89.49431610107422 46.08388519287109 90.30185699462891 45.77615356445313 91.13150787353516 45.80682754516602 C 91.96115875244141 45.83749389648438 92.74484252929688 46.20405197143555 93.30986022949219 46.82572174072266 L 103.5581436157227 58.11441040039063 Z M 67.51820373535156 126.4140396118164 C 68.69140625 127.7061004638672 68.61925506591797 129.7483978271484 67.35822296142578 130.9538726806641 L 60.53544616699219 137.4622650146484 C 59.26925277709961 138.6670227050781 57.28704452514648 138.5937805175781 56.10926818847656 137.2987518310547 L 16.45247077941895 93.59315490722656 C 15.27809715270996 92.29185485839844 15.34806632995605 90.26671600341797 16.60931587219238 89.05331420898438 L 23.41954040527344 82.54812622070313 C 24.68638038635254 81.34517669677734 26.66715812683105 81.41834259033203 27.84571838378906 82.71163177490234 L 67.51820373535156 126.4140396118164 Z M 133.5908966064453 59.68219757080078 C 134.7636108398438 60.98521423339844 134.6908416748047 63.01043319702148 133.4277801513672 64.22203063964844 L 126.6081466674805 70.73042297363281 C 125.3410949707031 71.93457794189453 123.3593826293945 71.86284637451172 122.178840637207 70.57012939453125 L 82.51576232910156 26.8741512298584 C 81.33965301513672 25.57351112365723 81.40966796875 23.54692459106445 82.67259216308594 22.33430862426758 L 89.51105499267578 15.80988693237305 C 90.11854553222656 15.23155879974365 90.92609405517578 14.923828125 91.75575256347656 14.95449733734131 C 92.58540344238281 14.98516750335693 93.36907958984375 15.35172557830811 93.93411254882813 15.97339820861816 L 133.5908966064453 59.68219757080078 Z M 148.1587066650391 45.7677116394043 C 149.3308715820313 47.07159805297852 149.2595672607422 49.09637832641602 147.9987182617188 50.31076431274414 L 141.1916198730469 56.81594085693359 C 140.5845489501953 57.39468002319336 139.7772369384766 57.70297622680664 138.9475860595703 57.67290496826172 C 138.1179351806641 57.6428337097168 137.3340148925781 57.2768669128418 136.7685852050781 56.65563583374023 L 97.11178588867188 12.9404239654541 C 95.93878173828125 11.63739967346191 96.00868225097656 9.612658500671387 97.26863098144531 8.39737606048584 L 104.0820007324219 1.879366874694824 C 104.6881790161133 1.300024747848511 105.4951324462891 0.9912708401679993 106.3245315551758 1.02135181427002 C 107.1539154052734 1.051432132720947 107.9374313354492 1.417869567871094 108.5018997192383 2.039670705795288 L 148.1587066650391 45.7677116394043 Z" fill="#513369" fill-opacity="0.08" stroke="none" stroke-width="1" stroke-opacity="0.08" stroke-miterlimit="4" stroke-linecap="butt" /></svg>';
const String emptyStar =
    '<svg viewBox="283.0 336.1 13.8 11.4" ><path transform="translate(280.7, 332.75)" d="M 15.56942558288574 7.33497953414917 L 11.02351665496826 7.33497953414917 L 9.642127990722656 3.666498184204102 C 9.574139595031738 3.487749099731445 9.385628700256348 3.375 9.172393798828125 3.375 C 8.959158897399902 3.375 8.770648002624512 3.487749099731445 8.702659606933594 3.666498184204102 L 7.321271896362305 7.33497953414917 L 2.744456768035889 7.33497953414917 L 2.744456768035889 7.33497953414917 C 2.472505569458008 7.33497953414917 2.25 7.532978534698486 2.25 7.774977207183838 C 2.25 7.799726963043213 2.253090381622314 7.827226161956787 2.259271144866943 7.849226474761963 C 2.265451908111572 7.945476055145264 2.314897298812866 8.052726745605469 2.466324806213379 8.159975051879883 L 6.202564239501953 10.50296401977539 L 4.76863956451416 14.21269416809082 C 4.69756031036377 14.39144325256348 4.76863956451416 14.59219169616699 4.938608169555664 14.70769119262695 C 5.028228759765625 14.76543998718262 5.111668586730957 14.81494140625 5.216740608215332 14.81494140625 C 5.318722724914551 14.81494140625 5.43924617767334 14.76819038391113 5.525775909423828 14.71594047546387 L 9.172393798828125 12.4032039642334 L 12.81901168823242 14.71594047546387 C 12.90554237365723 14.77094078063965 13.02606678009033 14.81494140625 13.12804698944092 14.81494140625 C 13.23311996459961 14.81494140625 13.31655883789063 14.76819038391113 13.40308952331543 14.70769119262695 C 13.57614898681641 14.59219169616699 13.64413642883301 14.39419174194336 13.57305908203125 14.21269416809082 L 12.13913440704346 10.50296211242676 L 15.8444709777832 8.137975692749023 L 15.93408966064453 8.069225311279297 C 16.01443862915039 7.992225170135498 16.09478759765625 7.887726306915283 16.09478759765625 7.774976253509521 C 16.09478759765625 7.532978534698486 15.84137916564941 7.33497953414917 15.56942558288574 7.33497953414917 Z M 11.63849544525146 9.878715515136719 C 11.3294620513916 10.07671546936035 11.19966602325439 10.43421363830566 11.32328033447266 10.75321197509766 L 12.2534761428833 13.16494941711426 C 12.29365062713623 13.26669883728027 12.16385555267334 13.35194778442383 12.06496524810791 13.29144859313965 L 9.673030853271484 11.7734546661377 C 9.521602630615234 11.67720603942871 9.345453262329102 11.63045692443848 9.169302940368652 11.63045692443848 C 8.993152618408203 11.63045692443848 8.81700325012207 11.67720603942871 8.668665885925293 11.7734546661377 L 6.276731491088867 13.28870010375977 C 6.177840232849121 13.35194778442383 6.048046112060547 13.26394844055176 6.08821964263916 13.16219902038574 L 7.018416404724121 10.75045967102051 C 7.142030715942383 10.42596435546875 7.012235641479492 10.0684642791748 6.70011043548584 9.870464324951172 L 4.19692325592041 8.300224304199219 C 4.098031044006348 8.239723205566406 4.147477149963379 8.09947395324707 4.268001556396484 8.09947395324707 L 7.318181037902832 8.09947395324707 C 7.689023017883301 8.09947395324707 8.019691467285156 7.887726306915283 8.137125015258789 7.574227809906006 L 9.051870346069336 5.148740291595459 C 9.088953971862793 5.049739837646484 9.249652862548828 5.049739837646484 9.286736488342285 5.148740291595459 L 10.20148181915283 7.574227809906006 C 10.31891536712646 7.887726306915283 10.649582862854 8.09947395324707 11.02042579650879 8.09947395324707 L 11.02042579650879 8.09947395324707 L 14.02733898162842 8.09947395324707 C 14.14786338806152 8.09947395324707 14.19730854034424 8.236974716186523 14.09841728210449 8.297473907470703 L 11.63849544525146 9.878715515136719 Z" fill="#ffff50" stroke="none" stroke-width="1" stroke-miterlimit="4" stroke-linecap="butt" /></svg>';
const String fullStar =
    '<svg viewBox="323.2 330.6 13.8 11.4" ><path transform="translate(320.18, 327.59)" d="M 9.920000076293945 12.19414806365967 L 14.19656085968018 14.44000053405762 L 13.06168079376221 10.207200050354 L 16.84000015258789 7.35924243927002 L 11.86452007293701 6.991957664489746 L 9.920000076293945 3 L 7.975480079650879 6.991957664489746 L 3 7.35924243927002 L 6.778319835662842 10.207200050354 L 5.643439769744873 14.44000053405762 L 9.920000076293945 12.19414806365967 Z" fill="#ffff50" stroke="none" stroke-width="1" stroke-miterlimit="4" stroke-linecap="butt" /></svg>';
const String halfStar =
    '<svg viewBox="279.2 324.6 13.8 11.4" ><path transform="translate(276.18, 321.59)" d="M 16.84000015258789 7.35924243927002 L 11.86452007293701 6.985937118530273 L 9.920000076293945 3 L 7.975480079650879 6.991957664489746 L 3 7.35924243927002 L 6.778319835662842 10.207200050354 L 5.643439769744873 14.44000053405762 L 9.920000076293945 12.19414806365967 L 14.19656085968018 14.44000053405762 L 13.06860065460205 10.207200050354 L 16.84000015258789 7.35924243927002 Z M 9.920000076293945 11.06821155548096 L 9.920000076293945 5.468631744384766 L 11.10332012176514 7.90113639831543 L 14.13428020477295 8.129936218261719 L 11.83684062957764 9.863999366760254 L 12.52884006500244 12.44101047515869 L 9.920000076293945 11.06821155548096 Z" fill="#ffff50" stroke="none" stroke-width="1" stroke-miterlimit="4" stroke-linecap="butt" /></svg>';
