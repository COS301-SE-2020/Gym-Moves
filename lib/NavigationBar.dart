/*
File Name:
  NavigationBar.dart

Author:
  Danel

Date Created:
  06/08/2020

Update History:
--------------------------------------------------------------------------------
Date          |    Author      |     Changes
--------------------------------------------------------------------------------

Functional Description:
  This file implements the NavigationBarState class. It creates the UI for users
  to be able to see the navigation bar, as well as actually navigates through
  the screens. It also implements the NavigationBar class which calls the other
  class to be built.

Classes in the File:
- NavigationBar
- NavigationBarState
 */

import 'package:flutter/material.dart';
import 'package:gym_moves/GymClass/ManagerViewClasses.dart';
import 'package:gym_moves/GymClass/MemberViewMyClasses.dart';
import 'package:shared_preferences/shared_preferences.dart';
import 'package:gym_moves/User/ViewMyProfile.dart';
import 'package:gym_moves/User/Welcome.dart';
import 'package:gym_moves/Announcement/SendAnnouncement.dart';
import 'package:gym_moves/GymClass/InstructorViewClasses.dart';

class NavigationBar extends StatefulWidget {
  final int index;
  NavigationBar({Key key, this.index}) : super(key: key);

  @override
  NavigationBarState createState() => NavigationBarState(selectedIndex: this.index);
}

class NavigationBarState extends State<NavigationBar> {
  int selectedIndex;
  int type;
  Future local;

  NavigationBarState({Key key, this.selectedIndex});

  @override
  void initState() {
    local = _getUserType();
    super.initState();
  }

  _getUserType() async {
    SharedPreferences prefs = await SharedPreferences.getInstance();
    type = prefs.get("type");
  }

  @override
  Widget build(BuildContext context) {

    List<Widget> _widgetOptions = <Widget>[
      Welcome(),
      ViewMyProfile(),
      SendAnnouncement(),
      (type == 0) ? MemberViewMyClasses() : ((type == 1) ? InstructorViewClasses()
          : ManagerViewClasses())
    ];

    return new FutureBuilder(
      future: local,
        builder: (BuildContext context, AsyncSnapshot snapshot) {
      if (snapshot.connectionState == ConnectionState.done) {
          return Scaffold(
            backgroundColor: Colors.white,
            bottomNavigationBar: new BottomNavigationBar(
              currentIndex: selectedIndex,
              showSelectedLabels: false,
              showUnselectedLabels: false,
              items: [
                BottomNavigationBarItem(
                  icon: Icon(
                      Icons.home,
                      color: const Color(0xff787878)
                  ),
                  title: Text('HOME'),
                  activeIcon: Icon(
                      Icons.home,
                      color: const Color(0xff7341E6)
                  ),
                ),
                BottomNavigationBarItem(
                    icon: Icon(
                        Icons.account_circle,
                        color: const Color(0xff787878)
                    ),
                    title: Text('PROFILE'),
                    activeIcon: Icon(
                        Icons.account_circle,
                        color: const Color(0xff7341E6)
                    )
                ),
                BottomNavigationBarItem(
                    icon: Icon(
                        Icons.announcement,
                        color: const Color(0xff787878)
                    ),
                    title: Text('ANNOUNCEMENTS'),
                    activeIcon: Icon(
                        Icons.announcement,
                        color: const Color(0xff7341E6)
                    )
                ),
                BottomNavigationBarItem(
                    icon: Icon(
                        Icons.school,
                        color: const Color(0xff787878)
                    ),
                    title: Text('CLASSES'),
                    activeIcon: Icon(
                        Icons.school,
                        color: const Color(0xff7341E6)
                    )
                )
              ],
              onTap: (index) {
                setState(() {
                  selectedIndex = index;
                });
              },
            ),
            body: _widgetOptions.elementAt(selectedIndex),
          );
        } else if (snapshot.hasError) {
          return Text("${snapshot.error}");
        }
        // By default, show a loading spinner.
        return Center(
            child: CircularProgressIndicator(
              backgroundColor: Colors.white,
            ));
      },
    );


  }

}