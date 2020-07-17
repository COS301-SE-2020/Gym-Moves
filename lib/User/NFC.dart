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
    StreamSubscription<NDEFMessage> _stream;

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
                            print("read NDEF message: ${message.payload}");
                        }, onError: (e) {
                            // Check error handling guide below
                        });
                    });
                }
            }
        );
    }
}