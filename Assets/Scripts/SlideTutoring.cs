﻿using UnityEngine;
using System.Collections;

public class SlideTutoring : MonoBehaviour {

	public GameObject exclamationPoint;
	private GameObject gob;
	public GUISkin tutoringSkin;
	private Rect tutoringRect;
	
	bool disp = false;
	private string tutoringMessage;
	// Use this for initialization
	void Start () {
		gob = (GameObject)Instantiate(exclamationPoint, new Vector3(0,0, -230), Quaternion.identity);
		gob.transform.parent = transform;
		
		tutoringRect = new Rect (Screen.width * 0.5f - 150, Screen.height * 0.5f - 100, 300, 300);
		tutoringMessage = "Swipe down to slide! \n \n \n \n \n (Tap to continue)";
	}
	
	void OnTriggerEnter(Collider collider){
		if (collider.tag == "Player") {
			disp = true;
		}
		
	}
	
	void OnGUI(){
		GUI.skin = tutoringSkin;
		
		if (disp){
			Time.timeScale = 0;
			GUI.Box(tutoringRect, tutoringMessage);
			if(GUI.Button(new Rect (625,300,100,50), "Resume Game")){
				Time.timeScale = 1;
				disp = false;
			}
		}
	}
	

}
