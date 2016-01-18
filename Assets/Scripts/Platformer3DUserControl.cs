using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

[RequireComponent(typeof(PlatformCharacter3D))]
public class Platformer3DUserControl : MonoBehaviour
{
    private PlatformCharacter3D m_Character;
    private bool m_Jump;


    private void Awake()
    {
        m_Character = GetComponent<PlatformCharacter3D>();
    }


    private void Update()
    {
        if (!m_Jump)
        {
            // Read the jump input in Update so button presses aren't missed.
            m_Jump = CrossPlatformInputManager.GetButtonDown("Jump");
        }
    }


    private void FixedUpdate()
    {
        // Read the inputs.
        bool crouch = Input.GetKey(KeyCode.LeftControl);
        float h = CrossPlatformInputManager.GetAxis("Horizontal");
        // Pass all parameters to the character control script.
        m_Character.Move(h, crouch, m_Jump);
        m_Jump = false;

        bool layerToggle = Input.GetKeyDown(KeyCode.F);
        if (layerToggle)
        {
            if (transform.position.z <= 5)
            {
                transform.Translate(0, 0, 5);
            }
            else
            {
                transform.Translate(0, 0, -10);
            }
        }
    }
}
