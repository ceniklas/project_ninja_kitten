using UnityEngine;
using System.Collections;

public class CreditsQuit : MonoBehaviour {

	public bool endGame = false;

	void OnMouseDown(){
		if (endGame) {
				Application.Quit ();
		}
	}
}
