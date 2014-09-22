using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PlayerPhysics))]
public class PlayerController : MonoBehaviour {

	//Player variables
	public const float gravity = 30;
	public const float walkSpeed = 4;
	public const float runSpeed = 8;
	public const float acceleration = 30;
	public const float jumpHeight = 12;
	public const float slideDeceleration = 2;

	//Player handling
	private float currentSpeed;
	private float targetSpeed;
	private Vector3 movement;
	private float speed;

	//Player States
	private bool jumping;
	private bool sliding;
	private bool running;
	private bool autoStarted;

	//Player components
	private PlayerPhysics playerPhysics;
	private Animator animator;

	// Use this for initialization
	void Start () {
		playerPhysics = GetComponent<PlayerPhysics>();
		animator = GetComponent<Animator> ();
		transform.eulerAngles = Vector3.up * 90;
		running = false;
	}
	
	// Update is called once per frame
	void Update () {
		float moveInput = 0;

		//Autorun
		if (Time.realtimeSinceStartup >= 3 && !autoStarted) {
			moveInput = 1;

			if (Time.realtimeSinceStartup >= 5) {
				running = true;
				autoStarted = true;
			}
		}
		if (!sliding) {
			//Increase speed if we're running
			if (running) {
				speed = runSpeed;
			} 
			else {
				speed = walkSpeed;
			}

			/* //MANUAL INPUT!
			//KEYBOARD VERTICAL CONTROLLER
			moveInput = Input.GetAxisRaw ("Vertical");

			//PHONE VERTICAL CONTROLLER
			if (Input.touchCount == 1) {
				moveInput = 1;
			}
			if (Input.touchCount == 2) {
				moveInput = -1;
			}*/

			//SET SPEED
			if (moveInput == 1 || moveInput == -1) {
				targetSpeed = moveInput * speed;
			}

			currentSpeed = IncrementToward (currentSpeed, targetSpeed, acceleration);

			//Face direction
			if(moveInput != 0){
				transform.eulerAngles = Vector3.up * 90 * moveInput;
			}
		}
		else{
			currentSpeed = IncrementToward(currentSpeed, targetSpeed, slideDeceleration);
		}

		if (playerPhysics.getOnGround()) {
			movement.y = 0;

			//If landed after a jump
			if(jumping){
				jumping = false;
				animator.SetBool("JumpingListener", false);
			}

			if(sliding){
				if(Mathf.Abs(currentSpeed) < runSpeed*0.8f){
					sliding = false;
					animator.SetBool("SlidingListener", false);
					targetSpeed = runSpeed;
					playerPhysics.ResetColliderSize();
				}
			}

			//Allow a jump if the player is on ground and not currently sliding
			if((Input.GetButtonDown("Jump") || Input.touchCount == 3) && !sliding){
				movement.y = jumpHeight;
				jumping = true;
				animator.SetBool("JumpingListener", true);
			}

			if(Input.GetButtonDown("Slide")){
				sliding = true;
				animator.SetBool("SlidingListener", true);
				targetSpeed = 0;

				playerPhysics.SetColliderSize(new Vector3(10.3f, 1.5f, 3.0f), new Vector3(0.35f, 0.75f, 0.0f));
			}
		}


		//If needed, add inc-towards to get smoother transitions
		animator.SetFloat("SpeedListener", Mathf.Abs(currentSpeed));

		movement.z = currentSpeed;
		movement.y -= gravity * Time.deltaTime;
		movement.x = 0;

		playerPhysics.move(movement * Time.deltaTime);
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
}
