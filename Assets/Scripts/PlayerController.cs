﻿using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PlayerPhysics))]
[RequireComponent(typeof(InputHandler))]
public class PlayerController : MonoBehaviour {

	#region Variables
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
	}

	void autoStartRun ()
	{
		if (Time.timeSinceLevelLoad < 3) {
			inputHandler.inputDisabled = true;
			
			if(Time.timeSinceLevelLoad >= 1){
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

		if (healthBarValue < 0) {
			playerDied = true;
			healthBarValue = -1000;
			currentSpeed = 0;

			if(timeSinceDeath == 0){
				timeSinceDeath = Time.time;
			}

			if(Time.time - timeSinceDeath > 2){
				playerDied = false;
				timeSinceDeath = 0;
				Application.LoadLevel(Application.loadedLevelName);
			}
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

	public GUISkin gameSkin;
	private bool playerDied = false;
	private float timeSinceDeath = 0;

	#region GUI-stuff
	public float healthBarValue = 0.0f; 
	private Vector2 healthBarSize = new Vector2(300,40); 
	private Vector2 healthBarPos = new Vector2(Screen.width *0.5f - 150,30); 
	public Texture2D progressBarEmpty; //Ge fina texturer i prefab om man vill
	public Texture2D progressBarFull;
	public Texture2D health;

	private void OnGUI(){
		GUI.skin = gameSkin;

		//draw the background
		GUI.Box (new Rect(healthBarPos.x - 50, healthBarPos.y - 5, 50, 50), health);
		GUI.BeginGroup(new Rect(healthBarPos.x, healthBarPos.y, healthBarSize.x, healthBarSize.y));
			GUI.Box (new Rect (0, 0, healthBarSize.x, healthBarSize.y), progressBarEmpty);

			//draw the filled-in part:
			GUI.BeginGroup(new Rect(0,0, healthBarSize.x * healthBarValue, healthBarSize.y));
				GUI.Box(new Rect(0,0, healthBarSize.x , healthBarSize.y + 35), progressBarFull);
			GUI.EndGroup();
		GUI.EndGroup();

		if (playerDied) {

			GUI.TextField(new Rect(700, 150, 500, 500), "Game Over \n Try again!");

		}
		
	}
	#endregion
}




















