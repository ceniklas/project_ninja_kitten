using UnityEngine;
using System.Collections;

public class PointPlacer : MonoBehaviour {

	public GameObject point;
	private GameObject placer;
	private int maximumPoints = 5;
	private int distanceBetween = 3;
	private int start = -115;
	private Vector3 pointPosition;
	private int xPos;
	private int margin = 6;
	private int[] posArray;
	private const int size = 22;

	// Use this for initialization
	void Start () {
		posArray = new int[size]{4,-4,0,4,4,-4,-4,4,-4,4,-4,-4,4,0,0,4,-4,4,0,0,0,0};

		for (int n=0; n<size; n++) {
			xPos = posArray [n];
			start = createRowOfCoins (xPos);

			if (n == 2) {
				start = -35;
			}
		}
	}

	int createRowOfCoins(int x){
		int newStart = 0;
		for (int i=start; i<maximumPoints + start; i++) {
			
			pointPosition = new Vector3(x,0, (i * distanceBetween));
			
			GameObject placer = (GameObject)Instantiate (point, pointPosition, Quaternion.identity);
			//Making obs childs of Obstacles
			placer.transform.parent = transform;

            newStart = i; 	                        
		}
		newStart += margin;

		return newStart;
	}
}
