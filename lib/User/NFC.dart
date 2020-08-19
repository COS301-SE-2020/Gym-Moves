import 'dart:async';

import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';
import 'package:nfc_in_flutter/nfc_in_flutter.dart';

class NFCReader extends StatefulWidget {
    @override
    _NFCReaderState createState() => _NFCReaderState();
}

class _NFCReaderState extends State {
    bool _supportsNFC = false;
    bool _reading = false;
    //we'll have to get original counter from the database, so we make an api request here to get number of people at the gym
    int counter =0;
    StreamSubscription<NDEFMessage> _stream;

    //api request

    @override
    void initState() {
        super.initState();
        // Check if the device supports NFC reading
        NFC.isNDEFSupported
            .then((bool isSupported) {
                setState(() {
                    _supportsNFC = isSupported;
                });
            });
    }

    @override
    Widget build(BuildContext context) {
        if (!_supportsNFC) {
            return RaisedButton(
                child: const Text("You device does not support NFC"),
                onPressed: null,
            );
        }
// i think we can scan again and see if theyve alreadsy scanned in,
// by having a temp variable and set it to 1 when they scan, and if they scan again then it'll mean they exiting the gym

        return RaisedButton(
            child: Text(_reading ? "Stop reading" : "Start reading"),
            onPressed: () {
                if (_reading) {
                    _stream?.cancel();
                    setState(() {
                        _reading = false;
                    });
                } else {
                    setState(() {
                        _reading = true;
                        // Start reading using NFC.readNDEF()
                        _stream = NFC.readNDEF(
                            once: true,
                            throwOnUserCancel: false,
                        ).listen((NDEFMessage message) {
                            counter+=1;
                            //we can send a request to the api here and then increase the counter, then display the message
                            // on the welcome page we can just make an api request and get this count variable
                            print("read NDEF message: ${message.payload}");
                            print("Number of gym members: ${counter}");
                        }, onError: (e) {
                            // Check error handling guide below
                        });
                    });
                }
            }
        );
    }
}