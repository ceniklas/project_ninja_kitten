using UnityEngine;
using System.Collections;

public class FirstSuperPoint : MonoBehaviour {

	public GUISkin tutoringSkin;
	public Texture2D phone;
	public GameObject superPoint;
	private GameObject sp;
	private Rect superRect;
	private float timer;
	private Vector3 firstPos;
	
	bool displayInfo = false;
	string info = "You picked up  \n a SuperPoint! \n \n Superpoints gives you \n an extra boost! \n \n But be careful, \n your boost is lost \n if you miss one!";

	void Start () {
		superRect = new Rect (Screen.width * 0.5f - 250, Screen.height * 0.5f - 400, 500, 500);

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
			GUI.TextField(superRect, info);
			if(GUI.Button(superRect, "Knapp")){
				displayInfo = false;
				Time.timeScale = 1;
			}
		}
	}

}
