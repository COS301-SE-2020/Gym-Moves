/*
File Name
  LogIn.dart

Author:
  Danel

Date Created
  26/06/2020

Update History:
--------------------------------------------------------------------------------
| Date       | Author       | Changes                             
--------------------------------------------------------------------------------
01/01/2020   |    Tia       |  Added outline of login function and LoginRequest 
--------------------------------------------------------------------------------
04/01/2020   |    Tia       |  Fixed login request 
--------------------------------------------------------------------------------

Functional Description:
  This file contains the LogIn class that calls the class that creates the UI.
  The LogInState class handles the building of the UI and making all the
  components functional and responsive.
  This file will also request the database to verify the account exists and then
  redirect the user.

Classes in the File:
- LogIn
- LogInState
 */

import 'package:flutter/material.dart';
import 'package:flutter_svg/flutter_svg.dart';
import 'package:gym_moves/User/InstructorPages.dart';
import 'package:gym_moves/User/ManagerPages.dart';
import 'package:gym_moves/User/MemberPages.dart';

import 'package:gym_moves/User/SignUp.dart';
import 'package:gym_moves/User/ForgotPassword.dart';

import 'dart:convert';
import 'package:http/http.dart' as http;
import 'package:shared_preferences/shared_preferences.dart';

/*
Class Name:
  LogIn

Purpose:
  This class creates the class that will build the page.
 */
class LogIn extends StatefulWidget {
  const LogIn({Key key}) : super(key: key);

  @override
  LogInState createState() => LogInState();
}

/*
Class Name:
  LogInState

Purpose:
  This class will build the page, and request verification to the database.
 */
class LogInState extends State<LogIn> {
  String password = "";
  String username = "";

  final logInFormKey = GlobalKey<FormState>();

