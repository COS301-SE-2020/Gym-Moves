/*
File Name
  AddAClass.dart
Author:
  Danel
Date Created
  27/06/2020
Update History:
--------------------------------------------------------------------------------
 Name               | Date              | Changes
--------------------------------------------------------------------------------
Danel               | 05/07/2020        | Added autocomplete day and time picker
--------------------------------------------------------------------------------
Danel               | 15/07/2020        | Added autocomplete instructor
--------------------------------------------------------------------------------

Functional Description:
  This file contains the AddAClass class that calls the class that creates the
  UI for a manager to add a new class. The EditState class handles the building
  of the UI and making all the components functional and responsive.
  This file will also handle sending the information that is entered to the
  database.

Classes in the File:
- AddAClass
- AddAClassState
- NewClass
- Instructor
 */

import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';
import 'package:flutter_datetime_picker/flutter_datetime_picker.dart';
import 'package:flutter_svg/flutter_svg.dart';
import 'package:autocomplete_textfield/autocomplete_textfield.dart';
import 'package:http/http.dart' as http;
import 'package:http/http.dart';
import 'dart:convert';
import 'package:shared_preferences/shared_preferences.dart';

/*
Class Name:
  AddAClass
Purpose:
  This class creates the class that will build the page.
 */
class AddAClass extends StatefulWidget {
  const AddAClass({Key key}) : super(key: key);

  @override
  AddAClassState createState() => AddAClassState();
}

/*
Class Name:
  AddAClassState
Purpose:
  This class will build the page, and send information to the database.
 */
class AddAClassState extends State<AddAClass> {
  String className = "";
  String instructorUsername = "";
  String instructorName = "";
  String day = "";

  String startHour = DateTime.now().hour.toString();
  String startMinute = DateTime.now().minute.toString();
  String startSecond = DateTime.now().second.toString();

  String endHour = DateTime.now().hour.toString();
  String endMinute = DateTime.now().minute.toString();
  String endSecond = DateTime.now().second.toString();

  String description = "";
  String max = "0";
  String current = "0";
  String startTime; //DateFormat('kk:mm:ss \n EEE d MMM').format(time);
  String endTime;// DateFormat('kk:mm:ss \n EEE d MMM').format(time);


  final nameHolder = TextEditingController();
  final dayHolder = TextEditingController();
  final instructorHolder = TextEditingController();
  final descriptionHolder = TextEditingController();
  final maxHolder = TextEditingController();
  final currentHolder = TextEditingController();

  final editFormKey = GlobalKey<FormState>();

  List<Instructor> instructors = [];

  @override
  void initState() {
    _getInstructors();
    super.initState();
  }

  /*
  Method Name:
    _getInstructors

  Purpose:
     This method is used to make a get request and fetch the instructors
     of a gym.
*/
  _getInstructors() async {
    final prefs = await SharedPreferences.getInstance();
    int gymId = prefs.get("gymId");
    String url = 'https://gymmoveswebapi.azurewebsites.net/api/user/allInstructors?gymID=' + gymId.toString();
    Response response = await get(url);
    String responseBody = response.body;

    List<dynamic> instructorsJSON = json.decode(responseBody);

    for (int i = 0; i < instructorsJSON.length; i++) {
      instructors.add(Instructor.fromJson(instructorsJSON[i]));
    }
  }

  AutoCompleteTextField searchTextField;
  GlobalKey<AutoCompleteTextFieldState<Instructor>> instructorKey = new GlobalKey();

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

