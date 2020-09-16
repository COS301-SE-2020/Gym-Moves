import 'package:flutter/material.dart';
import 'package:gym_moves/User/LogIn.dart';
import 'package:gym_moves/User/Welcome.dart';

import 'User/NfcScreen.dart';
import 'package:flutter/material.dart';
import 'package:gym_moves/User/LogIn.dart';
import 'package:gym_moves/Announcement/SendAnnouncement.dart';
import 'dart:async';
import 'package:firebase_messaging/firebase_messaging.dart';
import 'package:http/http.dart' as http;
import 'package:flutter/material.dart';
import 'dart:convert';
import 'package:shared_preferences/shared_preferences.dart';
import 'package:intl/intl.dart';
import 'package:schedule_controller/schedule_controller.dart';
import 'dart:io';

import 'package:flutter/foundation.dart';
import 'package:flutter/material.dart';

import 'package:localstorage/localstorage.dart';

void main() {
  if (!kIsWeb && Platform.isMacOS) {
    debugDefaultTargetPlatformOverride = TargetPlatform.fuchsia;
  }
  runApp(MyAppExample());
}

class Announcement {
  String body;
  String title;
  String date;
  Announcement({this.body, this.title, this.date});
  toJSONEncodable() {
    Map<String, dynamic> m = new Map();

    m['body'] = body;
    m['title'] = title;
    m['date'] = date;

    return m;
  }
}

class Announcements {
  List<Announcement> items;

  Announcements() {
    items = new List();
  }

  toJSONEncodable() {
    return items.map((item) {
      return item.toJSONEncodable();
    }).toList();
  }
}
class Details {
  final String body;
  final String title;
  final String day;
  final String month;
  final String year;
  final int gymid;
  Details({this.body, this.title, this.day, this.month, this.year, this.gymid});

  factory Details.fromJson(Map<String, dynamic> json) {
    return Details(
        body: json['body'],
        title: json['title'],
        day: json['day'],
        month: json['month'],
        year: json['year'],
        gymid: json['gymid']
    );
  }
}

/*
Class Name:
  Notifications

Purpose:
  This class helps with the return of the notifications and their results
  from the database.
 */
class Notifications {
  final bool push;
  final bool email;

  Notifications({this.push, this.email});

  factory Notifications.fromJson(Map<String, dynamic> json) {
    return Notifications(
        push: json['push'],
        email: json['email']
    );
  }
}



Future<dynamic> myBackgroundMessageHandler(Map<String, dynamic> message) {
  if (message.containsKey('data')) {
    // Handle data message
    final dynamic data = message['data'];
  }

  if (message.containsKey('notification')) {
    // Handle notification message
    final dynamic notification = message['notification'];
  }
  // Or do other work.
}

class MyAppExample extends StatefulWidget {
  @override
  MyApp createState() => MyApp();
}

class MyApp extends State<MyAppExample> {
  final FirebaseMessaging _firebaseMessaging = FirebaseMessaging();
  String mytitle = "";
  String mybody = "";
  String myday = "";
  String mymonth= "";
  String myyear = "";
  int gymid = 0;
  String url = "https://gymmoveswebapi.azurewebsites.net/api/notifications/";
  String username;
  int thisgymid;
  bool pushNotifications = false;


  final Announcements list = new Announcements();
  final LocalStorage storage = new LocalStorage('my_announcements');
  var items;
  bool initialized = false;

  ScheduleController controller;

  Future get(String key) async {
    SharedPreferences prefs = await SharedPreferences.getInstance();
    return prefs.get(key);
  }

  Future save(String key, String value) async {
    SharedPreferences prefs = await SharedPreferences.getInstance();
    prefs.setString(key, value);
  }

