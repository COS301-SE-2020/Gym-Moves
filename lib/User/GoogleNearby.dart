/*
File Name
  NfcScreen.dart
Author:
  Raeesa
Date Created
  07/08/2020
Update History:
--------------------------------------------------------------------------------
 Name               | Date              | Changes
--------------------------------------------------------------------------------
Functional Description:
  This file contains the Forget password functionality, which enables a user to
  change their respective passwords.
  The NfcScreenState class handles the building of the UI and making all the
  components functional and responsive.
  This file will also handle sending the information that is entered to change the
  password in the database.
Classes in the File:
- NfcScreen
- NfcScreenState
 */

import 'dart:convert';

import 'package:flutter/material.dart';
import 'package:flutter_svg/flutter_svg.dart';
import 'package:firebase_database/firebase_database.dart';
import 'package:nearby_connections/nearby_connections.dart';
import 'package:shared_preferences/shared_preferences.dart';
import 'package:http/http.dart' as http;

class NfcScreen extends StatefulWidget {
  NfcScreen({
    Key key,
  }) : super(key: key);

  @override
  NfcScreenState createState() => NfcScreenState();
}


FirebaseDatabase database = new FirebaseDatabase();
DatabaseReference _userRef=database.reference().child('users');

bool allowedtoexit = false;

class NfcScreenState extends State<NfcScreen> {

  Future<void> _permissions() async {
    if (!await Nearby().checkLocationPermission()) {
    Nearby().askLocationPermission();
    }

    if (!await Nearby().checkLocationEnabled()) {
    Nearby().enableLocationServices();
    }
  }

  @override
  void initState() {
    super.initState();

    _permissions();
  }

  @override
  Widget build(BuildContext context) {
    MediaQueryData media = MediaQuery.of(context);


    return Scaffold(
        backgroundColor: const Color(0xffffffff),
        body: ListView(children: <Widget>[
          Stack(children: <Widget>[
            Transform.translate(
                offset: Offset(0.2*media.size.width, -0.035 * media.size.height),
                child: Container(
                    width:0.6* media.size.width,
                    height: 0.4* media.size.height,
                    decoration: BoxDecoration(
                      image: DecorationImage(
                        image: const AssetImage('assets/nfc.png'),
                        fit: BoxFit.fill,

                      ),
                    )
                )
            ),

            Transform.translate(
                offset:
                Offset(0.04 * media.size.width, 0.03 * media.size.height),
                child: GestureDetector(
                    onTap: () {
                      Navigator.pop(context);
                    },
                    child: SvgPicture.string(backArrow,
                        allowDrawingOutsideViewBox: true,
                        width: 0.06 * media.size.width)
                )
            )
          ]),
          Container(
            width: media.size.width*0.95,
            child: Text(
              'Are you entering or exiting the gym? ',
              style: TextStyle(
                fontFamily: 'Lastwaerk',
                fontSize: 0.045 * media.size.width,
                color: const Color(0xff000000),
                fontWeight: FontWeight.w300,
              ),
              textAlign: TextAlign.center,
            ),
          ),
          Form(
            child: Column(
              children: <Widget>[
//                SizedBox(height: 0.1 * media.size.height),

                SizedBox(height: 0.03 * media.size.height),
                Center(
                    child: SizedBox(
                        width: 0.38 * media.size.width,
                        child: FlatButton(
                          shape: RoundedRectangleBorder(
                              borderRadius: BorderRadius.circular(10.0)),
                          color:const Color(0xff7341E6).withOpacity(0.8),
                          onPressed: () async {
                            await _connect(true);
                          },
                          textColor: Colors.white,
                          padding: const EdgeInsets.all(0.0),
                          child: Container(
                            padding: const EdgeInsets.all(10.0),
                            child: Text(
                              'Entering gym',
                              style: TextStyle(
                                  fontSize: 0.04 * media.size.width,
                                  fontFamily: 'Roboto'),
                            ),
                          ),
                        ))),
                SizedBox(height: 0.08 * media.size.height),

//                SizedBox(height: 0.025 * media.size.height),
//
//                SizedBox(height: 0.03 * media.size.height),
                Center(
                    child: SizedBox(
                        width: 0.38 * media.size.width,
                        child: FlatButton(
                          shape: RoundedRectangleBorder(
                              borderRadius: BorderRadius.circular(10.0)
                          ),
                          color: const Color(0xff7341E6).withOpacity(0.8),
                          onPressed: () async {
                            await _connect(false);
                          },
                          textColor: Colors.white,
                          padding: const EdgeInsets.all(0.0),
                          child: Container(
                            padding: const EdgeInsets.all(10.0),
                            child: Text(
                              'Exiting gym',
                              style: TextStyle(
                                  fontSize: 0.04 * media.size.width,
                                  fontFamily: 'Roboto'
                              ),
                            ),
                          ),
                        )
                    )
                ),

              ],
            ),
          )
        ])
    );
  }
  void check() async {
    SharedPreferences prefs = await SharedPreferences.getInstance();
    bool allowed = prefs.get("entered");
    int gymid = prefs.get('gymId');
    String gym = gymid.toString();
    String gymID = "gym" + gym;

    print(gymID);

    if(allowed!=null){
      allowedtoexit = allowed;
    }

    else{
      allowedtoexit = false;
    }
  }
  void _connect(bool entering) async {
    String username = (await SharedPreferences.getInstance()).get('username');
    Strategy strategy = Strategy.P2P_STAR;

    bool a = await Nearby().startDiscovery(
        username,
        strategy,
        onEndpointFound: (id, name, serviceId) {
          //if (serviceId == _serviceId) {
          print('[Google Nearby] Service Id: $serviceId');
          Nearby().requestConnection(
              username,
              id,
              onConnectionInitiated: (id, info) {
                Nearby().acceptConnection(id,
                    onPayLoadRecieved: (endId, payload) async {
                      if (payload.type == PayloadType.BYTES) {
                        String received = String.fromCharCodes(payload.bytes);

                        if (received.contains("GymId")) {
                          int id = int.parse(received.split("=")[1]);
                          print('[Google Nearby] Gym Id: $id');

                          if (entering) {
                            _entering(id);
                          } else {
                            _exiting(id);
                          }
                        }
                      }
                      await Nearby().disconnectFromEndpoint(id);
                      await Nearby().stopDiscovery();
                    }
                );
              },
              onConnectionResult: (id, status) async {
                await Nearby().stopDiscovery();
              },
              onDisconnected: (id) async {
                await Nearby().stopDiscovery();
              }
          );
          //}
        },
        onEndpointLost: (id) async {
          await Nearby().stopDiscovery();
        }
    );
  }

