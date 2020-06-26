import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';
import 'package:flutter_svg/flutter_svg.dart';
import 'ClassDetails.dart';

class EditClassesManager extends StatefulWidget {
  const EditClassesManager({Key key}) : super(key: key);

  @override
  EditClassesManagerState createState() => EditClassesManagerState();
}

class EditClassesManagerState extends State<EditClassesManager> {
  @override
  Widget build(BuildContext context) {
    /* gets screen properties */
    MediaQueryData media = MediaQuery.of(context);

    return Scaffold(
      backgroundColor: const Color(0xff513369),
      body: Column(children: <Widget>[
        Stack(children: <Widget>[
          Container(
            width: media.size.width,
            height: 1 / 3 * media.size.height,
            decoration: BoxDecoration(
              image: DecorationImage(
                image: const AssetImage('assets/images/RightSidePoolHalf.png'),
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
              offset: Offset(0.05 * media.size.width, 0.07 * media.size.width),
              child: GestureDetector(
                onTap: () {
                  Navigator.pop(context);
                },
                child: SvgPicture.string(
                  backButton,
                  allowDrawingOutsideViewBox: true,
                ),
              )),
          Transform.translate(
            offset: Offset(0.0, 0.13 * media.size.height),
            child: SizedBox(
              width: media.size.width,
              height: 0.1 * media.size.height,
              child: Text(
                'Classes',
                style: TextStyle(
                  fontFamily: 'FreestyleScript',
                  fontSize: 0.16 * media.size.width,
                  color: const Color(0xFFFFFFFF),
                  shadows: [
                    Shadow(
                      color: const Color(0xbd000000),
                      offset: Offset(0, 3),
                      blurRadius: 6,
                    ),
                  ],
                ),
                textAlign: TextAlign.center,
              ),
            ),
          )
        ]),
        Expanded(child: getClasses(media))
      ]),
    );
  }

  /* Can only implement once API working */
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
                    builder: (context) =>
                        ClassDetails(),
              ));
            },
            child: Stack(children: <Widget>[
              Container(
                  width: 0.95 * media.size.width,
                  height: 0.4 * media.size.height,
                  decoration: BoxDecoration(
                    borderRadius: BorderRadius.circular(22.0),
                    color: const Color(0x26ffffff),
                    border:
                    Border.all(width: 1.0, color: const Color(0x26707070)),
                    boxShadow: [
                      BoxShadow(
                        color: const Color(0x12000000),
                        offset: Offset(0, 3),
                        blurRadius: 6,
                      ),
                    ],
                  ))
            ])));

        classes.add(SizedBox(height: 20));
      }
    }

    return ListView(padding: const EdgeInsets.all(15), children: classes);
  }
}

const String backButton =
    '<svg viewBox="34.2 38.0 31.4 27.9" ><path transform="matrix(-1.0, 0.0, 0.0, -1.0, 71.61, 71.93)" d="M 21.68118286132813 6 L 18.91737365722656 8.460894584655762 L 29.85499572753906 18.21720886230469 L 6 18.21720886230469 L 6 21.70783996582031 L 29.85499572753906 21.70783996582031 L 18.91737365722656 31.46415710449219 L 21.68118286132813 33.925048828125 L 37.36236572265625 19.9625244140625 L 21.68118286132813 6 Z" fill="#fcfbfc" stroke="none" stroke-width="1" stroke-miterlimit="4" stroke-linecap="butt" /></svg>';
