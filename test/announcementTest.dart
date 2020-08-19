import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';
import 'package:flutter_test/flutter_test.dart';
import 'package:gym_moves/Announcement/SendAnnouncement.dart';
import 'package:shared_preferences/shared_preferences.dart';

Widget buildTestableWidget(Widget widget) {
   return MediaQuery(data: MediaQueryData(), child: MaterialApp(home: widget));
 }

void main() {

  testWidgets('Correct status code returned', (WidgetTester tester) async {
    //await tester.pumpWidget(SendAnnouncement());

    final StatefulWidget widg = SendAnnouncement();
    final SendAnnouncementState annState = widg.createState();
    await tester.pumpWidget(buildTestableWidget(widg));

    expect(find.byType(FlatButton), findsOneWidget);
    expect(find.byType(TextField), findsNWidgets(2));
    await tester.pump();

    await tester.enterText(find.byKey(Key('headingField')), "Test: Heading");
    await tester.pump();
    await tester.enterText(find.byKey(Key('detailsField')), "Testing: Details");
    //await tester.pumpWidget(buildTestableWidget(widg));
    await tester.pump();

    expect(annState.headingOfAnnouncement, "Test: Heading");
    expect(annState.detailsOfAnnouncement, "Testing: Details");

    final prefs = await SharedPreferences.getInstance();
    prefs.setInt('gymId', 1);
    await tester.pump();
    annState.sendValuesToNotify();
    await tester.pump();

    expect(annState.testRes, 200);
  });

  testWidgets('Correct status code returned', (WidgetTester tester) async {
    final StatefulWidget widg = SendAnnouncement();
    final SendAnnouncementState annState = widg.createState();
    await tester.pumpWidget(buildTestableWidget(widg));

    expect(find.byType(FlatButton), findsOneWidget);
    expect(find.byType(TextField), findsNWidgets(2));
    await tester.pump();

    await tester.enterText(find.byKey(Key('headingField')), "Test: Heading");
    await tester.pump();
    await tester.enterText(find.byKey(Key('detailsField')), "Testing: Details");
    await tester.pump();


    expect(annState.headingOfAnnouncement, "Test: Heading");

    expect(annState.detailsOfAnnouncement, "Testing: Details");

    final prefs = await SharedPreferences.getInstance();
    prefs.setInt('gymId', 0);
    await tester.pump();
    annState.sendValuesToNotify();
    await tester.pump();

    expect(annState.testRes, 500);
  });
}
