# ArduinoEsploraSerialCommunicationUnity
Arduino Esplora board serial communication - reading Arduino data from Unity3D.

Short video preview:

<a href="http://www.youtube.com/watch?feature=player_embedded&v=GylLGJPjvw4" target="_blank">
  <img src="http://img.youtube.com/vi/GylLGJPjvw4/0.jpg" 
      alt="Arduino Esplora + Unity3D" 
      width="240" height="180" border="10" />
</a>

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

#### How to use it all

There is a class ArduinoEsploraComponent. It is responsible for the most work. Add it as a component to a game object, set the port and baud rate. Hit play and it should work. In order to access the actual values of the sensors, you need to reference the component in any other class like this:

```c#
      public class Tester : MonoBehaviour
      {
            public ArduinoEsploraComponent ArduinoEsplora;

            public int Slider;
            public int Light;
            public int Temp;
            public int Mic;
            public bool ButtonDown;

            private void Start()
            {
            }

            private void Update()
            {
                  if (ArduinoEsplora.SensorValues != null)
                  {
                     Slider = ArduinoEsplora.SensorValues.Slider.Value;
                     Light = ArduinoEsplora.SensorValues.LightSensor.Value;
                     Temp = ArduinoEsplora.SensorValues.Temperature.Value;
                     Mic = ArduinoEsplora.SensorValues.Microphone.Value;
                     ButtonDown = ArduinoEsplora.SensorValues.ButtonDown.Value;
                  }
            }
      }
```
As an example of reading values and applying them, I have mapped the inputs of ThirdPersonUserControl to the Arduino Esplora sensor values. This (CustomCharacterControllerComponent.cs) is an edited version of ThirdPersonUserControl.cs from Unity Characters package:

```c#
      private void Update()
      {
            if (ArduinoEsplora.SensorValues != null)
            {
               if (!m_Jump)
               {
                  m_Jump = ArduinoEsplora.SensorValues.ButtonUp.Value;
               }
            }
      }
      
      private void FixedUpdate()
      {
            var lag = 10;
            var h = 0.0f;
            var v = 0.0f;
            var crouch = false;

            if (ArduinoEsplora.SensorValues != null)
            {
            
            // remove all values in range of [-10 < val < 10]
            float horizontalInputRaw = (ArduinoEsplora.SensorValues.JoystickX.Value <= lag && ArduinoEsplora.SensorValues.JoystickX.Value > 0)
                                  || (ArduinoEsplora.SensorValues.JoystickX.Value >= -lag && ArduinoEsplora.SensorValues.JoystickX.Value < 0)
                  ? 0
                  : ArduinoEsplora.SensorValues.JoystickX.Value;
            float vetrticalInputRaw = (ArduinoEsplora.SensorValues.JoystickY.Value <= lag && ArduinoEsplora.SensorValues.JoystickY.Value > 0)
                                 || (ArduinoEsplora.SensorValues.JoystickY.Value >= -lag && ArduinoEsplora.SensorValues.JoystickY.Value < 0)
                  ? 0
                  : ArduinoEsplora.SensorValues.JoystickY.Value;

            // remap values to range [-1.0 <= val <= 1.0]
            horizontalInputRaw = (horizontalInputRaw - -511) * (1.0f - -1.0f) / (511 - -511) + -1.0f;
            vetrticalInputRaw = (vetrticalInputRaw - -511) * (1.0f - -1.0f) / (511 - -511) + -1.0f;

            horizontalInputRaw *= -1;
            vetrticalInputRaw *= -1;

            h = Mathf.Clamp(horizontalInputRaw, -1.0f, 1.0f);
            v = Mathf.Clamp(vetrticalInputRaw, -1.0f, 1.0f);

            crouch = ArduinoEsplora.SensorValues.ButtonDown.Value;
      }
```

Have fun!
