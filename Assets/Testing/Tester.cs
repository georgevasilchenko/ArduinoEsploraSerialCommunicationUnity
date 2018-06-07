using ArduinoEsplora;
using UnityEngine;

public class Tester : MonoBehaviour
{
   public ArduinoEsploraComponent ArduinoEsplora;

   public int Slider;
   public int Light;
   public int Temp;
   public int Mic;
   public bool ButtonDown;

   private void Start()
   {
   }

   private void Update()
   {
      if (ArduinoEsplora.SensorValues != null)
      {
         Slider = ArduinoEsplora.SensorValues.Slider.Value;
         Light = ArduinoEsplora.SensorValues.LightSensor.Value;
         Temp = ArduinoEsplora.SensorValues.Temperature.Value;
         Mic = ArduinoEsplora.SensorValues.Microphone.Value;
         ButtonDown = ArduinoEsplora.SensorValues.ButtonDown.Value;
      }
   }
}