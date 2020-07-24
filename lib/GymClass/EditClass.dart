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

import 'package:autocomplete_textfield/autocomplete_textfield.dart';
import 'package:flutter/material.dart';
import 'package:flutter_datetime_picker/flutter_datetime_picker.dart';
import 'package:flutter_svg/flutter_svg.dart';

/*
Class Name:
  Edit

Purpose:
  This class creates the class that will build the page.
 */

class EditClass extends StatefulWidget {
  const EditClass({Key key}) : super(key: key);

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

  String className = "a";
  String instructorName = "a";
  String day = "Monday";
  String hour = DateTime.now().hour.toString();
  String minute = DateTime.now().minute.toString();
  String second = DateTime.now().second.toString();
  String description = "a";

  final editFormKey = GlobalKey<FormState>();

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
              initialValue: className,
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
            child: SimpleAutoCompleteTextField(

              style: TextStyle(
                color: Colors.black54,
              ),
              suggestions: [
                "Apple",
                "Armidillo",
                "Actual",
                "Actuary",
                "America",
                "Argentina",
                "Australia",
                "Antarctica",
              ],
              clearOnSubmit: false,
              textSubmitted: (text) => setState(() {
                instructorName = text;
              }),
              decoration: InputDecoration(
                  labelText: 'Instructor\'s name',
                  hintText: instructorName,
                  labelStyle: new TextStyle(color: Colors.black54),
                  border: InputBorder.none,
                  enabledBorder:
                  OutlineInputBorder(borderSide: BorderSide.none)),
            )),
        borderRadius: BorderRadius.all(Radius.circular(19.0)),
        color: Colors.white);

    final dayField = Material(
        shadowColor: Colors.black,
        elevation: 15,
        child: Container(
            padding: EdgeInsets.all(0.01 * media.size.width),
            alignment: Alignment.topLeft,
            width: 0.7 * media.size.width,
            height: 0.085 * media.size.height,
            child: SimpleAutoCompleteTextField(
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
                  hintText: day,
                  labelStyle: new TextStyle(color: Colors.black54),
                  border: InputBorder.none,
                  enabledBorder:
                  OutlineInputBorder(borderSide: BorderSide.none)),
            )),
        borderRadius: BorderRadius.all(Radius.circular(19.0)),
        color: Colors.white);

    final timeField = Material(
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
                      hour = value.hour.toString();
                      minute = value.minute.toString();
                      second = value.second.toString();

                      if(hour.length == 1){
                        hour = '0' + hour;
                      }

                      if(minute.length == 1){
                        minute = '0' + minute;
                      }

                      if(second.length == 1){
                        second = '0' + second;
                      }

                      setState(() {});
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
                                hour + ":" + minute + ":" + second,
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
              initialValue: description,
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

    return Scaffold(
      backgroundColor: const Color(0xff513369),
      body: ListView(children: <Widget>[
        Stack(children: <Widget>[
          Transform.translate(
              offset: Offset(0.0, -0.034 * media.size.height),
              child: Container(
                width: media.size.width,
                height: 0.13 * media.size.height,
                decoration: BoxDecoration(
                  image: DecorationImage(
                    image:
                        const AssetImage('assets/Banner.jpg'),
                    fit: BoxFit.fill,
                    colorFilter: new ColorFilter.mode(
                        Colors.black.withOpacity(0.52), BlendMode.dstIn),
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
            padding: EdgeInsets.fromLTRB(0.0, 0.0, 0.1 * media.size.width, 0.0),
            width: media.size.width,
            child: Text(
              'Edit the Class',
              style: TextStyle(
                fontFamily: 'FreestyleScript',
                fontSize: 0.1 * media.size.width,
                color: const Color(0xffffffff),
              ),
              textAlign: TextAlign.right,
            ),
          ),Transform.translate(
              offset:
              Offset(0.04 * media.size.width, 0.01 * media.size.height),
              child: GestureDetector(
                  onTap: () {
                    Navigator.pop(context);
                  },
                  child: SvgPicture.string(backArrow,
                      allowDrawingOutsideViewBox: true,
                      width: 0.06 * media.size.width)
              )
          ),
        ]
        ),

        Form(
            key: editFormKey,
            child: Column(children: <Widget>[
              classNameField,
              SizedBox(height: 0.04 * media.size.height),
              instructorNameField,
              SizedBox(height: 0.04 * media.size.height),
              dayField,
              SizedBox(height: 0.04 * media.size.height),
              timeField,
              SizedBox(height: 0.04 * media.size.height),
              descriptionField,
              SizedBox(height: 0.04 * media.size.height),
            ])),
        Center(
            child: SizedBox(
                width: 0.25 * media.size.width,
                child: RaisedButton(
                  shape: RoundedRectangleBorder(
                      borderRadius: BorderRadius.circular(10.0)
                  ),
                  color: const Color(0xffffffff).withOpacity(0.3),
                  onPressed: () {
                    _editClass();
                  },
                  textColor: Colors.white,
                  padding: const EdgeInsets.all(0.0),
                  child: Container(
                    padding: const EdgeInsets.all(10.0),
                    child: Text(
                      'Submit',
                      style: TextStyle(
                          fontSize: 0.05 * media.size.width,
                          fontFamily: 'Roboto'
                      ),
                    ),
                  ),
                )
            )
        ),
        SizedBox(height: 0.06 * media.size.height),
      ]
      ),
    );
  }

  /*
  Method name:
    _editClass

  Purpose:
    This function will send the updated details to the database.
   */
  _editClass() {}

}

const String backArrow =
    '<svg viewBox="28.2 38.0 31.4 27.9" ><path transform="matrix(-1.0, 0.0, 0.0, -1.0, 65.61, 71.93)" d="M 21.68118286132813 6 L 18.91737365722656 8.460894584655762 L 29.85499572753906 18.21720886230469 L 6 18.21720886230469 L 6 21.70783996582031 L 29.85499572753906 21.70783996582031 L 18.91737365722656 31.46415710449219 L 21.68118286132813 33.925048828125 L 37.36236572265625 19.9625244140625 L 21.68118286132813 6 Z" fill="#fcfbfc" stroke="none" stroke-width="1" stroke-miterlimit="4" stroke-linecap="butt" /></svg>';



