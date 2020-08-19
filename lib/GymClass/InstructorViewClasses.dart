import 'package:flutter/material.dart';
import 'package:flutter_svg/flutter_svg.dart';
import 'dart:convert';
import 'package:shared_preferences/shared_preferences.dart';
import 'package:http/http.dart';
import 'package:gym_moves/GymClass/InstructorClassDetails.dart';

class InstructorViewClasses extends StatefulWidget {
  const InstructorViewClasses({Key key}) : super(key: key);

  @override
  InstructorViewClassesState createState() => InstructorViewClassesState();
}

class InstructorViewClassesState extends State<InstructorViewClasses> {
  InstructorViewClassesState({Key key});

  List<GymClass> allClasses = [];

  String className, classDay, classStart, classEnd, instructorName;
  String instructorUsername, classDescription;
  bool cancelled;
  int classAvailableSpots, classID, maxSpots;

  Future<String> response;

  int gymId = 0;
  int type = 0;

  List<Instructor> instructors = [];

  Future idFromLocal, typeFromLocal;
  List<dynamic> classesJson = [];

  @override
  void initState() {
    idFromLocal = _getId();
    typeFromLocal = _getType();
    super.initState();
    response = _getAllClasses();
  }

  @override
  Widget build(BuildContext context) {
    MediaQueryData media = MediaQuery.of(context);

    return Scaffold(
        backgroundColor: const Color(0xffffffff),
        body: Column(children: <Widget>[
          Stack(children: <Widget>[
            Container(
                width: 0.8 * media.size.width,
                height: 0.3 * media.size.height,
                decoration: BoxDecoration(
                    image: DecorationImage(
                        image: const AssetImage('assets/Classes.png'),
                        fit: BoxFit.fill))),
            Container(
              padding: EdgeInsets.all(0.01 * media.size.width),
              width: media.size.width,
              height: 0.3 * media.size.height,
              alignment: Alignment.centerRight,
              child: Text(
                'Classes',
                style: TextStyle(
                  fontFamily: 'Lastwaerk',
                  fontSize: 0.1 * media.size.width,
                  color: const Color(0xFF3E3E3E),
                ),
                textAlign: TextAlign.right,
              ),
            ),
          ]),
          Expanded(
            child: FutureBuilder<String>(
                future: response,
                builder: (context, snapshot) {
                  if (snapshot.hasData) {
                    return getClasses(media);
                  } else if (snapshot.hasError) {
                    return Text("${snapshot.error}");
                  }
                  // By default, show a loading spinner.
                  return Center(
                      child: CircularProgressIndicator(
                          backgroundColor: Colors.white));
                }),
          )
        ]));
  }

/*
  Method Name:
    _getId
  Purpose:
     This method is used to get the gymId from local storage.
*/
  _getId() async {
    SharedPreferences prefs = await SharedPreferences.getInstance();
    gymId = prefs.get("gymId");
  }

/*
  Method Name:
    _getType
  Purpose:
     This method is used to get the user type from local storage.
*/
  _getType() async {
    SharedPreferences prefs = await SharedPreferences.getInstance();
    type = prefs.get("type");
  }

/*
  Method Name:
    _getAllClasses
  Purpose:
     This method is used to make a get request and fetch the all the classes
     available at a specific gym.
*/
  Future<String> _getAllClasses() async {
    await _getInstructors();

    String gID = gymId.toString();
    String url =
        'https://gymmoveswebapi.azurewebsites.net/api/classes/gymlist?gymid=$gID';
    Response response = await get(url);
    String responseBody = response.body;

    classesJson = json.decode(responseBody);

    if (response.statusCode == 200) {
      return responseBody;
    } else {
      throw Exception('Failed to retrieve user data. Please Try Again Later');
    }
  }

