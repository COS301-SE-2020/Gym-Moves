import 'package:flutter/material.dart';
import 'dart:async';



import 'StepCount.dart';

String formatDate(DateTime d) {
  return d.toString().substring(0, 19);
}
//
//void main() {
//  runApp(MyApp());
//}
//
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
  String _status = '?', _steps = '?';

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

  @override
  Widget build(BuildContext context) {
    MediaQueryData media = MediaQuery.of(context);
    return Scaffold(

        body: Center(

          child: Column(
            mainAxisAlignment: MainAxisAlignment.center,
            children: <Widget>[
              Text(
                'Steps taken:',
                style: TextStyle(fontSize: 30),
              ),
              Text(
                _steps,
                style: TextStyle(fontSize: 60),
              ),
              Divider(
                height: 100,
                thickness: 0,
                color: Colors.white,
              ),
              Text(
                'Pedestrian status:',
                style: TextStyle(fontSize: 30),
              ),
              Icon(
                _status == 'walking'
                    ? Icons.directions_walk
                    : _status == 'stopped'
                    ? Icons.accessibility_new
                    : Icons.error,
                size: 100,
              ),
              Center(
                child: Text(
                  _status,
                  style: _status == 'walking' || _status == 'stopped'
                      ? TextStyle(fontSize: 30)
                      : TextStyle(fontSize: 20, color: Colors.red),
                ),
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
      );
  }
}