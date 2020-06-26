/*
File Name
  ViewMyProfile.dart

Author:
  Danel

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
import '../GymClass/EditClassesInstructor.dart';
import '../Rating/InstructorViewMyRatings.dart';
import 'ViewMyProfile.dart';

class InstructorPages extends StatelessWidget {
  InstructorPages({
    Key key,
  }) : super(key: key);

  @override
  Widget build(BuildContext context) {
    /* gets screen properties */
    MediaQueryData media = MediaQuery.of(context);

    return Scaffold(
        backgroundColor: const Color(0xff513369),

        /* allows for the screen drag, each child is a screen it can slide to */
        body: PageView(

            children: <Widget>[
          /* Screen One: welcome page */
              Stack(

              /* Children is everything that appears on this one screen. */
                  children: <Widget>[

                    /* Child 1: background picture for the welcome page */
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

                    /* Child 2: welcome message */
                    Transform.translate(
                        offset: Offset(0.0, 0.4 * media.size.height),
                        child: Container(
                            height: 1 / 5 * media.size.height,
                            width: media.size.width,
                            /* This auto sizes text to make sure it fits in one line */
                            child: AutoSizeText(
                              'Welcome name!',
                              style: TextStyle(
                                fontFamily: 'Roboto',
                                fontSize: media.size.width * 0.1,
                                color: const Color(0xffffffff),
                                shadows: [
                                  Shadow(
                                    color: const Color(0xbd000000),
                                    offset: Offset(0, 3),
                                    blurRadius: 6,
                                  ),
                                ],
                              ),
                              maxLines: 1,
                              textAlign: TextAlign.center,
                            )
                        )
                    ),

                    /* Child 3: number of people at gym */

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
                            )
                        )
                    ),

                    /* Child 4: number */
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
                            )
                        )
                    )
                  ]
              ),

          // menu page
          Stack(
              children: <Widget>[
                /* background picture for the menu page*/
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
                /* First menu box */
                Transform.translate(
                  offset: Offset(0.1 * media.size.width, 1.2/5 * media.size.height),
                  child: Container(
                    width: 0.8 * media.size.width,
                    height: 0.1 * media.size.height,
                    decoration: BoxDecoration(
                      borderRadius: BorderRadius.circular(19.0),
                      color: const Color(0x30ffffff),
                      border: Border.all(width: 1.0, color: const Color(0x30707070)),
                    ),
                  ),
                ),
                /* Second menu box */
                Transform.translate(
                  offset: Offset(0.1 * media.size.width, 2.2/5 * media.size.height),
                  child: Container(
                    width: 0.8 * media.size.width,
                    height: 0.1 * media.size.height,
                    decoration: BoxDecoration(
                      borderRadius: BorderRadius.circular(19.0),
                      color: const Color(0x30ffffff),
                      border: Border.all(width: 1.0, color: const Color(0x30707070)),
                    ),
                  ),
                ),
                /* Third menu box */
                Transform.translate(
                  offset: Offset(0.1 * media.size.width, 3.2/5 * media.size.height),
                  child: Container(
                    width: 0.8 * media.size.width,
                    height: 0.1 * media.size.height,
                    decoration: BoxDecoration(
                      borderRadius: BorderRadius.circular(19.0),
                      color: const Color(0x30ffffff),
                      border: Border.all(width: 1.0, color: const Color(0x30707070)),
                    ),
                  ),
                ),
                /* Text for menu box 1 */
                Transform.translate(
                  offset: Offset(0.15 * media.size.width, 1.35/5 * media.size.height),
                  child: Text(
                    'Edit my classes',
                    style: TextStyle(
                      fontFamily: 'Roboto',
                      fontSize: 0.053 * media.size.width,
                      color: const Color(0xfffcfbfc),
                    ),
                    textAlign: TextAlign.left,
                  ),
                ),
                /* Text for menu box 2 */
                Transform.translate(
                  offset: Offset(0.15 * media.size.width, 2.35/5 * media.size.height),
                  child: Text(
                    'View my ratings',
                    style: TextStyle(
                      fontFamily: 'Roboto',
                      fontSize: 0.053 * media.size.width,
                      color: const Color(0xfffcfbfc),
                    ),
                    textAlign: TextAlign.left,
                  ),
                ),
                /* Text for menu box 3 */
                Transform.translate(
                  offset: Offset(0.15 * media.size.width, 3.35/5 * media.size.height),
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
                /*Arrow for first menu box*/
                Transform.translate(
                  offset: Offset(0.8 * media.size.width, 1.4/5 * media.size.height),
                  child:
                  // Adobe XD layer: 'Icon material-arrow…' (shape)
                  GestureDetector(
                    onTap: () {
                      Navigator.push(
                        context,
                        MaterialPageRoute(builder: (context) => EditClassesInstructor()),
                      );
                    },
                    child: SvgPicture.string(
                      frontArrow,
                      allowDrawingOutsideViewBox: true,
                    ),
                  )
                ),
                /*Arrow for second menu box*/
                Transform.translate(
                  offset: Offset(0.8 * media.size.width, 2.4/5 * media.size.height),
                  child:
                  GestureDetector(
                    onTap: () {
                      Navigator.push(
                        context,
                        MaterialPageRoute(builder: (context) => InstructorViewMyRatings()),
                      );
                    },
                    child: SvgPicture.string(
                      frontArrow,
                      allowDrawingOutsideViewBox: true,
                    ),
                  )
                ),
                /*Arrow for third menu box*/
                Transform.translate(
                  offset: Offset(0.8 * media.size.width, 3.4/5 * media.size.height),
                  child:
                  GestureDetector(
                    onTap: () {
                      Navigator.push(
                        context,
                        MaterialPageRoute(builder: (context) => ViewMyProfile()),
                      );
                    },
                    child: SvgPicture.string(
                      frontArrow,
                      allowDrawingOutsideViewBox: true,
                    ),
                  )
                )
              ]
            )
           ]
        )
        );
  }
}

const String frontArrow =
    '<svg viewBox="288.2 250.0 21.8 18.5" ><path transform="translate(282.25, 244.0)" d="M 16.87643432617188 6 L 14.95946311950684 7.633152008056641 L 22.54577445983887 14.10784912109375 L 6 14.10784912109375 L 6 16.42437744140625 L 22.54577445983887 16.42437744140625 L 14.95946216583252 22.89907455444336 L 16.87643432617188 24.5322265625 L 27.75286865234375 15.26611328125 L 16.87643432617188 6 Z" fill="#fcfbfc" stroke="none" stroke-width="1" stroke-miterlimit="4" stroke-linecap="butt" /></svg>';
