using System;
using UnityEngine;

public class PlatformerCharacter2D : MonoBehaviour
    {
       [SerializeField] private float m_JumpForce = 400f;                  // Amount of force added when the player jumps.
        //[Range(0, 1)] [SerializeField] private float m_CrouchSpeed = .36f;  // Amount of maxSpeed applied to crouching movement. 1 = 100%
        //[SerializeField] private bool m_AirControl = true;                 
        [SerializeField] private LayerMask m_WhatIsGround;    
		[SerializeField] private LayerMask m_WhatIsWall;  

		private Transform m_GroundCheck;    
        const float k_GroundedRadius = .2f; 
		const float k_WallRadius = .1f; 
		[SerializeField] private bool m_Grounded;  
		public bool isGrounded { get { return m_Grounded; } }

		public bool isAlive = true;
		[SerializeField] private bool m_Walled;  
		[SerializeField] private bool m_WalledLeft;  
		[SerializeField] private bool m_WalledRight;  
		[SerializeField] private Transform m_WallCheckR;   
		[SerializeField] private Transform m_WallCheckL;   
        //private Animator m_Anim;            // Reference to the player's animator component.
		[SerializeField]  private Rigidbody2D m_Rigidbody2D;
        public bool m_FacingRight = true;  // For determining which way the player is currently facing.
		private Transform _spriteTransform;

		[SerializeField] private bool _doubleJump = false;
		[SerializeField] private bool _inAir = false;
		[SerializeField] private Transform _playerSprite;  


        private void Awake()
        {
            // Setting up references.
            m_GroundCheck = transform.Find("GroundCheck");
			m_WallCheckR = transform.Find("WallCheckRightSide");
			m_WallCheckL = transform.Find("WallCheckLeftSide");
			_playerSprite = transform.Find("PlayerSprite");
            //m_Anim = GetComponent<Animator>();
            m_Rigidbody2D = GetComponent<Rigidbody2D>();
        }


        private void FixedUpdate()
        {
            m_Grounded = false;
			m_Walled = false;
            // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
            // This can be done using layers instead but Sample Assets will not overwrite your project settings.
			m_Grounded = Physics2D.OverlapCircle(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);

			m_WalledLeft =  Physics2D.OverlapCircle(m_WallCheckL.position, k_WallRadius, m_WhatIsWall)  && !m_Grounded;
			m_WalledRight = Physics2D.OverlapCircle(m_WallCheckR.position, k_WallRadius, m_WhatIsWall) && !m_Grounded;

			if (m_WalledLeft || m_WalledRight) {
				m_Rigidbody2D.gravityScale = 0.5f;
				m_Walled = true;
				if (_inAir)
					Flip ();
			}
//
			if (m_Walled || m_Grounded) {
				_inAir = false;
				_doubleJump = false;
			} else {
				m_Rigidbody2D.gravityScale = 1.3f;
				_inAir = true;
			}

		
        }

		public void Move(float move, bool jump)
        {
			if (m_Grounded)
            {
                m_Rigidbody2D.velocity = new Vector2(move, m_Rigidbody2D.velocity.y);
            }

			if ((m_Grounded || m_Walled) && jump)
			{
				m_Rigidbody2D.AddForce(new Vector2((m_WalledLeft ? 0.3f : -0.3f) * m_JumpForce * 3, m_JumpForce * 2));
			}

			if (_inAir && jump && !_doubleJump) {
				_doubleJump = true;
				m_Rigidbody2D.AddForce(new Vector2((m_FacingRight ?  0.3f : -0.3f) * m_JumpForce * 1.5f, m_JumpForce * 1.3f));
			}
        }


        public void Flip()
        {
            m_FacingRight = !m_FacingRight;

			Vector3 theScale = _playerSprite.localScale;
            theScale.x *= -1;
			_playerSprite.localScale = theScale;
        }

    }
