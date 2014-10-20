using UnityEngine;
using System.Collections;

public class SuperPointPlacer : MonoBehaviour {

	public GameObject superPoint;
	private GameObject superPlacer;
	private int maximumPoints = 1;
	private int distanceBetween = 12;
	private int start = -27;
	private Vector3 pointPosition, firstSuperPoint;
	private int xPos;
	private int margin = 6;
	private int[] posArray;
	private const int size = 12;
	
	// Use this for initialization
	void Start () {
		posArray = new int[size]{0,0,0,4,-4,4,-4,4,4,4,0, 0};
			
		for (int n=0; n<size; n++) {
			xPos = posArray [n];
			start = createRowOfCoins (xPos);
		}
	}
	
	int createRowOfCoins(int x){
		int newStart = 0;
		for (int i=start; i<maximumPoints + start; i++) {
			
			pointPosition = new Vector3(x,0, (i * distanceBetween));
			
			GameObject superPlacer = (GameObject)Instantiate (superPoint, pointPosition, Quaternion.identity);
			superPlacer.transform.parent = transform;
			
			newStart = i; 	                        
		}
		newStart += margin;
		
		return newStart;
	}

}
