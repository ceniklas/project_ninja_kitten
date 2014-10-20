using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public GameObject player;
	public GameObject mainCamera;
	public GameObject pointSystem;
	public Texture2D pauseButtonTexture;
	public GUISkin gameSkin;
	private PauseMenu pause;

	private CameraController cam;

	// Use this for initialization
	void Start () {
		Screen.orientation = ScreenOrientation.Landscape;
		//cam = GetComponent<CameraController>();
		spawnCamera ();
		spawnPointSytem ();
		spawnPlayer ();
		pause = cam.GetComponent<PauseMenu>();
	}

	private void spawnCamera ()
	{
		cam = ((GameObject)Instantiate (mainCamera)).GetComponent<CameraController>();
		cam.name = "Main Camera";
	}

	void spawnPointSytem ()
	{
		Instantiate (pointSystem).name = "PointSystem";
	}
	
	private void spawnPlayer(){
		cam.setTarget(((GameObject)(Instantiate (player, new Vector3(0,0,-400), Quaternion.identity))).transform);
	}

	private void OnGUI(){
		GUI.skin = gameSkin;
		//This is the only way I can come up with that will make the buttons move with the screen
		GUILayout.BeginArea(new Rect(Screen.width - (Screen.width - 20), Screen.height *0.85f ,100,100));
		if (GUI.Button (new Rect(0,0,100,100)," ")){
			pause.setPause();
		}
		GUILayout.EndArea ();
	}
}	