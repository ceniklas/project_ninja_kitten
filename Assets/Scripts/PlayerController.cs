using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PlayerPhysics))]
public class PlayerController : MonoBehaviour {

	#region Variables
	//Debugging Variables
	private const bool enableManualInput = false;

	//Player variables
	public const float gravityPull = 30;
	public const float walkSpeed = 4;
	public const float runSpeed = 12;
	public const float acceleration = 30;
	public const float jumpHeight = 12;
	public const float slideDeceleration = 3;

	//Player handling
	public Vector3 gravity;
	private float currentSpeed;
	private float targetSpeed;
	private Vector3 movement;
	private float speed;
	private int runningDirection;

	//Player States
	private bool jumping;
	private bool sliding;
	private bool running;
	private bool autoStarted;

	//Player components
	private PlayerPhysics playerPhysics;
	private Animator animator;
	#endregion

	// Use this for initialization
	void Start () {
		playerPhysics = GetComponent<PlayerPhysics>();
		animator = GetComponent<Animator> ();
		transform.eulerAngles = Vector3.up * 90;
		running = false;
		setGravityDown(gravityPull);
	}
	
	// Update is called once per frame
	void Update () {
		runningDirection = 0;

		#region Autorun
		if (Time.realtimeSinceStartup >= 3 && !autoStarted) {
				runningDirection = 1;

				if (Time.realtimeSinceStartup >= 5) {
						running = true;
						autoStarted = true;
				}
		}
		#endregion

		if (!sliding) {
			//Increase speed if we're running
			if (running) {
				speed = runSpeed;
			} 
			else {
				speed = walkSpeed;
			}

			#region Manual Input
			if (enableManualInput) {
				//KEYBOARD VERTICAL CONTROLLER
				runningDirection = (int)Input.GetAxisRaw("Vertical");
				
				//PHONE VERTICAL CONTROLLER
				if (Input.touchCount == 1) {
					runningDirection = 1;
				}
				if (Input.touchCount == 2) {
					runningDirection = -1;
				}
			}
			#endregion

			//SET SPEED
			if (runningDirection == 1 || runningDirection == -1) {
				targetSpeed = runningDirection * speed;
			}

			currentSpeed = IncrementToward (currentSpeed, targetSpeed, acceleration);

			//Face direction
			if(runningDirection != 0){
				transform.eulerAngles = Vector3.up * 90 * runningDirection;
			}
		}
		else{
			currentSpeed = IncrementToward(currentSpeed, targetSpeed, slideDeceleration);
		}

		if (playerPhysics.getOnGround()) {
			if(gravity.x == 0){
				movement.y = 0;
			}
			else if(gravity.y == 0){
				movement.x = 0;
			}


			#region Movement Restoration 
			//If landed after a jump
			if(jumping){
				jumping = false;
				animator.SetBool("JumpingListener", false);
			}

			//If we're sliding
			if(sliding){
				if(Mathf.Abs(currentSpeed) < runSpeed*0.8f){
					sliding = false;
					animator.SetBool("SlidingListener", false);
					targetSpeed = runSpeed;
					playerPhysics.ResetColliderSize();
				}
			}
			#endregion

			#region Player Movement Modification
			//Allow a jump if the player is on ground and not currently sliding
			if((Input.GetButtonDown("Jump") || Input.touchCount == 3) && !sliding){
				movement -= getGravityDirection() * jumpHeight;
				jumping = true;
				Debug.Log("JUMPED");
				animator.SetBool("JumpingListener", true);
			}

			if(Input.GetButtonDown("Slide")){
				sliding = true;
				animator.SetBool("SlidingListener", true);
				targetSpeed = 0;

				playerPhysics.SetColliderSize(new Vector3(10.3f, 1.5f, 3.0f), new Vector3(0.35f, 0.75f, 0.0f));
			}

			if(Input.GetButtonDown("GravityDown")){
				setGravityDown(gravityPull);
			}

			if(Input.GetButtonDown("GravityLeft")){
				setGravityLeft(gravityPull);
				Debug.Log("GOING LEFT");
				transform.eulerAngles = new Vector3(1, 1 , 0) * 90;
				//transform.Rotate(new Vector3(0,90,0));
			}

			if(Input.GetButtonDown("GravityRight")){
				setGravityRight(gravityPull);
				Debug.Log("GOING RIGHT");
			}
			#endregion
		}


		//If needed, add inc-towards to get smoother transitions
		animator.SetFloat("SpeedListener", Mathf.Abs(currentSpeed));

		movement.z = currentSpeed;
		movement.y += gravity.y * Time.deltaTime;
		movement.x += gravity.x * Time.deltaTime;

		playerPhysics.move(movement * Time.deltaTime, gravity);
	}

	private float IncrementToward (float _currentSpeed, float _targetSpeed, float _acceleration)
	{
		if (_currentSpeed == _targetSpeed) {
			return _currentSpeed;
		} else {
			float dir = Mathf.Sign(_targetSpeed - _currentSpeed);
			_currentSpeed += Time.deltaTime * _acceleration * dir;

			//Checking if speed excedes target speed
			if(dir == Mathf.Sign(_targetSpeed - _currentSpeed)){
				return _currentSpeed;
			}
			return _targetSpeed; 
		}
	}

	void setGravityDown(float gravityPull)
	{
		gravity = Vector3.down * gravityPull;
	}

	void setGravityLeft(float gravityPull)
	{
		gravity = Vector3.left * gravityPull;
	}

	void setGravityRight(float gravityPull)
	{
		gravity = Vector3.right * gravityPull;
	}

	public Vector3 getGravityDirection ()
	{
		return gravity.normalized;
	}
}
