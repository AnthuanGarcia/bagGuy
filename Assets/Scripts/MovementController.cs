using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class MovementController : MonoBehaviour
{
    [SerializeField] private float m_JumpForce = 400f;							// Amount of force added when the player jumps.
	[SerializeField] private float m_DashForce = 300f;							// Amount of force added when the player jumps.
	[SerializeField] private float m_DashTime = 2f;
	[Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;	// How much to smooth out the movement
	[Range(0, 2f)][SerializeField] private float m_CoyoteTime = 0.25f;
	//[Range(0, 2f)][SerializeField]private float m_JumpTime = 0.5f; 
	[SerializeField] private bool m_AirControl = false;							// Whether or not a player can steer while jumping;
	[SerializeField] private LayerMask m_WhatIsGround;							// A mask determining what is ground to the character
	[SerializeField] private Transform m_GroundCheck;							// A position marking where to check if the player is grounded.
	[SerializeField] private Transform m_CeilingCheck;							// A position marking where to check for ceilings
	[SerializeField] private Collider2D m_CrouchDisableCollider;				// A collider that will be disabled when crouching

	const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
	private bool m_Grounded;            // Whether or not the player is grounded.
	const float k_CeilingRadius = .2f; // Radius of the overlap circle to determine if the player can stand up
	private Rigidbody2D m_Rigidbody2D;
	private bool m_FacingRight = true;  // For determining which way the player is currently facing.
	private Vector3 m_Velocity = Vector3.zero;

	[Header("Events")]
	[Space]

	public UnityEvent OnLandEvent;

	[System.Serializable]
	public class BoolEvent : UnityEvent<bool> { }
	public BoolEvent OnCrouchEvent;

	[System.Serializable]
	public class DashEvent : UnityEvent<Vector3> { }
	public DashEvent OnDashEvent;
    Animator animator;
	bool coyoteJump;

	/* Dash variables */
	float originalGravity;
	float timePressDash;
	float startTime;
	bool hasTimeDash = true;
	bool onePress = true;
	/* ------------------- */
	//bool hasTimeJump;
	bool hasDash = true;
	WaveExplosionPost effect;

	private void Awake()
	{
		m_Rigidbody2D = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();
		originalGravity = m_Rigidbody2D.gravityScale;

		if (OnLandEvent == null)
			OnLandEvent = new UnityEvent();

		if (OnCrouchEvent == null)
			OnCrouchEvent = new BoolEvent();
	}

	private void Start()
	{
		effect = WaveExplosionPost.Get();
	}

	private void FixedUpdate()
	{
		bool wasGrounded = m_Grounded;
		m_Grounded = false;

		// The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
		// This can be done using layers instead but Sample Assets will not overwrite your project settings.
		Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);

		if (colliders.Length > 0)
		{
			m_Grounded = true;

			if (!wasGrounded)
				OnLandEvent.Invoke();
			
		}
		else
		{
			if(wasGrounded)
			{
				animator.SetTrigger("lol");
				StartCoroutine(CoyoteJumpDelay());
			}

		}

	}

	public void Move(float move, bool jump, bool downDash, bool horizontalDash)
	{

		//only control the player if grounded or airControl is turned on
		if (m_Grounded || m_AirControl)
		{
			float force = m_Grounded ? 10f : 15f;

			// Move the character by finding the target velocity
			Vector3 targetVelocity = new Vector2(move * force, m_Rigidbody2D.velocity.y);
			// And then smoothing it out and applying it to the character
			m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);

			// If the input is moving the player right and the player is facing left...
			if (move > 0 && !m_FacingRight)
			{
				// ... flip the player.
				Flip();
			}
			// Otherwise if the input is moving the player left and the player is facing right...
			else if (move < 0 && m_FacingRight)
			{
				// ... flip the player.
				Flip();
			}

		}

		if(m_Grounded)
		{
			if(!onePress)
			{
				m_Rigidbody2D.gravityScale = originalGravity;
				m_Rigidbody2D.velocity = Vector2.zero;
				onePress = true;
			}

			if(jump)
			{
				m_Grounded = false;
				//StartCoroutine(MaxPressTimeJump());
				m_Rigidbody2D.AddForce(Vector2.up * m_JumpForce);
			}

			if(hasTimeDash)
				StopCoroutine(MaxPressTimeDash());
			else
				hasTimeDash = true;
			
			downDash = false;
			hasDash = true;
			//hasTimeJump = true;
		}

		if(!m_Grounded)
		{
			if(horizontalDash && hasDash)
			{
				//Vector2 pos = Camera.main.WorldToScreenPoint(transform.position);
				//if(OnDashEvent != null) OnDashEvent.Invoke(Camera.main.WorldToScreenPoint(transform.position));
				effect.StartIt(Camera.main.WorldToScreenPoint(transform.position));
				float sign = m_FacingRight ? 1 : -1;
				m_Rigidbody2D.velocity = Vector2.zero;
				m_Rigidbody2D.AddRelativeForce(new Vector2(650f * sign, 210f));
				hasDash = false;
			}

			if(downDash && hasTimeDash) 
			{
				if(onePress)
				{
					StartCoroutine(MaxPressTimeDash());
					startTime = Time.time;
					onePress = false;
				}

				m_Rigidbody2D.gravityScale = 0.4f;
				Vector3 targetVelocity = new Vector2(move * 2f, -2f);
				m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);

			}
			else if(m_Rigidbody2D.gravityScale != originalGravity)
			{
				timePressDash = Time.time - startTime;
				
				m_Rigidbody2D.gravityScale = originalGravity;
				m_Rigidbody2D.velocity = Vector2.down * m_DashForce * timePressDash;				

				onePress = true;
			}

			bool goingUp = m_Rigidbody2D.velocity.y > 0f;

			if(coyoteJump && !goingUp && jump)
			{
				//StartCoroutine(MaxPressTimeJump());
				m_Rigidbody2D.velocity = Vector2.zero;
				m_Rigidbody2D.AddForce(Vector2.up * m_JumpForce);
			}
		}

        animator.SetBool("Grounded", m_Grounded);

	}

	private void Flip()
	{
		// Switch the way the player is labelled as facing.
		m_FacingRight = !m_FacingRight;

		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	IEnumerator CoyoteJumpDelay()
	{
		coyoteJump = true;
		yield return new WaitForSeconds(m_CoyoteTime);
		coyoteJump = false;
	}

	IEnumerator MaxPressTimeDash()
	{
		hasTimeDash = true;
		yield return new WaitForSeconds(m_DashTime);
		hasTimeDash = false;		
	}

	/*IEnumerator MaxPressTimeJump()
	{
		hasTimeJump = true;
		yield return new WaitForSeconds(m_JumpTime);
		hasTimeJump = false;
	}*/
}
