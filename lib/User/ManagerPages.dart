/*
File Name
  ManagerPages.dart

Author:
  Raeesa

Date Created
  13/06/2020

Update History:
--------------------------------------------------------------------------------
| Name               | Date              | Changes                             |
--------------------------------------------------------------------------------



Functional Description:
  This file contains the InstructorPages class that handles building the UI for
  the Welcome page and the menu. It also implements the scroll screen the user
  will first encounter.

Classes in the File:
- InstructorPages
 */

import 'package:flutter/material.dart';
import 'package:flutter_svg/flutter_svg.dart';
import 'package:auto_size_text/auto_size_text.dart';

import '../Rating/ManagerViewClassRatings.dart';
import '../Announcement/SendAnnouncement.dart';
import '../GymClass/EditClassesManager.dart';
import '../Dashboard/Dashboard.dart';
import 'ViewMyProfile.dart';

class ManagerPages extends StatelessWidget {
  ManagerPages({
    Key key,
  }) : super(key: key);

  @override
  Widget build(BuildContext context) {
    MediaQueryData media = MediaQuery.of(context);
    return Scaffold(
        backgroundColor: const Color(0xff513369),
        body: PageView(children: <Widget>[
          /*
              *  The menu page.
              */
          Stack(children: <Widget>[
            /*
                    *  The image of the welcome page.
                    */
            Container(
              width: media.size.width,
              height: media.size.height,
              decoration: BoxDecoration(
                image: DecorationImage(
                  image: const AssetImage('assets/images/LeftSidePool.png'),
                  fit: BoxFit.fill,
                  colorFilter: new ColorFilter.mode(
                      Colors.black.withOpacity(1.0), BlendMode.dstIn),
                ),
              ),
            ),
            /*
                    *  The welcome message.
                    */
            Transform.translate(
                offset: Offset(0.0, 0.4 * media.size.height),
                child: Container(
                    height: 1 / 5 * media.size.height,
                    width: media.size.width,
                    child: AutoSizeText(
                      'Welcome name!',
                      style: TextStyle(
                        fontFamily: 'Roboto',
                        fontSize: 42,
                        color: const Color(0xffffffff),
                        shadows: [
                          Shadow(
                            color: const Color(0xbd000000),
                            offset: Offset(0, 3),
                            blurRadius: 6,
                          )
                        ],
                      ),
                      maxLines: 1,
                      textAlign: TextAlign.center,
                    ))),
            /*
                    *  The number of people message.
                    */
            Transform.translate(
                offset: Offset(0.0, 0.5 * media.size.height),
                child: Container(
                    height: 1 / 10 * media.size.height,
                    width: media.size.width,
                    child: Text(
                      'Number of people at gymname:',
                      style: TextStyle(
                        fontFamily: 'Roboto',
                        fontSize: media.size.width * 0.05,
                        color: const Color(0xffffffff),
                        shadows: [
                          Shadow(
                            color: const Color(0xbd000000),
                            offset: Offset(0, 3),
                            blurRadius: 6,
                          ),
                        ],
                      ),
                      textAlign: TextAlign.center,
                    ))),
            /*
                    *  The actual number of people.
                    */
            Transform.translate(
                offset: Offset(0.0, 0.56 * media.size.height),
                child: Container(
                    height: 1 / 10 * media.size.height,
                    width: media.size.width,
                    child: Text(
                      '#',
                      style: TextStyle(
                        fontFamily: 'Roboto',
                        fontSize: media.size.width * 0.05,
                        color: const Color(0xffffffff),
                        shadows: [
                          Shadow(
                            color: const Color(0xbd000000),
                            offset: Offset(0, 3),
                            blurRadius: 6,
                          ),
                        ],
                      ),
                      textAlign: TextAlign.center,
                    )))
          ]),

          /*
              *  The menu page.
              */
          Stack(children: <Widget>[
            /*
                    *  The image of the menu page.
                    */
            Container(
              width: media.size.width,
              height: media.size.height,
              decoration: BoxDecoration(
                image: DecorationImage(
                  image: const AssetImage('assets/images/RightSidePool.png'),
                  fit: BoxFit.fill,
                  colorFilter: new ColorFilter.mode(
                      Colors.black.withOpacity(1.0), BlendMode.dstIn),
                ),
              ),
            ),
            /*
                    *  The first menu box.
                    */
            Transform.translate(
              offset:
                  Offset(0.1 * media.size.width, 0.8 / 6 * media.size.height),
              child: Container(
                width: 0.8 * media.size.width,
                height: 0.1 * media.size.height,
                decoration: BoxDecoration(
                  borderRadius: BorderRadius.circular(19.0),
                  color: const Color(0x30ffffff),
                  border:
                      Border.all(width: 1.0, color: const Color(0x30707070)),
                ),
              ),
            ),
            /*
                    *  The second menu box.
                    */
            Transform.translate(
              offset:
                  Offset(0.1 * media.size.width, 1.8 / 6 * media.size.height),
              child: Container(
                width: 0.8 * media.size.width,
                height: 0.1 * media.size.height,
                decoration: BoxDecoration(
                  borderRadius: BorderRadius.circular(19.0),
                  color: const Color(0x30ffffff),
                  border:
                      Border.all(width: 1.0, color: const Color(0x30707070)),
                ),
              ),
            ),
            /*
                    *  The third menu box.
                    */
            Transform.translate(
              offset:
                  Offset(0.1 * media.size.width, 2.8 / 6 * media.size.height),
              child: Container(
                width: 0.8 * media.size.width,
                height: 0.1 * media.size.height,
                decoration: BoxDecoration(
                  borderRadius: BorderRadius.circular(19.0),
                  color: const Color(0x30ffffff),
                  border:
                      Border.all(width: 1.0, color: const Color(0x30707070)),
                ),
              ),
            ),
            /*
                    *  The fourth menu box.
                    */
            Transform.translate(
              offset:
                  Offset(0.1 * media.size.width, 3.8 / 6 * media.size.height),
              child: Container(
                width: 0.8 * media.size.width,
                height: 0.1 * media.size.height,
                decoration: BoxDecoration(
                  borderRadius: BorderRadius.circular(19.0),
                  color: const Color(0x30ffffff),
                  border:
                      Border.all(width: 1.0, color: const Color(0x30707070)),
                ),
              ),
            ),
            /*
                    *  The fifth menu box.
                    */
            Transform.translate(
              offset:
                  Offset(0.1 * media.size.width, 4.8 / 6 * media.size.height),
              child: Container(
                width: 0.8 * media.size.width,
                height: 0.1 * media.size.height,
                decoration: BoxDecoration(
                  borderRadius: BorderRadius.circular(19.0),
                  color: const Color(0x30ffffff),
                  border:
                      Border.all(width: 1.0, color: const Color(0x30707070)),
                ),
              ),
            ),
            /*
                    *  The first menu box text.
                    */
            Transform.translate(
              offset:
                  Offset(0.15 * media.size.width, 1 / 6 * media.size.height),
              child: Text(
                'Send an announcement',
                style: TextStyle(
                  fontFamily: 'Roboto',
                  fontSize: 0.053 * media.size.width,
                  color: const Color(0xfffcfbfc),
                ),
                textAlign: TextAlign.left,
              ),
            ),
            /*
                    *  The second menu box text.
                    */
            Transform.translate(
              offset:
                  Offset(0.15 * media.size.width, 2 / 6 * media.size.height),
              child: Text(
                'Edit the classes',
                style: TextStyle(
                  fontFamily: 'Roboto',
                  fontSize: 0.053 * media.size.width,
                  color: const Color(0xfffcfbfc),
                ),
                textAlign: TextAlign.left,
              ),
            ),
            /*
                    *  The third menu box text.
                    */
            Transform.translate(
              offset:
                  Offset(0.15 * media.size.width, 3 / 6 * media.size.height),
              child: Text(
                'View all ratings',
                style: TextStyle(
                  fontFamily: 'Roboto',
                  fontSize: 0.053 * media.size.width,
                  color: const Color(0xfffcfbfc),
                ),
                textAlign: TextAlign.left,
              ),
            ),
            /*
                    *  The fourth menu box text.
                    */
            Transform.translate(
              offset:
                  Offset(0.15 * media.size.width, 4 / 6 * media.size.height),
              child: Text(
                'View the dashboard',
                style: TextStyle(
                  fontFamily: 'Roboto',
                  fontSize: 0.053 * media.size.width,
                  color: const Color(0xfffcfbfc),
                ),
                textAlign: TextAlign.left,
              ),
            ),
            /*
                    *  The fifth menu box text.
                    */
            Transform.translate(
              offset:
                  Offset(0.15 * media.size.width, 5 / 6 * media.size.height),
              child: Text(
                'View my profile',
                style: TextStyle(
                  fontFamily: 'Roboto',
                  fontSize: 0.053 * media.size.width,
                  color: const Color(0xfffcfbfc),
                ),
                textAlign: TextAlign.left,
              ),
            ),
            /*
                    *  The first menu box arrow.
                    */
            Transform.translate(
              offset: Offset(0.8 * media.size.width, 1 / 6 * media.size.height),
              child: GestureDetector(
                onTap: () {
                  Navigator.push(
                    context,
                    MaterialPageRoute(builder: (context) => SendAnnouncement()),
                  );
                },
                child: SvgPicture.string(
                  _svg_j4s3gg,
                  allowDrawingOutsideViewBox: true,
                ),
              ),
            ),
            /*
                    *  The second menu box arrow.
                    */
            Transform.translate(
              offset: Offset(0.8 * media.size.width, 2 / 6 * media.size.height),
              child: GestureDetector(
                onTap: () {
                  Navigator.push(
                    context,
                    MaterialPageRoute(
                        builder: (context) => EditClassesManager()),
                  );
                },
                child: SvgPicture.string(
                  _svg_j4s3gg,
                  allowDrawingOutsideViewBox: true,
                ),
              ),
            ),
            /*
                    *  The third menu box arrow.
                    */
            Transform.translate(
              offset: Offset(0.8 * media.size.width, 3 / 6 * media.size.height),
              child: GestureDetector(
                onTap: () {
                  Navigator.push(
                    context,
                    MaterialPageRoute(
                        builder: (context) => ManagerViewClassRatings()),
                  );
                },
                child: SvgPicture.string(
                  _svg_j4s3gg,
                  allowDrawingOutsideViewBox: true,
                ),
              ),
            ),
            /*
                    *  The fourth menu box arrow.
                    */
            Transform.translate(
                offset:
                    Offset(0.8 * media.size.width, 4 / 6 * media.size.height),
                child: GestureDetector(
                  onTap: () {
                    Navigator.push(
                      context,
                      MaterialPageRoute(builder: (context) => Dashboard()),
                    );
                  },
                  child: SvgPicture.string(
                    _svg_j4s3gg,
                    allowDrawingOutsideViewBox: true,
                  ),
                )),
            /*
                    *  The fifth menu box arrow.
                    */
            Transform.translate(
                offset:
                    Offset(0.8 * media.size.width, 5 / 6 * media.size.height),
                child: GestureDetector(
                  onTap: () {
                    Navigator.push(
                      context,
                      MaterialPageRoute(builder: (context) => ViewMyProfile()),
                    );
                  },
                  child: SvgPicture.string(
                    _svg_j4s3gg,
                    allowDrawingOutsideViewBox: true,
                  ),
                ))
          ])
        ]));
  }
}

const String _svg_j4s3gg =
    '<svg viewBox="288.2 250.0 21.8 18.5" ><path transform="translate(282.25, 244.0)" d="M 16.87643432617188 6 L 14.95946311950684 7.633152008056641 L 22.54577445983887 14.10784912109375 L 6 14.10784912109375 L 6 16.42437744140625 L 22.54577445983887 16.42437744140625 L 14.95946216583252 22.89907455444336 L 16.87643432617188 24.5322265625 L 27.75286865234375 15.26611328125 L 16.87643432617188 6 Z" fill="#fcfbfc" stroke="none" stroke-width="1" stroke-miterlimit="4" stroke-linecap="butt" /></svg>';
