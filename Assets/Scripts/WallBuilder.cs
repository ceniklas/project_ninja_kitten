using UnityEngine;
using System.Collections;

public class WallBuilder : MonoBehaviour {

	public const int courseLength =85;
	public GameObject wallSection;
	private int wallWidth = 4;
	private int rightSide = 10;
	private int leftSide = -10;


	// Use this for initialization
	void Start () {
		for (int i = -65; i < courseLength; i++) {

			Instantiate (wallSection, new Vector3(rightSide, 0, i*wallWidth), Quaternion.identity);
			Instantiate (wallSection, new Vector3(leftSide, 0, i*wallWidth), Quaternion.identity);
		}
	
	}
}
