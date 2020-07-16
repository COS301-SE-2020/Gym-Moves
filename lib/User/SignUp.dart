/*
File Name
  SignUp.dart
Author:
  Raeesa
Date Created
  15/06/2020
Update History:
--------------------------------------------------------------------------------
 Name               | Date         | Changes
--------------------------------------------------------------------------------
 Danel              | 24/06/2020   | Made UI responsive and functional
--------------------------------------------------------------------------------
 Danel              | 26/06/2020   | Added autocomplete field
--------------------------------------------------------------------------------
Raeesa              | 27/06/2020   | Added error messages, form
                    |              | validation and dialog boxes
--------------------------------------------------------------------------------
 Raeesa             | 28/06/2      | Added API and database functionality
--------------------------------------------------------------------------------
 Raeesa             | 03/07/2020   | Changed API
--------------------------------------------------------------------------------
 Raeesa             | 04/07/2020   | Added welcome page redirect and local
                                   |   storage
--------------------------------------------------------------------------------
 Danel              | 05/07/2020   | Added autocomplete
--------------------------------------------------------------------------------

Functional Description:
  This file contains the SignUp class that creates the class that creates the
  UI. The SignUpState class handles the building of the UI and making all the
  components functional and responsive.
  This file will also handle sending the information that is entered to the
  database to verify if they can create an account.

Classes in the File:
  - SignUp
  - SignUpState
  - User
  - Gym
 */

import 'package:flutter/material.dart';
import 'package:flutter_svg/flutter_svg.dart';
import 'package:autocomplete_textfield/autocomplete_textfield.dart';
import 'package:shared_preferences/shared_preferences.dart';
import 'package:gym_moves/User/MemberPages.dart';
import 'package:gym_moves/User/LogIn.dart';
import 'package:gym_moves/User/ManagerPages.dart';
import 'package:gym_moves/User/InstructorPages.dart';
import 'package:http/http.dart';
import 'package:http/http.dart' as http;
import 'dart:convert';

/*Class Name:
  SignUp

Purpose:
  This class creates the class that will build the page. It ensures state
  remains, so that when the keyboard closes the for fields do not clear.
 */
class SignUp extends StatefulWidget {
  const SignUp({
    Key key,
  }) : super(key: key);

  @override
  SignUpState createState() => SignUpState();
}

/*Class Name:
  Gym
Purpose:
  This class interprets the Json object of the gyms, received from the API.
 */
class Gym {
  final int gymId;
  final String gymName;
  final String gymBranch;

  Gym({this.gymId, this.gymName, this.gymBranch});

  factory Gym.fromJson(Map<String, dynamic> json) {
    return Gym(
        gymId: json['gymId'],
        gymName: json['gymName'],
        gymBranch: json['gymBranch']);
  }
}

/*
Class Name:
  SignUpState
Purpose:
  This class will build the page, and send information to the database.
 */

class SignUpState extends State<SignUp> {
  String gymMemberId = "";
  String password = "";
  String gym = "";
  String username = "";

  final signUpFormKey = GlobalKey<FormState>();

  List<Gym> gyms = [];
  int arrLength = 0;

  @override
  void initState() {
    _makeGetRequest();
    super.initState();
  }

  AutoCompleteTextField searchTextField;
  GlobalKey<AutoCompleteTextFieldState<Gym>> gymsKey = new GlobalKey();

  /*
  Method Name:
    _makeGetRequest

  Purpose:
     This method is used to make a get request and fetch the different gym's
    and their branches. This list will be used for the auto-complete field, "Gym".
*/
  _makeGetRequest() async {
    String url = 'https://gymmoveswebapi.azurewebsites.net/api/gym/getall';
    Response response = await get(url);
    String responseBody = response.body;

    List<dynamic> gymsJson = json.decode(responseBody);
    arrLength = gymsJson.length;

    for (int i = 0; i < arrLength; i++) {
      gyms.add(Gym.fromJson(gymsJson[i]));
    }
  }

