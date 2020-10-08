import 'dart:convert';

import 'package:auto_size_text/auto_size_text.dart';
import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';
import 'package:flutter_datetime_picker/flutter_datetime_picker.dart';
import 'package:shared_preferences/shared_preferences.dart';
import 'package:numberpicker/numberpicker.dart';
import 'package:http/http.dart' as http;

class Progress extends StatefulWidget {
  const Progress({Key key}) : super(key: key);

  @override
  ProgressState createState() => ProgressState();
}

class ProgressState extends State<Progress> {
  ProgressState({Key key});

  int membersWeight = 0;
  int membersHeight = 0;

  String bmi = "-";
  int change = 0;

  String bmiStatus = "";

  List<DataValues> data = [];

  Future dataFuture;
  String url = "https://gymmoveswebapi.azurewebsites.net/api/weightdata/";

  @override
  void initState() {
    dataFuture = _getDataValues();
    super.initState();
  }

  _getDataValues() async {
    SharedPreferences prefs = await SharedPreferences.getInstance();
    String username = prefs.get("username");

    final http.Response response =
        await http.get(url + "getWeight?username=" + username);

    List<dynamic> dataJSON = json.decode(response.body);

    for (int i = 0; i < dataJSON.length; i++) {
      data.add(DataValues.fromJson(dataJSON[i]));
    }

    if (dataJSON.length > 0) {
      membersWeight = data[data.length - 1].weight as int;
      membersHeight = data[data.length - 1].height as int;

      setState(() => bmi =
          (membersWeight / ((membersHeight / 100) * membersHeight / 100))
              .toStringAsFixed(2));

      if ((membersWeight / ((membersHeight / 100) * membersHeight / 100)) <
          18.5) {
        setState(() => bmiStatus = "Underweight");
      } else if ((membersWeight /
              ((membersHeight / 100) * membersHeight / 100)) <
          25.0) {
        setState(() => bmiStatus = "Normal");
      } else if ((membersWeight /
              ((membersHeight / 100) * membersHeight / 100)) <
          30.0) {
        setState(() => bmiStatus = "Overweight");
      } else {
        setState(() => bmiStatus = "Obese");
      }

      if (dataJSON.length > 1) {
        setState(() =>
            change = (data[data.length - 1].weight - data[0].weight) as int);
      } else {
        change = 0;
      }
    }
  }

  setDataValues() async {
    SharedPreferences prefs = await SharedPreferences.getInstance();
    String username = prefs.get("username");

    DateTime date = DateTime.now();

    final http.Response response = await http.post(
      url + "add",
      headers: <String, String>{
        'Content-Type': 'application/json',
      },
      body: jsonEncode({
        "Username": username,
        "Weight": membersWeight.toDouble(),
        "Height": membersHeight.toDouble(),
        "Year": date.year,
        "Month": date.month,
        "Day": date.day
      }),
    );

    setState(() {
      if (membersWeight > 0 && membersHeight > 0)
        bmi = (membersWeight / ((membersHeight / 100) * membersHeight / 100))
            .toStringAsFixed(2);

      if ((membersWeight / ((membersHeight / 100) * membersHeight / 100)) <
          18.5) {
        setState(() => bmiStatus = "Underweight");
      } else if ((membersWeight /
              ((membersHeight / 100) * membersHeight / 100)) <
          25.0) {
        setState(() => bmiStatus = "Normal");
      } else if ((membersWeight /
              ((membersHeight / 100) * membersHeight / 100)) <
          30.0) {
        setState(() => bmiStatus = "Overweight");
      } else {
        setState(() => bmiStatus = "Obese");
      }

      if (data.length > 1)
        setState(() =>  change = (data[data.length - 1].weight - data[0].weight) as int);
    });

  }

