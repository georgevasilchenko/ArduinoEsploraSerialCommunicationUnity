namespace ArduinoEsplora
{
   public class SensorValue<TValueType> : ISensorValue<TValueType> where TValueType : struct
   {
      public ArduinoSensorEnum Sensor { get; private set; }

      public TValueType Value { get; private set; }

      public SensorValue(ArduinoSensorEnum sensor)
      {
         Sensor = sensor;
         Value = default(TValueType);
      }

      public SensorValue(ArduinoSensorEnum sensor, TValueType value)
      {
         Sensor = sensor;
         Value = value;
      }

      public void UpdateValue(TValueType value)
      {
         Value = value;
      }
   }
}