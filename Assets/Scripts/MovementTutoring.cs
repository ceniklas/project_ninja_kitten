using UnityEngine;
using System.Collections;

public class MovementTutoring : MonoBehaviour {
	
	public GUISkin tutoringSkin;
	public Texture2D phone;
	public Texture2D tiltLeft;
	public Texture2D tiltRight;
	private Rect tutoringRect;
	private float timer;

	bool displayUp = false;
	bool displayLeft = false;
	bool displayRight = false;
	// Use this for initialization
	void Start () {
		tutoringRect = new Rect (Screen.width * 0.5f - 150, Screen.height * 0.5f - 400, 300, 300);

		timer = 0;
	}
	void Update(){
		if (timer != 0) {
			if(Time.time - timer > 1){
				displayLeft = true;
				displayUp = false;

				if(Time.time - timer > 2){
					displayLeft = false;
					displayRight = true;

					if(Time.time - timer > 3){
						displayRight = false;
					}
				}
			}

		}
	}

	void OnTriggerEnter(Collider collider){
		if (collider.tag == "Player") {
			displayUp = true;
			timer = Time.time;
		}

	}

	void OnGUI(){
		GUI.skin = tutoringSkin;
		
		if (displayUp){
			GUI.Box(tutoringRect, phone);
		}
		else if(displayLeft){
			GUI.Box(tutoringRect, tiltLeft);
		}
		else if(displayRight){
			GUI.Box(tutoringRect, tiltRight);
		}
	}
	


}
