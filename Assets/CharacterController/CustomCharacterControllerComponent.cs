using ArduinoEsplora;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

[RequireComponent(typeof(ThirdPersonCharacter))]
public class CustomCharacterControllerComponent : MonoBehaviour
{
   public ArduinoEsploraComponent ArduinoEsplora;

   private ThirdPersonCharacter m_Character; // A reference to the ThirdPersonCharacter on the object
   private Transform m_Cam;                  // A reference to the main camera in the scenes transform
   private Vector3 m_CamForward;             // The current forward direction of the camera
   private Vector3 m_Move;
   private bool m_Jump;                      // the world-relative desired move direction, calculated from the camForward and user input.

   private void Start()
   {
      // get the transform of the main camera
      if (Camera.main != null)
      {
         m_Cam = Camera.main.transform;
      }
      else
      {
         Debug.LogWarning(
             "Warning: no main camera found. Third person character needs a Camera tagged \"MainCamera\", for camera-relative controls.", gameObject);

         // we use self-relative controls in this case, which probably isn't what the user wants, but
         // hey, we warned them!
      }

      // get the third person character ( this should never be null due to require component )
      m_Character = GetComponent<ThirdPersonCharacter>();
   }

   private void Update()
   {
      if (ArduinoEsplora.SensorValues != null)
      {
         if (!m_Jump)
         {
            m_Jump = ArduinoEsplora.SensorValues.ButtonUp.Value;
         }
      }

      //if (!m_Jump)
      //{
      //   m_Jump = CrossPlatformInputManager.GetButtonDown("Jump");
      //}
   }

   // Fixed update is called in sync with physics
   private void FixedUpdate()
   {
      var lag = 10;
      var h = 0.0f;
      var v = 0.0f;
      var crouch = false;

      if (ArduinoEsplora.SensorValues != null)
      {
         // remove all values in range of [-10 < val < 10]

         float horizontalInputRaw = (ArduinoEsplora.SensorValues.JoystickX.Value <= lag && ArduinoEsplora.SensorValues.JoystickX.Value > 0)
                                  || (ArduinoEsplora.SensorValues.JoystickX.Value >= -lag && ArduinoEsplora.SensorValues.JoystickX.Value < 0)
            ? 0
            : ArduinoEsplora.SensorValues.JoystickX.Value;
         float vetrticalInputRaw = (ArduinoEsplora.SensorValues.JoystickY.Value <= lag && ArduinoEsplora.SensorValues.JoystickY.Value > 0)
                                 || (ArduinoEsplora.SensorValues.JoystickY.Value >= -lag && ArduinoEsplora.SensorValues.JoystickY.Value < 0)
            ? 0
            : ArduinoEsplora.SensorValues.JoystickY.Value;

         // remap values to range [-1.0 <= val <= 1.0]

         horizontalInputRaw = (horizontalInputRaw - -511) * (1.0f - -1.0f) / (511 - -511) + -1.0f;
         vetrticalInputRaw = (vetrticalInputRaw - -511) * (1.0f - -1.0f) / (511 - -511) + -1.0f;

         horizontalInputRaw *= -1;
         vetrticalInputRaw *= -1;

         h = Mathf.Clamp(horizontalInputRaw, -1.0f, 1.0f);
         v = Mathf.Clamp(vetrticalInputRaw, -1.0f, 1.0f);

         crouch = ArduinoEsplora.SensorValues.ButtonDown.Value;
      }

      // read inputs
      //float h = CrossPlatformInputManager.GetAxis("Horizontal");
      //float v = CrossPlatformInputManager.GetAxis("Vertical");
      //bool crouch = Input.GetKey(KeyCode.C);

      // calculate move direction to pass to character
      if (m_Cam != null)
      {
         // calculate camera relative direction to move:
         m_CamForward = Vector3.Scale(m_Cam.forward, new Vector3(1, 0, 1)).normalized;
         m_Move = v * m_CamForward + h * m_Cam.right;
      }
      else
      {
         // we use world-relative directions in the case of no main camera
         m_Move = v * Vector3.forward + h * Vector3.right;
      }
#if !MOBILE_INPUT

      // walk speed multiplier
      if (Input.GetKey(KeyCode.LeftShift)) m_Move *= 0.5f;
#endif

      // pass all parameters to the character control script
      m_Character.Move(m_Move, crouch, m_Jump);
      m_Jump = false;
   }
}