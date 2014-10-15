using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SwipeTouchHandler))]
public class InputHandler : MonoBehaviour {
	
	public bool inputDisabled;
	private bool swipedUp;
	private bool swipedDown;

	// Use this for initialization
	void Start () {
		inputDisabled = false;
		swipedUp = false;
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

	public float getHorizontalInput ()
	{
		//int runningDirection =(int)Input.GetAxisRaw("Horizontal");
		
		return Input.GetAxisRaw("Horizontal");//Input.acceleration.x;
		
		//return runningDirection;
	}

	public bool getJumpingInput(){
		#if UNITY_EDITOR
		return Input.GetButtonDown("Jump");
		#endif

		#if UNITY_ANDROID
		if (swipedUp) {
			swipedUp = false;
			return true;
		}
		return false;
		#endif

		return Input.GetButtonDown("Jump");
	}

	public bool getSlideInput(){
		#if UNITY_EDITOR
		return Input.GetButtonDown("Slide");
		#endif

		#if UNITY_ANDROID
		if (swipedDown) {
			swipedDown = false;
			return true;
		}
		return false;
		#endif

		return Input.GetButtonDown("Slide");
	}

	public void SwipedUp(){
		swipedUp = true;
	}

	public void SwipedDown(){
		swipedDown = true; 
	}
}
