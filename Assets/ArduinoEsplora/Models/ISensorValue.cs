namespace ArduinoEsplora
{
   public interface ISensorValue<TValueType> where TValueType : struct
   {
      ArduinoSensorEnum Sensor { get; }

      TValueType Value { get; }

      void UpdateValue(TValueType value);
   }
}