  /*
   Method Name:
    getClasses

   Purpose:
    This method gets the response from the API and displays it on screen.
   */
  Widget getClasses(MediaQueryData media) {
    List<Widget> classes = new List();

    if (classesJson.length == 0) {
      return Container(
          height: 1 / 10 * media.size.height,
          width: media.size.width,
          child: Text(
            'There are currently no available classes at the gym.',
            style: TextStyle(
              fontFamily: 'Roboto',
              fontSize: media.size.width * 0.05,
              color: const Color(0xFF3E3E3E),
            ),
            textAlign: TextAlign.center,
          ));
    } else {
      for (int i = 0; i < classesJson.length; i++) {
        allClasses.add(GymClass.fromJson(classesJson[i]));
      }

      int amountOfClasses = allClasses.length;

      for (int i = 0; i < amountOfClasses; i++) {
        instructorUsername = allClasses[i].instructorUsername;

        int j = 0;

        for (; j < instructors.length; j++) {
          if (instructors[j].username == instructorUsername) {
            break;
          }
        }

        className = allClasses[i].name;
        classDay = allClasses[i].day;
        classStart = allClasses[i].startTime;
        classEnd = allClasses[i].endTime;
        classAvailableSpots =
            allClasses[i].maxCapacity - allClasses[i].currentStudents;
        classDescription = allClasses[i].description;

        classes.add(GestureDetector(
            onTap: () {
              Navigator.push(
                  context,
                  MaterialPageRoute(
                      builder: (context) => InstructorClassDetails(
                            instructorName: instructors[j].name +
                                " " +
                                instructors[j].surname,
                            className: allClasses[i].name,
                            classDay: allClasses[i].day,
                            classStart: allClasses[i].startTime,
                            availableSpots: allClasses[i].maxCapacity -
                                allClasses[i].currentStudents,
                            description: allClasses[i].description.toString(),
                            classId: allClasses[i].classId,
                            cancel: allClasses[i].cancelled,
                            max: allClasses[i].maxCapacity,
                            instructorUsername:
                                allClasses[i].instructorUsername,
                            classEnd: allClasses[i].endTime,
                          )));
            },
            child: Row(
                mainAxisAlignment: MainAxisAlignment.center,
                children: <Widget>[
                  Stack(children: <Widget>[
                    allClasses[i].cancelled
                        ? Transform.translate(
                        offset: Offset(0.74 * 0.4 * media.size.width,
                            0.2 * 0.35 * media.size.height),
                        child: Container(
                            width: 0.7 * media.size.width * 0.7,
                            height: 0.3 * media.size.width * 0.7,
                            decoration: BoxDecoration(
                                image: DecorationImage(
                                    image: const AssetImage(
                                        'assets/Cancelled.png'),
                                    fit: BoxFit.fill))))
                        : Transform.translate(
                        offset: Offset(0.74 * 0.7 * media.size.width,
                            0.2 * 0.4 * media.size.height),
                        child: SvgPicture.string(
                          dumbbell,
                          width: 0.2 * media.size.width * 0.7,
                          allowDrawingOutsideViewBox: true,
                          color: Colors.black,
                        )),

                    Container(
                        width: 0.74 * media.size.width,
                        height: 0.2 * media.size.height,
                        decoration: BoxDecoration(
                          borderRadius: BorderRadius.circular(19.0),
                          color: const Color(0xff7341E6).withOpacity(0.03),
                          border: Border.all(
                              width: 1.0,
                              color: const Color(0xff7341E6).withOpacity(0.15)),
                        )),
                    Container(
                        alignment: Alignment.center,
                        width: 0.74 * media.size.width,
                        height: 0.2 * 0.5 * media.size.height,
                        child: Text(className,
                            style: TextStyle(
                                color: const Color(0xff3E3E3E),
                                fontSize: 0.06 * media.size.width,
                                fontFamily: "Lastwaerk"))),
                    Transform.translate(
                        offset: Offset(
                            0.05 * media.size.width, 0.095 * media.size.height),
                        child: Container(
                            child:
                                Icon(Icons.today, color: Color(0xff3E3E3E)))),
                    Transform.translate(
                        offset: Offset(
                            0.12 * media.size.width, 0.1 * media.size.height),
                        child: Container(
                            child: Text("   " + classDay,
                                style: TextStyle(
                                    color: const Color(0xff3E3E3E),
                                    fontSize: 0.038 * media.size.width)))),
                    Transform.translate(
                        offset: Offset(
                            0.05 * media.size.width, 0.135 * media.size.height),
                        child: Container(
                            child: Icon(Icons.access_time,
                                color: Color(0xff3E3E3E)))),
                    Transform.translate(
                        offset: Offset(
                            0.12 * media.size.width, 0.14 * media.size.height),
                        child: Container(
                            child: Text("   " + classStart,
                                style: TextStyle(
                                    color: const Color(0xff3E3E3E),
                                    fontSize: 0.038 * media.size.width)))),
                  ])
                ])));

        classes.add(SizedBox(height: 20));
      }
    }
    return ListView(padding: const EdgeInsets.all(15), children: classes);
  }

