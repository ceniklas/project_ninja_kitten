using UnityEngine;
using System.Collections;

public class HitPoint : MonoBehaviour {

	PointSystem thePointSystem;

	void Start(){
		//Finds the PointSystem script in totalpoints
		thePointSystem = GameObject.Find ("PointSystem").GetComponent<PointSystem>();
			
	}

	void OnTriggerEnter(Collider other) {

		//Make sure it's the player that has entered
		if (other.tag == "Player") {
			thePointSystem.addPoint();
			Destroy(transform.parent.gameObject);
		}
	}
}
