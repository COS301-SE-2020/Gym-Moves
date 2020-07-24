/*
File Name
  ForgotPassword.dart

Author:
  Raeesa

Date Created
  27/06/2020

Update History:
--------------------------------------------------------------------------------
 Name               | Date              | Changes
--------------------------------------------------------------------------------
 Raeesa             | 28/06/2020        | Made UI responsive and functional
--------------------------------------------------------------------------------
 Danel              | 30/06/2020        | Fixed hard coded values and added
                                        | functions
--------------------------------------------------------------------------------
 Danel              | 15/07/2020        | Added hashing
--------------------------------------------------------------------------------


Functional Description:
  This file contains the Forget password functionality, which enables a user to
  change their respective passwords.
  The ForgotPasswordState class handles the building of the UI and making all the
  components functional and responsive.
  This file will also handle sending the information that is entered to change the
  password in the database.

Classes in the File:
- ForgotPassword
- ForgotPasswordState
 */

import 'dart:convert';

import 'package:flutter/material.dart';
import 'package:flutter_svg/flutter_svg.dart';
import 'package:http/http.dart' as http;
import 'package:crypto/crypto.dart';

class ForgotPassword extends StatefulWidget {
  ForgotPassword({
    Key key,
  }) : super(key: key);

  @override
  ForgotPasswordState createState() => ForgotPasswordState();
}

/*
Class Name:
  ForgotPasswordState
Purpose:
  This class will build the page, and send information to the database.
 */

class ForgotPasswordState extends State<ForgotPassword> {
  String password = "";
  String code = "";
  String username = "";
  bool hidePassword = true;

  String url = "https://gymmoveswebapi.azurewebsites.net/api/user/";

  @override
  Widget build(BuildContext context) {
    MediaQueryData media = MediaQuery.of(context);

    final usernameField = Material(
        shadowColor: Colors.black,
        elevation: 15,
        child: Container(
            width: 0.7 * media.size.width,
            height: 0.085 * media.size.height,
            padding: EdgeInsets.all(0.01 * media.size.width),
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
        color: Colors.white);

    final passwordField = Material(
        shadowColor: Colors.black,
        elevation: 15,
        child: Container(
            width: 0.7 * media.size.width,
            height: 0.085 * media.size.height,
            padding: EdgeInsets.all(0.01 * media.size.width),
            child: TextField(
                cursorColor: Colors.black45,
                obscureText: hidePassword,
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
                        borderSide: BorderSide.none
                    ),
                    focusedBorder: OutlineInputBorder(
                        borderSide: BorderSide.none,
                        borderRadius: BorderRadius.circular(19.0))
                ),
                onChanged: (value) {
                  setState(() {
                    password = value;
                  });
                })
        ),
        borderRadius: BorderRadius.all(Radius.circular(19.0)),
        color: Colors.white
    );

