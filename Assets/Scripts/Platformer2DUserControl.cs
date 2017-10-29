using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets._2D
{
    [RequireComponent(typeof (PlatformerCharacter2D))]
    public class Platformer2DUserControl : MonoBehaviour
    {
        private PlatformerCharacter2D m_Character;
        private bool m_Jump;
		[SerializeField] private float _speed = 10f;     
		[SerializeField] private float h = 0;

        private void Awake()
        {
            m_Character = GetComponent<PlatformerCharacter2D>();   
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
            //bool crouch = Input.GetKey(KeyCode.LeftControl);

			h = (m_Character.m_FacingRight ? 1 : -1) * _speed * Time.deltaTime;

			if(transform.position.x >= 1.85f && m_Character.m_FacingRight && m_Character.isGrounded) {
				m_Character.Flip ();
			}

			if(transform.position.x <= -1.85 && !m_Character.m_FacingRight && m_Character.isGrounded) {
				m_Character.Flip ();
			}
            // Pass all parameters to the character control script.
            m_Character.Move(h, m_Jump);
            m_Jump = false;
        }
    }
}
