/*
File Name
  EditClass.dart

Author:
  Danel

Date Created
  16/06/2020

Update History:
--------------------------------------------------------------------------------
| Name               | Date              | Changes                             |
--------------------------------------------------------------------------------
Danel               | 05/08/2020        | AFixed UI
--------------------------------------------------------------------------------

Functional Description:
  This file contains the Edit class that calls the class that creates the UI.
  The EditState class handles the building of the UI and making all the
  components functional and responsive.
  This file will also handle sending the information that is entered to the
  database.

Classes in the File:
- Edit
- EditState
 */

import 'dart:convert';
import 'dart:math';

import 'package:autocomplete_textfield/autocomplete_textfield.dart';
import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';
import 'package:flutter_datetime_picker/flutter_datetime_picker.dart';
import 'package:flutter_svg/flutter_svg.dart';
import 'package:gym_moves/GymClass/ManagerClassDetails.dart';
import 'package:gym_moves/NavigationBar.dart';
import 'package:http/http.dart';
import 'package:shared_preferences/shared_preferences.dart';
import 'package:gym_moves/GymClass/InstructorClassDetails.dart';

/*
Class Name:
  Edit

Purpose:
  This class creates the class that will build the page.
 */

class EditClass extends StatefulWidget {
  final String className, instructorUsername, instructorName, day, description;
  final String max, current, startTime, endTime;
  final int classId;
  final bool cancelled;

  const EditClass(
      {Key key,
      this.classId,
      this.className,
      this.instructorName,
      this.instructorUsername,
      this.day,
      this.description,
      this.max,
      this.current,
      this.startTime,
      this.endTime,
      this.cancelled})
      : super(key: key);

  @override
  EditClassState createState() => EditClassState();
}

/*
Class Name:
  EditState

Purpose:
  This class will build the page, and send information to the database.
 */
class EditClassState extends State<EditClass> {
  String className, instructorUsername, instructorName, day, description;
  String max, startTime, endTime, current;
  int classId;
  bool cancelled;

  int userType;

  String startHour, startMinute, startSecond, endHour, endMinute, endSecond;

  final nameHolder = TextEditingController();
  final dayHolder = TextEditingController();
  final instructorHolder = TextEditingController();
  final descriptionHolder = TextEditingController();
  final maxHolder = TextEditingController();

  final editFormKey = GlobalKey<FormState>();

  Future gettingInstructors;
  AutoCompleteTextField searchTextField;
  List<Instructor> instructors = [];
  GlobalKey<AutoCompleteTextFieldState<Instructor>> instructorKey =
      new GlobalKey();

  @override
  void initState() {
    super.initState();
    gettingInstructors = _getInstructors();
    doDetails();
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
    userType = prefs.get("type");

    String url =
        'https://gymmoveswebapi.azurewebsites.net/api/user/allInstructors?gymID=' +
            gymId.toString();
    Response response = await get(url);

    List<dynamic> instructorsJSON = json.decode(response.body);

    for (int i = 0; i < instructorsJSON.length; i++) {
      instructors.add(Instructor.fromJson(instructorsJSON[i]));
    }

    return instructors;
  }

  doDetails() {
    className = this.widget.className;
    nameHolder.text = this.widget.className;

    dayHolder.text = this.widget.day;
    day = this.widget.day;

    instructorHolder.text = this.widget.instructorName +
        " (" +
        this.widget.instructorUsername +
        ")";
    instructorName = this.widget.instructorName;
    instructorUsername = this.widget.instructorUsername;

    description = this.widget.description;
    max = this.widget.max;
    classId = this.widget.classId;
    cancelled = this.widget.cancelled;
    current = this.widget.current;

    List start = this.widget.startTime.split(":");
    startHour = start[0];
    startMinute = start[1];
    startSecond = "00";

    startTime = this.widget.startTime;

    List end = this.widget.endTime.split(":");
    endHour = end[0];
    endMinute = end[1];
    endSecond = "00";

    endTime = this.widget.endTime;
  }

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

