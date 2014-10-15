using UnityEngine;
using System.Collections;

public class Test : MonoBehaviour {

	public PointsBarScript script;

	void OnGUI () {
		if (GUI.Button (new Rect (Screen.width * 0.5f, Screen.height *0.5f ,100, 25), "Add points")) {
			//script.ModifyPoints(1);
		}
	
	}

}
