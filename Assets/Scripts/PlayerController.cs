using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PlayerPhysics))]
[RequireComponent(typeof(InputHandler))]
public class PlayerController : MonoBehaviour {

	#region Variables
	//Debugging Variables
	private const bool enableVerticalInput = false;

	//Player variables
	public const float walkSpeed = 4;
	public const float runSpeed = 12;
	public const float acceleration = 30;
	public const float jumpHeight = 12;
	public const float slideDeceleration = 3;

	//Player handling
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
	private InputHandler inputHandler;
	#endregion

	// Use this for initialization
	void Start () {
		playerPhysics = GetComponent<PlayerPhysics>();
		inputHandler = GetComponent<InputHandler>();
		animator = GetComponent<Animator> ();
		transform.eulerAngles = Vector3.up * 90;
		running = false;
		jumping = false;
		sliding = false;
		autoStarted = false;


	}
	
	// Update is called once per frame
	void FixedUpdate () {

		transform.Translate (new Vector3 (0,0,1),Space.World);

		if (Input.GetKey(KeyCode.G)) {
			print("Applying");

		}

		rigidbody.AddForce(Vector3.left);

		//autoStartRun();

		/*if (!sliding) {
			//Increase speed if we're running
			if (running) {
				speed = runSpeed;
			} 
			else {
				speed = walkSpeed;
			}

			#region Manual Vertical Input
			if (enableVerticalInput) {
				//KEYBOARD VERTICAL CONTROLLER
				runningDirection = inputHandler.getVerticalInput();
			}
			else{
				runningDirection = 1;
			}
			#endregion


			//KEYBOARD HORIZONTAL CONTROLLER
			float xSteer = inputHandler.getHorizontalInput();
			if(Mathf.Abs(xSteer) > 0.1){
				movement.x += xSteer;
			}
			else{
				movement.x = 0;
			}

			//SET SPEED
			if (runningDirection != 0) { 
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

		if (jumping && !inputHandler.inputDisabled && Application.platform != RuntimePlatform.Android) {
			if(inputHandler.getJumpingInput()){
				movement -= getGravityDirection() * jumpHeight;
				inputHandler.inputDisabled = true;
			}
		}*/

		//if (playerPhysics.getOnGround()) {

			/*if(gravity.x == 0){
				movement.y = 0;
			}
			else if(gravity.y == 0){
				movement.x = 0;
			}*/


			#region Movement Restoration 
			//If landed after a jump
			/*if(jumping){
				inputHandler.inputDisabled = false;
				jumping = false;
				animator.SetBool("JumpingListener", false);
			}

			//If we should stop sliding
			if(sliding){
				if(Mathf.Abs(currentSpeed) < runSpeed*0.8f){
					sliding = false;
					animator.SetBool("SlidingListener", false);
					targetSpeed = runSpeed;
					playerPhysics.ResetColliderSize();
					inputHandler.inputDisabled = false;
				}
			}*/
			#endregion

			#region Player Movement Modification
			//Allow a jump if the player is on ground and not currently sliding
			/*if(inputHandler.getJumpingInput() && !sliding){ //Todo - use !inputdisabled instead
				movement -= getGravityDirection() * jumpHeight;
				jumping = true;
				animator.SetBool("JumpingListener", true);
			}

			if(inputHandler.getSlideInput() && !inputHandler.inputDisabled){
				sliding = true;
				inputHandler.inputDisabled = true;
				animator.SetBool("SlidingListener", true);
				targetSpeed = 0;

				playerPhysics.SetColliderSize(new Vector3(10.3f, 1.5f, 3.0f), new Vector3(0.35f, 0.75f, 0.0f));
			}*/

			/*if(Input.GetButtonDown("GravityDown")){
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
			}*/
			#endregion
		//}


		//If needed, add inc-towards to get smoother transitions
		//animator.SetFloat("SpeedListener", Mathf.Abs(currentSpeed));

		//movement.z = currentSpeed;
		//movement.y += gravity.y * Time.deltaTime;
		//movement.x += gravity.x * Time.deltaTime;

		//playerPhysics.move(movement * Time.deltaTime, new Vector3());
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

	void autoStartRun ()
	{
		if (Time.time < 5) {
			inputHandler.inputDisabled = true;
			
			if(Time.time >= 3){
				runningDirection = 1;
				speed = walkSpeed;
			}else{
				return;
			}
		}
		else{
			//Only run once.
			if(!autoStarted){
				inputHandler.inputDisabled = false;
				running = true;
				autoStarted = true;
			}
		}
	}

	void setGravityDown(float gravityPull)
	{
		//gravity = Vector3.down * gravityPull;
	}
	/*
	void setGravityLeft(float gravityPull)
	{
		gravity = Vector3.left * gravityPull;
	}

	void setGravityRight(float gravityPull)
	{
		gravity = Vector3.right * gravityPull;
	}
	*/
	public Vector3 getGravityDirection ()
	{
		return Vector3.zero;// gravity.normalized;
	}
}
