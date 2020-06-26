/*
File Name
  ViewMyProfile.dart

Author:
  Raeesa

Date Created
  15/06/2020

Update History:
--------------------------------------------------------------------------------
| Name               | Date              | Changes                             |
--------------------------------------------------------------------------------
| Danel              | 26/06/2020        | Made UI responsive and functional   |
--------------------------------------------------------------------------------


Functional Description:
  This file contains the ViewMyProfile class that handles building the UI for
  the profile screen of the users.

Classes in the File:
- ViewMyProfile
 */

import 'package:auto_size_text/auto_size_text.dart';
import 'package:flutter/material.dart';
import 'package:flutter_svg/flutter_svg.dart';

import 'package:gym_moves/Announcement/SetNotificationType.dart';
import 'package:gym_moves/User/ChangePassword.dart';
import 'package:gym_moves/User/HelpManual.dart';

/*
Class Name:
  ViewMyProfile

Purpose:
  This class builds the UI for the profile page. It also ensures all buttons
  are functional and that the UI is responsive to the screen size. It also
  makes sure the persons name displays on the screen.
 */
class ViewMyProfile extends StatelessWidget {
  ViewMyProfile({Key key}) : super(key: key);

  /*
   Method Name: Build

   Purpose: This method builds the UI for the screen. It calls the necessary
            function in order to display the name of the user.
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
              height: 0.4 * media.size.height,
              decoration: BoxDecoration(
                  image: DecorationImage(
                    image:
                        const AssetImage('assets/images/RightSidePoolHalf.png'),
                    fit: BoxFit.fill,
                    colorFilter: new ColorFilter.mode(
                        Colors.black.withOpacity(1.0), BlendMode.dstIn),
                  ),
                  boxShadow: [
                    BoxShadow(
                      color: const Color(0x46000000),
                      offset: Offset(0, 3),
                      blurRadius: 6,
                    )
                  ])),
          Transform.translate(
              offset: Offset(0.05 * media.size.width, 0.07 * media.size.width),
              child: GestureDetector(
                  onTap: () {
                    Navigator.pop(context);
                  },
                  child: SvgPicture.string(backButton,
                      height: 0.05 * media.size.height,
                      width: 0.07 * media.size.width,
                      allowDrawingOutsideViewBox: true))),
          Container(
              width: media.size.width,
              height: 0.4 * media.size.height,
              child: Center(
                  child: Container(
                      width: 0.48 * media.size.width,
                      height: 0.4 * 0.65 * media.size.height,
                      decoration: BoxDecoration(
                        borderRadius:
                            BorderRadius.all(Radius.elliptical(85.5, 81.0)),
                        color: const Color(0xffffffff),
                        border: Border.all(
                            width: 1.0, color: const Color(0xff707070)),
                      ),
                      child: Center(
                          child: AutoSizeText(
                        'Name',
                        style: TextStyle(
                          fontFamily: 'FreestyleScript',
                          fontSize: media.size.width * 0.12,
                          color: const Color(0xff391f57),
                          shadows: [
                            Shadow(
                              color: const Color(0xbd000000),
                              offset: Offset(0, 1),
                              blurRadius: 0,
                            ),
                          ],
                        ),
                        maxLines: 1,
                        textAlign: TextAlign.center,
                      )))))
        ]),
        SizedBox(height: 0.05 * media.size.height),
        Stack(children: <Widget>[
          GestureDetector(
              onTap: () {
                Navigator.push(context,
                    MaterialPageRoute(builder: (context) => ChangePassword()));
              },
              child: Container(
                  width: 0.8 * media.size.width,
                  height: 0.08 * media.size.height,
                  padding: EdgeInsets.all(0.025 * media.size.width),
                  decoration: BoxDecoration(
                      borderRadius: BorderRadius.circular(29.0),
                      color: const Color(0x3dffffff),
                      border: Border.all(
                          width: 1.0, color: const Color(0x3d707070)),
                      boxShadow: [
                        BoxShadow(
                          color: const Color(0x23000000),
                          offset: Offset(0, 3),
                          blurRadius: 6,
                        )
                      ]))),
          Transform.translate(
              offset: Offset(0.8 * 0.8 * media.size.width,
                  0.08 * 0.25 * media.size.height),
              child:
                  // Adobe XD layer: 'Icon open-account-l…' (shape)
                  SvgPicture.string(
                pen,
                allowDrawingOutsideViewBox: true,
                height: 0.08 * 0.5 * media.size.height,
              )),
          Transform.translate(
            offset: Offset(
                0.8 * 0.1 * media.size.width, 0.08 * 0.25 * media.size.height),
            child: Text(
              'Change Password',
              style: TextStyle(
                fontFamily: 'Roboto',
                fontSize: 0.05 * media.size.width,
                color: const Color(0xffffffff),
              ),
              textAlign: TextAlign.left,
            ),
          )
        ]),
        SizedBox(height: 0.05 * media.size.height),
        Stack(children: <Widget>[
          GestureDetector(
              onTap: () {
                Navigator.push(context,
                    MaterialPageRoute(builder: (context) => HelpManual()));
              },
              child: Container(
                  width: 0.8 * media.size.width,
                  height: 0.08 * media.size.height,
                  padding: EdgeInsets.all(0.025 * media.size.width),
                  decoration: BoxDecoration(
                      borderRadius: BorderRadius.circular(29.0),
                      color: const Color(0x3dffffff),
                      border: Border.all(
                          width: 1.0, color: const Color(0x3d707070)),
                      boxShadow: [
                        BoxShadow(
                          color: const Color(0x23000000),
                          offset: Offset(0, 3),
                          blurRadius: 6,
                        )
                      ]))),
          Transform.translate(
              offset: Offset(0.8 * 0.8 * media.size.width,
                  0.08 * 0.25 * media.size.height),
              child:
                  // Adobe XD layer: 'Icon open-account-l…' (shape)
                  SvgPicture.string(
                helpIcon,
                allowDrawingOutsideViewBox: true,
                height: 0.08 * 0.5 * media.size.height,
              )),
          Transform.translate(
            offset: Offset(
                0.8 * 0.1 * media.size.width, 0.08 * 0.25 * media.size.height),
            child: Text(
              'Help Manual',
              style: TextStyle(
                fontFamily: 'Roboto',
                fontSize: 0.05 * media.size.width,
                color: const Color(0xffffffff),
              ),
              textAlign: TextAlign.left,
            ),
          ),
        ]),
        SizedBox(height: 0.05 * media.size.height),
        Stack(children: <Widget>[
          GestureDetector(
              onTap: () {
                Navigator.push(context,
                    MaterialPageRoute(builder: (context) => Notifications()));
              },
              child: Container(
                  width: 0.8 * media.size.width,
                  height: 0.08 * media.size.height,
                  padding: EdgeInsets.all(0.025 * media.size.width),
                  decoration: BoxDecoration(
                      borderRadius: BorderRadius.circular(29.0),
                      color: const Color(0x3dffffff),
                      border: Border.all(
                          width: 1.0, color: const Color(0x3d707070)),
                      boxShadow: [
                        BoxShadow(
                          color: const Color(0x23000000),
                          offset: Offset(0, 3),
                          blurRadius: 6,
                        )
                      ]))),
          Transform.translate(
              offset: Offset(0.8 * 0.8 * media.size.width,
                  0.08 * 0.25 * media.size.height),
              child:
                  // Adobe XD layer: 'Icon open-account-l…' (shape)
                  SvgPicture.string(
                bell,
                allowDrawingOutsideViewBox: true,
                height: 0.08 * 0.5 * media.size.height,
              )),
          Transform.translate(
            offset: Offset(
                0.8 * 0.1 * media.size.width, 0.08 * 0.25 * media.size.height),
            child: Text(
              'Notifications',
              style: TextStyle(
                fontFamily: 'Roboto',
                fontSize: 0.05 * media.size.width,
                color: const Color(0xffffffff),
              ),
              textAlign: TextAlign.left,
            ),
          )
        ]),
        SizedBox(height: 0.05 * media.size.height),
        Stack(children: <Widget>[
          GestureDetector(
              onTap: () {
                Navigator.pop(context);
              },
              child: Container(
                  width: 0.8 * media.size.width,
                  height: 0.08 * media.size.height,
                  padding: EdgeInsets.all(0.025 * media.size.width),
                  decoration: BoxDecoration(
                      borderRadius: BorderRadius.circular(29.0),
                      color: const Color(0x3dffffff),
                      border: Border.all(
                          width: 1.0, color: const Color(0x3d707070)),
                      boxShadow: [
                        BoxShadow(
                          color: const Color(0x23000000),
                          offset: Offset(0, 3),
                          blurRadius: 6,
                        )
                      ]))),
          Transform.translate(
              offset: Offset(0.8 * 0.8 * media.size.width,
                  0.08 * 0.25 * media.size.height),
              child:
                  // Adobe XD layer: 'Icon open-account-l…' (shape)
                  SvgPicture.string(
                logout,
                allowDrawingOutsideViewBox: true,
                height: 0.08 * 0.5 * media.size.height,
              )),
          Transform.translate(
            offset: Offset(
                0.8 * 0.1 * media.size.width, 0.08 * 0.25 * media.size.height),
            child: Text(
              'Logout',
              style: TextStyle(
                fontFamily: 'Roboto',
                fontSize: 0.05 * media.size.width,
                color: const Color(0xffffffff),
              ),
              textAlign: TextAlign.left,
            ),
          )
        ]),
      ]),
    );
  }
}

const String backButton =
    '<svg viewBox="28.2 38.0 31.4 27.9" ><path transform="matrix(-1.0, 0.0, 0.0, -1.0, 65.61, 71.93)" d="M 21.68118286132813 6 L 18.91737365722656 8.460894584655762 L 29.85499572753906 18.21720886230469 L 6 18.21720886230469 L 6 21.70783996582031 L 29.85499572753906 21.70783996582031 L 18.91737365722656 31.46415710449219 L 21.68118286132813 33.925048828125 L 37.36236572265625 19.9625244140625 L 21.68118286132813 6 Z" fill="#fcfbfc" stroke="none" stroke-width="1" stroke-miterlimit="4" stroke-linecap="butt" /></svg>';

const String logout =
    '<svg viewBox="77.0 578.0 31.0 28.0" ><defs><path transform="translate(77.0, 578.0)" d="M 11.625 0 L 11.625 4 L 27.125 4 L 27.125 24 L 11.625 24 L 11.625 28 L 31 28 L 31 0 L 11.625 0 Z M 7.75 8 L 0 14 L 7.75 20 L 7.75 16 L 23.25 16 L 23.25 12 L 7.75 12 L 7.75 8 Z" fill="#fcfbfc" stroke="none" stroke-width="1" stroke-miterlimit="4" stroke-linecap="butt" filter="url(#shadow)"/></svg>';

const String bell =
    '<svg viewBox="71.0 389.0 24.0 29.3" ><path transform="translate(65.0, 385.25)" d="M 18 33 C 19.64999961853027 33 21 31.64999961853027 21 30 L 15 30 C 15 31.64999961853027 16.33499908447266 33 18 33 Z M 27 24 L 27 16.5 C 27 11.89500045776367 24.54000091552734 8.039999961853027 20.25 7.020000457763672 L 20.25 6 C 20.25 4.755000114440918 19.2450008392334 3.75 18 3.75 C 16.7549991607666 3.75 15.75 4.755000114440918 15.75 6 L 15.75 7.019999980926514 C 11.44499969482422 8.039999961853027 9 11.88000011444092 9 16.5 L 9 24 L 6 27 L 6 28.5 L 30 28.5 L 30 27 L 27 24 Z" fill="#ffffff" stroke="none" stroke-width="1" stroke-miterlimit="4" stroke-linecap="butt" /></svg>';

const String pen =
    '<svg viewBox="73.0 315.0 27.0 27.0" ><path transform="translate(68.5, 310.5)" d="M 4.5 25.875 L 4.5 31.5 L 10.125 31.5 L 26.71500015258789 14.90999984741211 L 21.09000015258789 9.284999847412109 L 4.5 25.875 Z M 31.06500053405762 10.5600004196167 C 31.64999961853027 9.975000381469727 31.64999961853027 9.030000686645508 31.06500053405762 8.445000648498535 L 27.55500030517578 4.935000419616699 C 26.97000122070313 4.350000381469727 26.02499961853027 4.350000381469727 25.44000053405762 4.935000419616699 L 22.69499969482422 7.680000305175781 L 28.31999969482422 13.30500030517578 L 31.06499862670898 10.5600004196167 Z" fill="#ffffff" stroke="none" stroke-width="1" stroke-miterlimit="4" stroke-linecap="butt" /></svg>';

const String helpIcon =
    '<svg viewBox="3.0 3.0 30.0 30.0" ><path  d="M 33 18 C 33 26.28427124023438 26.28427124023438 33 18 33 C 9.715728759765625 33 3 26.28427124023438 3 18 C 3 9.715728759765625 9.715728759765625 3 18 3 C 26.28427124023438 3 33 9.715728759765625 33 18 Z" fill="none" stroke="#ffffff" stroke-width="3" stroke-linecap="round" stroke-linejoin="round" /><path  d="M 13.63500022888184 13.5 C 14.3631534576416 11.43006229400635 16.47807502746582 10.18710136413574 18.64076614379883 10.55806159973145 C 20.80345726013184 10.92901992797852 22.38327407836914 12.80572700500488 22.38000106811523 15 C 22.38000106811523 18 17.88000106811523 19.5 17.88000106811523 19.5" fill="none" stroke="#ffffff" stroke-width="3" stroke-linecap="round" stroke-linejoin="round" /><path  d="M 18 25.5 L 18 25.5" fill="none" stroke="#ffffff" stroke-width="3" stroke-linecap="round" stroke-linejoin="round" /></svg>';