  /*
  Method Name:
    _getInstructors

  Purpose:
     This method is used to make a get request and fetch the instructors
     of a gym.
*/
  _getInstructors() async {
    final prefs = await SharedPreferences.getInstance();
    int gymId = prefs.get("gymId");

    print(gymId);

    String url =
        'https://gymmoveswebapi.azurewebsites.net/api/user/allInstructors?gymID=' +
            gymId.toString();
    Response response = await get(url);

    List<dynamic> instructorsJSON = json.decode(response.body);

    for (int i = 0; i < instructorsJSON.length; i++) {
      instructors.add(Instructor.fromJson(instructorsJSON[i]));
    }
  }
}

/*
Class Name:
  GymClass
Purpose:
  Structure of a gym class.
*/
class GymClass {
  final int classId, gymId, maxCapacity, currentStudents;
  final String instructorUsername, name, description, day, startTime, endTime;
  final bool cancelled;

  GymClass(
      {this.classId,
      this.gymId,
      this.instructorUsername,
      this.name,
      this.description,
      this.day,
      this.startTime,
      this.endTime,
      this.maxCapacity,
      this.currentStudents,
      this.cancelled});

  factory GymClass.fromJson(Map<String, dynamic> json) {
    return GymClass(
        classId: json['classId'],
        gymId: json['gymId'],
        instructorUsername: json['instructor'],
        name: json['name'],
        description: json['description'],
        day: json['day'],
        startTime: json['startTime'],
        endTime: json['endTime'],
        maxCapacity: json['maxCapacity'],
        currentStudents: json['currentStudents'],
        cancelled: json['cancelled']);
  }
}

/*
Class Name:
  Instructor
Purpose:
  This class is the structure of an instructor.
 */
class Instructor {
  final String name;
  final String surname;
  final String username;

  Instructor({this.name, this.surname, this.username});

  factory Instructor.fromJson(Map<String, dynamic> json) {
    return Instructor(
        name: json['name'],
        surname: json['surname'],
        username: json['username']);
  }
}

const String backArrow =
    '<svg viewBox="28.2 38.0 31.4 27.9" ><path transform="matrix(-1.0, 0.0, 0.0, -1.0, 65.61, 71.93)" d="M 21.68118286132813 6 L 18.91737365722656 8.460894584655762 L 29.85499572753906 18.21720886230469 L 6 18.21720886230469 L 6 21.70783996582031 L 29.85499572753906 21.70783996582031 L 18.91737365722656 31.46415710449219 L 21.68118286132813 33.925048828125 L 37.36236572265625 19.9625244140625 L 21.68118286132813 6 Z" fill="#fcfbfc" stroke="none" stroke-width="1" stroke-miterlimit="4" stroke-linecap="butt" /></svg>';
