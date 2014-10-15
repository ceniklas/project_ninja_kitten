using UnityEngine;
using System.Collections;

public class PointsBarScript : MonoBehaviour {

	public GameObject rainbowBarPrefab;
	public Texture[] images;

	[HideInInspector]
	public PointSystem thePointSystem;

	private int currentTextureNumber;
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
		currentTextureNumber = thePointSystem.getPointsInRow() - ((times-1)*6);

		if (currentTextureNumber > 5) {
			currentTextureNumber = 5;
		}

		theRainbowBar.texture = images[currentTextureNumber];
		transform.guiText.text = times.ToString () + "x";
	}
}
