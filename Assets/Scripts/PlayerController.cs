using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PlayerPhysics))]
public class PlayerController : MonoBehaviour {

	public const float gravity = 30.0f;
	public const float speed = 8;
	public const float acceleration = 30;
	public const float jumpHeight = 12.0f;

	private float currentSpeed;
	private float targetSpeed;
	private Vector3 movement;

	private PlayerPhysics playerPhysics;

	// Use this for initialization
	void Start () {
		playerPhysics = GetComponent<PlayerPhysics>();
	}
	
	// Update is called once per frame
	void Update () {
		targetSpeed = Input.GetAxisRaw("Horizontal") * speed;
		currentSpeed = IncrementToward(currentSpeed, targetSpeed, acceleration);

		if (playerPhysics.getOnGround()) {
			movement.y = 0;

			//Allow a jump if the player is on ground.
			if(Input.GetButtonDown("Jump")){
				movement.y = jumpHeight;
			}
		}

		movement.z = currentSpeed;
		movement.y -= gravity * Time.deltaTime;
		movement.x = 0;

		//Time.deltaTime????
		playerPhysics.move(movement * Time.deltaTime);
	}

	private float IncrementToward (float _currentSpeed, float _targetSpeed, float _acceleration)
	{
		if (_currentSpeed >= _targetSpeed) {
			return _currentSpeed;
		} else {
			return _currentSpeed += Time.deltaTime * _acceleration;
		}
	}
}
