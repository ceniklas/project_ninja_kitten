using UnityEngine;
using System.Collections;

public class PointSystem : MonoBehaviour
{
	private GUIText totalPointsText;

	private int totalPoints;
	private int boosterMeter;
	private int pointsInRow;

	void Start(){
		totalPoints = 0;
		boosterMeter = 1;
		pointsInRow = 0;
	}

	void Update(){

		GUIText[] c = GetComponentsInChildren<GUIText> ();
		totalPointsText = c [0];

		totalPointsText.text = totalPoints.ToString();
	}

	public void removePoint(){

	}

	public void addPoint(int coinValue){
		totalPoints = totalPoints + coinValue*boosterMeter;
	
		addToBoosterMeter();
	}
	
	
	public int getCurrentPoints ()
	{
		return pointsInRow;
	}
}

