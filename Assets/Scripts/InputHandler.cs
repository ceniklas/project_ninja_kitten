﻿using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SwipeTouchHandler))]
public class InputHandler : MonoBehaviour {
	
	public bool inputDisabled;
	private bool swipedDown;

	// Use this for initialization
	void Start () {
		inputDisabled = false;
		swipedDown = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public int getVerticalInput ()
	{
		int runningDirection =(int)Input.GetAxisRaw("Vertical");
		
		//PHONE VERTICAL CONTROLLER
		if (Input.touchCount == 3) {
			runningDirection = 1;
		}
		if (Input.touchCount == 4) {
			runningDirection = -1;
		}

		return runningDirection;
	}

	public bool getJumpingInput(){
		if (Input.GetButtonDown ("Jump") || Input.touchCount == 2) {
			return true;
		}
		return false;
	}

	public bool getSlideInput(){
		if (swipedDown) {
			swipedDown = false;
			return true;
		}
		return Input.GetButtonDown("Slide");
	}

	public void SwipedDown(){
		swipedDown = true; 
	}
}
