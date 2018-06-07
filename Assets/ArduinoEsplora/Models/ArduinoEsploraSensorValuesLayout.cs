using System;

namespace ArduinoEsplora
{
   public class ArduinoEsploraSensorValuesLayout : IArduinoEsploraSensorValuesLayout
   {
      public SensorValue<short> Slider { get; private set; }

      public SensorValue<short> LightSensor { get; private set; }

      public SensorValue<short> Temperature { get; private set; }

      public SensorValue<short> Microphone { get; private set; }

      public SensorValue<short> JoystickSwitch { get; private set; }

      public SensorValue<bool> JoystickButton { get; private set; }

      public SensorValue<short> AccelerometerX { get; private set; }

      public SensorValue<short> AccelerometerY { get; private set; }

      public SensorValue<short> AccelerometerZ { get; private set; }

      public SensorValue<bool> ButtonDown { get; private set; }

      public SensorValue<bool> ButtonLeft { get; private set; }

      public SensorValue<bool> ButtonUp { get; private set; }

      public SensorValue<bool> ButtonRight { get; private set; }

      public SensorValue<short> JoystickX { get; private set; }

      public SensorValue<short> JoystickY { get; private set; }

      public SensorValue<byte> LedRed { get; private set; }

      public SensorValue<byte> LedGreen { get; private set; }

      public SensorValue<byte> LedBlue { get; private set; }

      public ArduinoEsploraSensorValuesLayout()
      {
         Slider = new SensorValue<short>(ArduinoSensorEnum.Slider);
         LightSensor = new SensorValue<short>(ArduinoSensorEnum.LightSensor);
         Temperature = new SensorValue<short>(ArduinoSensorEnum.Temperature);
         Microphone = new SensorValue<short>(ArduinoSensorEnum.Microphone);

         JoystickSwitch = new SensorValue<short>(ArduinoSensorEnum.JoystickSwitch);
         JoystickButton = new SensorValue<bool>(ArduinoSensorEnum.JoystickButton);

         AccelerometerX = new SensorValue<short>(ArduinoSensorEnum.AccelerometerX);
         AccelerometerY = new SensorValue<short>(ArduinoSensorEnum.AccelerometerY);
         AccelerometerZ = new SensorValue<short>(ArduinoSensorEnum.AccelerometerZ);

         ButtonDown = new SensorValue<bool>(ArduinoSensorEnum.ButtonDown);
         ButtonLeft = new SensorValue<bool>(ArduinoSensorEnum.ButtonLeft);
         ButtonUp = new SensorValue<bool>(ArduinoSensorEnum.ButtonUp);
         ButtonRight = new SensorValue<bool>(ArduinoSensorEnum.ButtonRight);

         JoystickX = new SensorValue<short>(ArduinoSensorEnum.JoystickX);
         JoystickY = new SensorValue<short>(ArduinoSensorEnum.JoystickY);

         LedRed = new SensorValue<byte>(ArduinoSensorEnum.LedRed);
         LedGreen = new SensorValue<byte>(ArduinoSensorEnum.LedGreen);
         LedBlue = new SensorValue<byte>(ArduinoSensorEnum.LedBlue);
      }

