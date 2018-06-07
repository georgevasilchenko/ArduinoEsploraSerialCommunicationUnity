using System;

namespace ArduinoEsplora
{
   public class ArduinoDecoderService : IArduinoDecoderService
   {
      private readonly IArduinoEsploraSensorValuesLayout _arduinoEsploraSensorValuesLayout;

      public ArduinoDecoderService()
      {
         _arduinoEsploraSensorValuesLayout = new ArduinoEsploraSensorValuesLayout();
      }

      public IArduinoEsploraSensorValuesLayout GetUpdatedSensorValues(string data)
      {
         if (string.IsNullOrEmpty(data))
         {
            throw new Exception("Incomming data is incomplete");
         }

         var dataParts = data.Split(';');

         _arduinoEsploraSensorValuesLayout.UpdateSensorValues(dataParts);

         return _arduinoEsploraSensorValuesLayout;
      }
   }
}