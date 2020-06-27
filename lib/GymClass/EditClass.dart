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

import 'package:flutter/material.dart';
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

  String className = "name";
  String instructorName = "name";
  String day = "Mondays";
  String time = "12:00";
  String description = "a description";

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
            width: 0.7 * media.size.width,
            height: 0.08 * media.size.height,
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
                        borderSide: BorderSide.none
                    ),
                    focusedBorder: OutlineInputBorder(
                        borderSide: BorderSide.none,
                        borderRadius: BorderRadius.circular(19.0)
                    )
                ),
                onChanged: (value) {
                  setState(() {
                    className = value;
                  });
                })
        ),
        borderRadius: BorderRadius.all(Radius.circular(19.0)),
        color: Colors.transparent
    );

    final instructorNameField = Material(
        shadowColor: Colors.black,
        elevation: 15,
        child: Container(
            width: 0.7 * media.size.width,
            height: 0.08 * media.size.height,
            child: TextFormField(
                initialValue: instructorName,
                cursorColor: Colors.black45,
                style: TextStyle(
                  color: Colors.black54,
                ),
                decoration: InputDecoration(
                    filled: true,
                    fillColor: Colors.white,
                    labelText: 'Instructor Name',
                    contentPadding: const EdgeInsets.all(15.0),
                    border: InputBorder.none,
                    labelStyle: new TextStyle(color: Colors.black54),
                    enabledBorder: OutlineInputBorder(
                        borderRadius: BorderRadius.all(Radius.circular(19.0)),
                        borderSide: BorderSide.none
                    ),
                    focusedBorder: OutlineInputBorder(
                        borderSide: BorderSide.none,
                        borderRadius: BorderRadius.circular(19.0)
                    )
                ),
                onChanged: (value) {
                  setState(() {
                    instructorName = value;
                  });
                })
        ),
        borderRadius: BorderRadius.all(Radius.circular(19.0)),
        color: Colors.transparent
    );

    final dayField = Material(
        shadowColor: Colors.black,
        elevation: 15,
        child: Container(
            width: 0.7 * media.size.width,
            height: 0.08 * media.size.height,
            child: TextFormField(
                initialValue: day,
                cursorColor: Colors.black45,
                style: TextStyle(
                  color: Colors.black54,
                ),
                decoration: InputDecoration(
                    filled: true,
                    fillColor: Colors.white,
                    labelText: 'Day of Class',
                    border: InputBorder.none,
                    contentPadding: const EdgeInsets.all(15.0),
                    labelStyle: new TextStyle(color: Colors.black54),
                    enabledBorder: OutlineInputBorder(
                        borderRadius: BorderRadius.all(Radius.circular(19.0)),
                        borderSide: BorderSide.none),
                    focusedBorder: OutlineInputBorder(
                        borderSide: BorderSide.none,
                        borderRadius: BorderRadius.circular(19.0)
                    )
                ),
                onChanged: (value) {
                  setState(() {
                    day = value;
                  });
                })
        ),
        borderRadius: BorderRadius.all(Radius.circular(19.0)),
        color: Colors.transparent
    );

    final timeField = Material(
        shadowColor: Colors.black,
        elevation: 15,
        child: Container(
            width: 0.7 * media.size.width,
            height: 0.08 * media.size.height,
            child: TextFormField(
                initialValue: time,
                cursorColor: Colors.black45,
                style: TextStyle(
                  color: Colors.black54,
                ),
                decoration: InputDecoration(
                    filled: true,
                    fillColor: Colors.white,
                    labelText: 'Time of Class',
                    border: InputBorder.none,
                    contentPadding: const EdgeInsets.all(15.0),
                    labelStyle: new TextStyle(color: Colors.black54),
                    enabledBorder: OutlineInputBorder(
                        borderRadius: BorderRadius.all(Radius.circular(19.0)),
                        borderSide: BorderSide.none
                    ),
                    focusedBorder: OutlineInputBorder(
                        borderSide: BorderSide.none,
                        borderRadius: BorderRadius.circular(19.0)
                    )
                ),
                onChanged: (value) {
                  setState(() {
                    day = value;
                  });
                })
        ),
        borderRadius: BorderRadius.all(Radius.circular(19.0)),
        color: Colors.transparent
    );

    final descriptionField = Material(
        shadowColor: Colors.black,
        elevation: 15,
        child: Container(
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
                    labelText: 'Description of class',
                    border: InputBorder.none,
                    contentPadding: const EdgeInsets.all(15.0),
                    labelStyle: new TextStyle(color: Colors.black54),
                    enabledBorder: OutlineInputBorder(
                        borderRadius: BorderRadius.all(Radius.circular(19.0)),
                        borderSide: BorderSide.none
                    ),
                    focusedBorder: OutlineInputBorder(
                        borderSide: BorderSide.none,
                        borderRadius: BorderRadius.circular(19.0)
                    )
                ),
                onChanged: (value) {
                  setState(() {
                    description = value;
                  });
                })
        ),
        borderRadius: BorderRadius.all(Radius.circular(19.0)),
        color: Colors.transparent
    );

    return Scaffold(
      backgroundColor: const Color(0xff513369),
      body: ListView(children: <Widget>[
        Stack(children: <Widget>[
          Transform.translate(
              offset: Offset(0.0, -0.033 * media.size.height),
              child: Container(
                width: media.size.width,
                height: 1 / 4 * media.size.height,
                decoration: BoxDecoration(
                  image: DecorationImage(
                    image:
                        const AssetImage('assets/images/rightSidePoolHalf.png'),
                    fit: BoxFit.fill,
                    colorFilter: new ColorFilter.mode(
                        Colors.black.withOpacity(1.0), BlendMode.dstIn
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
          Transform.translate(
                  offset:
                      Offset(0.04 * media.size.width, 0.01 * media.size.height),
                  child: GestureDetector(
                    onTap: () {
                      Navigator.pop(context);
                    },
                    child: SvgPicture.string(backButton,
                        allowDrawingOutsideViewBox: true,
                        width: 0.07 * media.size.width
                    )
                  )
          )
        ]
        ),
        Form(
            key: editFormKey,
            child: Column(children: <Widget>[
              classNameField,
              SizedBox(height: 0.06 * media.size.height),
              instructorNameField,
              SizedBox(height: 0.06 * media.size.height),
              dayField,
              SizedBox(height: 0.06 * media.size.height),
              timeField,
              SizedBox(height: 0.06 * media.size.height),
              descriptionField,
              SizedBox(height: 0.06 * media.size.height),
            ]
            )
        ),
        Center(
            child: SizedBox(
                width: 0.25 * media.size.width,
                child: RaisedButton(
                  shape: RoundedRectangleBorder(
                      borderRadius: BorderRadius.circular(10.0)
                  ),
                  color: const Color(0xffffffff).withOpacity(0.3),
                  onPressed: () {
                    sendValuesToDatabase(
                        className, instructorName, day, time, description);
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
    sendValuesToDatabase

  Purpose:
    This function will send the updated details to the database.
   */
  sendValuesToDatabase(className, instructorName, day, time, description) {}

}

const String backButton =
    '<svg viewBox="34.2 38.0 31.4 27.9" ><path transform="matrix(-1.0, 0.0, 0.0, -1.0, 71.61, 71.93)" d="M 21.68118286132813 6 L 18.91737365722656 8.460894584655762 L 29.85499572753906 18.21720886230469 L 6 18.21720886230469 L 6 21.70783996582031 L 29.85499572753906 21.70783996582031 L 18.91737365722656 31.46415710449219 L 21.68118286132813 33.925048828125 L 37.36236572265625 19.9625244140625 L 21.68118286132813 6 Z" fill="#fcfbfc" stroke="none" stroke-width="1" stroke-miterlimit="4" stroke-linecap="butt" /></svg>';
