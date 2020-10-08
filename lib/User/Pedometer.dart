import 'package:flutter/material.dart';
import 'dart:async';
import 'StepCount.dart';

String formatDate(DateTime d) {
  return d.toString().substring(0, 19);
}

class pedometer extends StatefulWidget {
  pedometer({
    Key key,
  }) : super(key: key);
  @override
  pedometerState createState() => pedometerState();
}

class pedometerState extends State<pedometer> {
  Stream<pedoCount> _pedoCountStream;
  Stream<PedestrianStatus> _pedestrianStatusStream;
  String _status = '', _steps ="";
 String cals = "";
// var temp = "";
  @override
  void initState() {
    super.initState();
    initPlatformState();
  }

  void onpedoCount(pedoCount event) {
    print(event);
    setState(() {
      _steps = event.steps.toString();
    });
  }

  void onPedestrianStatusChanged(PedestrianStatus event) {
    print(event);
    setState(() {
      _status = event.status;
    });
  }

  void onPedestrianStatusError(error) {
    print('onPedestrianStatusError: $error');
    setState(() {
      _status = 'Pedestrian Status not available';
    });
    print(_status);
  }

  void onpedoCountError(error) {
    print('onpedoCountError: $error');
    setState(() {
      _steps = 'Step Count not available';
    });
  }

  void initPlatformState() {
    _pedestrianStatusStream = Pedometer.pedestrianStatusStream;
    _pedestrianStatusStream
        .listen(onPedestrianStatusChanged)
        .onError(onPedestrianStatusError);

    _pedoCountStream = Pedometer.pedoCountStream;
    _pedoCountStream.listen(onpedoCount).onError(onpedoCountError);

    if (!mounted) return;
  }
String calc (){
    if(_steps!="") {
      var temp2 = double.parse(_steps);
      var temp = 0.063 * temp2;
      cals = _steps.toString();

      return temp.toString();
    }
    else return "";
}
  @override
  Widget build(BuildContext context) {
    MediaQueryData media = MediaQuery.of(context);
    return new Scaffold(
      body: ListView(children: <Widget>[
        Center(

          child: Column(
            mainAxisAlignment: MainAxisAlignment.center,
            children: <Widget>[
              Image(
                  image: AssetImage('assets/step.png')
              ),
              Divider(
                height: 100,
                thickness: 0,
                color: Colors.white,
              ),
              RichText(
                text: TextSpan(
                  children: [
                    TextSpan(
                      text: "Steps taken ",
                      style: TextStyle(fontSize: 30,color: Colors.black, fontFamily: 'Lastwaerk',),
                    ),
                    WidgetSpan(
                      child: Icon(Icons.directions_walk, size:0.08*0.5*media.size.height,color: Color(0xff7341E6)),
                    ),
                    TextSpan(
                      text: "\n \t \t" +_steps,
                      style: TextStyle(fontSize: 50,color: Colors.black, fontFamily: 'Lastwaerk',),
                    ),
                  ],
                ),
              ),
//              Text(
//                'Steps taken:',
//                style: TextStyle(fontSize: 30),
//              ),
//              Icon(
//                  Icons.directions_walk,
//                  color: Color(0xff7341E6),
//                  size: 0.08*0.5*media.size.height
//              ),
//              Text(
//                _steps,
//                style: TextStyle(fontSize: 60),
//              ),
              Divider(
                height: 50,
                thickness: 0,
                color: Colors.white,
              ),
//              Text(
//                'Calories burned:',
//                style: TextStyle(fontSize: 30),
//              ),
//        Icon(
//            Icons.whatshot,
//            color: Color(0xff7341E6),
//            size: 0.08*0.5*media.size.height
//        ),
//              Text(
//                calc(),
//                style: TextStyle(fontSize: 60),
//              ),
              RichText(
                text: TextSpan(
                  children: [
                    TextSpan(
                      text: "Calories burned ",
                      style: TextStyle(fontSize: 30,color: Colors.black, fontFamily: 'Lastwaerk',),
                    ),
                    WidgetSpan(
                      child: Icon(Icons.whatshot, size:0.08*0.5*media.size.height, color: Color(0xff7341E6)),
                    ),
                    TextSpan(
                      text:"\n \t \t" + calc(),
                      style: TextStyle(fontSize: 50,color: Colors.black, fontFamily: 'Lastwaerk',),
                    ),
                  ],
                ),
              ),
              Divider(
                height: 100,
                thickness: 0,
                color: Colors.white,
              ),

//              Transform.translate(
//                  offset: Offset(0*media.size.width, -0.035 * media.size.height),
//                  child: Container(
//                      width:0.6* media.size.width,
//                      height: 0.4* media.size.height,
//                      decoration: BoxDecoration(
//                        image: DecorationImage(
//                          image: const AssetImage('assets/step.png'),
//                          fit: BoxFit.fill,
//
//                        ),
//                      )
//                  )
//              ),
            ],
          ),
        ),
      ]),
    );
  }
}