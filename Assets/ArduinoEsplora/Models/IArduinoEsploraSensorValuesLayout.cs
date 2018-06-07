namespace ArduinoEsplora
{
   public interface IArduinoEsploraSensorValuesLayout
   {
      ISensorValue<short> Slider { get; }

      ISensorValue<short> LightSensor { get; }

      ISensorValue<short> Temperature { get; }

      ISensorValue<short> Microphone { get; }

      ISensorValue<short> JoystickSwitch { get; }

      ISensorValue<bool> JoystickButton { get; }

      ISensorValue<short> AccelerometerX { get; }

      ISensorValue<short> AccelerometerY { get; }

      ISensorValue<short> AccelerometerZ { get; }

      ISensorValue<bool> ButtonDown { get; }

      ISensorValue<bool> ButtonLeft { get; }

      ISensorValue<bool> ButtonUp { get; }

      ISensorValue<bool> ButtonRight { get; }

      ISensorValue<short> JoystickX { get; }

      ISensorValue<short> JoystickY { get; }

      ISensorValue<byte> LedRed { get; }

      ISensorValue<byte> LedGreen { get; }

      ISensorValue<byte> LedBlue { get; }

      void UpdateSensorValues(string[] rawValues);
   }
}