using UnityEngine;
using System.Collections;

public class PointSystem : MonoBehaviour
{
	private GUIText totalPointsText;
	private GUIText boosterMeterText;

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
		boosterMeterText = c [1];

		totalPointsText.text = totalPoints.ToString();
		boosterMeterText.text = printBoosterMeter ();
	}

	public void removePoint(){
		removeFromBoosterMeter();
	}

	public void addPoint(int coinValue){
		totalPoints = totalPoints + coinValue*boosterMeter;
	
		addToBoosterMeter();
	}

	void addToBoosterMeter ()
	{
		if (pointsInRow == 2) {
			pointsInRow = -1;

			if (boosterMeter != 4) {
				boosterMeter++;
			}
		}
	
		pointsInRow++;
	}

	void removeFromBoosterMeter ()
	{
		pointsInRow = 0;

		if (boosterMeter != 1) {
			boosterMeter--;
		}
	}

	string printBoosterMeter ()
	{
		string s = boosterMeter.ToString();

		for (int i = 0; i < pointsInRow; i++) {
			s += '+';
		}

		return s;
	}
}

