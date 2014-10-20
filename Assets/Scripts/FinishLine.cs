using UnityEngine;
using System.Collections;

public class FinishLine : MonoBehaviour {

	public GameObject finishLine;
	public GUISkin finishSkin;
	private GameObject fin;
	private Vector3 finishPosition;
	private Rect r, pointBox;
	private bool displayFinish = false;
	private int thePoints, theStreak;
	private string stringStreak, stringPoint, s;

	// Use this for initialization
	void Start () {

		finishPosition = new Vector3 (0, 0, 480);
		r = new Rect (Screen.width * 0.5f -500, Screen.height * 0.5f - 500, 1000, 1000);
		pointBox = new Rect (Screen.width * 0.15f, Screen.height * 0.4f, 500, 200);

		fin = (GameObject)Instantiate (finishLine, finishPosition, Quaternion.identity);
		fin.transform.parent = transform;

	}

	void OnTriggerEnter(Collider c) {

		if (c.tag == "Player") {
			Time.timeScale = 0;
			displayFinish = true;
		}
	}

	int getPoints(){
		int totPoints;
		PointSystem p = GameObject.Find ("PointSystem").GetComponent<PointSystem>();
		totPoints = p.getTotalPoints();
		return totPoints;
	}

	int getStreak(){
		int streak;
		PointSystem p = GameObject.Find ("PointSystem").GetComponent<PointSystem>();
		streak = p.getLongestStreak();
		return streak;
	}

	
	void OnGUI(){
		GUI.skin = finishSkin;

		if(displayFinish){
			string message = "Congratulations! \n You finished the game!";
			GUI.Window(0, r, displayPoints, message);


		}
	}

	void displayPoints(int id){
		thePoints = getPoints ();
		theStreak = getStreak ();
		stringPoint = "Total points: " + thePoints.ToString ();
		stringStreak = "Longest streak: " + theStreak.ToString ();
		s = stringPoint + "\n \n" + stringStreak;
		GUI.Box (pointBox, s);
		if (GUI.Button (new Rect (Screen.width * 0.15f, Screen.height * 0.6f, 250, 100), "Main Menu")) {
			Application.LoadLevel("mainmenu");
		}

		if (GUI.Button (new Rect (Screen.width * 0.3f, Screen.height * 0.6f, 250, 100), "Next Level")) {
			Application.LoadLevel(0);
		}
	}
}
