using UnityEngine;
using System.Collections;

public class HitCoin : MonoBehaviour {

	PointSystem p;

	void Start(){
		//Finds the PointSystem script in totalpoints
		p = GameObject.Find ("PointSystem").GetComponent<PointSystem>();
			//transform.parent.parent.gameObject.GetComponentInChildren<PointSystem> ();
	}

	void OnTriggerEnter(Collider other) {

		//Make sure it's the player that has entered
		if (other.tag == "Player") {
			p.addPoint(1);
			Destroy(transform.parent.gameObject);
		}

	}
}
