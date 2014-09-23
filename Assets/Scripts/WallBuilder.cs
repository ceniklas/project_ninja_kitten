using UnityEngine;
using System.Collections;

public class WallBuilder : MonoBehaviour {

	public const int courseLength =85;
	public GameObject wallSection;


	// Use this for initialization
	void Start () {
		for (int i = -65; i < courseLength; i++) {

			Instantiate (wallSection, new Vector3(10, 0, i*6), Quaternion.identity);
			Instantiate (wallSection, new Vector3(-10, 0, i*6), Quaternion.identity);
		}
	
	}
}