  @override
  Widget build(BuildContext context) {
    MediaQueryData media = MediaQuery.of(context);

    return new FutureBuilder(
        future: dataFuture,
        builder: (BuildContext context, AsyncSnapshot snapshot) {
          if (snapshot.connectionState == ConnectionState.done) {
            return new Scaffold(
                backgroundColor: const Color(0xffffffff),
                body: ListView(children: <Widget>[
                  Stack(children: <Widget>[
                    Container(
                        alignment: Alignment.centerRight,
                        child: Container(
                            width: media.size.width,
                            height: 0.35 * media.size.height,
                            decoration: BoxDecoration(
                                image: DecorationImage(
                              image:
                                  const AssetImage('assets/ProgressScreen.png'),
                              fit: BoxFit.fill,
                            )))),
                    Container(
                        padding: EdgeInsets.all(0.1 * media.size.width),
                        alignment: Alignment.bottomLeft,
                        width: media.size.width,
                        height: 0.33 * media.size.height,
                        child: AutoSizeText("Progress",
                            style: TextStyle(
                              fontFamily: 'Lastwaerk',
                              fontSize: media.size.width * 0.1,
                              color: const Color(0xff3E3E3E),
                            ),
                            maxLines: 1))
                  ]),
                  Row(children: <Widget>[
                    Expanded(
                        flex: 1,
                        child: Column(children: <Widget>[
                          Container(
                            alignment: Alignment.bottomCenter,
                            height: 0.45 * 1 / 2 * media.size.height,
                            child: Center(
                                child: Container(
                                    padding: EdgeInsets.fromLTRB(0, 0, 0, 20),
                                    alignment: Alignment.bottomCenter,
                                    width: 0.2 * media.size.width,
                                    child: FlatButton(
                                        shape: RoundedRectangleBorder(
                                            borderRadius:
                                                BorderRadius.circular(10.0)),
                                        color: const Color(0xff7341E6),
                                        onPressed: () {
                                          showDialog<int>(
                                              context: context,
                                              builder: (BuildContext context) {
                                                return new NumberPickerDialog
                                                        .integer(
                                                    minValue: 0,
                                                    maxValue: 300,
                                                    initialIntegerValue:
                                                        membersHeight,
                                                    title: Text(
                                                        "Choose your height (cm)"));
                                              }).then((int value) {
                                            if (value != null) {
                                              setState(
                                                  () => membersHeight = value);
                                              setDataValues();
                                            }
                                          });
                                        },
                                        textColor: Colors.white,
                                        padding: const EdgeInsets.all(0.0),
                                        child: Container(
                                            padding: const EdgeInsets.all(10.0),
                                            child: Text('Height',
                                                style: TextStyle(
                                                    fontSize:
                                                        0.04 * media.size.width,
                                                    fontFamily: 'Roboto')))))),
                          ),
                          Container(
                            alignment: Alignment.topCenter,
                            height: 0.45 * 1 / 2 * media.size.height,
                            child: Center(
                                child: Container(
                                    padding: EdgeInsets.fromLTRB(0, 20, 0, 0),
                                    alignment: Alignment.topCenter,
                                    width: 0.2 * media.size.width,
                                    child: FlatButton(
                                        shape: RoundedRectangleBorder(
                                            borderRadius:
                                                BorderRadius.circular(10.0)),
                                        color: const Color(0xff7341E6),
                                        onPressed: () {
                                          showDialog<int>(
                                              context: context,
                                              builder: (BuildContext context) {
                                                return new NumberPickerDialog
                                                        .integer(
                                                    minValue: 0,
                                                    maxValue: 300,
                                                    initialIntegerValue:
                                                        membersWeight,
                                                    title: Text(
                                                        "Choose your weight (kgs)"));
                                              }).then((int value) {
                                            if (value != null) {
                                              setState(
                                                  () => membersWeight = value);
                                              setDataValues();
                                            }
                                          });
                                        },
                                        textColor: Colors.white,
                                        padding: const EdgeInsets.all(0.0),
                                        child: Container(
                                            padding: const EdgeInsets.all(10.0),
                                            child: Text('Weight',
                                                style: TextStyle(
                                                    fontSize:
                                                        0.04 * media.size.width,
                                                    fontFamily: 'Roboto')))))),
                          )
                        ])),
                    Expanded(
                        flex: 2,
                        child: Column(children: <Widget>[
                          Container(
                              padding: EdgeInsets.fromLTRB(
                                  0.0,
                                  0.45 * 0.6 * 0.5 * media.size.height,
                                  0.0,
                                  0.0),
                              alignment: Alignment.topCenter,
                              height: 0.6 * 0.5 * media.size.height,
                              child: Text(
                                "Your BMI is:\n" + bmi,
                                textAlign: TextAlign.center,
                                style: TextStyle(
                                  fontSize: media.size.width * 0.085,
                                  color: const Color(0xff3E3E3E),
                                ),
                              )),
                          Container(
                              padding: EdgeInsets.fromLTRB(0.0, 0.0, 0.0,
                                  0.45 * 0.6 * 0.5 * media.size.height),
                              alignment: Alignment.center,
                              height: 0.6 * 0.5 * media.size.height,
                              child: Text.rich(
                                TextSpan(children: [
                                  TextSpan(
                                      text: bmiStatus,
                                      style: TextStyle(
                                        fontSize: media.size.width * 0.07,
                                        color: (bmiStatus == "Underweight")
                                            ? Colors.orange
                                            : (bmiStatus == "Normal")
                                                ? Colors.green
                                                : (bmiStatus == "Overweight")
                                                    ? Colors.orange
                                                    : Colors.red,
                                      )),
                                  TextSpan(
                                      text: (change > 0)? "\n\n\n Total change: +" +
                                          change.toString() + "kgs" :
                                      "\n\n\n Total change: " +
                                          change.toString() + "kgs",
                                      style: TextStyle(
                                        fontSize: media.size.width * 0.04,
                                        color: const Color(0xff3E3E3E),
                                      ))
                                ]),
                                textAlign: TextAlign.center,
                              ))
                        ]))
                  ])
                ]));
          } else {
            return new Scaffold(
                backgroundColor: const Color(0xffffffff),
                body: ListView(children: <Widget>[
                  Stack(children: <Widget>[
                    Container(
                        alignment: Alignment.centerRight,
                        child: Container(
                            width: media.size.width,
                            height: 0.35 * media.size.height,
                            decoration: BoxDecoration(
                                image: DecorationImage(
                              image:
                                  const AssetImage('assets/ProgressScreen.png'),
                              fit: BoxFit.fill,
                            )))),
                    Container(
                        padding: EdgeInsets.all(0.1 * media.size.width),
                        alignment: Alignment.bottomLeft,
                        width: media.size.width,
                        height: 0.33 * media.size.height,
                        child: AutoSizeText("Progress",
                            style: TextStyle(
                              fontFamily: 'Lastwaerk',
                              fontSize: media.size.width * 0.1,
                              color: const Color(0xff3E3E3E),
                            ),
                            maxLines: 1))
                  ]),
                  Row(children: <Widget>[
                    Expanded(
                        flex: 1,
                        child: Column(children: <Widget>[
                          Container(
                            alignment: Alignment.bottomCenter,
                            height: 0.45 * 1 / 2 * media.size.height,
                            child: Center(
                                child: Container(
                                    padding: EdgeInsets.fromLTRB(0, 0, 0, 20),
                                    alignment: Alignment.bottomCenter,
                                    width: 0.2 * media.size.width,
                                    child: FlatButton(
                                        shape: RoundedRectangleBorder(
                                            borderRadius:
                                                BorderRadius.circular(10.0)),
                                        color: const Color(0xff7341E6),
                                        onPressed: () {
                                          showDialog<int>(
                                              context: context,
                                              builder: (BuildContext context) {
                                                return new NumberPickerDialog
                                                        .integer(
                                                    minValue: 0,
                                                    maxValue: 300,
                                                    initialIntegerValue:
                                                        membersHeight,
                                                    title: Text(
                                                        "Choose your height (cm)"));
                                              }).then((int value) {
                                            if (value != null) {
                                              setState(
                                                  () => membersHeight = value);
                                              setDataValues();
                                            }
                                          });
                                        },
                                        textColor: Colors.white,
                                        padding: const EdgeInsets.all(0.0),
                                        child: Container(
                                            padding: const EdgeInsets.all(10.0),
                                            child: Text('Height',
                                                style: TextStyle(
                                                    fontSize:
                                                        0.04 * media.size.width,
                                                    fontFamily: 'Roboto')))))),
                          ),
                          Container(
                            alignment: Alignment.topCenter,
                            height: 0.45 * 1 / 2 * media.size.height,
                            child: Center(
                                child: Container(
                                    padding: EdgeInsets.fromLTRB(0, 20, 0, 0),
                                    alignment: Alignment.topCenter,
                                    width: 0.2 * media.size.width,
                                    child: FlatButton(
                                        shape: RoundedRectangleBorder(
                                            borderRadius:
                                                BorderRadius.circular(10.0)),
                                        color: const Color(0xff7341E6),
                                        onPressed: () {
                                          showDialog<int>(
                                              context: context,
                                              builder: (BuildContext context) {
                                                return new NumberPickerDialog
                                                        .integer(
                                                    minValue: 0,
                                                    maxValue: 300,
                                                    initialIntegerValue:
                                                        membersWeight,
                                                    title: Text(
                                                        "Choose your weight (kgs)"));
                                              }).then((int value) {
                                            if (value != null) {
                                              setState(
                                                  () => membersWeight = value);
                                              setDataValues();
                                            }
                                          });
                                        },
                                        textColor: Colors.white,
                                        padding: const EdgeInsets.all(0.0),
                                        child: Container(
                                            padding: const EdgeInsets.all(10.0),
                                            child: Text('Weight',
                                                style: TextStyle(
                                                    fontSize:
                                                        0.04 * media.size.width,
                                                    fontFamily: 'Roboto')))))),
                          )
                        ])),
                    Expanded(
                        flex: 2,
                        child: Column(children: <Widget>[
                          Container(
                              padding: EdgeInsets.fromLTRB(
                                  0.0,
                                  0.45 * 0.6 * 0.5 * media.size.height,
                                  0.0,
                                  0.0),
                              alignment: Alignment.topCenter,
                              height: 0.6 * 0.5 * media.size.height,
                              child: Text(
                                "Your BMI is:\n" + bmi,
                                textAlign: TextAlign.center,
                                style: TextStyle(
                                  fontSize: media.size.width * 0.085,
                                  color: const Color(0xff3E3E3E),
                                ),
                              )),
                          Container(
                              padding: EdgeInsets.fromLTRB(0.0, 0.0, 0.0,
                                  0.45 * 0.6 * 0.5 * media.size.height),
                              alignment: Alignment.center,
                              height: 0.6 * 0.5 * media.size.height,
                              child: Text.rich(
                                TextSpan(children: [
                                  TextSpan(
                                      text: bmiStatus,
                                      style: TextStyle(
                                        fontSize: media.size.width * 0.07,
                                        color: const Color(0xff3E3E3E),
                                      )),
                                  TextSpan(
                                      text: "\n\n\n Total change: " +
                                          change.toString() +
                                          "kgs",
                                      style: TextStyle(
                                        fontSize: media.size.width * 0.04,
                                        color: const Color(0xff3E3E3E),
                                      ))
                                ]),
                                textAlign: TextAlign.center,
                              ))
                        ]))
                  ])
                ]));
          }
        });
  }
}

