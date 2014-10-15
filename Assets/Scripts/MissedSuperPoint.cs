using UnityEngine;
using System.Collections;

public class MissedSuperPoint : MonoBehaviour {

	PointSystem thePointSystem;
	
	void Start(){
		//Finds the PointSystem script 
		thePointSystem = GameObject.Find ("PointSystem").GetComponent<PointSystem>();
	}

	void OnTriggerEnter(Collider other) {
		
		//Make sure it's the player that has entered
		if (other.tag == "Player") {
			thePointSystem.removeSuperPoint();
		}
	}
}