    final classNameField = Material(
        shadowColor: Colors.black,
        elevation: 15,
        child: Container(
            alignment: Alignment.centerLeft,
            width: 0.7 * media.size.width,
            height: 0.085 * media.size.height,
            child: TextFormField(
                controller : nameHolder,
                cursorColor: Colors.black45,
                style: TextStyle(
                  color: Colors.black54,
                ),
                decoration: InputDecoration(
                    filled: true,
                    fillColor: Colors.white,
                    labelText: 'Class Name',
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
                    className = value;
                  });
                })),
        borderRadius: BorderRadius.all(Radius.circular(19.0)),
        color: Colors.white);

    final instructorNameField = Material(
        shadowColor: Colors.black,
        elevation: 15,
        child: Container(
            padding: EdgeInsets.all(0.01 * media.size.width),
            alignment: Alignment.topLeft,
            width: 0.7 * media.size.width,
            height: 0.085 * media.size.height,
            child: searchTextField),
        borderRadius: BorderRadius.all(Radius.circular(19.0)),
        color: Colors.white);

    searchTextField = AutoCompleteTextField<Instructor>(
      style: TextStyle(
        color: Colors.black54,
      ),
      itemBuilder: (context, item) {
        return Row(
          mainAxisAlignment: MainAxisAlignment.spaceBetween,
          children: <Widget>[
            Text(
              item.name + " " + item.surname,
              style: TextStyle(fontSize: 0.04 * media.size.width),
            )
          ],
        );
      },
      itemFilter: (item, query) {
        return item.name.toLowerCase().startsWith(query.toLowerCase());
      },
      itemSorter: (a, b) {
        return a.name.compareTo(b.name);
      },
      itemSubmitted: (item) {
        setState(() => searchTextField.textField.controller.text =
            item.name + " " + item.surname + "(" + item.username +")");
        instructorName = item.name + " " + item.surname;
        instructorUsername = item.username;
      },
      key: instructorKey,
      suggestions: instructors,
      clearOnSubmit: false,
      decoration: InputDecoration(
          labelText: 'Instructor',
          labelStyle: new TextStyle(color: Colors.black54),
          border: InputBorder.none,
          enabledBorder: OutlineInputBorder(borderSide: BorderSide.none)),
    );

    final dayField = Material(
        shadowColor: Colors.black,
        elevation: 15,
        child: Container(
            padding: EdgeInsets.all(0.01 * media.size.width),
            alignment: Alignment.topLeft,
            width: 0.7 * media.size.width,
            height: 0.085 * media.size.height,
            child: SimpleAutoCompleteTextField(
              controller : instructorHolder,
              style: TextStyle(
                color: Colors.black54,
              ),
              suggestions: [
                "Monday",
                "Tuesday",
                "Wednesday",
                "Thursday",
                "Friday",
                "Saturday",
                "Sunday"
              ],
              clearOnSubmit: false,
              textSubmitted: (text) => setState(() {
                day = text;
              }),
              decoration: InputDecoration(
                  labelText: "Day of class",
                  labelStyle: new TextStyle(color: Colors.black54),
                  border: InputBorder.none,
                  enabledBorder:
                  OutlineInputBorder(borderSide: BorderSide.none)),
            )),
        borderRadius: BorderRadius.all(Radius.circular(19.0)),
        color: Colors.white);

    final startTimeField = Material(
        shadowColor: Colors.black,
        elevation: 15,
        child: Container(
            alignment: Alignment.centerLeft,
            width: 0.7 * media.size.width,
            height: 0.085 * media.size.height,
            child: FlatButton(
              onPressed: () {
                DatePicker.showTimePicker(context,
                    showTitleActions: true,
                    currentTime: DateTime.now(), onConfirm: (value) {
                      startHour = value.hour.toString();
                      startMinute = value.minute.toString();

                      if(startHour.length == 1){
                        startHour = '0' + startHour;
                      }

                      if(startMinute.length == 1){
                        startMinute = '0' + startMinute;
                      }

                      startSecond = "00";

                      setState(() {
                        startTime = startHour+":"+startMinute;
                      });
                    });
              },
              child: Container(
                alignment: Alignment.center,
                width: 0.63 * media.size.width,
                height: 0.075 * media.size.height,
                child: Row(
                  mainAxisAlignment: MainAxisAlignment.spaceBetween,
                  children: <Widget>[
                    Row(
                      children: <Widget>[
                        Container(
                          child: Row(
                            children: <Widget>[
                              Text(
                                startHour + ":" + startMinute,
                                style: TextStyle(
                                    color: Colors.black54,
                                    fontSize: 0.04 * media.size.width),
                              ),
                            ],
                          ),
                        )
                      ],
                    ),
                    Text(
                      "  Change",
                      style: TextStyle(
                          color: Colors.black54,
                          fontSize: 0.04 * media.size.width),
                    ),
                  ],
                ),
              ),
              color: Colors.transparent,
            )),
        borderRadius: BorderRadius.all(Radius.circular(19.0)),
        color: Colors.white);

    final endTimeField = Material(
        shadowColor: Colors.black,
        elevation: 15,
        child: Container(
            alignment: Alignment.centerLeft,
            width: 0.7 * media.size.width,
            height: 0.085 * media.size.height,
            child: FlatButton(
              onPressed: () {
                DatePicker.showTimePicker(context,
                    showTitleActions: true,
                    currentTime: DateTime.now(), onConfirm: (value) {
                      endHour = value.hour.toString();
                      endMinute = value.minute.toString();

                      if(endHour.length == 1){
                        endHour = '0' + endHour;
                      }

                      if(endMinute.length == 1){
                        endMinute = '0' + endMinute;
                      }

                      endSecond = "00";

                      setState(() {
                        endTime = endHour+":"+endMinute;
                      });
                    });
              },
              child: Container(
                alignment: Alignment.center,
                width: 0.63 * media.size.width,
                height: 0.075 * media.size.height,
                child: Row(
                  mainAxisAlignment: MainAxisAlignment.spaceBetween,
                  children: <Widget>[
                    Row(
                      children: <Widget>[
                        Container(
                          child: Row(
                            children: <Widget>[
                              Text(
                                endHour + ":" + endMinute,
                                style: TextStyle(
                                    color: Colors.black54,
                                    fontSize: 0.04 * media.size.width),
                              ),
                            ],
                          ),
                        )
                      ],
                    ),
                    Text(
                      "  Change",
                      style: TextStyle(
                          color: Colors.black54,
                          fontSize: 0.04 * media.size.width),
                    ),
                  ],
                ),
              ),
              color: Colors.transparent,
            )),
        borderRadius: BorderRadius.all(Radius.circular(19.0)),
        color: Colors.white);

    final descriptionField = Material(
        shadowColor: Colors.black,
        elevation: 15,
        child: Container(
            alignment: Alignment.topLeft,
            padding: EdgeInsets.all(0.01 * media.size.width),
            height: 0.3 * media.size.height,
            width: 0.7 * media.size.width,
            child: TextFormField(
                controller : descriptionHolder,
                cursorColor: Colors.black45,
                style: TextStyle(
                  color: Colors.black54,
                ),
                maxLines: 9,
                minLines: 1,
                decoration: InputDecoration(
                    filled: true,
                    fillColor: Colors.white,
                    labelText: 'Description of class',
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
                    description = value;
                  });
                })),
        borderRadius: BorderRadius.all(Radius.circular(19.0)),
        color: Colors.white);

    final maxField = Material(
        shadowColor: Colors.black,
        elevation: 15,
        child: Container(
            alignment: Alignment.topLeft,
            padding: EdgeInsets.all(0.01 * media.size.width),
            height: 0.085 * media.size.height,
            width:  0.7 * media.size.width,
            child: TextFormField(
                controller : maxHolder,
                cursorColor: Colors.black45,
                style: TextStyle(
                  color: Colors.black54,
                ),
                maxLines: 1,
                minLines: 1,
                keyboardType: TextInputType.number,
                decoration: InputDecoration(
                    filled: true,
                    fillColor: Colors.white,
                    labelText: 'Maximum students',
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
                    max = value;
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
                'Add a New Class',
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
            key: editFormKey,
            child: Column(children: <Widget>[
              classNameField,
              SizedBox(height: 0.04 * media.size.height),
              instructorNameField,
              SizedBox(height: 0.04 * media.size.height),
              dayField,
              SizedBox(height: 0.04 * media.size.height),
              startTimeField,
              SizedBox(height: 0.04 * media.size.height),
              endTimeField,
              SizedBox(height: 0.04 * media.size.height),
              maxField,
              SizedBox(height: 0.04 * media.size.height),
              descriptionField,
              SizedBox(height: 0.04 * media.size.height)
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
      ]),
    );
  }

  /*
  Method Name:
    _showAlertDialog
  Purpose:
    This method is used when adding a new class to the database.
           If a class is added successfully, the alert dialog will show to confirm this.
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
  Method name:
    sendValuesToDatabase
  Purpose:
    This function will send the updated details to the database.
   */
  sendValuesToDatabase() {
    if (className == "" ||
        instructorName == "" ||
        day == "" ||
        description == "") {
      _errorDialogue("All fields need to be filled in.");

      return;
    }

    if (int.parse(startHour) < 6 || int.parse(startHour) > 19) {
      _errorDialogue("Time has to be after 6am and before 8pm.");
      return;
    }

    if (int.parse(endHour) < 6 || int.parse(endHour) > 19) {
      _errorDialogue("Time has to be after 6am and before 8pm.");
      return;
    }

    _addClass();

  }


/*
  Method Name:
    _defaultFields
  Purpose:
   Once a class has been added successfully the fields are
    returned to default values so that the manager can add another class.
*/
  void _defaultFields() {
    nameHolder.clear();
    dayHolder.clear();
    instructorHolder.clear();
    descriptionHolder.clear();
    maxHolder.clear();
    currentHolder.clear();
  }

/*
  Method Name:
    _addClass
  Purpose:
    This method is called when the all the fields in the form have been verified.
    It makes a post request to our API with the respective fields, to allow a manager to add
    a new class to the database. Once the class has been added successfully the fields are
    returned to default values and the manager can add another class.
*/
  _addClass() async {
    String url = 'https://gymmoveswebapi.azurewebsites.net/api/classes/add';

    final prefs = await SharedPreferences.getInstance();
    NewClass myClass = new  NewClass(prefs.get("gymId"), instructorUsername,
        className, description, day, startTime, endTime, int.parse(max));

    var map = new Map<String, dynamic>();
    map["Username"] = prefs.get("username");
    map["NewClass"] = myClass;

    final http.Response response = await http.post(
        url,
        headers: <String, String>{'Content-type': 'application/json'},
        body: jsonEncode(map)
    );

    if (response.statusCode == 200) {
      _showAlertDialog("The class was added successfully."  , "Great news!");

      _defaultFields();
    } else {
      _showAlertDialog(response.body, "Oh no!");
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
}

class NewClass {
  final int gymId;
  final String instructor;
  final String name;
  final String description;
  final String day;
  final String startTime;
  final String endTime;
  final int maxCapacity;

  NewClass(this.gymId, this.instructor, this.name, this.description, this.day,
      this.startTime, this.endTime, this.maxCapacity);

  Map<String, dynamic> toJson() =>
      {
        'GymId': gymId,
        'Instructor': instructor,
        'Name': name,
        'Description': description,
        'Day': day,
        'StartTime': startTime,
        'EndTime': endTime,
        'MaxCapacity': maxCapacity
      };
}

/*
Class Name:
  Instructor
Purpose:
  This class is the structure of an instructor.
 */
class Instructor {
  final String name;
  final String surname;
  final String username;

  Instructor({this.name, this.surname, this.username});

  factory Instructor.fromJson(Map<String, dynamic> json) {
    return Instructor(
        name: json['name'],
        surname: json['surname'],
        username: json['username']);
  }
}

const String backArrow =
    '<svg viewBox="28.2 38.0 31.4 27.9" ><path transform="matrix(-1.0, 0.0, 0.0, -1.0, 65.61, 71.93)" d="M 21.68118286132813 6 L 18.91737365722656 8.460894584655762 L 29.85499572753906 18.21720886230469 L 6 18.21720886230469 L 6 21.70783996582031 L 29.85499572753906 21.70783996582031 L 18.91737365722656 31.46415710449219 L 21.68118286132813 33.925048828125 L 37.36236572265625 19.9625244140625 L 21.68118286132813 6 Z" fill="#fcfbfc" stroke="none" stroke-width="1" stroke-miterlimit="4" stroke-linecap="butt" /></svg>';
