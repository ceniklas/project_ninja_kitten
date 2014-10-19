using UnityEngine;
using System.Collections;

public class SwipeTutoring : MonoBehaviour {

	public GUISkin tutoringSkin;
	public Texture2D jumpPic;
	public Texture2D slidePic;
	private Rect tutoringRect;
	
	bool displayJump = false;
	bool displaySlide = false;

	float timer;

	// Use this for initialization
	void Start () {
		tutoringRect = new Rect (Screen.width * 0.5f - 150, Screen.height * 0.5f - 400, 300, 300);
		timer = 0;
	}

	void Update(){
		if (timer != 0) {
			if(Time.time - timer > 3){
				displayJump = false;

					if(Time.time - timer > 8){
						displaySlide = true;

						if(Time.time - timer > 10){
							displaySlide = false;
						}
					}
			}
			
		}
	}
	
	void OnTriggerEnter(Collider collider){
		if (collider.tag == "Player") {
			displayJump = true;
			timer = Time.time;
		}
		
	}
	
	void OnGUI(){
		GUI.skin = tutoringSkin;
		
		if (displayJump){
			GUI.Box(tutoringRect, jumpPic);
		}
		else if(displaySlide){
			GUI.Box(tutoringRect, slidePic);
		}
	}
	

}
