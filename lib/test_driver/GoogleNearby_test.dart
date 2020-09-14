import 'package:flutter_driver/flutter_driver.dart';
import 'package:test/test.dart';


void main() {
  group('Count users', () {

    final enterFinder = find.byValueKey('enter');
    final exitFinder = find.byValueKey('exit');

    FlutterDriver driver;
    setUpAll(() async {
      driver = await FlutterDriver.connect();
    });

    tearDownAll(() async {
      if (driver != null) {
        driver.close();
      }
    });

    test('starts at 0', () async {
      expect(await driver.getText(enterFinder), "0");
    });

    test('increments the counter', () async {
      await driver.tap(enterFinder);
      expect(await driver.getText(enterFinder), "1");
    });

    test('decrements the counter', () async {
      await driver.tap(exitFinder);
      expect(await driver.getText(enterFinder), "0");
    });
  });
}

