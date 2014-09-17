using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PlayerPhysics))]
public class PlayerController : MonoBehaviour {

	public float speed = 8;
	public float acceleration = 3;

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

		movement.z = currentSpeed;
		movement.y = 0;
		movement.x = 0;

		//Time.deltaTime????
		playerPhysics.move(movement);
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
