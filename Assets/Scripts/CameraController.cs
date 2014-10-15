using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	private Transform target;
	//private float trackSpeed = 100;

	public void setTarget(Transform t){
		target = t;
	}
	
	void LateUpdate(){
		if (target) {
			float x = transform.position.x;
			float y = transform.position.y;
			float z = target.position.z -15;
			//float z = IncrementToward(transform.position.z, target.position.z-5, trackSpeed);
			transform.position = new Vector3(x, y, z);
		}
	}

	/*private float IncrementToward(float _currentSpeed, float _targetSpeed, float _acceleration)
	{
		if (_currentSpeed >= _targetSpeed) {
			return _currentSpeed;
		} else {
			return _currentSpeed += Time.deltaTime * _acceleration; //Camera with acceleration
		}
	}*/


}
