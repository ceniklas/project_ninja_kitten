using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SwipeTouchHandler))]
public class InputHandler : MonoBehaviour {
	
	public bool inputDisabled;

#if UNITY_ANDROID
	private bool swipedUp;
	private bool swipedDown;
#endif

	// Use this for initialization
	void Start () {
		inputDisabled = false;
		swipedUp = false;
		swipedDown = false;
	}

	public float getHorizontalInput ()
	{
		#if UNITY_EDITOR
		return Input.GetAxisRaw("Horizontal");
   		#elif UNITY_ANDROID
		return Input.acceleration.x;
		#else
		return Input.GetAxisRaw("Horizontal");
		#endif
	}

	public bool getJumpingInput(){
		#if UNITY_EDITOR
		return Input.GetButtonDown("Jump");
		#elif UNITY_ANDROID
		if (swipedUp) {
			swipedUp = false;
			return true;
		}
		return false;
		#else
		return Input.GetButtonDown("Jump");
		#endif
	}

	public bool getSlideInput(){
		#if UNITY_EDITOR
		return Input.GetButtonDown("Slide");
		#elif UNITY_ANDROID
		if (swipedDown) {
			swipedDown = false;
			return true;
		}
		return false;
		#else
		return Input.GetButtonDown("Slide");
		#endif
	}

	public void SwipedUp(){
		swipedUp = true;
	}

	public void SwipedDown(){
		swipedDown = true; 
	}
}
