using UnityEngine;
using System.Collections;

public class TextOverlayController : MonoBehaviour {

	public int time;
	public GUIText timer;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		int theTime = (int)Time.realtimeSinceStartup;
		timer.enabled = false;

		if (theTime >= 1 && theTime <= 3) {
			timer.text = theTime.ToString();	
			timer.enabled = true;
		}

		if (theTime == 5) {
			timer.text = "RUN!";
			timer.enabled = true;
		}

		switch (theTime) {
			case 1:
				timer.color = Color.red;
				break;
			case 2:
				timer.color = Color.yellow;
				break;
			case 3:
				timer.color = Color.green;
				break;
			case 5:
				timer.color = Color.magenta;
				break;
		}
	}
}
