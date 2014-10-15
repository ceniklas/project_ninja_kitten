using UnityEngine;
using System.Collections;

public class HitSuperPoint : MonoBehaviour {

	public Color32 SuperCubeColor;
	//Note: Color c = new Color32 (0, 0, 255, 0); This would be equal to a blue Color32 SuperCube. 

	PointSystem thePointSystem;
	
	void Start () {
		//Finds the PointSystem script 
		thePointSystem = GameObject.Find ("PointSystem").GetComponent<PointSystem>();

		//Assign chosen color to the transform
		renderer.material.color = SuperCubeColor;

	}
	
	void OnTriggerEnter(Collider other) {
		
		//Make sure it's the player that has entered
		if (other.tag == "Player") {
			thePointSystem.addSuperPoint(SuperCubeColor);
			Destroy(transform.parent.gameObject);
		}
		
	}

}
