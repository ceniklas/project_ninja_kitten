using UnityEngine;
using System.Collections;

public class Spin : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate (new Vector3 (0, Time.deltaTime*50, 0));
	}
}
