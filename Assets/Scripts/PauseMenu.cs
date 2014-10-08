using UnityEngine;
using System.Collections;

public class PauseMenu : MonoBehaviour {

	public GUISkin pauseSkin;
	private Rect pauseRect;
	private bool ifPaused = false;

	// Use this for initialization
	void Start () {
		//Rect(float left, float top, float width, float height);
		pauseRect = new Rect (Screen.width * 0.5f - 100, Screen.height * 0.5f - 100, 200, 200);
	}

	// Update is called once per frame
	void Update (){
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
	}

	void OnGUI(){
		if (ifPaused){
			pauseRect = GUI.Window(0, pauseRect, pauseFunc, "Pause Menu");
		}
	}

	void pauseFunc(int id){
		if (GUILayout.Button ("Resume Game")){
			ifPaused = false;
			Time.timeScale = 1;
		}

	}
}