const String dumbbell =
    '<svg viewBox="105.7 62.6 148.0 151.2" ><path transform="translate(104.73, 61.61)" d="M 52.92843627929688 140.3509826660156 C 54.10280227661133 141.6522674560547 54.0328369140625 143.6774139404297 52.77159881591797 144.8908081054688 L 45.95822906494141 151.4024047851563 C 44.69138336181641 152.6053619384766 42.71061325073242 152.5321807861328 41.53205490112305 151.2389068603516 L 1.853297233581543 107.5332946777344 C 0.6797584295272827 106.2311325073242 0.7511200904846191 104.2059478759766 2.0132737159729 102.9934539794922 L 8.836055755615234 96.49147796630859 C 10.1022424697876 95.28672790527344 12.08445072174072 95.35995483398438 13.26223087310791 96.65498352050781 L 52.92843627929688 140.3509826660156 Z M 103.5581436157227 58.11441802978516 C 104.731689453125 59.41657638549805 104.6603317260742 61.44176483154297 103.3981552124023 62.65425872802734 L 60.51661682128906 103.5961990356445 C 59.24977874755859 104.7991409301758 57.26900863647461 104.7259674072266 56.09044647216797 103.4326858520508 L 45.85470962524414 92.14398956298828 C 44.68115997314453 90.84183502197266 44.75253295898438 88.816650390625 46.01469421386719 87.60414886474609 L 88.88682556152344 46.66221237182617 C 89.49431610107422 46.08388519287109 90.30185699462891 45.77615356445313 91.13150787353516 45.80682754516602 C 91.96115875244141 45.83749389648438 92.74484252929688 46.20405197143555 93.30986022949219 46.82572174072266 L 103.5581436157227 58.11441040039063 Z M 67.51820373535156 126.4140396118164 C 68.69140625 127.7061004638672 68.61925506591797 129.7483978271484 67.35822296142578 130.9538726806641 L 60.53544616699219 137.4622650146484 C 59.26925277709961 138.6670227050781 57.28704452514648 138.5937805175781 56.10926818847656 137.2987518310547 L 16.45247077941895 93.59315490722656 C 15.27809715270996 92.29185485839844 15.34806632995605 90.26671600341797 16.60931587219238 89.05331420898438 L 23.41954040527344 82.54812622070313 C 24.68638038635254 81.34517669677734 26.66715812683105 81.41834259033203 27.84571838378906 82.71163177490234 L 67.51820373535156 126.4140396118164 Z M 133.5908966064453 59.68219757080078 C 134.7636108398438 60.98521423339844 134.6908416748047 63.01043319702148 133.4277801513672 64.22203063964844 L 126.6081466674805 70.73042297363281 C 125.3410949707031 71.93457794189453 123.3593826293945 71.86284637451172 122.178840637207 70.57012939453125 L 82.51576232910156 26.8741512298584 C 81.33965301513672 25.57351112365723 81.40966796875 23.54692459106445 82.67259216308594 22.33430862426758 L 89.51105499267578 15.80988693237305 C 90.11854553222656 15.23155879974365 90.92609405517578 14.923828125 91.75575256347656 14.95449733734131 C 92.58540344238281 14.98516750335693 93.36907958984375 15.35172557830811 93.93411254882813 15.97339820861816 L 133.5908966064453 59.68219757080078 Z M 148.1587066650391 45.7677116394043 C 149.3308715820313 47.07159805297852 149.2595672607422 49.09637832641602 147.9987182617188 50.31076431274414 L 141.1916198730469 56.81594085693359 C 140.5845489501953 57.39468002319336 139.7772369384766 57.70297622680664 138.9475860595703 57.67290496826172 C 138.1179351806641 57.6428337097168 137.3340148925781 57.2768669128418 136.7685852050781 56.65563583374023 L 97.11178588867188 12.9404239654541 C 95.93878173828125 11.63739967346191 96.00868225097656 9.612658500671387 97.26863098144531 8.39737606048584 L 104.0820007324219 1.879366874694824 C 104.6881790161133 1.300024747848511 105.4951324462891 0.9912708401679993 106.3245315551758 1.02135181427002 C 107.1539154052734 1.051432132720947 107.9374313354492 1.417869567871094 108.5018997192383 2.039670705795288 L 148.1587066650391 45.7677116394043 Z" fill="#513369" fill-opacity="0.08" stroke="none" stroke-width="1" stroke-opacity="0.5" stroke-miterlimit="4" stroke-linecap="butt" /></svg>';
