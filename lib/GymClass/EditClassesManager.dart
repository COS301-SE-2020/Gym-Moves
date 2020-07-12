/*
File Name
  EditClassesManager.dart

Author:
  Danel

Date Created
  25/06/2020

Update History:
--------------------------------------------------------------------------------
| Name               | Date              | Changes                             |
--------------------------------------------------------------------------------

Functional Description:


Classes in the File:
- EditClassesManager
- EditClassesManagerState
 */

import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';
import 'package:flutter_svg/flutter_svg.dart';

import 'package:gymmoves/GymClass/EditClass.dart';
import 'package:gymmoves/GymClass/AddAClass.dart';

/*
Class Name:
  EditClassesManager

Purpose:
  This class creates the class that will build the page.
 */
class EditClassesManager extends StatefulWidget {
  const EditClassesManager({Key key}) : super(key: key);

  @override
  EditClassesManagerState createState() => EditClassesManagerState();
}

/*
Class Name:
EditClassesManagerState

Purpose:

 */
class EditClassesManagerState extends State<EditClassesManager> {
  /*
   Method Name:
    build

   Purpose:

   */
  @override
  Widget build(BuildContext context) {
    MediaQueryData media = MediaQuery.of(context);

    return Scaffold(
      backgroundColor: const Color(0xff513369),
      body: Column(children: <Widget>[
        Stack(children: <Widget>[
          Container(
            width: media.size.width,
            height: 0.13 * media.size.height,
            decoration: BoxDecoration(
              image: DecorationImage(
                image: const AssetImage('assets/Banner.jpg'),
                fit: BoxFit.fill,
                colorFilter: new ColorFilter.mode(
                    Colors.black.withOpacity(0.55), BlendMode.dstIn),
              ),
              boxShadow: [
                BoxShadow(
                  color: const Color(0x46000000),
                  offset: Offset(0, 3),
                  blurRadius: 6,
                ),
              ],
            ),
          ),
          Transform.translate(
              offset: Offset(0.05 * media.size.width, 0.09 * media.size.width),
              child: GestureDetector(
                onTap: () {
                  Navigator.pop(context);
                },
                  child: SvgPicture.string(backArrow,
                      allowDrawingOutsideViewBox: true,
                      width: 0.06 * media.size.width)
              ))
        ]),
        Row(
            mainAxisAlignment: MainAxisAlignment.spaceBetween,
            children: <Widget>[
              Container(
                  padding: EdgeInsets.all(0.02 * media.size.width),
                  child: Text(
                    "Choose a class to edit: ",
                    style: TextStyle(
                        fontSize: 0.04 * media.size.width,
                        fontFamily: 'Roboto',
                        color: Colors.white),
                  )),
              Container(
                  padding: EdgeInsets.all(0.02 * media.size.width),
                  child: FlatButton(
                    shape: RoundedRectangleBorder(
                        borderRadius: BorderRadius.circular(10.0)),
                    color: const Color(0x26ffffff),
                    onPressed: () {
                      Navigator.push(
                          context,
                          MaterialPageRoute(
                            builder: (context) => AddAClass(),
                          ));
                    },
                    textColor: Colors.white,
                    padding: const EdgeInsets.all(0.0),
                    child: Container(
                      padding: const EdgeInsets.all(10.0),
                      child: Text(
                        'New Class',
                        style: TextStyle(
                            fontSize: 0.04 * media.size.width,
                            fontFamily: 'Roboto'),
                      ),
                    ),
                  ))
            ]),
        Expanded(child: getClasses(media))
      ]),
    );
  }

  /*
   Method Name:
    getClasses

   Purpose:

   */
  Widget getClasses(MediaQueryData media) {
    List<Widget> classes = new List();

    /*
  Explanation : This will be when there are no classes assigned to the
                instructor.
   */
    if (false) {
      /*
    A pop up dialog would be nice for this.
     */
      classes.add(Text(
        "You are currently not instructing any classes!",
        textAlign: TextAlign.center,
        style: TextStyle(
            fontFamily: 'Roboto',
            fontSize: 0.05 * media.size.width,
            color: Colors.white70),
      ));
    }
    /*
  Explanation : This will be when there are classes assigned to the
                instructor.
   */
    else {
      int amountOfClasses = 5;

      for (int i = 0; i < amountOfClasses; i++) {
        classes.add(GestureDetector(
            onTap: () {
              Navigator.push(
                  context,
                  MaterialPageRoute(
                    builder: (context) => EditClass(),
                  ));
            },
            child: Row(mainAxisAlignment:MainAxisAlignment.center,children: <Widget>[ Stack(children: <Widget>[
              Container(
                  width: 0.7 * media.size.width,
                  height: 0.15 * media.size.height,
                  decoration: BoxDecoration(
                    borderRadius: BorderRadius.circular(22.0),
                    color: const Color(0x26ffffff),
                    border:
                    Border.all(width: 1.0, color: const Color(0x26707070)),
                  )),
              Transform.translate(
                  offset:
                  Offset(0.05 * media.size.width, 0.02 * media.size.height),
                  child: SizedBox(
                      width: 0.7 * media.size.width,
                      child: Text("Class Name: ",
                          style: TextStyle(
                              color: Colors.white,
                              fontSize: 0.035 * media.size.width)))),
              Transform.translate(
                  offset:
                  Offset(0.05 * media.size.width, 0.06 * media.size.height),
                  child: SizedBox(
                      width:0.7 * media.size.width,
                      child: Text("Class Day: ",
                          style: TextStyle(
                              color: Colors.white,
                              fontSize: 0.035 * media.size.width)))),
              Transform.translate(
                  offset:
                  Offset(0.05 * media.size.width, 0.1 * media.size.height),
                  child: SizedBox(
                      width: 0.7 * media.size.width,
                      child: Text("Class Time: ",
                          style: TextStyle(
                              color: Colors.white,
                              fontSize: 0.035 * media.size.width))))
            ])])));

        classes.add(SizedBox(height: 20));
      }
    }

    return ListView(padding: const EdgeInsets.all(15), children: classes);
  }
}

const String backArrow =
    '<svg viewBox="28.2 38.0 31.4 27.9" ><path transform="matrix(-1.0, 0.0, 0.0, -1.0, 65.61, 71.93)" d="M 21.68118286132813 6 L 18.91737365722656 8.460894584655762 L 29.85499572753906 18.21720886230469 L 6 18.21720886230469 L 6 21.70783996582031 L 29.85499572753906 21.70783996582031 L 18.91737365722656 31.46415710449219 L 21.68118286132813 33.925048828125 L 37.36236572265625 19.9625244140625 L 21.68118286132813 6 Z" fill="#fcfbfc" stroke="none" stroke-width="1" stroke-miterlimit="4" stroke-linecap="butt" /></svg>';
