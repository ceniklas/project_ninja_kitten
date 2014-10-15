using UnityEngine;
using System.Collections;

public class PointSystem : MonoBehaviour
{
	private GUIText totalPointsText;

	private int totalPoints;
	private int pointsTimes;
	private int pointsInRow;

	void Start(){
		totalPoints = 0;
		pointsTimes = 1;
		pointsInRow = 0;
	}

	void Update(){

		GUIText[] c = GetComponentsInChildren<GUIText> ();
		totalPointsText = c [0];

		totalPointsText.text = totalPoints.ToString();
	}

	public void addPoint(){

		totalPoints += pointsTimes;
	}

	public void removePoint(){

	}

	public void addSuperPoint(Color32 superCubeColor){

		//if (superCubeColor.Equals (new Color32(0,0,255,0))) {	} //TODO: Check if it's the correct color.

		pointsInRow++;

		if (pointsInRow > 17) {
			pointsTimes = 4;
		}
		else if (pointsInRow > 11) {
			pointsTimes = 3;
		}
		else if (pointsInRow > 5) {
			pointsTimes = 2;
		}
	}

	public void removeSuperPoint(){
		pointsInRow = 0;
		pointsTimes = 1;
	}
	
	public int getPointsInRow ()
	{
		return pointsInRow;
	}

	public int getPointsTimes ()
	{
		return pointsTimes;
	}
}
