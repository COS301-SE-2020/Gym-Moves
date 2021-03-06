/*
File Name
  ChangePassword.dart

Author:
  Danel

Date Created
  30/06/2020

Update History:
--------------------------------------------------------------------------------
| Name               | Date              | Changes
--------------------------------------------------------------------------------
  Danel              |  15/07/2020       | Added hashing
--------------------------------------------------------------------------------


Functional Description:
  This file is the screen that the user can change their password with.

Classes in the File:
- ChangePassword
- ChangePasswordState
 */

import 'dart:convert';
import 'package:crypto/crypto.dart';
import 'package:http/http.dart' as http;
import 'package:flutter/material.dart';
import 'package:flutter/cupertino.dart';
import 'package:flutter_svg/flutter_svg.dart';
import 'package:shared_preferences/shared_preferences.dart';

/*
Class Name:
  ChangePassword

Purpose:
  This class creates the class that will build the page.
 */
class ChangePassword extends StatefulWidget {
  const ChangePassword({Key key}) : super(key: key);

  @override
  ChangePasswordState createState() => ChangePasswordState();
}

/*
Class Name:
  ChangePasswordState

Purpose:
  This class will build the page, and send information to the database.
 */
class ChangePasswordState extends State<ChangePassword> {
  String newPassword = "";
  String oldPassword = "";
  String username = "";

  final changeFormKey = GlobalKey<FormState>();

  String url = "https://gymmoveswebapi.azurewebsites.net/api/user/";

  bool hideOldPassword = true;
  bool hideNewPassword = true;

