using UnityEngine;
using System.Collections;

public class PlayerPhysics : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	public void move(Vector3 movement){

		float deltaX = movement.x;
		float deltaY = movement.y;
		float deltaZ = movement.z;

		//Vector3 p = transform.position;

		transform.Translate(new Vector3(deltaX, deltaY, deltaZ));
	}
}