class DataValues {
  final int weight;
  final int height;
  final DateTime date;

  DataValues({this.weight, this.height, this.date});

  factory DataValues.fromJson(Map<dynamic, dynamic> json) {
    return DataValues(
        weight: json['weight'],
        height: json['height'],
        date: DateTime.parse(json['date']));
  }
}

const String backArrow =
    '<svg viewBox="28.2 38.0 31.4 27.9" ><path transform="matrix(-1.0, 0.0, 0.0, -1.0, 65.61, 71.93)" d="M 21.68118286132813 6 L 18.91737365722656 8.460894584655762 L 29.85499572753906 18.21720886230469 L 6 18.21720886230469 L 6 21.70783996582031 L 29.85499572753906 21.70783996582031 L 18.91737365722656 31.46415710449219 L 21.68118286132813 33.925048828125 L 37.36236572265625 19.9625244140625 L 21.68118286132813 6 Z" fill="#fcfbfc" stroke="none" stroke-width="1" stroke-miterlimit="4" stroke-linecap="butt" /></svg>';
const String dumbbell =
    '<svg viewBox="105.7 62.6 148.0 151.2" ><path transform="translate(104.73, 61.61)" d="M 52.92843627929688 140.3509826660156 C 54.10280227661133 141.6522674560547 54.0328369140625 143.6774139404297 52.77159881591797 144.8908081054688 L 45.95822906494141 151.4024047851563 C 44.69138336181641 152.6053619384766 42.71061325073242 152.5321807861328 41.53205490112305 151.2389068603516 L 1.853297233581543 107.5332946777344 C 0.6797584295272827 106.2311325073242 0.7511200904846191 104.2059478759766 2.0132737159729 102.9934539794922 L 8.836055755615234 96.49147796630859 C 10.1022424697876 95.28672790527344 12.08445072174072 95.35995483398438 13.26223087310791 96.65498352050781 L 52.92843627929688 140.3509826660156 Z M 103.5581436157227 58.11441802978516 C 104.731689453125 59.41657638549805 104.6603317260742 61.44176483154297 103.3981552124023 62.65425872802734 L 60.51661682128906 103.5961990356445 C 59.24977874755859 104.7991409301758 57.26900863647461 104.7259674072266 56.09044647216797 103.4326858520508 L 45.85470962524414 92.14398956298828 C 44.68115997314453 90.84183502197266 44.75253295898438 88.816650390625 46.01469421386719 87.60414886474609 L 88.88682556152344 46.66221237182617 C 89.49431610107422 46.08388519287109 90.30185699462891 45.77615356445313 91.13150787353516 45.80682754516602 C 91.96115875244141 45.83749389648438 92.74484252929688 46.20405197143555 93.30986022949219 46.82572174072266 L 103.5581436157227 58.11441040039063 Z M 67.51820373535156 126.4140396118164 C 68.69140625 127.7061004638672 68.61925506591797 129.7483978271484 67.35822296142578 130.9538726806641 L 60.53544616699219 137.4622650146484 C 59.26925277709961 138.6670227050781 57.28704452514648 138.5937805175781 56.10926818847656 137.2987518310547 L 16.45247077941895 93.59315490722656 C 15.27809715270996 92.29185485839844 15.34806632995605 90.26671600341797 16.60931587219238 89.05331420898438 L 23.41954040527344 82.54812622070313 C 24.68638038635254 81.34517669677734 26.66715812683105 81.41834259033203 27.84571838378906 82.71163177490234 L 67.51820373535156 126.4140396118164 Z M 133.5908966064453 59.68219757080078 C 134.7636108398438 60.98521423339844 134.6908416748047 63.01043319702148 133.4277801513672 64.22203063964844 L 126.6081466674805 70.73042297363281 C 125.3410949707031 71.93457794189453 123.3593826293945 71.86284637451172 122.178840637207 70.57012939453125 L 82.51576232910156 26.8741512298584 C 81.33965301513672 25.57351112365723 81.40966796875 23.54692459106445 82.67259216308594 22.33430862426758 L 89.51105499267578 15.80988693237305 C 90.11854553222656 15.23155879974365 90.92609405517578 14.923828125 91.75575256347656 14.95449733734131 C 92.58540344238281 14.98516750335693 93.36907958984375 15.35172557830811 93.93411254882813 15.97339820861816 L 133.5908966064453 59.68219757080078 Z M 148.1587066650391 45.7677116394043 C 149.3308715820313 47.07159805297852 149.2595672607422 49.09637832641602 147.9987182617188 50.31076431274414 L 141.1916198730469 56.81594085693359 C 140.5845489501953 57.39468002319336 139.7772369384766 57.70297622680664 138.9475860595703 57.67290496826172 C 138.1179351806641 57.6428337097168 137.3340148925781 57.2768669128418 136.7685852050781 56.65563583374023 L 97.11178588867188 12.9404239654541 C 95.93878173828125 11.63739967346191 96.00868225097656 9.612658500671387 97.26863098144531 8.39737606048584 L 104.0820007324219 1.879366874694824 C 104.6881790161133 1.300024747848511 105.4951324462891 0.9912708401679993 106.3245315551758 1.02135181427002 C 107.1539154052734 1.051432132720947 107.9374313354492 1.417869567871094 108.5018997192383 2.039670705795288 L 148.1587066650391 45.7677116394043 Z" fill="#513369" fill-opacity="0.08" stroke="none" stroke-width="1" stroke-opacity="0.5" stroke-miterlimit="4" stroke-linecap="butt" /></svg>';