      public void UpdateSensorValues(string[] rawValues)
      {
         if (rawValues == null || rawValues.Length == 0)
         {
            throw new Exception("Incomming data is incomplete");
         }

         Slider.UpdateValue(Convert.ToInt16(rawValues[SensorValuesOrderTable.SliderOrder]));
         LightSensor.UpdateValue(Convert.ToInt16(rawValues[SensorValuesOrderTable.LightSensorOrder]));
         Temperature.UpdateValue(Convert.ToInt16(rawValues[SensorValuesOrderTable.TemperatureOrder]));
         Microphone.UpdateValue(Convert.ToInt16(rawValues[SensorValuesOrderTable.MicrophoneOrder]));

         JoystickSwitch.UpdateValue(Convert.ToInt16(rawValues[SensorValuesOrderTable.JoystickSwitchOrder]));
         JoystickButton.UpdateValue(rawValues[SensorValuesOrderTable.JoystickButtonOrder] != "1");

         AccelerometerX.UpdateValue(Convert.ToInt16(rawValues[SensorValuesOrderTable.AccelerometerXOrder]));
         AccelerometerY.UpdateValue(Convert.ToInt16(rawValues[SensorValuesOrderTable.AccelerometerYOrder]));
         AccelerometerZ.UpdateValue(Convert.ToInt16(rawValues[SensorValuesOrderTable.AccelerometerZOrder]));

         ButtonDown.UpdateValue(rawValues[SensorValuesOrderTable.ButtonDownOrder] != "1");
         ButtonLeft.UpdateValue(rawValues[SensorValuesOrderTable.ButtonLeftOrder] != "1");
         ButtonUp.UpdateValue(rawValues[SensorValuesOrderTable.ButtonUpOrder] != "1");
         ButtonRight.UpdateValue(rawValues[SensorValuesOrderTable.ButtonRightOrder] != "1");

         JoystickX.UpdateValue(Convert.ToInt16(rawValues[SensorValuesOrderTable.JoystickXOrder]));
         JoystickY.UpdateValue(Convert.ToInt16(rawValues[SensorValuesOrderTable.JoystickYOrder]));

         LedRed.UpdateValue(Convert.ToByte(rawValues[SensorValuesOrderTable.LedRedOrder]));
         LedGreen.UpdateValue(Convert.ToByte(rawValues[SensorValuesOrderTable.LedGreenOrder]));
         LedBlue.UpdateValue(Convert.ToByte(rawValues[SensorValuesOrderTable.LedBlueOrder]));
      }

      #region interface explicit implementation

      ISensorValue<short> IArduinoEsploraSensorValuesLayout.Slider
      {
         get { return Slider; }
      }

      ISensorValue<short> IArduinoEsploraSensorValuesLayout.LightSensor
      {
         get { return LightSensor; }
      }

      ISensorValue<short> IArduinoEsploraSensorValuesLayout.Temperature
      {
         get { return Temperature; }
      }

      ISensorValue<short> IArduinoEsploraSensorValuesLayout.Microphone
      {
         get { return Microphone; }
      }

      ISensorValue<short> IArduinoEsploraSensorValuesLayout.JoystickSwitch
      {
         get { return JoystickSwitch; }
      }

      ISensorValue<bool> IArduinoEsploraSensorValuesLayout.JoystickButton
      {
         get { return JoystickButton; }
      }

      ISensorValue<short> IArduinoEsploraSensorValuesLayout.AccelerometerX
      {
         get { return AccelerometerX; }
      }

      ISensorValue<short> IArduinoEsploraSensorValuesLayout.AccelerometerY
      {
         get { return AccelerometerY; }
      }

      ISensorValue<short> IArduinoEsploraSensorValuesLayout.AccelerometerZ
      {
         get { return AccelerometerZ; }
      }

      ISensorValue<bool> IArduinoEsploraSensorValuesLayout.ButtonDown
      {
         get { return ButtonDown; }
      }

      ISensorValue<bool> IArduinoEsploraSensorValuesLayout.ButtonLeft
      {
         get { return ButtonLeft; }
      }

      ISensorValue<bool> IArduinoEsploraSensorValuesLayout.ButtonUp
      {
         get { return ButtonUp; }
      }

      ISensorValue<bool> IArduinoEsploraSensorValuesLayout.ButtonRight
      {
         get { return ButtonRight; }
      }

      ISensorValue<short> IArduinoEsploraSensorValuesLayout.JoystickX
      {
         get { return JoystickX; }
      }

      ISensorValue<short> IArduinoEsploraSensorValuesLayout.JoystickY
      {
         get { return JoystickY; }
      }

      ISensorValue<byte> IArduinoEsploraSensorValuesLayout.LedRed
      {
         get { return LedRed; }
      }

      ISensorValue<byte> IArduinoEsploraSensorValuesLayout.LedGreen
      {
         get { return LedGreen; }
      }

      ISensorValue<byte> IArduinoEsploraSensorValuesLayout.LedBlue
      {
         get { return LedBlue; }
      }

      #endregion interface explicit implementation
   }
}