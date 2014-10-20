using UnityEngine;
using System.Collections;

public class FirstSuperPoint : MonoBehaviour {

	public GUISkin tutoringSkin;
	public Texture2D phone;
	public GameObject superPoint;
	private GameObject sp;
	private Rect superRect, buttonRect;
	private float timer;
	private Vector3 firstPos;
	
	bool displayInfo = false;
	string info = "You picked up  \n a SuperPoint! \n \n Superpoints gives you \n an extra boost! \n \n But be careful, \n your boost is lost \n if you miss one!";

	void Start () {
		superRect = new Rect (Screen.width * 0.5f - 250, Screen.height * 0.5f - 400, 500, 500);
		buttonRect = new Rect (Screen.width * 0.5f - 250, Screen.height * 0.5f - 400, 200, 200);

		firstPos = new Vector3 (0,0,-370);

		GameObject sp = (GameObject)Instantiate (superPoint, firstPos, Quaternion.identity);
		sp.transform.parent = transform;
	}
	
	void OnTriggerEnter(Collider c){
		if (c.tag == "Player") {
			displayInfo = true;
			Time.timeScale = 0;
		}
	}

	void OnGUI(){
		GUI.skin = tutoringSkin;
		
		if (displayInfo){
			superRect = GUI.Window(0, superRect, superFunc, " ");
		}
	}
	void superFunc(int id){
		GUI.TextField(new Rect (10,80,500,300), info);
		if (GUI.Button(new Rect (220,380,100,50), "OK")){
			displayInfo = false;
			Time.timeScale = 1;
		}
	}

}
