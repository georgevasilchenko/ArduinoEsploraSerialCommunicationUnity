# ArduinoEsploraSerialCommunicationUnity
Arduino Esplora board serial communication - reading Arduino data from Unity3D

This is the implementation of the Unity side in Arduino Esplora serial communication.
Here we read the data sent via serial connection from Arduino.

The most important point here and probably is the reason that many people failed trying to set the serial communication is explicitly enabling two extra pins.

#### DTR and RTS

Since Arduino Esplora is derived from Leonardo board, DTR and RTS are not enabled by default.
After compiling and uploading the sketch to the board it is crutial to enable those on a "listener". 
For example in C# Console Application:

```c#
      _serialPort = new SerialPort(portName, baudRate)
      {
        ...
        DtrEnable = true,
        RtsEnable = true
      };
```

#### NewLine feed

It is also a good idea to explicitly set the new line feed property to '\n' on the "listener" side. 
I use Serial.println() method to write to the serial port and it appends this character by default. 
Sometimes Unity won't automatically detect the line feed character and that's why it has to be set explicitly.

```c#
      _serialPort = new SerialPort(portName, baudRate)
      {
        ...
        NewLine = "\n"
      };
```

#### Separate thread

Unity is very picky about the UI thread and won't allow any potentially blocking operations to run on UI and will simply crash.
I use a separate thread for reading the serial port. 
In order to communicate the received data I use delegates.
Event handlers update the UI properties.

#### Unity Player Settings

Api Compatibility Level has to be set to .NET 2.0 in order to make System.IO.Ports available.