    final codeField = Material(
        shadowColor: Colors.black,
        elevation: 15,
        child: Container(
            width: 0.7 * media.size.width,
            height: 0.085 * media.size.height,
            padding: EdgeInsets.all(0.01 * media.size.width),
            child: TextField(
                cursorColor: Colors.black45,
                style: TextStyle(
                  color: Colors.black54,
                ),
                decoration: InputDecoration(
                    filled: true,
                    fillColor: Colors.white,
                    labelText: 'Code',
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
                    code = value;
                  });
                })),
        borderRadius: BorderRadius.all(Radius.circular(19.0)),
        color: Colors.white);

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
                              Colors.black.withOpacity(0.52), BlendMode.dstIn
                          ),
                        ),
                        boxShadow: [
                          BoxShadow(
                            color: const Color(0x46000000),
                            offset: Offset(0, 3),
                            blurRadius: 6,
                          )
                        ])
                )
            ),
            Container(
              width: media.size.width,
              child: Text(
                'Gym Moves ',
                style: TextStyle(
                  fontFamily: 'FreestyleScript',
                  fontSize: 0.13 * media.size.width,
                  color: const Color(0xffffffff),
                ),
                textAlign: TextAlign.right,
              ),
            ),
            Transform.translate(
                offset:
                    Offset(0.04 * media.size.width, 0.03 * media.size.height),
                child: GestureDetector(
                    onTap: () {
                      Navigator.pop(context);
                    },
                    child: SvgPicture.string(backArrow,
                        allowDrawingOutsideViewBox: true,
                        width: 0.06 * media.size.width)
                )
            )
          ]),
          Container(
            width: media.size.width*0.95,
            child: Text(
              'We will send a code to your email, please enter it in order to '
                  'reset your password.',
              style: TextStyle(
                fontFamily: 'Roboto',
                fontSize: 0.045 * media.size.width,
                color: const Color(0xffffffff),
                fontWeight: FontWeight.w300,
              ),
              textAlign: TextAlign.center,
            ),
          ),
          Form(
            child: Column(
              children: <Widget>[
                SizedBox(height: 0.1 * media.size.height),
                Stack(children: <Widget>[
                  usernameField,
                  Transform.translate(
                      offset: Offset(0.7 * 0.85 * media.size.width,
                          0.08 * 0.3 * media.size.height),
                      child: SvgPicture.string(
                        person,
                        width: media.size.width * 0.04,
                        color: Colors.black45,
                        allowDrawingOutsideViewBox: true,
                      ))
                ]),
                SizedBox(height: 0.03 * media.size.height),
                Center(
                    child: SizedBox(
                        width: 0.25 * media.size.width,
                        child: FlatButton(
                          shape: RoundedRectangleBorder(
                              borderRadius: BorderRadius.circular(10.0)),
                          color: const Color(0xffffffff).withOpacity(0.3),
                          onPressed: () {
                            getCode();
                          },
                          textColor: Colors.white,
                          padding: const EdgeInsets.all(0.0),
                          child: Container(
                            padding: const EdgeInsets.all(10.0),
                            child: Text(
                              'Get Code',
                              style: TextStyle(
                                  fontSize: 0.04 * media.size.width,
                                  fontFamily: 'Roboto'),
                            ),
                          ),
                        ))),
                SizedBox(height: 0.08 * media.size.height),
                Stack(children: <Widget>[
                  codeField,
                  Transform.translate(
                      offset: Offset(0.7 * 0.85 * media.size.width,
                          0.08 * 0.3 * media.size.height),
                      child: SvgPicture.string(
                        keypad,
                        width: media.size.width * 0.035,
                        color: Colors.black45,
                        allowDrawingOutsideViewBox: true,
                      ))
                ]),
                SizedBox(height: 0.025 * media.size.height),
                Stack(children: <Widget>[
                  passwordField,
                  Transform.translate(
                      offset: Offset(0.7 * 0.85 * media.size.width,
                          0.08 * 0.3 * media.size.height
                      ),
                      child: GestureDetector(
                          onTap: () {
                            setState(() {
                              hidePassword = !hidePassword;
                            });
                          },
                          child: Icon(
                            hidePassword
                                ? Icons.visibility_off
                                : Icons.visibility,
                            color: Colors.grey,
                          ))
                  )
                ]),
                SizedBox(height: 0.03 * media.size.height),
                Center(
                    child: SizedBox(
                        width: 0.38 * media.size.width,
                        child: FlatButton(
                          shape: RoundedRectangleBorder(
                              borderRadius: BorderRadius.circular(10.0)
                          ),
                          color: const Color(0xffffffff).withOpacity(0.3),
                          onPressed: () {
                            forgotPassword();
                          },
                          textColor: Colors.white,
                          padding: const EdgeInsets.all(0.0),
                          child: Container(
                            padding: const EdgeInsets.all(10.0),
                            child: Text(
                              'Change password',
                              style: TextStyle(
                                  fontSize: 0.04 * media.size.width,
                                  fontFamily: 'Roboto'
                              ),
                            ),
                          ),
                        )
                    )
                ),
              ],
            ),
          )
        ])
    );
  }

  getCode() async {
    final http.Response response = await http.get(url + "getCode?username=" +
        username);

    if(response.statusCode != 200){
      _errorDialogue(response.body);
    }
    else{
      _successDialogue("Code sent to your email successfully.");
    }
  }

  forgotPassword() async {

    var bytes = utf8.encode(password);
    var hashPassword = sha256.convert(bytes);

    final http.Response response = await http.post(
      url + "forgotPassword",
      headers: <String, String>{
        'Content-Type': 'application/json',
      },
      body: jsonEncode(
          {'username': username, 'code': code, 'password': hashPassword.toString()}),
    );

    if (response.statusCode == 200) {
      _successDialogue(response.body);
    } else {
      _errorDialogue(response.body);
    }
  }

  /*
   Method Name:
    _errorDialogue

   Purpose:
     This method shows a dialogue if there was an error with getting or sending
     information from or to the database.
   */
  _errorDialogue(text) async {
    return showDialog<void>(
      context: context,
      barrierDismissible: false,
      builder: (BuildContext context) {
        return AlertDialog(
          title: Text('Oh no!'),
          content: Text(text),
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

  _successDialogue(text) async {
    return showDialog<void>(
      context: context,
      barrierDismissible: false,
      builder: (BuildContext context) {
        return AlertDialog(
          title: Text('Great news!'),
          content: Text(text),
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


const String backArrow =
    '<svg viewBox="28.2 38.0 31.4 27.9" ><path transform="matrix(-1.0, 0.0, 0.0, -1.0, 65.61, 71.93)" d="M 21.68118286132813 6 L 18.91737365722656 8.460894584655762 L 29.85499572753906 18.21720886230469 L 6 18.21720886230469 L 6 21.70783996582031 L 29.85499572753906 21.70783996582031 L 18.91737365722656 31.46415710449219 L 21.68118286132813 33.925048828125 L 37.36236572265625 19.9625244140625 L 21.68118286132813 6 Z" fill="#fcfbfc" stroke="none" stroke-width="1" stroke-miterlimit="4" stroke-linecap="butt" /></svg>';
const String person =
    '<svg viewBox="291.0 393.0 16.9 21.1" ><path transform="translate(291.0, 393.0)" d="M 8.433197021484375 10.53529357910156 C 11.09492588043213 10.53529357910156 13.25216770172119 8.17719841003418 13.25216770172119 5.267646789550781 C 13.25216770172119 2.358094692230225 11.09492588043213 0 8.433197021484375 0 C 5.771469116210938 0 3.614227533340454 2.358094930648804 3.614227533340454 5.267646789550781 C 3.614227533340454 8.17719841003418 5.771469116210938 10.53529357910156 8.433197021484375 10.53529357910156 Z M 11.80647563934326 11.85220527648926 L 11.17775058746338 11.85220527648926 C 10.34195995330811 12.27197074890137 9.412049293518066 12.51066112518311 8.433197021484375 12.51066112518311 C 7.454344272613525 12.51066112518311 6.528197765350342 12.27197074890137 5.688642978668213 11.85220527648926 L 5.05991792678833 11.85220527648926 C 2.266421794891357 11.85220527648926 0 14.32964515686035 0 17.38323402404785 L 0 19.09521865844727 C 0 20.18578720092773 0.8094363808631897 21.07058715820313 1.807113766670227 21.07058715820313 L 15.05928134918213 21.07058715820313 C 16.05695915222168 21.07058715820313 16.86639404296875 20.18578720092773 16.86639404296875 19.09521865844727 L 16.86639404296875 17.38323402404785 C 16.86639404296875 14.32964515686035 14.59997272491455 11.85220527648926 11.80647563934326 11.85220527648926 Z" fill="#b9a8bf" stroke="none" stroke-width="1" stroke-miterlimit="4" stroke-linecap="butt" /></svg>';
const String lock =
    '<svg viewBox="281.0 481.0 16.0 17.5" ><path transform="translate(281.0, 481.0)" d="M 14.28200817108154 7.65625 L 13.42508792877197 7.65625 L 13.42508792877197 5.1953125 C 13.42508792877197 2.3310546875 10.99000549316406 0 7.9979248046875 0 C 5.005844116210938 0 2.570761442184448 2.3310546875 2.570761442184448 5.1953125 L 2.570761442184448 7.65625 L 1.713841080665588 7.65625 C 0.7676579356193542 7.65625 0 8.39111328125 0 9.296875 L 0 15.859375 C 0 16.76513671875 0.7676579356193542 17.5 1.713841080665588 17.5 L 14.28200817108154 17.5 C 15.22819137573242 17.5 15.995849609375 16.76513671875 15.995849609375 15.859375 L 15.995849609375 9.296875 C 15.995849609375 8.39111328125 15.22819137573242 7.65625 14.28200817108154 7.65625 Z M 10.56868648529053 7.65625 L 5.427163124084473 7.65625 L 5.427163124084473 5.1953125 C 5.427163124084473 3.83837890625 6.580435276031494 2.734375 7.9979248046875 2.734375 C 9.415414810180664 2.734375 10.56868648529053 3.83837890625 10.56868648529053 5.1953125 L 10.56868648529053 7.65625 Z" fill="#b9a8bf" stroke="none" stroke-width="1" stroke-miterlimit="4" stroke-linecap="butt" /></svg>';
const String keypad =
    '<svg viewBox="297.0 417.0 15.0 23.6" ><path transform="translate(290.25, 414.75)" d="M 14.2520751953125 21.52135848999023 C 13.20647430419922 21.52135848999023 12.35311222076416 22.48361015319824 12.35311222076416 23.66145324707031 C 12.35311222076416 24.83929061889648 13.20647430419922 25.80154418945313 14.2520751953125 25.80154418945313 C 15.29767799377441 25.80154418945313 16.15103912353516 24.83929061889648 16.15103912353516 23.66145324707031 C 16.15103912353516 22.48887062072754 15.29767799377441 21.52135848999023 14.2520751953125 21.52135848999023 Z M 8.64896297454834 2.25 C 7.6033616065979 2.25 6.75 3.212253332138062 6.75 4.390093803405762 C 6.75 5.567933559417725 7.6033616065979 6.530187129974365 8.64896297454834 6.530187129974365 C 9.694564819335938 6.530187129974365 10.54792594909668 5.567934036254883 10.54792594909668 4.390093803405762 C 10.54792594909668 3.212252855300903 9.694564819335938 2.25 8.64896297454834 2.25 Z M 8.64896297454834 8.675539016723633 C 7.6033616065979 8.675539016723633 6.75 9.637791633605957 6.75 10.81563282012939 C 6.75 11.99347400665283 7.6033616065979 12.95572662353516 8.64896297454834 12.95572662353516 C 9.694564819335938 12.95572662353516 10.54792594909668 11.99347400665283 10.54792594909668 10.81563282012939 C 10.54792594909668 9.637791633605957 9.694564819335938 8.675539016723633 8.64896297454834 8.675539016723633 Z M 8.64896297454834 15.10107707977295 C 7.6033616065979 15.10107707977295 6.75 16.06333160400391 6.75 17.24117088317871 C 6.75 18.41901206970215 7.6033616065979 19.38126564025879 8.64896297454834 19.38126564025879 C 9.694564819335938 19.38126564025879 10.54792594909668 18.41901206970215 10.54792594909668 17.24117088317871 C 10.54792594909668 16.06333160400391 9.694564819335938 15.10107707977295 8.64896297454834 15.10107707977295 Z M 19.85518836975098 6.535445690155029 C 20.90078926086426 6.535445690155029 21.754150390625 5.573192119598389 21.754150390625 4.395352363586426 C 21.754150390625 3.217511653900146 20.90078926086426 2.25 19.85518836975098 2.25 C 18.8095874786377 2.25 17.95622444152832 3.212253332138062 17.95622444152832 4.390093803405762 C 17.95622444152832 5.567933559417725 18.8095874786377 6.535445690155029 19.85518836975098 6.535445690155029 Z M 14.2520751953125 15.10107707977295 C 13.20647430419922 15.10107707977295 12.35311222076416 16.06333160400391 12.35311222076416 17.24117088317871 C 12.35311222076416 18.41901206970215 13.20647430419922 19.38126564025879 14.2520751953125 19.38126564025879 C 15.29767799377441 19.38126564025879 16.15103912353516 18.41901206970215 16.15103912353516 17.24117088317871 C 16.15103912353516 16.06333160400391 15.29767799377441 15.10107707977295 14.2520751953125 15.10107707977295 Z M 19.85518836975098 15.10107707977295 C 18.8095874786377 15.10107707977295 17.95622444152832 16.06333160400391 17.95622444152832 17.24117088317871 C 17.95622444152832 18.41901206970215 18.8095874786377 19.38126564025879 19.85518836975098 19.38126564025879 C 20.90078926086426 19.38126564025879 21.754150390625 18.41901206970215 21.754150390625 17.24117088317871 C 21.754150390625 16.06333160400391 20.90078926086426 15.10107707977295 19.85518836975098 15.10107707977295 Z M 19.85518836975098 8.675539016723633 C 18.8095874786377 8.675539016723633 17.95622444152832 9.637791633605957 17.95622444152832 10.81563282012939 C 17.95622444152832 11.99347400665283 18.8095874786377 12.95572662353516 19.85518836975098 12.95572662353516 C 20.90078926086426 12.95572662353516 21.754150390625 11.99347400665283 21.754150390625 10.81563282012939 C 21.754150390625 9.637791633605957 20.90078926086426 8.675539016723633 19.85518836975098 8.675539016723633 Z M 14.2520751953125 8.675539016723633 C 13.20647430419922 8.675539016723633 12.35311222076416 9.637791633605957 12.35311222076416 10.81563282012939 C 12.35311222076416 11.99347400665283 13.20647430419922 12.95572662353516 14.2520751953125 12.95572662353516 C 15.29767799377441 12.95572662353516 16.15103912353516 11.99347400665283 16.15103912353516 10.81563282012939 C 16.15103912353516 9.637791633605957 15.29767799377441 8.675539016723633 14.2520751953125 8.675539016723633 Z M 14.2520751953125 2.25 C 13.20647430419922 2.25 12.35311222076416 3.212253332138062 12.35311222076416 4.390093803405762 C 12.35311222076416 5.567933559417725 13.20647430419922 6.530187129974365 14.2520751953125 6.530187129974365 C 15.29767799377441 6.530187129974365 16.15103912353516 5.567933559417725 16.15103912353516 4.390093803405762 C 16.15103912353516 3.212253332138062 15.29767799377441 2.25 14.2520751953125 2.25 Z" fill="#b9a8bf" stroke="none" stroke-width="1" stroke-miterlimit="4" stroke-linecap="butt" /></svg>';
