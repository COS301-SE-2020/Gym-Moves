/*
File Name
  ViewMyClassesMember.dart

Author:
  Danel

Date Created
  27/06/2020

Update History:
--------------------------------------------------------------------------------
| Name               | Date              | Changes                             |
--------------------------------------------------------------------------------

Functional Description:


Classes in the File:
- ViewMyClassesMember
- ViewMyClassesMemberState
 */

import 'package:flutter/material.dart';
import 'package:flutter_svg/flutter_svg.dart';

import 'package:gym_moves/GymClass/ClassDetails.dart';
import 'package:gym_moves/GymClass/ViewAllClassesMember.dart';

/*
Class Name:
  ViewMyClassesMember

Purpose:
  This class creates the class that will build the page.
 */


class ViewMyClassesMember extends StatefulWidget {
  const ViewMyClassesMember({Key key}) : super(key: key);

  @override
  ViewMyClassesMemberState createState() => ViewMyClassesMemberState();
}

/*
Class Name:
  ViewMyClassesMemberState

Purpose:

 */
class ViewMyClassesMemberState extends State<ViewMyClassesMember> {

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
              height: 0.3 * media.size.height,
              decoration: BoxDecoration(
                image: DecorationImage(
                  image:
                  const AssetImage('assets/RightSidePoolHalf.png'),
                  fit: BoxFit.fill,
                  colorFilter: new ColorFilter.mode(
                      Colors.black.withOpacity(1.0), BlendMode.dstIn),
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
                offset:
                Offset(0.04 * media.size.width, 0.04 * media.size.height),
                child: GestureDetector(
                  onTap: () {
                    Navigator.pop(context);
                  },
                  child: SvgPicture.string(
                    backArrow,
                    width: 0.06 * media.size.width,
                    allowDrawingOutsideViewBox: true,
                  ),
                )),
            Transform.translate(
              offset: Offset(0.0, 0.3 * media.size.height),
              child: SvgPicture.string(
                underline,
                width: media.size.width * 0.5,
                allowDrawingOutsideViewBox: true,
              ),
            ),
            Transform.translate(
                offset:
                Offset(0.5 * media.size.width, 0.23 * media.size.height),
                child: Container(
                    child: GestureDetector(
                      onTap: () {
                        Navigator.pop(context);
                        Navigator.push(
                          context,
                          MaterialPageRoute(
                              builder: (context) => ViewAllClassesMember()),
                        );
                      },
                      child: Container(
                        child: Text(
                          'View All',
                          style: TextStyle(
                            fontFamily: 'Roboto',
                            fontSize: 0.05 * media.size.width,
                            color: const Color(0xffffffff),
                            fontWeight: FontWeight.w300,
                          ),
                          textAlign: TextAlign.center,
                        ),
                        width: 0.5 * media.size.width,
                        height: 0.1 * media.size.height,
                        padding: EdgeInsets.all(10.0),
                      ),
                    ))),
            Transform.translate(
                offset: Offset(0.0, 0.23 * media.size.height),
                child: Container(
                  child: Text(
                    'View Mine',
                    style: TextStyle(
                      fontFamily: 'Roboto',
                      fontSize: 0.05 * media.size.width,
                      color: const Color(0xffffffff),
                      fontWeight: FontWeight.w300,
                    ),
                    textAlign: TextAlign.center,
                  ),
                  width: 0.5 * media.size.width,
                  height: 0.075 * media.size.height,
                  color: Colors.white.withOpacity(0.1),
                  padding: EdgeInsets.all(10.0),
                ))
          ]),
          Expanded(child: getClasses(media))
        ]));
  }


  /*
   Method Name:
    getClasses

   Purpose:

   */
  Widget getClasses(MediaQueryData media) {
    List<Widget> classes = new List();

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
              /* Navigator.push(
                context,
                MaterialPageRoute(
                    builder: (context) =>
                        ClassDetails(className: "Spin Busters")),
              );*/
            },
            child: Row(mainAxisAlignment:MainAxisAlignment.center,children: <Widget>[ Stack(children: <Widget>[
              GestureDetector(
                  onTap: () {
                    Navigator.push(
                        context,
                        MaterialPageRoute(
                          builder: (context) => ClassDetails(),
                        ));
                  },
                  child: Container(
                      width: 0.7 * media.size.width,
                      height: 0.2 * media.size.height,
                      decoration: BoxDecoration(
                        borderRadius: BorderRadius.circular(19.0),
                        color: const Color(0x26ffffff),
                        border: Border.all(
                            width: 1.0, color: const Color(0x26707070)),
                      ))),
              Transform.translate(
                  offset: Offset(0.33 * 0.8 * media.size.width,
                      0.65 * 0.25 * media.size.height),
                  child: Row(children: getStarsForClass(media))),
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
                  Offset(0.05 * media.size.width, 0.07 * media.size.height),
                  child: SizedBox(
                      width:0.7 * media.size.width,
                      child: Text("Class Day: ",
                          style: TextStyle(
                              color: Colors.white,
                              fontSize: 0.035 * media.size.width)))),
              Transform.translate(
                  offset:
                  Offset(0.05 * media.size.width, 0.12 * media.size.height),
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


/*
   Method Name: getStarsForClass

   Purpose: This method will get the rating for the specific class and show the
            correct stars.

   Extra: Rating is currently hardcoded. This will be changed.
   */

List<Widget> getStarsForClass(MediaQueryData media) {
  List<Widget> stars = [];

  int full = 3;
  int half = 1;
  int empty = 1;

  for (int i = full; i > 0; i--) {
    stars.add(SvgPicture.string(
      fullStar,
      height: 0.02 * media.size.height,
      width: 0.02 * media.size.width,
      allowDrawingOutsideViewBox: true,
    ));
  }

  for (int i = half; i > 0; i--) {
    stars.add(SvgPicture.string(
      halfStar,
      height: 0.02 * media.size.height,
      width: 0.02 * media.size.width,
      allowDrawingOutsideViewBox: true,
    ));
  }

  for (int i = empty; i > 0; i--) {
    stars.add(SvgPicture.string(
      emptyStar,
      height: 0.02 * media.size.height,
      width: 0.02 * media.size.width,
      allowDrawingOutsideViewBox: true,
    ));
  }

  return stars;
}

const String underline =
    '<svg viewBox="203.5 276.5 162.0 1.0" ><path transform="translate(203.5, 276.5)" d="M 0 0 L 162 0" fill="none" stroke="#ffffff" stroke-width="3" stroke-miterlimit="4" stroke-linecap="butt" /></svg>';
const String backArrow =
    '<svg viewBox="28.2 38.0 31.4 27.9" ><path transform="matrix(-1.0, 0.0, 0.0, -1.0, 65.61, 71.93)" d="M 21.68118286132813 6 L 18.91737365722656 8.460894584655762 L 29.85499572753906 18.21720886230469 L 6 18.21720886230469 L 6 21.70783996582031 L 29.85499572753906 21.70783996582031 L 18.91737365722656 31.46415710449219 L 21.68118286132813 33.925048828125 L 37.36236572265625 19.9625244140625 L 21.68118286132813 6 Z" fill="#fcfbfc" stroke="none" stroke-width="1" stroke-miterlimit="4" stroke-linecap="butt" /></svg>';
const String emptyStar =
    '<svg viewBox="283.0 336.1 13.8 11.4" ><path transform="translate(280.7, 332.75)" d="M 15.56942558288574 7.33497953414917 L 11.02351665496826 7.33497953414917 L 9.642127990722656 3.666498184204102 C 9.574139595031738 3.487749099731445 9.385628700256348 3.375 9.172393798828125 3.375 C 8.959158897399902 3.375 8.770648002624512 3.487749099731445 8.702659606933594 3.666498184204102 L 7.321271896362305 7.33497953414917 L 2.744456768035889 7.33497953414917 L 2.744456768035889 7.33497953414917 C 2.472505569458008 7.33497953414917 2.25 7.532978534698486 2.25 7.774977207183838 C 2.25 7.799726963043213 2.253090381622314 7.827226161956787 2.259271144866943 7.849226474761963 C 2.265451908111572 7.945476055145264 2.314897298812866 8.052726745605469 2.466324806213379 8.159975051879883 L 6.202564239501953 10.50296401977539 L 4.76863956451416 14.21269416809082 C 4.69756031036377 14.39144325256348 4.76863956451416 14.59219169616699 4.938608169555664 14.70769119262695 C 5.028228759765625 14.76543998718262 5.111668586730957 14.81494140625 5.216740608215332 14.81494140625 C 5.318722724914551 14.81494140625 5.43924617767334 14.76819038391113 5.525775909423828 14.71594047546387 L 9.172393798828125 12.4032039642334 L 12.81901168823242 14.71594047546387 C 12.90554237365723 14.77094078063965 13.02606678009033 14.81494140625 13.12804698944092 14.81494140625 C 13.23311996459961 14.81494140625 13.31655883789063 14.76819038391113 13.40308952331543 14.70769119262695 C 13.57614898681641 14.59219169616699 13.64413642883301 14.39419174194336 13.57305908203125 14.21269416809082 L 12.13913440704346 10.50296211242676 L 15.8444709777832 8.137975692749023 L 15.93408966064453 8.069225311279297 C 16.01443862915039 7.992225170135498 16.09478759765625 7.887726306915283 16.09478759765625 7.774976253509521 C 16.09478759765625 7.532978534698486 15.84137916564941 7.33497953414917 15.56942558288574 7.33497953414917 Z M 11.63849544525146 9.878715515136719 C 11.3294620513916 10.07671546936035 11.19966602325439 10.43421363830566 11.32328033447266 10.75321197509766 L 12.2534761428833 13.16494941711426 C 12.29365062713623 13.26669883728027 12.16385555267334 13.35194778442383 12.06496524810791 13.29144859313965 L 9.673030853271484 11.7734546661377 C 9.521602630615234 11.67720603942871 9.345453262329102 11.63045692443848 9.169302940368652 11.63045692443848 C 8.993152618408203 11.63045692443848 8.81700325012207 11.67720603942871 8.668665885925293 11.7734546661377 L 6.276731491088867 13.28870010375977 C 6.177840232849121 13.35194778442383 6.048046112060547 13.26394844055176 6.08821964263916 13.16219902038574 L 7.018416404724121 10.75045967102051 C 7.142030715942383 10.42596435546875 7.012235641479492 10.0684642791748 6.70011043548584 9.870464324951172 L 4.19692325592041 8.300224304199219 C 4.098031044006348 8.239723205566406 4.147477149963379 8.09947395324707 4.268001556396484 8.09947395324707 L 7.318181037902832 8.09947395324707 C 7.689023017883301 8.09947395324707 8.019691467285156 7.887726306915283 8.137125015258789 7.574227809906006 L 9.051870346069336 5.148740291595459 C 9.088953971862793 5.049739837646484 9.249652862548828 5.049739837646484 9.286736488342285 5.148740291595459 L 10.20148181915283 7.574227809906006 C 10.31891536712646 7.887726306915283 10.649582862854 8.09947395324707 11.02042579650879 8.09947395324707 L 11.02042579650879 8.09947395324707 L 14.02733898162842 8.09947395324707 C 14.14786338806152 8.09947395324707 14.19730854034424 8.236974716186523 14.09841728210449 8.297473907470703 L 11.63849544525146 9.878715515136719 Z" fill="#ffff50" stroke="none" stroke-width="1" stroke-miterlimit="4" stroke-linecap="butt" /></svg>';
const String fullStar =
    '<svg viewBox="323.2 330.6 13.8 11.4" ><path transform="translate(320.18, 327.59)" d="M 9.920000076293945 12.19414806365967 L 14.19656085968018 14.44000053405762 L 13.06168079376221 10.207200050354 L 16.84000015258789 7.35924243927002 L 11.86452007293701 6.991957664489746 L 9.920000076293945 3 L 7.975480079650879 6.991957664489746 L 3 7.35924243927002 L 6.778319835662842 10.207200050354 L 5.643439769744873 14.44000053405762 L 9.920000076293945 12.19414806365967 Z" fill="#ffff50" stroke="none" stroke-width="1" stroke-miterlimit="4" stroke-linecap="butt" /></svg>';
const String halfStar =
    '<svg viewBox="279.2 324.6 13.8 11.4" ><path transform="translate(276.18, 321.59)" d="M 16.84000015258789 7.35924243927002 L 11.86452007293701 6.985937118530273 L 9.920000076293945 3 L 7.975480079650879 6.991957664489746 L 3 7.35924243927002 L 6.778319835662842 10.207200050354 L 5.643439769744873 14.44000053405762 L 9.920000076293945 12.19414806365967 L 14.19656085968018 14.44000053405762 L 13.06860065460205 10.207200050354 L 16.84000015258789 7.35924243927002 Z M 9.920000076293945 11.06821155548096 L 9.920000076293945 5.468631744384766 L 11.10332012176514 7.90113639831543 L 14.13428020477295 8.129936218261719 L 11.83684062957764 9.863999366760254 L 12.52884006500244 12.44101047515869 L 9.920000076293945 11.06821155548096 Z" fill="#ffff50" stroke="none" stroke-width="1" stroke-miterlimit="4" stroke-linecap="butt" /></svg>';
