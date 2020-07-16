/*
File Name
  ForgotPassword.dart
Author:
  Raeesa
Date Created
  27/06/2020
Update History:
--------------------------------------------------------------------------------
| Name               | Date              | Changes                             |
--------------------------------------------------------------------------------
| Raeesa             | 28/06/2020        | Made UI responsive and functional   |
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


import 'package:flutter/material.dart';
import 'package:flutter_svg/flutter_svg.dart';
import 'package:autocomplete_textfield/autocomplete_textfield.dart';
import 'package:gym_moves/User/MemberPages.dart';
import 'package:gym_moves/User/LogIn.dart';
import 'package:gym_moves/User/ManagerPages.dart';
import 'package:gym_moves/User/InstructorPages.dart';
import 'package:http/http.dart';
import 'dart:convert';
import 'dart:async';


class ForgotPassword extends StatefulWidget {
  const ForgotPassword({
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
  String code="";
  String username = "";

  final FormKey = GlobalKey<FormState>();

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
                    labelText: 'User name',
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
//                maxLines: 9,
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

    final codeField = Material(
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
//                maxLines: 9,
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
        color: Colors.transparent);


    return Scaffold(
        backgroundColor: const Color(0xff513369),
        body: ListView(children: <Widget>[
          Stack(children: <Widget>[
            SvgPicture.string(
              _svg_8ykgd0,
              allowDrawingOutsideViewBox: true,
            ),
            Transform.translate(
              offset: Offset(260.0, 21.0),
              child: Text(
                'Gym Moves',
                style: TextStyle(
                  fontFamily: 'FreestyleScript',
                  fontSize: 48,
                  color: const Color(0xffffffff),
                ),
                textAlign: TextAlign.left,
              ),
            ),
            Transform.translate(
              offset: Offset(13.0, 33.0),
              child:
              // Adobe XD layer: 'Icon ionic-md-arrow…' (shape)
              SvgPicture.string(
                arrow,
                allowDrawingOutsideViewBox: true,
              ),
            ),

            Transform.translate(
              offset: Offset(50.0, 126.0),
              child: Text(
                'We will send a code to your mobile\ndevice through sms, please enter it \nin order to reset your password.',
                style: TextStyle(
                  fontFamily: 'Roboto',
                  fontSize: 19,
                  color: const Color(0xffffffff),
                  fontWeight: FontWeight.w300,
                ),
                textAlign: TextAlign.left,
              ),

            ),
          ]),
          SizedBox(height: 0.06* media.size.height),
          Form(
            key: FormKey,
            child: Column(children: <Widget>[

              SizedBox(height: 0.15 * media.size.height),
              Stack(children: <Widget>[
                usernameField,
                Transform.translate(
                    offset: Offset(0.7 * 0.85 * media.size.width,
                        0.08 * 0.3 * media.size.height),
                    child: SvgPicture.string(
                      card,
                      color: Colors.black45,
                      allowDrawingOutsideViewBox: true,
                    ))
              ]),
              SizedBox(height: 0.03* media.size.height),
              Center(

                  child: SizedBox(
                      width: 150,
                      child: RaisedButton(
                        shape: RoundedRectangleBorder(
                            borderRadius: BorderRadius.circular(10.0)),
                        color: const Color(0xffffffff).withOpacity(0.3),
                        onPressed: () {
                      sendCode(username);
                        },
                        textColor: Colors.white,
                        padding: const EdgeInsets.all(0.0),
                        child: Container(
                          padding: const EdgeInsets.all(10.0),
                          child: Text(
                            'Get Code',
                            style: TextStyle(
                                fontSize: 14,
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
                      color: Colors.black45,
                      allowDrawingOutsideViewBox: true,
                    ))
              ]),
              SizedBox(height: 0.025* media.size.height),

              Stack(children: <Widget>[
                passwordField,
                Transform.translate(
                    offset: Offset(0.7 * 0.85 * media.size.width,
                        0.08 * 0.3 * media.size.height),
                    child: SvgPicture.string(
                      lock,
                      color: Colors.black45,
                      allowDrawingOutsideViewBox: true,
                    ))
              ]),
              SizedBox(height: 0.03* media.size.height),
              Center(

                  child: SizedBox(
                      width: 150,
                      child: RaisedButton(
                        shape: RoundedRectangleBorder(
                            borderRadius: BorderRadius.circular(10.0)),
                        color: const Color(0xffffffff).withOpacity(0.3),
                        onPressed: () {
                      sendUpdatedPassword(password, code);
                        },
                        textColor: Colors.white,
                        padding: const EdgeInsets.all(0.0),
                        child: Container(
                          padding: const EdgeInsets.all(10.0),
                          child: Text(
                            'Change Password',
                            style: TextStyle(
                                fontSize: 14,
                                fontFamily: 'Roboto'),
                          ),
                        ),
                      ))),



            ],
            ),
          )]));
  }
  /*
  Method Name: _showAlertDialog
  Purpose: This method is used when validating the form.
           If a field is invalid or incomplete, the alert dialog will show.
*/


  void _showAlertDialog(String message, String message2) {
    // set up the button
    Widget okButton = FlatButton(
      child: Text("OK"),
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
  Method Name: sendCode
  Purpose: This method validates the username field, before a post request is made,
           requesting a code for a new password.
           If a field is invalid or incomplete, the alert dialog will show.
*/

  sendCode(username) {

    if (username == "") {
      _showAlertDialog("User name is required to receive a code.", "Empty Field");

    }


    else {
      _sendCode(username);
    }
  }

  /*
  Method Name: sendUpdatedPassword
  Purpose: This method validates the code and password field, before a post request is made,
           requesting to change the password.
           If a field is invalid or incomplete, the alert dialog will show.
*/

  sendUpdatedPassword(password, code) {
    bool secure = validateStructure(password);


    if (password == "" || code == "") {
      _showAlertDialog("Please fill in the code, and password fields.", "Both fields are required");

    }

    else if (secure == false) {
      _showAlertDialog("Your password needs to be 8 characters long, with at least one special character, number, small letter and capital letter", "Password invalid");

    }

    else {
      _makePostRequest(password,code);
    }
  }


  /*
  Method Name: _sendCode
  Purpose: This method makes a post request,
           requesting a code for a new password.
*/
  _sendCode(username) async {
    String url = 'https://gymmoveswebapi.azurewebsites.net/api/user/ForgotPassword';
    Map<String, String> headers = {"Content-type": "application/json"};
    String jsonString = '{""User": username}';
    Response response = (await post(url, headers: headers, body: jsonString));
    int statusCode = response.statusCode;
    String body = response.body;

    if (statusCode == 200) {
      APIresponse userjson = APIresponse.fromJson(json.decode(body));

      bool uservalid = userjson.uservalid;


      if (uservalid == false) {
        _showAlertDialog("Your username is not registered to the gym.",
            'User name invalid');
      }
    }
  }

  /*
  Method Name: _makePostRequest
  Purpose: This method is called when the all the fields in the form has been verified.
           It makes a post request to our API.
*/
  _makePostRequest(password, code) async {
    // set up POST request arguments
    String url = 'https://gymmoveswebapi.azurewebsites.net/api/user/ForgotPassword';
    Map<String, String> headers = {"Content-type": "application/json"};
    String jsonString = '{""password": password, "code": code}';
    // make POST request
    Response response = (await post(url, headers: headers, body: jsonString)) ;
    // check the status code for the result
    int statusCode = response.statusCode;
    String body = response.body;

    if (statusCode == 200) {
      APIresponse userjson = APIresponse.fromJson(json.decode(body));


      bool codevalid = userjson.codevalid;
      int userType = userjson.userType;


      if (codevalid == false) {
        _showAlertDialog(
            "Did you make a typo in the code?", "Code invalid");
      }

      else if (codevalid == true) {
        //take to welcome/login page
        if (userType == 0) {
          MaterialPageRoute(builder: (context) => MemberPages());
        }
        else if (userType == 1) {
          MaterialPageRoute(builder: (context) => InstructorPages());
        }

        else if (userType == 2) {
          MaterialPageRoute(builder: (context) => ManagerPages());
        }



      } else {
        _showAlertDialog(
            "Try again in a few minutes", "Sorry, there's a problem on our side");
      }
    }
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
this is the response body that we receive from the API
 */
class APIresponse{
  final bool uservalid;
  final bool codevalid;
  final int userType;

  APIresponse({this.uservalid, this.codevalid, this.userType});

  factory APIresponse.fromJson(Map<String, dynamic> json){
    return APIresponse(
      uservalid: json['user'],
      codevalid: json['code'],
      userType: json['userType']

    );
  }

}


const String _svg_8ykgd0 =
    '<svg viewBox="0.0 0.0 423.0 99.0" ><defs><filter id="shadow"><feDropShadow dx="0" dy="3" stdDeviation="6"/></filter></defs><path  d="M 0 0 L 422.9787292480469 0 L 422.9787292480469 99 L 0 99 L 0 0 Z" fill="#907d9f" stroke="none" stroke-width="1" stroke-miterlimit="4" stroke-linecap="butt" filter="url(#shadow)"/></svg>';
const String arrow =
    '<svg viewBox="13.0 33.0 39.8 33.4" ><defs><filter id="shadow"><feDropShadow dx="0" dy="3" stdDeviation="6"/></filter></defs><path transform="translate(7.38, 26.67)" d="M 41.99232482910156 19.79138946533203 L 17.77761459350586 19.79138946533203 L 27.16930198669434 11.81345558166504 C 28.51581954956055 10.5574836730957 28.51581954956055 8.527832984924316 27.16930198669434 7.271862030029297 C 25.82278251647949 6.015890598297119 23.63893127441406 6.015890598297119 22.28109741210938 7.271862030029297 L 6.643375873565674 20.73587799072266 C 5.96445894241333 21.31864929199219 5.625000476837158 22.1124267578125 5.625000476837158 22.98657989501953 L 5.625000476837158 23.02677536010742 C 5.625000476837158 23.90093231201172 5.96445894241333 24.6947021484375 6.643375873565674 25.2774772644043 L 22.26978302001953 38.74148941040039 C 23.62761306762695 39.99746322631836 25.81146812438965 39.99746322631836 27.15798377990723 38.74148941040039 C 28.50450134277344 37.48551559448242 28.50450134277344 35.45586776733398 27.15798377990723 34.19989395141602 L 17.76629829406738 26.22196578979492 L 41.98100662231445 26.22196578979492 C 43.89329147338867 26.22196578979492 45.4434814453125 24.78513717651367 45.4434814453125 23.00667953491211 C 45.45479965209961 21.19807815551758 43.90460205078125 19.79138946533203 41.99232482910156 19.79138946533203 Z" fill="#ffffff" stroke="#ffffff" stroke-width="1" stroke-miterlimit="4" stroke-linecap="butt" filter="url(#shadow)"/></svg>';
const String card =
    '<svg viewBox="284.0 249.0 23.6 16.5" ><path transform="translate(284.0, 246.75)" d="M 21.62522506713867 2.25 L 1.965929627418518 2.25 C 0.8805726170539856 2.25 0 3.041852474212646 0 4.017857074737549 L 0 4.607143402099609 L 23.59115600585938 4.607143402099609 L 23.59115600585938 4.017857074737549 C 23.59115600585938 3.041852474212646 22.7105827331543 2.25 21.62522506713867 2.25 Z M 0 16.98214149475098 C 0 17.95814514160156 0.8805726170539856 18.75 1.965929627418518 18.75 L 21.62522506713867 18.75 C 22.7105827331543 18.75 23.59115600585938 17.95814514160156 23.59115600585938 16.98214149475098 L 23.59115600585938 5.785714626312256 L 0 5.785714626312256 L 0 16.98214149475098 Z M 14.41681671142578 8.437499046325684 C 14.41681671142578 8.275445938110352 14.5642614364624 8.142857551574707 14.74447154998779 8.142857551574707 L 20.64226150512695 8.142857551574707 C 20.82247161865234 8.142857551574707 20.96991539001465 8.275445938110352 20.96991539001465 8.437499046325684 L 20.96991539001465 9.026785850524902 C 20.96991539001465 9.188838958740234 20.82247161865234 9.321427345275879 20.64226150512695 9.321427345275879 L 14.74447154998779 9.321427345275879 C 14.5642614364624 9.321427345275879 14.41681671142578 9.188838958740234 14.41681671142578 9.026785850524902 L 14.41681671142578 8.437499046325684 Z M 14.41681671142578 10.79464149475098 C 14.41681671142578 10.63258838653564 14.5642614364624 10.49999904632568 14.74447154998779 10.49999904632568 L 20.64226150512695 10.49999904632568 C 20.82247161865234 10.49999904632568 20.96991539001465 10.63258838653564 20.96991539001465 10.79464149475098 L 20.96991539001465 11.3839282989502 C 20.96991539001465 11.54598140716553 20.82247161865234 11.67857074737549 20.64226150512695 11.67857074737549 L 14.74447154998779 11.67857074737549 C 14.5642614364624 11.67857074737549 14.41681671142578 11.54598140716553 14.41681671142578 11.3839282989502 L 14.41681671142578 10.79464149475098 Z M 14.41681671142578 13.15178489685059 C 14.41681671142578 12.98972988128662 14.5642614364624 12.85714054107666 14.74447154998779 12.85714054107666 L 20.64226150512695 12.85714054107666 C 20.82247161865234 12.85714054107666 20.96991539001465 12.98972988128662 20.96991539001465 13.15178489685059 L 20.96991539001465 13.74106979370117 C 20.96991539001465 13.90312385559082 20.82247161865234 14.03571224212646 20.64226150512695 14.03571224212646 L 14.74447154998779 14.03571224212646 C 14.5642614364624 14.03571224212646 14.41681671142578 13.90312385559082 14.41681671142578 13.74106979370117 L 14.41681671142578 13.15178489685059 Z M 7.208408355712891 8.142857551574707 C 8.65418529510498 8.142857551574707 9.829648017883301 9.199888229370117 9.829648017883301 10.49999904632568 C 9.829648017883301 11.80011081695557 8.65418529510498 12.85714054107666 7.208408355712891 12.85714054107666 C 5.762631416320801 12.85714054107666 4.587169170379639 11.80011081695557 4.587169170379639 10.49999904632568 C 4.587169170379639 9.199888229370117 5.762631416320801 8.142857551574707 7.208408355712891 8.142857551574707 Z M 2.748205900192261 15.66361427307129 C 3.092243432998657 14.71707439422607 4.079304218292236 14.03571224212646 5.242478847503662 14.03571224212646 L 5.578325271606445 14.03571224212646 C 6.082094669342041 14.22354698181152 6.630917072296143 14.33035564422607 7.208408355712891 14.33035564422607 C 7.785899639129639 14.33035564422607 8.338818550109863 14.22354698181152 8.838491439819336 14.03571224212646 L 9.174338340759277 14.03571224212646 C 10.33751392364502 14.03571224212646 11.3245735168457 14.71707439422607 11.66861152648926 15.66361427307129 C 11.79967403411865 16.02823448181152 11.4556360244751 16.39285469055176 11.02968502044678 16.39285469055176 L 3.387132883071899 16.39285469055176 C 2.961181402206421 16.39285469055176 2.617143630981445 16.02455139160156 2.748205900192261 15.66361427307129 Z" fill="#b9a8bf" stroke="none" stroke-width="1" stroke-miterlimit="4" stroke-linecap="butt" /></svg>';
const String lock =
    '<svg viewBox="281.0 481.0 16.0 17.5" ><path transform="translate(281.0, 481.0)" d="M 14.28200817108154 7.65625 L 13.42508792877197 7.65625 L 13.42508792877197 5.1953125 C 13.42508792877197 2.3310546875 10.99000549316406 0 7.9979248046875 0 C 5.005844116210938 0 2.570761442184448 2.3310546875 2.570761442184448 5.1953125 L 2.570761442184448 7.65625 L 1.713841080665588 7.65625 C 0.7676579356193542 7.65625 0 8.39111328125 0 9.296875 L 0 15.859375 C 0 16.76513671875 0.7676579356193542 17.5 1.713841080665588 17.5 L 14.28200817108154 17.5 C 15.22819137573242 17.5 15.995849609375 16.76513671875 15.995849609375 15.859375 L 15.995849609375 9.296875 C 15.995849609375 8.39111328125 15.22819137573242 7.65625 14.28200817108154 7.65625 Z M 10.56868648529053 7.65625 L 5.427163124084473 7.65625 L 5.427163124084473 5.1953125 C 5.427163124084473 3.83837890625 6.580435276031494 2.734375 7.9979248046875 2.734375 C 9.415414810180664 2.734375 10.56868648529053 3.83837890625 10.56868648529053 5.1953125 L 10.56868648529053 7.65625 Z" fill="#b9a8bf" stroke="none" stroke-width="1" stroke-miterlimit="4" stroke-linecap="butt" /></svg>';
const String keypad =
    '<svg viewBox="297.0 417.0 15.0 23.6" ><path transform="translate(290.25, 414.75)" d="M 14.2520751953125 21.52135848999023 C 13.20647430419922 21.52135848999023 12.35311222076416 22.48361015319824 12.35311222076416 23.66145324707031 C 12.35311222076416 24.83929061889648 13.20647430419922 25.80154418945313 14.2520751953125 25.80154418945313 C 15.29767799377441 25.80154418945313 16.15103912353516 24.83929061889648 16.15103912353516 23.66145324707031 C 16.15103912353516 22.48887062072754 15.29767799377441 21.52135848999023 14.2520751953125 21.52135848999023 Z M 8.64896297454834 2.25 C 7.6033616065979 2.25 6.75 3.212253332138062 6.75 4.390093803405762 C 6.75 5.567933559417725 7.6033616065979 6.530187129974365 8.64896297454834 6.530187129974365 C 9.694564819335938 6.530187129974365 10.54792594909668 5.567934036254883 10.54792594909668 4.390093803405762 C 10.54792594909668 3.212252855300903 9.694564819335938 2.25 8.64896297454834 2.25 Z M 8.64896297454834 8.675539016723633 C 7.6033616065979 8.675539016723633 6.75 9.637791633605957 6.75 10.81563282012939 C 6.75 11.99347400665283 7.6033616065979 12.95572662353516 8.64896297454834 12.95572662353516 C 9.694564819335938 12.95572662353516 10.54792594909668 11.99347400665283 10.54792594909668 10.81563282012939 C 10.54792594909668 9.637791633605957 9.694564819335938 8.675539016723633 8.64896297454834 8.675539016723633 Z M 8.64896297454834 15.10107707977295 C 7.6033616065979 15.10107707977295 6.75 16.06333160400391 6.75 17.24117088317871 C 6.75 18.41901206970215 7.6033616065979 19.38126564025879 8.64896297454834 19.38126564025879 C 9.694564819335938 19.38126564025879 10.54792594909668 18.41901206970215 10.54792594909668 17.24117088317871 C 10.54792594909668 16.06333160400391 9.694564819335938 15.10107707977295 8.64896297454834 15.10107707977295 Z M 19.85518836975098 6.535445690155029 C 20.90078926086426 6.535445690155029 21.754150390625 5.573192119598389 21.754150390625 4.395352363586426 C 21.754150390625 3.217511653900146 20.90078926086426 2.25 19.85518836975098 2.25 C 18.8095874786377 2.25 17.95622444152832 3.212253332138062 17.95622444152832 4.390093803405762 C 17.95622444152832 5.567933559417725 18.8095874786377 6.535445690155029 19.85518836975098 6.535445690155029 Z M 14.2520751953125 15.10107707977295 C 13.20647430419922 15.10107707977295 12.35311222076416 16.06333160400391 12.35311222076416 17.24117088317871 C 12.35311222076416 18.41901206970215 13.20647430419922 19.38126564025879 14.2520751953125 19.38126564025879 C 15.29767799377441 19.38126564025879 16.15103912353516 18.41901206970215 16.15103912353516 17.24117088317871 C 16.15103912353516 16.06333160400391 15.29767799377441 15.10107707977295 14.2520751953125 15.10107707977295 Z M 19.85518836975098 15.10107707977295 C 18.8095874786377 15.10107707977295 17.95622444152832 16.06333160400391 17.95622444152832 17.24117088317871 C 17.95622444152832 18.41901206970215 18.8095874786377 19.38126564025879 19.85518836975098 19.38126564025879 C 20.90078926086426 19.38126564025879 21.754150390625 18.41901206970215 21.754150390625 17.24117088317871 C 21.754150390625 16.06333160400391 20.90078926086426 15.10107707977295 19.85518836975098 15.10107707977295 Z M 19.85518836975098 8.675539016723633 C 18.8095874786377 8.675539016723633 17.95622444152832 9.637791633605957 17.95622444152832 10.81563282012939 C 17.95622444152832 11.99347400665283 18.8095874786377 12.95572662353516 19.85518836975098 12.95572662353516 C 20.90078926086426 12.95572662353516 21.754150390625 11.99347400665283 21.754150390625 10.81563282012939 C 21.754150390625 9.637791633605957 20.90078926086426 8.675539016723633 19.85518836975098 8.675539016723633 Z M 14.2520751953125 8.675539016723633 C 13.20647430419922 8.675539016723633 12.35311222076416 9.637791633605957 12.35311222076416 10.81563282012939 C 12.35311222076416 11.99347400665283 13.20647430419922 12.95572662353516 14.2520751953125 12.95572662353516 C 15.29767799377441 12.95572662353516 16.15103912353516 11.99347400665283 16.15103912353516 10.81563282012939 C 16.15103912353516 9.637791633605957 15.29767799377441 8.675539016723633 14.2520751953125 8.675539016723633 Z M 14.2520751953125 2.25 C 13.20647430419922 2.25 12.35311222076416 3.212253332138062 12.35311222076416 4.390093803405762 C 12.35311222076416 5.567933559417725 13.20647430419922 6.530187129974365 14.2520751953125 6.530187129974365 C 15.29767799377441 6.530187129974365 16.15103912353516 5.567933559417725 16.15103912353516 4.390093803405762 C 16.15103912353516 3.212253332138062 15.29767799377441 2.25 14.2520751953125 2.25 Z" fill="#b9a8bf" stroke="none" stroke-width="1" stroke-miterlimit="4" stroke-linecap="butt" /></svg>';