  /*
   Method Name:
    build

   Purpose:
    This method builds the UI and also calls the necessary functions that send
    the new class details to the database.
   */
  @override
  Widget build(BuildContext context) {
    MediaQueryData media = MediaQuery.of(context);

    final currentPasswordField = Container(
        width: 0.7 * media.size.width,
        height: 0.085 * media.size.height,
        alignment: Alignment.centerLeft,
        child: TextField(
            cursorColor: Color(0xff787878),
            obscureText: hideOldPassword,
            style: TextStyle(
              color: Color(0xff787878),
            ),
            decoration: InputDecoration(
                filled: true,
                fillColor: Colors.white,
                labelText: 'Current Password',
                contentPadding: const EdgeInsets.all(20.0),
                labelStyle: new TextStyle(color: Color(0xff787878)),
                enabledBorder: OutlineInputBorder(
                    borderRadius: BorderRadius.all(Radius.circular(15.0)),
                    borderSide: new BorderSide(color: Color(0xff787878))),
                focusedBorder: OutlineInputBorder(
                    borderSide: new BorderSide(color: Color(0xff787878)),
                    borderRadius: BorderRadius.circular(15.0))),
            onChanged: (value) {
              setState(() {
                oldPassword = value;
              });
            }));

    final newPasswordField = Container(
        width: 0.7 * media.size.width,
        height: 0.085 * media.size.height,
        alignment: Alignment.centerLeft,
        child: TextField(
            cursorColor: Color(0xff787878),
            obscureText: hideNewPassword,
            style: TextStyle(
              color: Color(0xff787878),
            ),
            decoration: InputDecoration(
                filled: true,
                fillColor: Colors.white,
                labelText: 'New Password',
                contentPadding: const EdgeInsets.all(20.0),
                labelStyle: new TextStyle(color: Color(0xff787878)),
                enabledBorder: OutlineInputBorder(
                    borderRadius: BorderRadius.all(Radius.circular(15.0)),
                    borderSide: new BorderSide(color: Color(0xff787878))),
                focusedBorder: OutlineInputBorder(
                    borderSide: new BorderSide(color: Color(0xff787878)),
                    borderRadius: BorderRadius.circular(15.0))),
            onChanged: (value) {
              setState(() {
                newPassword = value;
              });
            }));

    return Scaffold(
      backgroundColor: const Color(0xffffffff),
      body: ListView(children: <Widget>[
        Stack(children: <Widget>[
          Transform.translate(
              offset: Offset(0.1 * media.size.width, 0.0),
              child: Container(
                  alignment: Alignment.center,
                  width: media.size.width * 0.8,
                  height: 0.4 * media.size.height,
                  decoration: BoxDecoration(
                      image: DecorationImage(
                    image: const AssetImage('assets/ForgotPasswordPicture.png'),
                    fit: BoxFit.fill,
                  )))),
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
                          width: 0.06 * media.size.width,
                          color: const Color(0xff7341E6)))))
        ]),
        Container(
            alignment: Alignment.topCenter,
            width: media.size.width,
            child: Text('Change password',
                style: TextStyle(
                  fontFamily: 'Lastwaerk',
                  fontSize: 0.08 * media.size.width,
                  color: const Color(0xff3e3e3e),
                ))),
        SizedBox(height: 0.05 * media.size.height),
        Form(
            key: changeFormKey,
            child: Column(children: <Widget>[
              Stack(children: <Widget>[
                currentPasswordField,
                Transform.translate(
                    offset: Offset(0.7 * 0.85 * media.size.width,
                        0.08 * 0.3 * media.size.height),
                    child: GestureDetector(
                        onTap: () {
                          setState(() {
                            hideOldPassword = !hideOldPassword;
                          });
                        },
                        child: Icon(
                          hideOldPassword
                              ? Icons.visibility_off
                              : Icons.visibility,
                          color: Colors.grey,
                        ))),
              ]),
              SizedBox(height: 0.06 * media.size.height),
              Stack(children: <Widget>[
                newPasswordField,
                Transform.translate(
                    offset: Offset(0.7 * 0.85 * media.size.width,
                        0.08 * 0.3 * media.size.height),
                    child: GestureDetector(
                        onTap: () {
                          setState(() {
                            hideNewPassword = !hideNewPassword;
                          });
                        },
                        child: Icon(
                          hideNewPassword
                              ? Icons.visibility_off
                              : Icons.visibility,
                          color: Colors.grey,
                        )))
              ]),
              SizedBox(height: 0.06 * media.size.height),
            ])),
        Center(
            child: SizedBox(
                width: 0.4 * media.size.width,
                child: FlatButton(
                    shape: RoundedRectangleBorder(
                        borderRadius: BorderRadius.circular(10.0)),
                    color: const Color(0xff7341E6),
                    onPressed: () {
                      changePassword();
                    },
                    textColor: Colors.white,
                    padding: const EdgeInsets.all(0.0),
                    child: Container(
                        padding: const EdgeInsets.all(10.0),
                        child: Text('Change',
                            style: TextStyle(
                                fontSize: 0.04 * media.size.width,
                                fontFamily: 'Roboto')))))),
        SizedBox(height: 0.06 * media.size.height),
      ]),
    );
  }

  /*
  Method name:
    changePassword

  Purpose:
    This function will send the updated details to the database.
   */
  changePassword() async {
    final prefs = await SharedPreferences.getInstance();
    username = await prefs.get("username");

    bool secure = validateStructure(newPassword);

    if (newPassword == "" || oldPassword == "") {
      _errorDialogue("Please fill in both the fields");
      return;
    } else if (secure == false) {
      _errorDialogue("Your new password needs to be 8 characters long, with at "
          "least one special character, one number, one small letter and one "
          "capital letter");
      return;
    }

    var bytesOld = utf8.encode(oldPassword);
    var hashPasswordOld = sha256.convert(bytesOld);

    var bytesNew = utf8.encode(newPassword);
    var hashPasswordNew = sha256.convert(bytesNew);

    final http.Response response = await http.post(
      url + "changePassword?username=" + username,
      headers: <String, String>{
        'Content-Type': 'application/json',
      },
      body: jsonEncode(<String, String>{
        'username': username,
        'oldPassword': hashPasswordOld.toString(),
        'newPassword': hashPasswordNew.toString()
      }),
    );

    if (response.statusCode != 200) {
      _errorDialogue(response.body);
    } else {
      _successDialogue(response.body);
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
                style: TextStyle(color: Color(0xff7341E6)),
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
                style: TextStyle(color: Color(0xff7341E6)),
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

  /*
  Method Name: validateStructure
  Purpose: This method validates that the password the user entered, is secure.
*/
  bool validateStructure(String password) {
    String pattern =
        r'^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[!@#\$&*~]).{8,}$';
    RegExp regExp = new RegExp(pattern);
    return regExp.hasMatch(password);
  }
}

const String backArrow =
    '<svg viewBox="28.2 38.0 31.4 27.9" ><path transform="matrix(-1.0, 0.0, 0.0, -1.0, 65.61, 71.93)" d="M 21.68118286132813 6 L 18.91737365722656 8.460894584655762 L 29.85499572753906 18.21720886230469 L 6 18.21720886230469 L 6 21.70783996582031 L 29.85499572753906 21.70783996582031 L 18.91737365722656 31.46415710449219 L 21.68118286132813 33.925048828125 L 37.36236572265625 19.9625244140625 L 21.68118286132813 6 Z" fill="#fcfbfc" stroke="none" stroke-width="1" stroke-miterlimit="4" stroke-linecap="butt" /></svg>';
