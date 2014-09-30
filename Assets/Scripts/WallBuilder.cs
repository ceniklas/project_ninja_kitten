using UnityEngine;
using System.Collections;

public class WallBuilder : MonoBehaviour {

	public const int courseLength =125;
	public GameObject wallSection;
	private int wallWidth = 4;
	private int rightSide = 10;
	private int leftSide = -10;
	private int beginning = -100;

	// Use this for initialization
	void Start () {
		for (int i = beginning; i < courseLength; i++) {
			GameObject rightWall = (GameObject)Instantiate (wallSection, new Vector3(rightSide, 0, i*wallWidth), Quaternion.identity);
			GameObject leftWall = (GameObject)Instantiate (wallSection, new Vector3(leftSide, 0, i*wallWidth), Quaternion.identity);
			//Making leftWall and rightWall childs of "Walls"
			leftWall.transform.parent = transform;
			rightWall.transform.parent = transform;
		}
	}
}
