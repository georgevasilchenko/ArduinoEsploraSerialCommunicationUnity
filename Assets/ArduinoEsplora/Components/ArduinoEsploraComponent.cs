using System;
using System.Linq;
using UnityEngine;

namespace ArduinoEsplora
{
   public class ArduinoEsploraComponent : MonoBehaviour
   {
      public IArduinoEsploraSensorValuesLayout SensorValues { get; private set; }

      [SerializeField] private string _arduinoSerialPortName = "COM6";
      [SerializeField] private int _arduinoSerialPortBaudRate = 9600;

      private IArduinoDecoderService _arduinoDecoderService;
      private ISerialControllerService _serialControllerService;

      #region Handlers

      private static void OnPortErrorEventHandler(Exception exception)
      {
         Debug.LogWarning(exception);
      }

      private static void OnPortCloseEventHandler()
      {
         Debug.Log("Arduino communication is closed");
      }

      private void OnPortOpenEventHandler()
      {
         Debug.LogFormat("Arduino communication is open, port: {0}", _arduinoSerialPortName);
      }

      private void OnDataReceivedEventHandler(string data)
      {
         SensorValues = _arduinoDecoderService.GetUpdatedSensorValues(data);
      }

      #endregion Handlers

      #region Unity API

      private void Awake()
      {
         var existingArduinoEsploraComponents = FindObjectsOfType<ArduinoEsploraComponent>();
         if (existingArduinoEsploraComponents.Length >= 1 && existingArduinoEsploraComponents.All(o => o != this))
         {
            throw new Exception("There must be only one Arduino Esplora Component in the scene.");
         }

         _arduinoDecoderService = new ArduinoDecoderService();
         _serialControllerService = new SerialControllerService(_arduinoSerialPortName, _arduinoSerialPortBaudRate);

         _serialControllerService.OnDataReceivedEvent += OnDataReceivedEventHandler;
         _serialControllerService.OnPortOpenEvent += OnPortOpenEventHandler;
         _serialControllerService.OnPortCloseEvent += OnPortCloseEventHandler;
         _serialControllerService.OnPortErrorEvent += OnPortErrorEventHandler;
      }

      private void Start()
      {
         _serialControllerService.Start();
      }

      private void OnApplicationQuit()
      {
         OnDestroy();
      }

      private void OnDestroy()
      {
         if (_serialControllerService != null)
         {
            _serialControllerService.Abort();
         }
         Unsubscribe();
      }

      #endregion Unity API

      private void Unsubscribe()
      {
         _serialControllerService.OnDataReceivedEvent -= OnDataReceivedEventHandler;
         _serialControllerService.OnPortOpenEvent -= OnPortOpenEventHandler;
         _serialControllerService.OnPortCloseEvent -= OnPortCloseEventHandler;
         _serialControllerService.OnPortErrorEvent -= OnPortErrorEventHandler;
      }
   }
}