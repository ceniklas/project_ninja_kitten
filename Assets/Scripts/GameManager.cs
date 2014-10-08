using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public GameObject player;
	private CameraController cam;

	// Use this for initialization
	void Start () {
		Screen.orientation = ScreenOrientation.Portrait;
		cam = GetComponent<CameraController>();
		spawnPlayer ();
	}
	
	private void spawnPlayer(){
		cam.setTarget(((GameObject)(Instantiate (player, Vector3.zero, Quaternion.identity))).transform);
	}
}