    final classNameField = Container(
        width: 0.7 * media.size.width,
        alignment: Alignment.centerLeft,
        child: TextFormField(
            controller: nameHolder,
            cursorColor: Color(0xff787878),
            obscureText: false,
            style: TextStyle(
              color: Color(0xff787878),
            ),
            decoration: InputDecoration(
                filled: true,
                fillColor: Colors.white,
                labelText: 'Class Name',
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
                className = value;
              });
            }));

    final instructorNameField = Container(
        alignment: Alignment.topLeft,
        width: 0.7 * media.size.width,
        child: searchTextField);

    searchTextField = AutoCompleteTextField<Instructor>(
      style: TextStyle(
        color: Colors.black54,
      ),
      itemBuilder: (context, item) {
        return Row(
          mainAxisAlignment: MainAxisAlignment.spaceBetween,
          children: <Widget>[
            Text(
              item.name + " " + item.surname + " (" + item.username + ")",
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
            item.name + " " + item.surname + " (" + item.username + ")");
        instructorName = item.name + " " + item.surname;
        instructorUsername = item.username;
      },
      key: instructorKey,
      suggestions: instructors,
      clearOnSubmit: false,
      controller: instructorHolder,
      decoration: InputDecoration(
          labelText: 'Instructor Name',
          labelStyle: new TextStyle(color: Color(0xff787878)),
          contentPadding: const EdgeInsets.all(20.0),
          enabledBorder: OutlineInputBorder(
              borderRadius: BorderRadius.all(Radius.circular(15.0)),
              borderSide: new BorderSide(color: Color(0xff787878))),
          focusedBorder: OutlineInputBorder(
              borderSide: new BorderSide(color: Color(0xff787878)),
              borderRadius: BorderRadius.circular(15.0))),
    );

    final dayField = Container(
        padding: EdgeInsets.all(0.01 * media.size.width),
        alignment: Alignment.topLeft,
        width: 0.7 * media.size.width,
        child: SimpleAutoCompleteTextField(
          controller: dayHolder,
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
              filled: true,
              fillColor: Colors.white,
              labelText: 'Day',
              contentPadding: const EdgeInsets.all(20.0),
              labelStyle: new TextStyle(color: Color(0xff787878)),
              enabledBorder: OutlineInputBorder(
                  borderRadius: BorderRadius.all(Radius.circular(15.0)),
                  borderSide: new BorderSide(color: Color(0xff787878))),
              focusedBorder: OutlineInputBorder(
                  borderSide: new BorderSide(color: Color(0xff787878)),
                  borderRadius: BorderRadius.circular(15.0))),
        ));

    final startTimeField = Container(
        alignment: Alignment.centerLeft,
        width: 0.7 * media.size.width,
        height: 0.14 * media.size.width,
        decoration: BoxDecoration(
            borderRadius: BorderRadius.all(Radius.circular(15.0)),
            border: Border.all(
              color: Color(0xff787878),
            )),
        child: FlatButton(
          onPressed: () {
            DatePicker.showTimePicker(context,
                showTitleActions: true,
                currentTime: DateTime.now(), onConfirm: (value) {
              startHour = value.hour.toString();
              startMinute = value.minute.toString();

              if (startHour.length == 1) {
                startHour = '0' + startHour;
              }

              if (startMinute.length == 1) {
                startMinute = '0' + startMinute;
              }

              startSecond = "00";

              setState(() {
                startTime = startHour + ":" + startMinute;
              });
            });
          },
          child: Container(
            alignment: Alignment.center,
            width: 0.63 * media.size.width,
            child: Row(
              mainAxisAlignment: MainAxisAlignment.spaceBetween,
              children: <Widget>[
                Row(
                  children: <Widget>[
                    Container(
                      child: Row(
                        children: <Widget>[
                          Text(
                            " Start time:    " + startHour + ":" + startMinute,
                            style: TextStyle(
                                color: Colors.black54,
                                fontSize: 0.04 * media.size.width),
                          ),
                        ],
                      ),
                    )
                  ],
                ),
              ],
            ),
          ),
          color: Colors.transparent,
        ));

    final endTimeField = Container(
        alignment: Alignment.centerLeft,
        width: 0.7 * media.size.width,
        height: 0.14 * media.size.width,
        decoration: BoxDecoration(
            borderRadius: BorderRadius.all(Radius.circular(15.0)),
            border: Border.all(
              color: Color(0xff787878),
            )),
        child: FlatButton(
          onPressed: () {
            DatePicker.showTimePicker(context,
                showTitleActions: true,
                currentTime: DateTime.now(), onConfirm: (value) {
              endHour = value.hour.toString();
              endMinute = value.minute.toString();

              if (endHour.length == 1) {
                endHour = '0' + endHour;
              }

              if (endMinute.length == 1) {
                endMinute = '0' + endMinute;
              }

              endSecond = "00";

              setState(() {
                endTime = endHour + ":" + endMinute;
              });
            });
          },
          child: Container(
            alignment: Alignment.center,
            width: 0.63 * media.size.width,
            child: Row(
              mainAxisAlignment: MainAxisAlignment.spaceBetween,
              children: <Widget>[
                Row(
                  children: <Widget>[
                    Container(
                      child: Row(
                        children: <Widget>[
                          Text(
                            " End time:    " + endHour + ":" + endMinute,
                            style: TextStyle(
                                color: Colors.black54,
                                fontSize: 0.04 * media.size.width),
                          ),
                        ],
                      ),
                    )
                  ],
                ),
              ],
            ),
          ),
          color: Colors.transparent,
        ));

    final descriptionField = Container(
        alignment: Alignment.topLeft,
        padding: EdgeInsets.all(0.01 * media.size.width),
        width: 0.7 * media.size.width,
        child: TextFormField(
            initialValue: description,
            cursorColor: Colors.black45,
            style: TextStyle(
              color: Colors.black54,
            ),
            maxLines: 9,
            decoration: InputDecoration(
                filled: true,
                fillColor: Colors.white,
                labelText: 'Description',
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
                description = value;
              });
            }));

    final maxField = Container(
        alignment: Alignment.topLeft,
        padding: EdgeInsets.all(0.01 * media.size.width),
        width: 0.7 * media.size.width,
        child: TextFormField(
            initialValue: max,
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
                labelText: 'Class Capacity',
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
                max = value;
              });
            }));

    return Scaffold(
      backgroundColor: const Color(0xffffffff),
      body: ListView(children: <Widget>[
        Stack(children: <Widget>[
          Transform.translate(
              offset: Offset(0.05 * media.size.width, 0.0),
              child: Container(
                  width: 0.9 * media.size.width,
                  height: 0.3 * media.size.height,
                  decoration: BoxDecoration(
                      image: DecorationImage(
                          image: const AssetImage('assets/AddClassPicture.png'),
                          fit: BoxFit.fill)))),
          Transform.translate(
              offset: Offset(0.0, 0.04 * media.size.height),
              child: Transform.translate(
                  offset: Offset(
                      0.05 * media.size.width, -0.02 * media.size.height),
                  child: GestureDetector(
                      onTap: () {
                        Navigator.pop(context);
                        Navigator.pop(context);
                        userType == 1
                            ? Navigator.push(
                                context,
                                MaterialPageRoute(
                                    builder: (context) =>
                                        InstructorClassDetails(
                                          className: className,
                                          instructorName: instructorName,
                                          instructorUsername:
                                              instructorUsername,
                                          classId: classId,
                                          classDay: day,
                                          description: description,
                                          max: int.parse(max),
                                          classStart:
                                              startHour + ":" + startMinute,
                                          classEnd: endHour + ":" + endMinute,
                                          cancel: cancelled,
                                          availableSpots: int.parse(current),
                                        )))
                            : Navigator.push(
                                context,
                                MaterialPageRoute(
                                    builder: (context) => ManagerClassDetails(
                                          className: className,
                                          instructorName: instructorName,
                                          instructorUsername:
                                              instructorUsername,
                                          classId: classId,
                                          classDay: day,
                                          description: description,
                                          max: int.parse(max),
                                          classStart:
                                              startHour + ":" + startMinute,
                                          classEnd: endHour + ":" + endMinute,
                                          cancel: cancelled,
                                          availableSpots: int.parse(current),
                                        )));
                      },
                      child: SvgPicture.string(
                        backArrow,
                        allowDrawingOutsideViewBox: true,
                        width: 0.06 * media.size.width,
                        color: const Color(0xff7341E6),
                      )))),
          Container(
              alignment: Alignment.bottomCenter,
              width: media.size.width,
              height: 0.33 * media.size.height,
              padding: EdgeInsets.all(0.01 * media.size.width),
              child: Text('Edit a Class',
                  style: TextStyle(
                    fontFamily: 'Lastwaerk',
                    fontSize: 0.08 * media.size.width,
                    color: const Color(0xff3e3e3e),
                  )))
        ]),
        SizedBox(height: 0.05 * media.size.height),
        Form(
            key: editFormKey,
            child: Column(children: <Widget>[
              classNameField,
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
              SizedBox(height: 0.04 * media.size.height),
              instructorNameField,
              SizedBox(height: 0.04 * media.size.height)
            ])),
        Center(
            child: SizedBox(
                width: 0.4 * media.size.width,
                child: FlatButton(
                    shape: RoundedRectangleBorder(
                        borderRadius: BorderRadius.circular(10.0)),
                    color: const Color(0xff7341E6),
                    onPressed: () {
                      _editClass();
                    },
                    textColor: Colors.white,
                    padding: const EdgeInsets.all(0.0),
                    child: Container(
                        padding: const EdgeInsets.all(10.0),
                        child: Text('Submit',
                            style: TextStyle(
                                fontSize: 0.04 * media.size.width,
                                fontFamily: 'Roboto')))))),
        SizedBox(height: 0.06 * media.size.height),
      ]),
    );
  }

  /*
  Method name:
    _editClass

  Purpose:
    This function will send the updated details to the database.
   */
  _editClass() async {
    var start =
        new DateTime(0, 0, 0, int.parse(startHour), int.parse(startMinute));
    var end = new DateTime(0, 0, 0, int.parse(endHour), int.parse(endMinute));

    if (end.difference(start).inMinutes < 20) {
      _showAlertDialog(
          "Oh no!", "Classes cannot be less than 20 minutes long.");
      return;
    }

    String url = 'https://gymmoveswebapi.azurewebsites.net/api/classes/edit';

    SharedPreferences prefs = await SharedPreferences.getInstance();
    String user = prefs.get("username");

    final Response response = await post(url,
        headers: <String, String>{'Content-type': 'application/json'},
        body: jsonEncode({
          "EditorUsername": user,
          "InstructorUsername": instructorUsername,
          "Name": className,
          "Day": day,
          "StartTime": startTime,
          "EndTime": endTime,
          "MaxCapacity": int.parse(max),
          "ClassId": classId,
          "Cancelled": cancelled,
          "Description": description
        }));

    if (response.statusCode == 200) {
      _showAlertDialog("Great news!", "The class was edited successfully.");
    } else {
      _showAlertDialog("Oh no!", response.body);
    }
  }

  /*
  Method name:
    _showAlertDialog

  Purpose:
    Show alert.
   */

  void _showAlertDialog(String heading, String body) {
    // set up the button
    Widget okButton = FlatButton(
      child: Text("Ok", style: TextStyle(color: Color(0xff7341E6))),
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
