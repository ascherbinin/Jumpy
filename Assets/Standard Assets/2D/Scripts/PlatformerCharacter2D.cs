using System;
using UnityEngine;

namespace UnityStandardAssets._2D
{
    public class PlatformerCharacter2D : MonoBehaviour
    {
        [SerializeField] private float m_MaxSpeed = 10f;                    // The fastest the player can travel in the x axis.
        [SerializeField] private float m_JumpForce = 400f;                  // Amount of force added when the player jumps.
        //[Range(0, 1)] [SerializeField] private float m_CrouchSpeed = .36f;  // Amount of maxSpeed applied to crouching movement. 1 = 100%
        //[SerializeField] private bool m_AirControl = true;                 
        [SerializeField] private LayerMask m_WhatIsGround;    
		[SerializeField] private LayerMask m_WhatIsWall;  

		private Transform m_GroundCheck;    
        const float k_GroundedRadius = .2f; 
		const float k_WallRadius = .1f; 
		private bool m_Grounded;     
		[SerializeField] private bool m_Walled;  
		[SerializeField] private Transform m_WallCheck;   
        const float k_CeilingRadius = .01f; // Radius of the overlap circle to determine if the player can stand up
        //private Animator m_Anim;            // Reference to the player's animator component.
		[SerializeField]  private Rigidbody2D m_Rigidbody2D;
        public bool m_FacingRight = true;  // For determining which way the player is currently facing.

		[SerializeField] private float _startSpriteScaleX;
		private Transform _spriteTransform;
		//double jump
		bool m_doubleJump = false;


        private void Awake()
        {
            // Setting up references.
            m_GroundCheck = transform.Find("GroundCheck");
			m_WallCheck = transform.Find("WallCheck");
            //m_Anim = GetComponent<Animator>();
            m_Rigidbody2D = GetComponent<Rigidbody2D>();
			_spriteTransform = transform.Find("PlayerSprite");
			_startSpriteScaleX = _spriteTransform.localScale.x;
        }


        private void FixedUpdate()
        {
            m_Grounded = false;
			m_Walled = false;
            // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
            // This can be done using layers instead but Sample Assets will not overwrite your project settings.
            Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
			foreach (var item in colliders) {
				if (item.gameObject != gameObject) {
					m_Grounded = true;
				}
			}

			m_Walled = Physics2D.OverlapCircle(m_WallCheck.position, k_WallRadius, m_WhatIsWall);

			if (m_Walled && !m_Grounded) 
			{
				FlipSprite (!m_FacingRight);
				//m_Grounded = false; 
				m_doubleJump = false; 
			}
            //m_Anim.SetBool("Ground", m_Grounded);

            // Set the vertical animation
            //m_Anim.SetFloat("vSpeed", m_Rigidbody2D.velocity.y);
        }


		public void Move(float move, bool jump)
        {
//            // If crouching, check to see if the character can stand up
//            if (!crouch && m_Anim.GetBool("Crouch"))
//            {
//                // If the character has a ceiling preventing them from standing up, keep them crouching
//                if (Physics2D.OverlapCircle(m_CeilingCheck.position, k_CeilingRadius, m_WhatIsGround))
//                {
//                    crouch = true;
//                }
//            }

            // Set whether or not the character is crouching in the animator
           // m_Anim.SetBool("Crouch", crouch);

            //only control the player if grounded or airControl is turned on
			if (m_Grounded)
            {
//
//                // The Speed animator parameter is set to the absolute value of the horizontal input.
//                //m_Anim.SetFloat("Speed", Mathf.Abs(move));
//
//                // Move the character
                m_Rigidbody2D.velocity = new Vector2(move, m_Rigidbody2D.velocity.y);
				Debug.Log (move);
////                // If the input is moving the player right and the player is facing left...
////                if (move > 0 && !m_FacingRight)
////                {
////                    // ... flip the player.
////                    Flip();
////                }
////                    // Otherwise if the input is moving the player left and the player is facing right...
////                else if (move < 0 && m_FacingRight)
////                {
////                    // ... flip the player.
////                    Flip();
////                }
            }
            // If the player should jump...
			if (m_Grounded && jump)
            {
                // Add a vertical force to the player.

                //m_Anim.SetBool("Ground", false);
				m_Rigidbody2D.AddForce(new Vector2((m_FacingRight ? 1 : -1) * m_JumpForce, m_JumpForce));
            }

			if (m_Walled && jump) {
				Flip ();
				m_Rigidbody2D.AddForce(new Vector2((m_FacingRight ? 1 : -1) * m_JumpForce, m_JumpForce));
			}
        }


        public void Flip()
        {
            // Switch the way the player is labelled as facing.
            m_FacingRight = !m_FacingRight;

            // Multiply the player's x local scale by -1.
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }

		private void FlipSprite(bool toRight)
		{
			var xScale = toRight ? _startSpriteScaleX : -_startSpriteScaleX;
			_spriteTransform.localScale = new Vector3 (xScale, _spriteTransform.localScale.y, _spriteTransform.localScale.z);
		}
    }
}