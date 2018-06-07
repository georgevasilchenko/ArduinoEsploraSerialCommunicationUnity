using System;
using System.IO.Ports;
using System.Threading;

public class SerialController : ISerialController
{
   private static bool _isWorking = true;

   private readonly SerialPort _serialPort;

   private readonly Thread _serialPortReaderThread;

   public SerialController(string portName, int baudRate)
   {
      _serialPort = new SerialPort(portName, baudRate)
      {
         DtrEnable = true,
         RtsEnable = true,
         ReadTimeout = 1000,
         NewLine = "\n"
      };
      _serialPortReaderThread = new Thread(Receive);
   }

   public event OnPortOpenDelegate OnPortOpenEvent;

   public event OnPortCloseDelegate OnPortCloseEvent;

   public event OnPortErrorDelegate OnPortErrorEvent;

   public event OnDataReceivedDelegate OnDataReceivedEvent;

   public void Start()
   {
      try
      {
         _serialPort.Open();
      }
      catch (Exception exception)
      {
         if (OnPortErrorEvent != null)
         {
            OnPortErrorEvent.Invoke(exception);
         }
      }

      if (OnPortOpenEvent != null)
      {
         OnPortOpenEvent.Invoke();
      }

      _serialPortReaderThread.Start();
   }

   public void Abort()
   {
      _isWorking = false;

      if (OnPortCloseEvent != null)
      {
         OnPortCloseEvent.Invoke();
      }

      _serialPortReaderThread.Join();
   }

   private void Receive()
   {
      try
      {
         while (_isWorking)
         {
            if (_serialPort.IsOpen)
            {
               var data = _serialPort.ReadLine();

               if (OnDataReceivedEvent != null)
               {
                  OnDataReceivedEvent.Invoke(data);
               }
            }
            else
            {
               _isWorking = false;
            }
         }

         _serialPort.Close();
      }
      catch (Exception exception)
      {
         if (OnPortErrorEvent != null)
         {
            OnPortErrorEvent.Invoke(exception);
         }
      }
      finally
      {
         _serialPort.Close();
      }
   }
}