  /*
Class Name:
  _alertDialog

Purpose:
  This method shows the message as a notification on the user's screen.
 */
  _alertDialog(text1, text2 ) async {
    return showDialog<void>(
      //context: context,
      barrierDismissible: false,
      builder: (BuildContext context) {
        return AlertDialog(
          title: Text(text1),
          content: Text(text2),
          actions: <Widget>[
            FlatButton(
              child: Text(
                'Ok',
                style: TextStyle(color: Color(0xff513369)),
              ),
              onPressed: () {
                Navigator.of(context).pop();
              },
            ),
          ],
        );
      },
    );
  }
  saveToStorage() {
    storage.setItem('my_push', list.toJSONEncodable());
  }
  addAnnouncement(String body, String title, String date) {
    setState(() {
      final item = new Announcement(body: body, title: title, date: date);
      list.items.add(item);
      saveToStorage();
    });
  }

  delAnnouncement(int x) {
    setState(() {
      list.items.removeAt(x);
      saveToStorage();
    });
  }

  void getAnnouncements ()
  {
    items = storage.getItem('my_announcements');
  }
  void checkAnnouncements() {
    items = storage.getItem('my_announcements');

    if (items != null) {
      list.items = List<Announcement>.from(
        (items as List).map(
              (item) =>
              Announcement(
                  body: item['body'],
                  title: item['title'],
                  date: item['date']
              ),
        ),
      );
    }

    String x = new DateFormat("dd-MM-yyyy").format(DateTime.now());
    DateTime currentdate = new DateFormat("dd-MM-yyyy").parse(x);

    for (var i = 0; i < list.items.length; i++) {
      String mydate = list.items[i].date;
      DateTime tempDate = new DateFormat("dd-MM-yyyy").parse(mydate);
      if (tempDate == currentdate) {
        _alertDialog(list.items[i].title, list.items[i].body);
        delAnnouncement(i);
      }
    }
  }



  /*
   Method Name:
    getNotificationValues

   Purpose:
     This method gets what the members current notification preferences are.
   */
  getNotificationValues() async {
    SharedPreferences prefs = await SharedPreferences.getInstance();
    username = prefs.get("username");
    thisgymid = prefs.getInt("gymId");


    final http.Response response = await http.get(
        url + "getNotificationSettings?username="  + username);

    if (response.statusCode != 200) {
      //_showDialogue("Oh no!", response.body);
    } else {
      Notifications notifications =
      Notifications.fromJson(json.decode(response.body));

      pushNotifications = notifications.push;
    }
  }
/*
Class Name:
  DecodeNotification

Purpose:
  This method deserialises the response received from the api.
 */
  String DecodeNotification(message)
  {
    getNotificationValues();

    Details res = Details.fromJson(json.decode(message.body));
    mytitle = res.title;
    mybody = res.body;
    myday = res.day;
    mymonth = res.month;
    myyear = res.year;
    gymid = res.gymid;
    String date = myday+mymonth+ myyear;
    String x = new DateFormat("dd-MM-yyyy").format(DateTime.now());
    DateTime currentdate = new DateFormat("dd-MM-yyyy").parse(x);
    DateTime tempDate = new DateFormat("dd-MM-yyyy").parse(date);

    if(gymid==thisgymid && pushNotifications==true) {
      if (currentdate == tempDate) {
        _alertDialog(mytitle, mybody);
      }
      else
      {
        getAnnouncements();
        addAnnouncement(mybody, mytitle, date);
      }
    }

  }


  @override
  void initState() {
    super.initState();
    controller = ScheduleController([
      Schedule(
        timeOutRunOnce: true,
        timing: [8, 18],
        readFn: () async => await get('schedule'),
        writeFn: (String data) async {
          debugPrint(data);
          await save('schedule', data);
        },
        callback: () {
          getAnnouncements();
          checkAnnouncements();

        },
      ),
    ]);
    controller.run();
    _firebaseMessaging.configure(
      onMessage: (Map<String, dynamic> message) async {
        print("onMessage: $message");
        DecodeNotification(message);
      },
      onLaunch: (Map<String, dynamic> message) async {
        print("onLaunch: $message");
        DecodeNotification(message);
      },
      onResume: (Map<String, dynamic> message) async {
        print("onResume: $message");
        DecodeNotification(message);
      },
    );

  }

  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      title: 'Gym Moves',
      theme: ThemeData(

        visualDensity: VisualDensity.adaptivePlatformDensity,
      ),
      home:(LogIn()),
    );
  }
}


