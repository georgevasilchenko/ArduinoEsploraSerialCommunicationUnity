public interface IArduinoStreamDecoder
{
   void DecodeArduinoStream(string data);

   int GetValueOfSensor(ArduinoSensorEnum arduinoSensor);
}