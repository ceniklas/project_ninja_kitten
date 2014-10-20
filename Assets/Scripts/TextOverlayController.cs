using UnityEngine;
using System.Collections;

public class TextOverlayController : MonoBehaviour {

	public int time;
	public GameObject CountDownTextPrefab;

	private GUIText timer;

	// Use this for initialization
	void Start () {
		timer = ((GameObject)Instantiate(CountDownTextPrefab)).GetComponent<GUIText>();
	}
	
	// Update is called once per frame
	void Update () {
		int theTime = (int)Time.timeSinceLevelLoad;
		timer.enabled = false;

		if (theTime == 3) {
			timer.text = "RUN!";
			timer.enabled = true;
		}
	}
}