  /*
   Method Name:
    build

   Purpose:
    This method builds the UI for the screen for a user to sign up. It calls the
    necessary function in order to send the data to the database. If the sign up
    is successful, the user will be redirected to their relevant home screen.
   */
  @override
  Widget build(BuildContext context) {
    MediaQueryData media = MediaQuery.of(context);

    final gymIdField = Material(
        shadowColor: Colors.black,
        elevation: 15,
        child: Container(
            width: 0.7 * media.size.width,
            height: 0.085 * media.size.height,
            alignment: Alignment.centerLeft,
            child: TextField(
                cursorColor: Colors.black45,
                obscureText: false,
                style: TextStyle(
                  color: Colors.black54,
                ),
                decoration: InputDecoration(
                    filled: true,
                    fillColor: Colors.white,
                    labelText: 'Member ID',
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
                    gymMemberId = value;
                  });
                })),
        borderRadius: BorderRadius.all(Radius.circular(19.0)),
        color: Colors.white);

    final usernameField = Material(
        shadowColor: Colors.black,
        elevation: 15,
        child: Container(
            width: 0.7 * media.size.width,
            height: 0.085 * media.size.height,
            alignment: Alignment.centerLeft,
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
            alignment: Alignment.centerLeft,
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
        color: Colors.white);

    searchTextField = AutoCompleteTextField<Gym>(
      style: TextStyle(
        color: Colors.black54,
      ),
      itemBuilder: (context, item) {
        return Row(
          mainAxisAlignment: MainAxisAlignment.spaceBetween,
          children: <Widget>[
            Text(
              item.gymName + ", " + item.gymBranch,
              style: TextStyle(fontSize: 0.04 * media.size.width),
            )
          ],
        );
      },
      itemFilter: (item, query) {
        return item.gymName.toLowerCase().startsWith(query.toLowerCase());
      },
      itemSorter: (a, b) {
        return a.gymName.compareTo(b.gymName);
      },
      itemSubmitted: (item) {
        setState(() => searchTextField.textField.controller.text =
            item.gymName + ", " + item.gymBranch);
        gym = item.gymName + ", " + item.gymBranch;
      },
      key: gymsKey,
      suggestions: gyms,
      clearOnSubmit: false,
      decoration: InputDecoration(
          labelText: 'Gym',
          labelStyle: new TextStyle(color: Colors.black54),
          border: InputBorder.none,
          enabledBorder: OutlineInputBorder(borderSide: BorderSide.none)),
    );

    final gymField = Material(
        shadowColor: Colors.black,
        elevation: 15,
        child: Container(
            padding: EdgeInsets.all(0.01 * media.size.width),
            width: 0.7 * media.size.width,
            height: 0.085 * media.size.height,
            alignment: Alignment.centerLeft,
            child: searchTextField),
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
        SizedBox(height: 0.04 * media.size.height),
        Form(
            key: signUpFormKey,
            child: Column(children: <Widget>[
              Stack(children: <Widget>[
                gymIdField,
                Transform.translate(
                    offset: Offset(0.7 * 0.85 * media.size.width,
                        0.08 * 0.3 * media.size.height),
                    child: SvgPicture.string(
                      idCard,
                      width: media.size.width * 0.06,
                      color: Colors.black45,
                      allowDrawingOutsideViewBox: true,
                    ))
              ]),
              SizedBox(height: 0.06 * media.size.height),
              Stack(children: <Widget>[
                gymField,
                Transform.translate(
                    offset: Offset(0.7 * 0.85 * media.size.width,
                        0.08 * 0.3 * media.size.height),
                    child: SvgPicture.string(
                      dumbbell,
                      height: 0.04 * media.size.height,
                      width: 0.04 * media.size.width,
                      color: Colors.black45,
                      allowDrawingOutsideViewBox: true,
                    ))
              ]),
              SizedBox(height: 0.06 * media.size.height),
              Stack(children: <Widget>[
                usernameField,
                Transform.translate(
                    offset: Offset(0.7 * 0.85 * media.size.width,
                        0.08 * 0.25 * media.size.height),
                    child: SvgPicture.string(
                      person,
                      width: media.size.width * 0.05,
                      color: Colors.black45,
                      allowDrawingOutsideViewBox: true,
                    ))
              ]),
              SizedBox(height: 0.06 * media.size.height),
              Stack(children: <Widget>[
                passwordField,
                Transform.translate(
                    offset: Offset(0.7 * 0.85 * media.size.width,
                        0.08 * 0.3 * media.size.height),
                    child: SvgPicture.string(
                      lock,
                      width: media.size.width * 0.05,
                      color: Colors.black45,
                      allowDrawingOutsideViewBox: true,
                    ))
              ]),
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
                      'Submit',
                      style: TextStyle(
                          fontSize: 0.05 * media.size.width,
                          fontFamily: 'Roboto'),
                    ),
                  ),
                ))),
        SizedBox(height: 0.06 * media.size.height),
        Center(
            child: GestureDetector(
          onTap: () {
            Navigator.pop(context);
            Navigator.push(
              context,
              MaterialPageRoute(builder: (context) => LogIn()),
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
                  text: 'Have an account? ',
                ),
                TextSpan(
                  text: 'Log in!',
                  style: TextStyle(
                    fontWeight: FontWeight.w800,
                  ),
                )
              ],
            ),
            textAlign: TextAlign.center,
          ),
        )),
        SizedBox(height: 0.05 * media.size.height),
      ]),
    );
  }

  /*
  Method Name:
    _showAlertDialog
  Purpose:
    This method is used when validating the form.
           If a field is invalid or incomplete, the alert dialog will show.
*/

  void _showAlertDialog(String message, String message2) {
    // set up the button
    Widget okButton = FlatButton(
      child: Text("Ok", style: TextStyle(color: Color(0xff513369))),
      onPressed: () {
        Navigator.pop(context);
      },
    );

    AlertDialog alert = AlertDialog(
      title: Text(message2),
      content: Text(message),
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
    sendValuesToDatabase
  Purpose:
    This method is called when the send button is pressed. It sends the values
    to the database to be stored.
*/
  sendValuesToDatabase() async {
    bool secure = validateStructure(password);

    if (gymMemberId == "" || password == "" || gym == "" || username == "") {
      _showAlertDialog(
          "Please fill in missing fields", "All fields are required");
    } else if (secure == false) {
      _showAlertDialog(
          "Your password needs to be 8 characters long, with at least one special character, number, small letter and capital letter",
          "Password invalid");
    } else {
      _makePostRequest();
    }
  }

  /*
  Method Name:
    _makePostRequest
  Purpose:
    This method is called when the all the fields in the form has been verified.
    It makes a post request to our API with the respective fields, ensuring that
    this user is registered to a gym. Once they're logged in, it redirects them to
    their respective welcome pages.
*/
  _makePostRequest() async {
    String url = 'https://gymmoveswebapi.azurewebsites.net/api/user/signup';

    var gymArray = gym.split(", ");

    final http.Response response = await http.post(
      url,
      headers: <String, String>{'Content-type': 'application/json'},
      body: jsonEncode({
        "username": username,
        "password": password,
        "gymMemberID": gymMemberId,
        "gymName": gymArray[0],
        "gymBranch": gymArray[1],
      }),
    );

    String responseBody = response.body;

    User userjson = User.fromJson(json.decode(responseBody));
    bool usernameValid = userjson.usernameValid;
    bool gymMemberIdValid = userjson.gymMemberIdValid;
    int userType = userjson.userType;
    String name = userjson.name;

    if (usernameValid == false && gymMemberIdValid == true) {
      _showAlertDialog(
          "Your username is taken, try a different one.", 'Username invalid');
    } else if (gymMemberIdValid == false && usernameValid == true) {
      _showAlertDialog("Did you make a typo in your Gym ID?", "Gym ID invalid");
    } else if (gymMemberIdValid == false && usernameValid == false) {
      _showAlertDialog(
          "Gym ID does not exist, and username is already taken.", "Invalid");
    } else if (usernameValid == true && gymMemberIdValid == true) {
      /* local storage */
      final prefs = await SharedPreferences.getInstance();
      /* set value */
      prefs.setString('gymId', gymMemberId);
      prefs.setString('username', username);
      prefs.setInt('type', userType);
      prefs.setString("name", name);
      prefs.setString("gymName", gymArray[0]);

      if (userType == 0) {
        Navigator.of(context).pop();
        Navigator.of(context)
            .push(new MaterialPageRoute(builder: (BuildContext context) {
          return new MemberPages();
        }));
      } else if (userType == 1) {
        Navigator.of(context).pop();
        Navigator.of(context)
            .push(new MaterialPageRoute(builder: (BuildContext context) {
          return new InstructorPages();
        }));
      } else if (userType == 2) {
        Navigator.of(context).pop();
        Navigator.of(context)
            .push(new MaterialPageRoute(builder: (BuildContext context) {
          return new ManagerPages();
        }));
      } else {
        _showAlertDialog("Not a valid gym member", "Gym ID invalid");
      }
    } else {
      _showAlertDialog(
          "Try again in a few minutes", "Sorry, there's a problem on our side");
    }
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

/*
Class Name:
  User
Purpose:
This class is the response body that we receive from the API
 */
class User {
  final bool usernameValid;
  final bool gymMemberIdValid;
  final int userType;
  final String name;

  User({this.usernameValid, this.gymMemberIdValid, this.userType, this.name});

  factory User.fromJson(Map<String, dynamic> json) {
    return User(
      usernameValid: json['usernameValid'],
      gymMemberIdValid: json['gymMemberIdValid'],
      userType: json['userType'],
      name: json['name'],
    );
  }
}

const String idCard =
    '<svg viewBox="291.0 327.0 23.6 16.5" ><path transform="translate(291.0, 324.75)" d="M 21.62522506713867 2.25 L 1.965929627418518 2.25 C 0.8805726170539856 2.25 0 3.041852474212646 0 4.017857074737549 L 0 4.607143402099609 L 23.59115600585938 4.607143402099609 L 23.59115600585938 4.017857074737549 C 23.59115600585938 3.041852474212646 22.7105827331543 2.25 21.62522506713867 2.25 Z M 0 16.98214149475098 C 0 17.95814514160156 0.8805726170539856 18.75 1.965929627418518 18.75 L 21.62522506713867 18.75 C 22.7105827331543 18.75 23.59115600585938 17.95814514160156 23.59115600585938 16.98214149475098 L 23.59115600585938 5.785714626312256 L 0 5.785714626312256 L 0 16.98214149475098 Z M 14.41681671142578 8.437499046325684 C 14.41681671142578 8.275445938110352 14.5642614364624 8.142857551574707 14.74447154998779 8.142857551574707 L 20.64226150512695 8.142857551574707 C 20.82247161865234 8.142857551574707 20.96991539001465 8.275445938110352 20.96991539001465 8.437499046325684 L 20.96991539001465 9.026785850524902 C 20.96991539001465 9.188838958740234 20.82247161865234 9.321427345275879 20.64226150512695 9.321427345275879 L 14.74447154998779 9.321427345275879 C 14.5642614364624 9.321427345275879 14.41681671142578 9.188838958740234 14.41681671142578 9.026785850524902 L 14.41681671142578 8.437499046325684 Z M 14.41681671142578 10.79464149475098 C 14.41681671142578 10.63258838653564 14.5642614364624 10.49999904632568 14.74447154998779 10.49999904632568 L 20.64226150512695 10.49999904632568 C 20.82247161865234 10.49999904632568 20.96991539001465 10.63258838653564 20.96991539001465 10.79464149475098 L 20.96991539001465 11.3839282989502 C 20.96991539001465 11.54598140716553 20.82247161865234 11.67857074737549 20.64226150512695 11.67857074737549 L 14.74447154998779 11.67857074737549 C 14.5642614364624 11.67857074737549 14.41681671142578 11.54598140716553 14.41681671142578 11.3839282989502 L 14.41681671142578 10.79464149475098 Z M 14.41681671142578 13.15178489685059 C 14.41681671142578 12.98972988128662 14.5642614364624 12.85714054107666 14.74447154998779 12.85714054107666 L 20.64226150512695 12.85714054107666 C 20.82247161865234 12.85714054107666 20.96991539001465 12.98972988128662 20.96991539001465 13.15178489685059 L 20.96991539001465 13.74106979370117 C 20.96991539001465 13.90312385559082 20.82247161865234 14.03571224212646 20.64226150512695 14.03571224212646 L 14.74447154998779 14.03571224212646 C 14.5642614364624 14.03571224212646 14.41681671142578 13.90312385559082 14.41681671142578 13.74106979370117 L 14.41681671142578 13.15178489685059 Z M 7.208408355712891 8.142857551574707 C 8.65418529510498 8.142857551574707 9.829648017883301 9.199888229370117 9.829648017883301 10.49999904632568 C 9.829648017883301 11.80011081695557 8.65418529510498 12.85714054107666 7.208408355712891 12.85714054107666 C 5.762631416320801 12.85714054107666 4.587169170379639 11.80011081695557 4.587169170379639 10.49999904632568 C 4.587169170379639 9.199888229370117 5.762631416320801 8.142857551574707 7.208408355712891 8.142857551574707 Z M 2.748205900192261 15.66361427307129 C 3.092243432998657 14.71707439422607 4.079304218292236 14.03571224212646 5.242478847503662 14.03571224212646 L 5.578325271606445 14.03571224212646 C 6.082094669342041 14.22354698181152 6.630917072296143 14.33035564422607 7.208408355712891 14.33035564422607 C 7.785899639129639 14.33035564422607 8.338818550109863 14.22354698181152 8.838491439819336 14.03571224212646 L 9.174338340759277 14.03571224212646 C 10.33751392364502 14.03571224212646 11.3245735168457 14.71707439422607 11.66861152648926 15.66361427307129 C 11.79967403411865 16.02823448181152 11.4556360244751 16.39285469055176 11.02968502044678 16.39285469055176 L 3.387132883071899 16.39285469055176 C 2.961181402206421 16.39285469055176 2.617143630981445 16.02455139160156 2.748205900192261 15.66361427307129 Z" fill="#b9a8bf" stroke="none" stroke-width="1" stroke-miterlimit="4" stroke-linecap="butt" /></svg>';

const String lock =
    '<svg viewBox="292.0 395.0 16.0 17.5" ><path transform="translate(292.0, 395.0)" d="M 14.28200817108154 7.65625 L 13.42508792877197 7.65625 L 13.42508792877197 5.1953125 C 13.42508792877197 2.3310546875 10.99000549316406 0 7.9979248046875 0 C 5.005844116210938 0 2.570761442184448 2.3310546875 2.570761442184448 5.1953125 L 2.570761442184448 7.65625 L 1.713841080665588 7.65625 C 0.7676579356193542 7.65625 0 8.39111328125 0 9.296875 L 0 15.859375 C 0 16.76513671875 0.7676579356193542 17.5 1.713841080665588 17.5 L 14.28200817108154 17.5 C 15.22819137573242 17.5 15.995849609375 16.76513671875 15.995849609375 15.859375 L 15.995849609375 9.296875 C 15.995849609375 8.39111328125 15.22819137573242 7.65625 14.28200817108154 7.65625 Z M 10.56868648529053 7.65625 L 5.427163124084473 7.65625 L 5.427163124084473 5.1953125 C 5.427163124084473 3.83837890625 6.580435276031494 2.734375 7.9979248046875 2.734375 C 9.415414810180664 2.734375 10.56868648529053 3.83837890625 10.56868648529053 5.1953125 L 10.56868648529053 7.65625 Z" fill="#b9a8bf" stroke="none" stroke-width="1" stroke-miterlimit="4" stroke-linecap="butt" /></svg>';

const String person =
    '<svg viewBox="291.0 393.0 16.9 21.1" ><path transform="translate(291.0, 393.0)" d="M 8.433197021484375 10.53529357910156 C 11.09492588043213 10.53529357910156 13.25216770172119 8.17719841003418 13.25216770172119 5.267646789550781 C 13.25216770172119 2.358094692230225 11.09492588043213 0 8.433197021484375 0 C 5.771469116210938 0 3.614227533340454 2.358094930648804 3.614227533340454 5.267646789550781 C 3.614227533340454 8.17719841003418 5.771469116210938 10.53529357910156 8.433197021484375 10.53529357910156 Z M 11.80647563934326 11.85220527648926 L 11.17775058746338 11.85220527648926 C 10.34195995330811 12.27197074890137 9.412049293518066 12.51066112518311 8.433197021484375 12.51066112518311 C 7.454344272613525 12.51066112518311 6.528197765350342 12.27197074890137 5.688642978668213 11.85220527648926 L 5.05991792678833 11.85220527648926 C 2.266421794891357 11.85220527648926 0 14.32964515686035 0 17.38323402404785 L 0 19.09521865844727 C 0 20.18578720092773 0.8094363808631897 21.07058715820313 1.807113766670227 21.07058715820313 L 15.05928134918213 21.07058715820313 C 16.05695915222168 21.07058715820313 16.86639404296875 20.18578720092773 16.86639404296875 19.09521865844727 L 16.86639404296875 17.38323402404785 C 16.86639404296875 14.32964515686035 14.59997272491455 11.85220527648926 11.80647563934326 11.85220527648926 Z" fill="#b9a8bf" stroke="none" stroke-width="1" stroke-miterlimit="4" stroke-linecap="butt" /></svg>';

const String dumbbell =
    '<svg viewBox="105.7 62.6 148.0 151.2" ><path transform="translate(104.73, 61.61)" d="M 52.92843627929688 140.3509826660156 C 54.10280227661133 141.6522674560547 54.0328369140625 143.6774139404297 52.77159881591797 144.8908081054688 L 45.95822906494141 151.4024047851563 C 44.69138336181641 152.6053619384766 42.71061325073242 152.5321807861328 41.53205490112305 151.2389068603516 L 1.853297233581543 107.5332946777344 C 0.6797584295272827 106.2311325073242 0.7511200904846191 104.2059478759766 2.0132737159729 102.9934539794922 L 8.836055755615234 96.49147796630859 C 10.1022424697876 95.28672790527344 12.08445072174072 95.35995483398438 13.26223087310791 96.65498352050781 L 52.92843627929688 140.3509826660156 Z M 103.5581436157227 58.11441802978516 C 104.731689453125 59.41657638549805 104.6603317260742 61.44176483154297 103.3981552124023 62.65425872802734 L 60.51661682128906 103.5961990356445 C 59.24977874755859 104.7991409301758 57.26900863647461 104.7259674072266 56.09044647216797 103.4326858520508 L 45.85470962524414 92.14398956298828 C 44.68115997314453 90.84183502197266 44.75253295898438 88.816650390625 46.01469421386719 87.60414886474609 L 88.88682556152344 46.66221237182617 C 89.49431610107422 46.08388519287109 90.30185699462891 45.77615356445313 91.13150787353516 45.80682754516602 C 91.96115875244141 45.83749389648438 92.74484252929688 46.20405197143555 93.30986022949219 46.82572174072266 L 103.5581436157227 58.11441040039063 Z M 67.51820373535156 126.4140396118164 C 68.69140625 127.7061004638672 68.61925506591797 129.7483978271484 67.35822296142578 130.9538726806641 L 60.53544616699219 137.4622650146484 C 59.26925277709961 138.6670227050781 57.28704452514648 138.5937805175781 56.10926818847656 137.2987518310547 L 16.45247077941895 93.59315490722656 C 15.27809715270996 92.29185485839844 15.34806632995605 90.26671600341797 16.60931587219238 89.05331420898438 L 23.41954040527344 82.54812622070313 C 24.68638038635254 81.34517669677734 26.66715812683105 81.41834259033203 27.84571838378906 82.71163177490234 L 67.51820373535156 126.4140396118164 Z M 133.5908966064453 59.68219757080078 C 134.7636108398438 60.98521423339844 134.6908416748047 63.01043319702148 133.4277801513672 64.22203063964844 L 126.6081466674805 70.73042297363281 C 125.3410949707031 71.93457794189453 123.3593826293945 71.86284637451172 122.178840637207 70.57012939453125 L 82.51576232910156 26.8741512298584 C 81.33965301513672 25.57351112365723 81.40966796875 23.54692459106445 82.67259216308594 22.33430862426758 L 89.51105499267578 15.80988693237305 C 90.11854553222656 15.23155879974365 90.92609405517578 14.923828125 91.75575256347656 14.95449733734131 C 92.58540344238281 14.98516750335693 93.36907958984375 15.35172557830811 93.93411254882813 15.97339820861816 L 133.5908966064453 59.68219757080078 Z M 148.1587066650391 45.7677116394043 C 149.3308715820313 47.07159805297852 149.2595672607422 49.09637832641602 147.9987182617188 50.31076431274414 L 141.1916198730469 56.81594085693359 C 140.5845489501953 57.39468002319336 139.7772369384766 57.70297622680664 138.9475860595703 57.67290496826172 C 138.1179351806641 57.6428337097168 137.3340148925781 57.2768669128418 136.7685852050781 56.65563583374023 L 97.11178588867188 12.9404239654541 C 95.93878173828125 11.63739967346191 96.00868225097656 9.612658500671387 97.26863098144531 8.39737606048584 L 104.0820007324219 1.879366874694824 C 104.6881790161133 1.300024747848511 105.4951324462891 0.9912708401679993 106.3245315551758 1.02135181427002 C 107.1539154052734 1.051432132720947 107.9374313354492 1.417869567871094 108.5018997192383 2.039670705795288 L 148.1587066650391 45.7677116394043 Z" fill="#b9a8bf" fill-opacity="1.0" stroke="none" stroke-width="1" stroke-opacity="0.08" stroke-miterlimit="4" stroke-linecap="butt" /></svg>';
