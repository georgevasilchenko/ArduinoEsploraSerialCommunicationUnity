public interface ISerialController
{
   event OnPortOpenDelegate OnPortOpenEvent;

   event OnPortCloseDelegate OnPortCloseEvent;

   event OnPortErrorDelegate OnPortErrorEvent;

   event OnDataReceivedDelegate OnDataReceivedEvent;

   void Start();

   void Abort();
}