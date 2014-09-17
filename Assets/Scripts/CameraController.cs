using UnityEngine;
using System.Collections;

[RequireComponent(typeof(GameManager))]
public class CameraController : MonoBehaviour {

	private Transform target;
	private float trackSpeed = 100;

	public void setTarget(Transform t){
		target = t;
	}
	
	void LateUpdate(){
		if (target) {
			//float z  = IncrementToward(transform.position.z, target.position.z - 5, trackSpeed);
			//float x,y;
			//transform.position = new Vector3(transform.position.x, transform.position.y, z);
			transform.position = new Vector3(transform.position.x, transform.position.y, target.position.z - 5);
		}
	}

	/*private float IncrementToward (float _currentSpeed, float _targetSpeed, float _acceleration)
	{

		if (_currentSpeed >= _targetSpeed) {
			return _currentSpeed;
		} else {
			return _currentSpeed += Time.deltaTime * _acceleration; //Camera with acceleration
		}
	}*/

}
