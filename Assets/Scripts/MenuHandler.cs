using UnityEngine;
using System.Collections;

public class MenuHandler : MonoBehaviour {

	private Color defaultColor = new Color32(255,45,195,255);
	public bool gameQuit = false;

	void OnMouseEnter(){
		renderer.material.color = Color.black;
	}

	void OnMouseExit(){
		renderer.material.color = defaultColor;
	}

	void OnMouseDown(){
		if (gameQuit) {
			Application.Quit();
		}
		else{
			Application.LoadLevel(1);
		}

	}
}
