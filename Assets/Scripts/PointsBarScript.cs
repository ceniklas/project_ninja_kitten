using UnityEngine;
using System.Collections;

public class PointsBarScript : MonoBehaviour {

	public PointSystem system;
	public GameObject rainbowGUI;
	private int currentPoints;
	private int startingPoints = 0;
	private int maxPoints = 5;
	private GUITexture p;
	public Texture[] images;

	// Use this for initialization
	void Start () {
		system = GameObject.Find ("PointSystem").GetComponent<PointSystem>();
		p=(GUITexture)Instantiate (rainbowGUI);
		p.transform.parent = transform;
	}

	public void ModifyPoints(int p){
		if(currentPoints < maxPoints){
			currentPoints += p;
			Debug.Log(currentPoints);

		}
		else{
			currentPoints = startingPoints;
		}
	}

	void updateBar(){
		currentPoints = system.getCurrentPoints();
		p.texture = images[currentPoints];
	}
}
