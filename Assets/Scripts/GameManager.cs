using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public GameObject player;
	public GameObject mainCamera;
	public GameObject pointSystem;

	private CameraController cam;

	// Use this for initialization
	void Start () {
		Screen.orientation = ScreenOrientation.Portrait;
		//cam = GetComponent<CameraController>();
		spawnCamera ();
		spawnPointSytem ();
		spawnPlayer ();
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
		cam.setTarget(((GameObject)(Instantiate (player, new Vector3(0,0, -400), Quaternion.identity))).transform);
	}

	public void gameFinished ()
	{
		print ("WOOPA! FINISHED THE GAME!");
		PlayerController p = player.GetComponent<PlayerController> ();
		p.GameFinished ();
		print (p.gameFinished);
	}
}