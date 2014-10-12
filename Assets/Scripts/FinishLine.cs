using UnityEngine;
using System.Collections;

public class FinishLine : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other) {
	
		GameManager g = GameObject.Find ("Main Camera").GetComponent<GameManager> (); //Todo - maybe detach gamemanager from maincamera
		g.gameFinished();


	}
}
