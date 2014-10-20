using UnityEngine;
using System.Collections;

public class MenuHandler : MonoBehaviour {

	public Color defaultColor;
	public bool gameQuit = false;
	public bool gamePaused = false;
	public Color hoverColor;

	void Start(){
		renderer.material.color = defaultColor;
	}

	void OnMouseEnter(){
		renderer.material.color = hoverColor;
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
			Time.timeScale = 1;
		}

	}
}
