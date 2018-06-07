namespace ArduinoEsplora
{
   public interface ISerialControllerService
   {
      event OnPortOpenDelegate OnPortOpenEvent;

      event OnPortCloseDelegate OnPortCloseEvent;

      event OnPortErrorDelegate OnPortErrorEvent;

      event OnDataReceivedDelegate OnDataReceivedEvent;

      void Start();

      void Abort();
   }
}