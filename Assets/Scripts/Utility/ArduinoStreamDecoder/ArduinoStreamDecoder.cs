using System;
using System.Collections.Generic;

public class ArduinoStreamDecoder : IArduinoStreamDecoder
{
   private const byte _sliderOrder = 0;
   private const byte _lightSensorOrder = 1;
   private const byte _temperatureOrder = 2;
   private const byte _microphoneOrder = 3;
   private const byte _joystickSwitchOrder = 4;
   private const byte _joystickButtonOrder = 5;
   private const byte _accelerometerXOrder = 6;
   private const byte _accelerometerYOrder = 7;
   private const byte _accelerometerZOrder = 8;

   private readonly Dictionary<ArduinoSensorEnum, int> _decodedStreamDictionary;

   public ArduinoStreamDecoder()
   {
      _decodedStreamDictionary = new Dictionary<ArduinoSensorEnum, int>
      {
         { ArduinoSensorEnum.Slider, 0},
         { ArduinoSensorEnum.LightSensor, 0},
         { ArduinoSensorEnum.Temperature, 0}
      };
   }

   public void DecodeArduinoStream(string data)
   {
      var dataParts = data.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
      Array.Resize(ref dataParts, dataParts.Length - 1);

      if (dataParts.Length < 3)
      {
         throw new Exception("Incomming data is incomplete");
      }

      _decodedStreamDictionary[ArduinoSensorEnum.Slider] = Convert.ToInt32(dataParts[_sliderOrder]);
      _decodedStreamDictionary[ArduinoSensorEnum.LightSensor] = Convert.ToInt32(dataParts[_lightSensorOrder]);
      _decodedStreamDictionary[ArduinoSensorEnum.Temperature] = Convert.ToInt32(dataParts[_temperatureOrder]);
      _decodedStreamDictionary[ArduinoSensorEnum.Microphone] = Convert.ToInt32(dataParts[_microphoneOrder]);
      _decodedStreamDictionary[ArduinoSensorEnum.JoystickSwitch] = Convert.ToInt32(dataParts[_joystickSwitchOrder]);
      _decodedStreamDictionary[ArduinoSensorEnum.JoystickButton] = Convert.ToInt32(dataParts[_joystickButtonOrder]);
      _decodedStreamDictionary[ArduinoSensorEnum.AccelerometerX] = Convert.ToInt32(dataParts[_accelerometerXOrder]);
      _decodedStreamDictionary[ArduinoSensorEnum.AccelerometerY] = Convert.ToInt32(dataParts[_accelerometerYOrder]);
      _decodedStreamDictionary[ArduinoSensorEnum.AccelerometerZ] = Convert.ToInt32(dataParts[_accelerometerZOrder]);
   }

   public int GetValueOfSensor(ArduinoSensorEnum arduinoSensor)
   {
      return _decodedStreamDictionary[arduinoSensor];
   }
}