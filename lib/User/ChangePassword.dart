import 'dart:convert';

import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';
import 'package:flutter_svg/flutter_svg.dart';
import 'package:http/http.dart' as http;

/*
Class Name:
  AddAClass

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
  AddAClassState

Purpose:
  This class will build the page, and send information to the database.
 */

class ChangePasswordState extends State<ChangePassword> {
  String newPassword = "";
  String oldPassword = "";
  String username = "myusername";

  final changeFormKey = GlobalKey<FormState>();

  /*Url of API*/
  String url = "https://gymmoveswebapi.azurewebsites.net/api/user/";

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

    final currentPasswordField = Material(
        shadowColor: Colors.black,
        elevation: 15,
        child: Container(
            width: 0.7 * media.size.width,
            height: 0.085 * media.size.height,
            padding: EdgeInsets.all(0.01 * media.size.width),
            child: TextFormField(
                obscureText: true,
                cursorColor: Colors.black45,
                style: TextStyle(
                  color: Colors.black54,
                ),
                decoration: InputDecoration(
                    filled: true,
                    fillColor: Colors.white,
                    labelText: 'Current password',
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
                    oldPassword = value;
                  });
                })),
        borderRadius: BorderRadius.all(Radius.circular(19.0)),
        color: Colors.white);

    final newPasswordField = Material(
        shadowColor: Colors.black,
        elevation: 15,
        child: Container(
            width: 0.7 * media.size.width,
            height: 0.085 * media.size.height,
            padding: EdgeInsets.all(0.01 * media.size.width),
            child: TextFormField(
                obscureText: true,
                cursorColor: Colors.black45,
                style: TextStyle(
                  color: Colors.black54,
                ),
                decoration: InputDecoration(
                    filled: true,
                    fillColor: Colors.white,
                    labelText: 'New password',
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
                    newPassword = value;
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
                'Change password',
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
        SizedBox(height: 0.03 * media.size.height),
        Form(
            key: changeFormKey,
            child: Column(children: <Widget>[
              currentPasswordField,
              SizedBox(height: 0.06 * media.size.height),
              newPasswordField,
              SizedBox(height: 0.06 * media.size.height),
            ])),
        Center(
            child: SizedBox(
                width: 0.25 * media.size.width,
                child: FlatButton(
                  shape: RoundedRectangleBorder(
                      borderRadius: BorderRadius.circular(10.0)),
                  color: const Color(0xffffffff).withOpacity(0.3),
                  onPressed: () {
                    sendValuesToDatabase();
                  },
                  textColor: Colors.white,
                  padding: const EdgeInsets.all(0.0),
                  child: Container(
                    padding: const EdgeInsets.all(10.0),
                    child: Text(
                      'Change',
                      style: TextStyle(
                          fontSize: 0.05 * media.size.width,
                          fontFamily: 'Roboto'),
                    ),
                  ),
                ))),
        SizedBox(height: 0.06 * media.size.height),
      ]),
    );
  }

  /*
  Method name:
    sendValuesToDatabase

  Purpose:
    This function will send the updated details to the database.
   */
  sendValuesToDatabase() async {

    bool secure = validateStructure(newPassword);


    if (newPassword == "" || oldPassword == "") {
      _errorDialogue("Please fill in both the fields");
      return;
    }
    else if (secure == false) {
      _errorDialogue("Your new password needs to be 8 characters long, with at "
          "least one special character, one number, one small letter and one "
          "capital letter");
      return;
    }

    final http.Response response = await http.post(
      url + "changepassword",
      headers: <String, String>{
        'Content-Type': 'application/json',
      },
      body: jsonEncode(<String, String>{
        'username': username,
        'oldPassword': oldPassword,
        'newPassword': newPassword,
      }),
    );

    ChangePasswordMessage message =
        ChangePasswordMessage.fromJson(json.decode(response.body));

    if (response.statusCode != 200) {
      _errorDialogue(message.message);
    } else {
      _successDialogue(message.message);
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

  /*
  Method Name: validateStructure
  Purpose: This method validates that the password the user entered, is secure.
*/
  bool validateStructure(String password) {
    String pattern = r'^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[!@#\$&*~]).{8,}$';
    RegExp regExp = new RegExp(pattern);
    return regExp.hasMatch(password);
  }
}

/*
Class Name:


Purpose:

 */
class ChangePasswordMessage {
  final String message;

  ChangePasswordMessage({this.message});

  factory ChangePasswordMessage.fromJson(Map<String, dynamic> json) {
    return ChangePasswordMessage(message: json['message']);
  }
}

const String backArrow =
    '<svg viewBox="28.2 38.0 31.4 27.9" ><path transform="matrix(-1.0, 0.0, 0.0, -1.0, 65.61, 71.93)" d="M 21.68118286132813 6 L 18.91737365722656 8.460894584655762 L 29.85499572753906 18.21720886230469 L 6 18.21720886230469 L 6 21.70783996582031 L 29.85499572753906 21.70783996582031 L 18.91737365722656 31.46415710449219 L 21.68118286132813 33.925048828125 L 37.36236572265625 19.9625244140625 L 21.68118286132813 6 Z" fill="#fcfbfc" stroke="none" stroke-width="1" stroke-miterlimit="4" stroke-linecap="butt" /></svg>';
