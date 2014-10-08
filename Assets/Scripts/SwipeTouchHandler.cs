using UnityEngine;
using System.Collections;

[RequireComponent(typeof(InputHandler))]
public class SwipeTouchHandler : MonoBehaviour {

	InputHandler i;

	float startTime;
	Vector2 startPos;
	bool couldBeSwipe;
	float comfortZone;
	float minSwipeDist;
	float maxSwipeTime;
	
	void Start(){
		i = GetComponent<InputHandler>();
		comfortZone = 300f; //0 to screenRes
		minSwipeDist = 300f; //0 to screenRes (500 works for 1920)
		maxSwipeTime = 0.6f;
	}

	void Update () {
		if (Input.touchCount > 0) {
			Touch touch = Input.touches[0];
			
			switch (touch.phase) {
			case TouchPhase.Began:
				//Debug.Log("Began");
				couldBeSwipe = true;
				startPos = touch.position;
				startTime = Time.time;
				break;
				
			case TouchPhase.Moved:
				//Debug.Log("Moved = " + (touch.position.x - startPos.x) + " Comf = " + comfortZone);
				if (Mathf.Abs(touch.position.x - startPos.x) > comfortZone) {
					Debug.Log("Moved = " + (touch.position.x - startPos.x) + " Comf = " + comfortZone);
					couldBeSwipe = false;
				}
				break;
				
			case TouchPhase.Stationary:
				Debug.Log("Stationary T=" + (Time.time - startTime));

				if (Time.time - startTime > 0.2f) {
					couldBeSwipe = false;
				}

				break;
				
			case TouchPhase.Ended:
				float swipeTime = Time.time - startTime;
				float swipeDist = (touch.position - startPos).magnitude;

				Debug.Log("Ended: " + couldBeSwipe + " T="+swipeTime+ " D=" + swipeDist);

				if (couldBeSwipe && (swipeTime < maxSwipeTime) && (swipeDist > minSwipeDist)) {
					// It's a swiiiiiiiiiiiipe!
					//Debug.Log ("It's a swiiiiiiiiiiiipe!!!!!!!!!!!!!");
					int swipeDirection = (int)Mathf.Sign(touch.position.y - startPos.y);

					if(swipeDirection == 1){
						i.SendMessage("SwipedUp", SendMessageOptions.DontRequireReceiver);
					}
					else if(swipeDirection == -1){
						i.SendMessage("SwipedDown",SendMessageOptions.DontRequireReceiver);
					}

					// Do something here in reaction to the swipe.
				}
				break;
			}
		}
	}
}
