import 'package:flutter/material.dart';
import 'dart:ui' as ui;
import 'package:flutter_svg/flutter_svg.dart';

class ClassInfo extends StatelessWidget {
  ClassInfo({
    Key key,
  }) : super(key: key);
  @override
  Widget build(BuildContext context) {
    return Scaffold(
      backgroundColor: const Color(0xffffffff),
      body: Stack(
        children: <Widget>[
          Transform.translate(
            offset: Offset(-70.0, 153.0),
            child: Stack(
              overflow: Overflow.visible,
              children: <Widget>[
                Container(
                  width: 167.0,
                  height: 54.0,
                  decoration: BoxDecoration(
                    color: const Color(0x75ffffff),
                    border:
                    Border.all(width: 1.0, color: const Color(0x75707070)),
                  ),
                ),
                Positioned(
                  left: -12.0,
                  top: -12.0,
                  width: 191.0,
                  height: 78.0,
                  child: ClipRect(
                    child: BackdropFilter(
                      filter: ui.ImageFilter.blur(sigmaX: 12.0, sigmaY: 12.0),
                      child: Container(color: const Color(0x00000000)),
                    ),
                  ),
                ),
              ],
            ),
          ),
          // Adobe XD layer: 'MenuBackgroundHalf' (shape)
          Container(
            width: 500.0,
            height: 277.5,
            decoration: BoxDecoration(
              image: DecorationImage(
                image: const AssetImage('assets/RightSidePoolHalf.png'),
                fit: BoxFit.fill,
                colorFilter: new ColorFilter.mode(
                    Colors.black.withOpacity(0.98), BlendMode.dstIn),
              ),
              boxShadow: [
                BoxShadow(
                  color: const Color(0x57000000),
                  offset: Offset(0, 3),
                  blurRadius: 6,
                ),
              ],
            ),
          ),
          Transform.translate(
            offset: Offset(0.0, 278.0),
            child: Container(
              width: 600.0,
              height: 500.0,
              decoration: BoxDecoration(
                color: const Color(0xff513369),
                border: Border.all(width: 1.0, color: const Color(0xff707070)),
              ),
            ),
          ),
          Transform.translate(
            offset: Offset(80.0, 121.0),
            child: Text(
              'Class Information',
              style: TextStyle(
                fontFamily: 'FreestyleScript',
                fontSize: 55,
                color: const Color(0xffffffff),
              ),
              textAlign: TextAlign.left,
            ),
          ),
          Transform.translate(
            offset: Offset(70.0, 355.0),
            child: Container(
              width: 278.0,
              height: 211.0,
              decoration: BoxDecoration(
                borderRadius: BorderRadius.circular(24.0),
                color: const Color(0xff917ea0),
                border: Border.all(width: 1.0, color: const Color(0xff707070)),
              ),
            ),
          ),
          Transform.translate(
            offset: Offset(80.0, 373.0),
            child: SizedBox(
              width: 251.0,
              height: 52.0,
              child: Text(
                'Class instructor: Millanie Wessels',
                style: TextStyle(
                  fontFamily: 'Roboto',
                  fontSize: 20,
                  color: const Color(0xffffffff),
                ),
                textAlign: TextAlign.left,
              ),
            ),
          ),
          Transform.translate(
            offset: Offset(80.0, 450.0),
            child: SizedBox(
              width: 251.0,
              height: 38.0,
              child: Text(
                'Class Name: Spinning',
                style: TextStyle(
                  fontFamily: 'Roboto',
                  fontSize: 20,
                  color: const Color(0xffffffff),
                ),
                textAlign: TextAlign.left,
              ),
            ),
          ),
          Transform.translate(
            offset: Offset(80.0, 501.0),
            child: SizedBox(
              width: 251.0,
              height: 52.0,
              child: Text(
                'Class Time: Wednesday, 4PM',
                style: TextStyle(
                  fontFamily: 'Roboto',
                  fontSize: 20,
                  color: const Color(0xffffffff),
                ),
                textAlign: TextAlign.left,
              ),
            ),
          ),
          Transform.translate(
            offset: Offset(180.0, 209.0),
            child:
            // Adobe XD layer: 'Icon awesome-info-c…' (shape)
            SvgPicture.string(
              _svg_a9nlho,
              allowDrawingOutsideViewBox: true,
            ),
          ),
        ],
      ),
    );
  }
}

const String _svg_a9nlho =
    '<svg viewBox="168.0 209.0 34.9 34.9" ><path transform="translate(167.44, 208.44)" d="M 18 0.5625 C 8.370210647583008 0.5625 0.5625 8.37302303314209 0.5625 18 C 0.5625 27.63260269165039 8.370210647583008 35.4375 18 35.4375 C 27.62978935241699 35.4375 35.4375 27.63260269165039 35.4375 18 C 35.4375 8.37302303314209 27.62978935241699 0.5625 18 0.5625 Z M 18 8.296875 C 19.63096809387207 8.296875 20.953125 9.619030952453613 20.953125 11.25 C 20.953125 12.88096904754639 19.63096809387207 14.203125 18 14.203125 C 16.36903190612793 14.203125 15.046875 12.88096904754639 15.046875 11.25 C 15.046875 9.619030952453613 16.36903190612793 8.296875 18 8.296875 Z M 21.9375 26.15625 C 21.9375 26.62221145629883 21.55971145629883 27 21.09375 27 L 14.90625 27 C 14.44028949737549 27 14.0625 26.62221145629883 14.0625 26.15625 L 14.0625 24.46875 C 14.0625 24.00278854370117 14.44028949737549 23.625 14.90625 23.625 L 15.75 23.625 L 15.75 19.125 L 14.90625 19.125 C 14.44028949737549 19.125 14.0625 18.74721145629883 14.0625 18.28125 L 14.0625 16.59375 C 14.0625 16.12778854370117 14.44028949737549 15.75 14.90625 15.75 L 19.40625 15.75 C 19.87221145629883 15.75 20.25 16.12778854370117 20.25 16.59375 L 20.25 23.625 L 21.09375 23.625 C 21.55971145629883 23.625 21.9375 24.00278854370117 21.9375 24.46875 L 21.9375 26.15625 Z" fill="#f5f5f5" stroke="none" stroke-width="1" stroke-miterlimit="4" stroke-linecap="butt" /></svg>';
