using UnityEngine;
using System.Collections;

public class PointsBarScript : MonoBehaviour {

	public GameObject rainbowBarPrefab;
	public Texture[] images;

	[HideInInspector]
	public PointSystem thePointSystem;

	private int currentTextureNumber;
	private int currentPointsInRow;
	private int currentTimesNumber;
	private GUITexture theRainbowBar;
	
	void Start () {
		thePointSystem = GameObject.Find ("PointSystem").GetComponent<PointSystem>();
		theRainbowBar = ((GameObject)Instantiate (rainbowBarPrefab)).guiTexture;
		theRainbowBar.transform.parent = transform;
	}

	void Update(){
		updateBar ();
	}
	
	void updateBar(){
		int times = thePointSystem.getPointsTimes();
		int current = thePointSystem.getPointsInRow ();
	
		if (current != currentPointsInRow) { //If the player has taken a point
				currentPointsInRow = current;
				currentTextureNumber = current - (times-1) * 5;
		}

		if ((currentTextureNumber == 0 && current != 0) || current > 15){
			currentTextureNumber = 5;
		}

		theRainbowBar.texture = images [currentTextureNumber];

		transform.guiText.text = times.ToString () + "x";
	}
}
