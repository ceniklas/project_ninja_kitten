using UnityEngine;
using System.Collections;

public class PauseMenu : MonoBehaviour {

	public GUISkin pauseSkin;
	private Rect pauseRect, mainRect;
	private bool ifPaused = false;

	// Use this for initialization
	void Start () {
		//Rect(float left, float top, float width, float height);
		pauseRect = new Rect (Screen.width * 0.5f - 200, Screen.height * 0.5f - 100, 200, 200);
		mainRect = new Rect (Screen.width * 0.5f, Screen.height * 0.5f - 100, 200, 200);
	}

	// Update is called once per frame
	/*void Update (){
		if (Input.GetKeyDown(KeyCode.Escape)) {

			//if the game is paused, unpause it
			if(ifPaused){
				ifPaused = false;
				Time.timeScale = 1;
			}
			//pause the game
			else{
				ifPaused = true;
				Time.timeScale = 0;
			}
		}
	}*/

	public void setPause (){
		ifPaused = true;
		Time.timeScale = 0;
	}

	void OnGUI(){
		GUI.skin = pauseSkin;

		if (ifPaused){
			pauseRect = GUI.Window(0, pauseRect, pauseFunc, " ");
			mainRect = GUI.Window(1, mainRect, mainFunc, " ");
		}
	}

	void pauseFunc(int id){
		if (GUI.Button (new Rect (55,85,100,50), "Resume Game")){
			ifPaused = false;
			Time.timeScale = 1;
		}
	}

	void mainFunc(int id){
		if (GUI.Button (new Rect (55,85,100,50), "Main Menu")){
			ifPaused = false;
			Time.timeScale = 1;
			Application.LoadLevel(0);
		}
	}
}