  @override
  Widget build(BuildContext context) {
    MediaQueryData media = MediaQuery.of(context);

    final usernameField = Material(
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
                ),
                decoration: InputDecoration(
                    filled: true,
                    fillColor: Colors.white,
                    labelText: 'Username',
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
                    username = value;
                  });
                })),
        borderRadius: BorderRadius.all(Radius.circular(19.0)),
        color: Colors.transparent);

    final passwordField = Material(
        shadowColor: Colors.black,
        elevation: 15,
        child: Container(
            width: 0.7 * media.size.width,
            height: 0.08 * media.size.height,
            child: TextField(
                cursorColor: Colors.black45,
                obscureText: true,
                style: TextStyle(
                  color: Colors.black54,
                ),
                decoration: InputDecoration(
                    filled: true,
                    fillColor: Colors.white,
                    labelText: 'Password',
                    border: InputBorder.none,
                    contentPadding: const EdgeInsets.all(15.0),
                    labelStyle: new TextStyle(color: Colors.black54),
                    enabledBorder: OutlineInputBorder(
                        borderRadius: BorderRadius.all(Radius.circular(19.0)),
                        borderSide: BorderSide.none),
                    focusedBorder: OutlineInputBorder(
                        borderSide: BorderSide.none,
                        borderRadius: BorderRadius.circular(19.0))),
                onChanged: (value) {
                  setState(() {
                    password = value;
                  });
                })),
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
              height: 0.4 * media.size.height,
              decoration: BoxDecoration(
                image: DecorationImage(
                  image: const AssetImage('assets/Bicycles.jpg'),
                  fit: BoxFit.fill,
                  colorFilter: new ColorFilter.mode(
                      Colors.black.withOpacity(0.82), BlendMode.dstIn),
                ),
                boxShadow: [
                  BoxShadow(
                    color: const Color(0x22000000),
                    offset: Offset(0, 3),
                    blurRadius: 6,
                  ),
                ],
              ),
            ),
          ),
          Transform.translate(
            offset: Offset(0.08 * media.size.width, 0.08 * media.size.height),
            child: Text(
              'Gym Moves',
              style: TextStyle(
                fontFamily: 'FreestyleScript',
                fontSize: 0.28 * media.size.width,
                color: const Color(0xffffffff),
              ),
              textAlign: TextAlign.left,
            ),
          ),
        ]),
        SizedBox(height: 10.0),
        Form(
            key: logInFormKey,
            child: Column(children: <Widget>[
              SizedBox(height: 0.02 * media.size.height),
              Stack(children: <Widget>[
                usernameField,
                Transform.translate(
                    offset: Offset(0.7 * 0.85 * media.size.width,
                        0.08 * 0.25 * media.size.height),
                    child: SvgPicture.string(
                      person,
                      width: media.size.width * 0.04,
                      color: Colors.black45,
                      allowDrawingOutsideViewBox: true,
                    ))
              ]),
              SizedBox(height: 0.05 * media.size.height),
              Stack(children: <Widget>[
                passwordField,
                Transform.translate(
                    offset: Offset(0.7 * 0.85 * media.size.width,
                        0.08 * 0.3 * media.size.height),
                    child: SvgPicture.string(
                      lock,
                      width: media.size.width * 0.04,
                      color: Colors.black45,
                      allowDrawingOutsideViewBox: true,
                    ))
              ])
            ])),
        Container(
            padding: EdgeInsets.fromLTRB(0.05 * media.size.height, 0.0,
                0.18 * media.size.width, 0.05 * media.size.height),
            width: media.size.width,
            child: GestureDetector(
                onTap: () {
                  Navigator.push(
                    context,
                    MaterialPageRoute(builder: (context) => ForgotPassword()),
                  );
                },
                child: Text(
                  "Forgot password?",
                  textAlign: TextAlign.right,
                  style: TextStyle(color: Colors.white),
                ))),
        Center(
            child: SizedBox(
                width: 0.25 * media.size.width,
                child: FlatButton(
                  shape: RoundedRectangleBorder(
                      borderRadius: BorderRadius.circular(10.0)),
                  color: const Color(0xffffffff).withOpacity(0.3),
                  onPressed: () {
                    verifyUser(username, password);
                  },
                  textColor: Colors.white,
                  padding: const EdgeInsets.all(0.0),
                  child: Container(
                    padding: const EdgeInsets.all(10.0),
                    child: Text(
                      'Submit',
                      style: TextStyle(
                          fontSize: 0.05 * media.size.width,
                          fontFamily: 'Roboto'),
                    ),
                  ),
                ))),
        SizedBox(height: 30),
        Center(
            child: GestureDetector(
                onTap: () {
                  Navigator.pop(context);
                  Navigator.push(
                    context,
                    MaterialPageRoute(builder: (context) => SignUp()),
                  );
                },
                child: Text.rich(
                  TextSpan(
                      style: TextStyle(
                        fontFamily: 'Roboto',
                        fontSize: 0.04 * media.size.width,
                        color: const Color(0xffffffff),
                      ),
                      children: [
                        TextSpan(
                          text: 'Don\'t have an account? ',
                        ),
                        TextSpan(
                          text: 'Sign up!',
                          style: TextStyle(
                            fontWeight: FontWeight.w800,
                          ),
                        )
                      ]),
                  textAlign: TextAlign.center,
                ))),
        SizedBox(height: 30),
      ]),
    );
  }

  /*
  Method Name: verifyUser

  Purpose: This method is called when the send button is pressed. It verifies
           that the user does exist and what type of user they are.
*/

  verifyUser(username, password) async {
    final http.Response response = await http.post(
      'https://gymmoveswebapi.azurewebsites.net/api/login',
      headers: <String, String>{'Content-Type': 'application/json'},
      body: jsonEncode(
          <String, String>{'username': username, 'password': password}),
    );
    Widget okButton = FlatButton(
          child: Text("OK"), onPressed: () => Navigator.pop(context));

      AlertDialog alert = AlertDialog(
        title: Text("Login Error"),
        content: Text(response.body),
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
    
    LoginResponse res = LoginResponse.fromJson(json.decode(response.body));

    if (res.passwordValid && res.usernameValid) {
      final prefs = await SharedPreferences.getInstance();

      prefs.setInt('gymId', res.gymID);
      prefs.setString('userName', res.name);
      prefs.setInt('type', res.userType);
      Navigator.pop(context);

      if (res.userType == 0) {
        Navigator.push(
          context,
          MaterialPageRoute(builder: (context) => MemberPages()),
        );
      }
      else if(res.userType == 2){
        Navigator.push(
          context,
          MaterialPageRoute(builder: (context) => ManagerPages()),
        );
      } else if(res.userType == 1){
        Navigator.push(
          context,
          MaterialPageRoute(builder: (context) => InstructorPages()),
        );
      }
    } else {
      String errMessage = "";
      if (!res.passwordValid && res.usernameValid) {
        errMessage =
            "Please ensure that the password is correct and try again.";
      } else if (res.passwordValid && !res.usernameValid) {
        errMessage =
            "Please ensure that the username is correct and try again.";
      } else {
        errMessage =
            "Your username or password is incorrect. Please try again.";
      }

      Widget okButton = FlatButton(
          child: Text("OK"), onPressed: () => Navigator.pop(context));

      AlertDialog alert = AlertDialog(
        title: Text("Login Error"),
        content: Text(errMessage),
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
}

/*
Class Name:
  LoginResponse

Purpose:
  This class will be used to parse the response from the api and allow the user to log in.
*/

class LoginResponse {
  final bool usernameValid;
  final bool passwordValid;
  final int gymID;
  final int userType;
  final String gymMemberID;
  final String name;

  LoginResponse(
      {this.usernameValid,
      this.passwordValid,
      this.gymID,
      this.userType,
      this.gymMemberID,
      this.name});

  factory LoginResponse.fromJson(Map<String, dynamic> json) {
    return LoginResponse(
      gymID: json['gymId'],
      gymMemberID: json['gymMemberID'],
      name: json['name'],
      userType: json['userType'],
      usernameValid: json['usernameValid'],
      passwordValid: json['passwordValid'],
    );
  }
}

const String lock =
    '<svg viewBox="292.0 395.0 16.0 17.5" ><path transform="translate(292.0, 395.0)" d="M 14.28200817108154 7.65625 L 13.42508792877197 7.65625 L 13.42508792877197 5.1953125 C 13.42508792877197 2.3310546875 10.99000549316406 0 7.9979248046875 0 C 5.005844116210938 0 2.570761442184448 2.3310546875 2.570761442184448 5.1953125 L 2.570761442184448 7.65625 L 1.713841080665588 7.65625 C 0.7676579356193542 7.65625 0 8.39111328125 0 9.296875 L 0 15.859375 C 0 16.76513671875 0.7676579356193542 17.5 1.713841080665588 17.5 L 14.28200817108154 17.5 C 15.22819137573242 17.5 15.995849609375 16.76513671875 15.995849609375 15.859375 L 15.995849609375 9.296875 C 15.995849609375 8.39111328125 15.22819137573242 7.65625 14.28200817108154 7.65625 Z M 10.56868648529053 7.65625 L 5.427163124084473 7.65625 L 5.427163124084473 5.1953125 C 5.427163124084473 3.83837890625 6.580435276031494 2.734375 7.9979248046875 2.734375 C 9.415414810180664 2.734375 10.56868648529053 3.83837890625 10.56868648529053 5.1953125 L 10.56868648529053 7.65625 Z" fill="#b9a8bf" stroke="none" stroke-width="1" stroke-miterlimit="4" stroke-linecap="butt" /></svg>';

const String person =
    '<svg viewBox="291.0 393.0 16.9 21.1" ><path transform="translate(291.0, 393.0)" d="M 8.433197021484375 10.53529357910156 C 11.09492588043213 10.53529357910156 13.25216770172119 8.17719841003418 13.25216770172119 5.267646789550781 C 13.25216770172119 2.358094692230225 11.09492588043213 0 8.433197021484375 0 C 5.771469116210938 0 3.614227533340454 2.358094930648804 3.614227533340454 5.267646789550781 C 3.614227533340454 8.17719841003418 5.771469116210938 10.53529357910156 8.433197021484375 10.53529357910156 Z M 11.80647563934326 11.85220527648926 L 11.17775058746338 11.85220527648926 C 10.34195995330811 12.27197074890137 9.412049293518066 12.51066112518311 8.433197021484375 12.51066112518311 C 7.454344272613525 12.51066112518311 6.528197765350342 12.27197074890137 5.688642978668213 11.85220527648926 L 5.05991792678833 11.85220527648926 C 2.266421794891357 11.85220527648926 0 14.32964515686035 0 17.38323402404785 L 0 19.09521865844727 C 0 20.18578720092773 0.8094363808631897 21.07058715820313 1.807113766670227 21.07058715820313 L 15.05928134918213 21.07058715820313 C 16.05695915222168 21.07058715820313 16.86639404296875 20.18578720092773 16.86639404296875 19.09521865844727 L 16.86639404296875 17.38323402404785 C 16.86639404296875 14.32964515686035 14.59997272491455 11.85220527648926 11.80647563934326 11.85220527648926 Z" fill="#b9a8bf" stroke="none" stroke-width="1" stroke-miterlimit="4" stroke-linecap="butt" /></svg>';
