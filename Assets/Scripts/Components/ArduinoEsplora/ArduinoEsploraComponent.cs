using UnityEngine;

public class ArduinoEsploraComponent : MonoBehaviour
{
   public int SliderValue
   {
      get { return _sliderValue; }
   }

   public int LightSensorValue
   {
      get { return _lightSensorValue; }
   }

   public int TemperatureValue
   {
      get { return _temperatureValue; }
   }

   public int MicrophoneValue
   {
      get { return _microphoneValue; }
   }

   public int JoystickSwitchValue
   {
      get { return _joystickSwitchValue; }
   }

   public int JoystickButtonValue
   {
      get { return _joystickButtonValue; }
   }

   public int AccelerometerXValue
   {
      get { return _accelerometerXValue; }
   }

   public int AccelerometerYValue
   {
      get { return _accelerometerYValue; }
   }

   public int AccelerometerZValue
   {
      get { return _accelerometerZValue; }
   }

   public string DebugText = "";
   public string ArduinoSerialPortName = "COM6";
   public int ArduinoSerialPortBaudRate = 9600;

   private IArduinoStreamDecoder _arduinoStreamDecoder;
   private ISerialController _serialController;

   [SerializeField] private int _sliderValue;
   [SerializeField] private int _lightSensorValue;
   [SerializeField] private int _temperatureValue;
   [SerializeField] private int _microphoneValue;
   [SerializeField] private int _joystickSwitchValue;
   [SerializeField] private int _joystickButtonValue;
   [SerializeField] private int _accelerometerXValue;
   [SerializeField] private int _accelerometerYValue;
   [SerializeField] private int _accelerometerZValue;

   private static void OnPortErrorEventHandler(System.Exception exception)
   {
      Debug.LogWarning(exception);
   }

   private static void OnPortCloseEventHandler()
   {
      Debug.Log("Arduino communication is closed");
   }

   private void OnPortOpenEventHandler()
   {
      Debug.LogFormat("Arduino communication is open, port: {0}", ArduinoSerialPortName);
   }

   private void Start()
   {
      _arduinoStreamDecoder = new ArduinoStreamDecoder();

      _serialController = new SerialController(ArduinoSerialPortName, ArduinoSerialPortBaudRate);
      _serialController.OnDataReceivedEvent += OnDataReceivedEventHandler;
      _serialController.OnPortOpenEvent += OnPortOpenEventHandler;
      _serialController.OnPortCloseEvent += OnPortCloseEventHandler;
      _serialController.OnPortErrorEvent += OnPortErrorEventHandler;
      _serialController.Start();
   }

   private void OnDataReceivedEventHandler(string data)
   {
      _arduinoStreamDecoder.DecodeArduinoStream(data);

      _sliderValue = _arduinoStreamDecoder.GetValueOfSensor(ArduinoSensorEnum.Slider);
      _lightSensorValue = _arduinoStreamDecoder.GetValueOfSensor(ArduinoSensorEnum.LightSensor);
      _temperatureValue = _arduinoStreamDecoder.GetValueOfSensor(ArduinoSensorEnum.Temperature);
      _microphoneValue = _arduinoStreamDecoder.GetValueOfSensor(ArduinoSensorEnum.Microphone);
      _joystickSwitchValue = _arduinoStreamDecoder.GetValueOfSensor(ArduinoSensorEnum.JoystickSwitch);
      _joystickButtonValue = _arduinoStreamDecoder.GetValueOfSensor(ArduinoSensorEnum.JoystickButton);
      _accelerometerXValue = _arduinoStreamDecoder.GetValueOfSensor(ArduinoSensorEnum.AccelerometerX);
      _accelerometerYValue = _arduinoStreamDecoder.GetValueOfSensor(ArduinoSensorEnum.AccelerometerY);
      _accelerometerZValue = _arduinoStreamDecoder.GetValueOfSensor(ArduinoSensorEnum.AccelerometerZ);

      DebugText = data;
   }

   private void OnApplicationQuit()
   {
      OnDestroy();
   }

   private void OnDestroy()
   {
      _serialController.Abort();
      Unsubscribe();
   }

   private void Unsubscribe()
   {
      _serialController.OnDataReceivedEvent -= OnDataReceivedEventHandler;
      _serialController.OnPortOpenEvent -= OnPortOpenEventHandler;
      _serialController.OnPortCloseEvent -= OnPortCloseEventHandler;
      _serialController.OnPortErrorEvent -= OnPortErrorEventHandler;
   }
}