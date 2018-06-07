namespace ArduinoEsplora
{
   public interface IArduinoDecoderService
   {
      IArduinoEsploraSensorValuesLayout GetUpdatedSensorValues(string data);
   }
}