  void _entering(int gymid) async{
    SharedPreferences prefs = await SharedPreferences.getInstance();
    //int gymid = prefs.get('gymId');
    String gym = gymid.toString();
    String gymID = "gym" + gym;

    check();

    if(allowedtoexit==false) {


      int day = (new DateTime.now().day);
      int month = (new DateTime.now().month);
      int year = (new DateTime.now().year);
      String startHour = (new DateTime.now().hour.toString());
      String time = startHour + ":00";

      String url = 'https://gymmoveswebapi.azurewebsites.net/api/gymattendance/change';

      final http.Response response = await http.post(
        url,
        headers: <String, String>{'Content-type': 'application/json'},
        body: jsonEncode({
          "gymid" : gymid,
          "time": time,
          "day": day,
          "month": month,
          "year": year,

        }),
      );


      if (response.statusCode == 200) {
        int result;
        final prefs = await SharedPreferences.getInstance();
        await _userRef.child("uizCT8uR8oWSKgOIiVYy/count/" + gymID)
            .once()
            .then((snapshot) {
          if(snapshot.value!=null)
            result = snapshot.value;
          else result =0;
        });
        int finalresult = result + 1;
        await _userRef.child("uizCT8uR8oWSKgOIiVYy").update({
          "count/" + gymID: finalresult,
        }).then((_) {
          prefs.setBool('entered', true);
          allowedtoexit = true;
          print('Transaction  committed.');
        });
      }
      else {
        _showAlertDialog(response.body, "Error");
      }
    }
    else{
      _showAlertDialog("You've already entered the gym.", "Error");
    }
  }

  void _exiting(int gymid) async {
    check();
    SharedPreferences prefs = await SharedPreferences.getInstance();
    //int gymid = prefs.get('gymId');
    String gym = gymid.toString();
    String gymID = "gym" + gym;
    if (allowedtoexit == true) {
      int result;
      final prefs = await SharedPreferences.getInstance();
      await _userRef.child("uizCT8uR8oWSKgOIiVYy/count/" +gymID)
          .once()
          .then((snapshot) {
        if(snapshot.value!=null)
          result = snapshot.value;
        else result =0;
      });
      int finalresult = result - 1;
      await _userRef.child("uizCT8uR8oWSKgOIiVYy").update({
        "count/"+gymID: finalresult,
      }).then((_) {
        allowedtoexit = false;
        prefs.setBool('entered', false);
        print('Transaction  committed.');
      });
    }
    else{
      _showAlertDialog("You haven't entered the gym", "Can't exit");
    }
  }

  void _showAlertDialog(String message, String message2) {
    // set up the button
    Widget okButton = FlatButton(
      child: Text("Ok", style: TextStyle(color: Color(0xff513369))),
      onPressed: () {
        Navigator.pop(context);
      },
    );

    AlertDialog alert = AlertDialog(
      title: Text(message2),
      content: Text(message),
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


const String backArrow =
    '<svg viewBox="28.2 38.0 31.4 27.9" ><path transform="matrix(-1.0, 0.0, 0.0, -1.0, 65.61, 71.93)" d="M 21.68118286132813 6 L 18.91737365722656 8.460894584655762 L 29.85499572753906 18.21720886230469 L 6 18.21720886230469 L 6 21.70783996582031 L 29.85499572753906 21.70783996582031 L 18.91737365722656 31.46415710449219 L 21.68118286132813 33.925048828125 L 37.36236572265625 19.9625244140625 L 21.68118286132813 6 Z" fill="#fcfbfc" stroke="none" stroke-width="1" stroke-miterlimit="4" stroke-linecap="butt" /></svg>';
