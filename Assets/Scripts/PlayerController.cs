using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PlayerPhysics))]
[RequireComponent(typeof(InputHandler))]
public class PlayerController : MonoBehaviour {

	#region Variables
	public bool gameFinished;

	//Debugging Variables
	private const bool enableVerticalInput = false;

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

	//Player States
	private bool jumping;
	private bool sliding;
	//private bool running;
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
		//running = false;
		jumping = false;
		sliding = false;
		autoStarted = false;
		gravity = Vector3.down * gravityPull;
		gameFinished = false;
	}

	void autoStartRun ()
	{
		if (Time.timeSinceLevelLoad < 5) {
			inputHandler.inputDisabled = true;
			
			if(Time.timeSinceLevelLoad >= 3){
				speed = walkSpeed;
			}else{
				return;
			}
		}
		else{
			//Only run once.
			if(!autoStarted){
				inputHandler.inputDisabled = false;
				speed = runSpeed;
				autoStarted = true;
			}
		}
	}

	// Update is called once per frame
	void Update () {
		if (healthBarValue < 1.0f) {
			healthBarValue += Time.deltaTime*0.05f;
		}

		autoStartRun ();

		if (!sliding) {

				//KEYBOARD HORIZONTAL CONTROLLER
				float xSteer = inputHandler.getHorizontalInput ();
				if (Mathf.Abs (xSteer) > 0.1 && !inputHandler.inputDisabled) {
						movement.x += xSteer;
				} else {
						movement.x = 0;
				}

				//SET SPEED
				targetSpeed = speed;

				currentSpeed = IncrementToward (currentSpeed, targetSpeed, acceleration);

		} else {
				currentSpeed = IncrementToward (currentSpeed, targetSpeed, slideDeceleration);
		}

		if (jumping && !inputHandler.inputDisabled && Application.platform != RuntimePlatform.Android) {
				if (inputHandler.getJumpingInput ()) {
						movement -= getGravityDirection () * jumpHeight;
						inputHandler.inputDisabled = true;
				}
		}

		if (playerPhysics.getOnGround ()) {
				if (gravity.x == 0) {
						movement.y = 0;
				} else if (gravity.y == 0) {
						movement.x = 0;
				}


				#region Movement Restoration 
				//If landed after a jump
				if (jumping) {
						inputHandler.inputDisabled = false;
						jumping = false;
						animator.SetBool ("JumpingListener", false);
				}

				//If we should stop sliding
				if (sliding) {
						if (Mathf.Abs (currentSpeed) < runSpeed * 0.8f) {
								sliding = false;
								animator.SetBool ("SlidingListener", false);
								targetSpeed = runSpeed;
								playerPhysics.ResetColliderSize ();
								inputHandler.inputDisabled = false;
						}
				}
				#endregion

				#region Player Movement Modification
				//Allow a jump if the player is on ground and not currently sliding
				if (inputHandler.getJumpingInput () && !sliding) { //Todo - use !inputdisabled instead
						movement -= getGravityDirection () * jumpHeight;
						jumping = true;
						animator.SetBool ("JumpingListener", true);
				}

				if (inputHandler.getSlideInput () && !inputHandler.inputDisabled) {
						sliding = true;
						inputHandler.inputDisabled = true;
						animator.SetBool ("SlidingListener", true);
						targetSpeed = 0;

						playerPhysics.SetColliderSize (new Vector3 (10.3f, 1.5f, 3.0f), new Vector3 (0.35f, 0.75f, 0.0f));
				}

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
		}

		//If needed, add inc-towards to get smoother transitions
		animator.SetFloat ("SpeedListener", Mathf.Abs (currentSpeed));

		movement.z = currentSpeed;
		movement.y += gravity.y * Time.deltaTime;
		movement.x += gravity.x * Time.deltaTime;

		playerPhysics.move (movement * Time.deltaTime, gravity);
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

	public Vector3 getGravityDirection ()
	{
		return gravity.normalized;
	}

	#region HEALTHBAR
	public float healthBarValue = 0.0f; 
	private Vector2 healthBarSize = new Vector2(300,40); 
	private Vector2 healthBarPos = new Vector2(Screen.width/2 - 150,40); 
	public Texture2D progressBarEmpty; //Ge fina texturer i prefab om man vill
	public Texture2D progressBarFull;

	private void OnGUI(){
		//draw the background
		GUI.BeginGroup(new Rect(healthBarPos.x, healthBarPos.y, healthBarSize.x, healthBarSize.y));
			GUI.Box (new Rect (0, 0, healthBarSize.x, healthBarSize.y), progressBarEmpty);

			//draw the filled-in part:
			GUI.BeginGroup(new Rect(0,0, healthBarSize.x * healthBarValue, healthBarSize.y));
				GUI.Box(new Rect(0,0, healthBarSize.x, healthBarSize.y), progressBarFull);
			GUI.EndGroup();
		GUI.EndGroup();
		
	}
	#endregion
}




















