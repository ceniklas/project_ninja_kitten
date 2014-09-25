using UnityEngine;
using System.Collections;

public class ObstaclePlacer : MonoBehaviour {

	public GameObject jumpObstacle;
	public GameObject slideObstacle;
	public GameObject obstacle;
	private int nrOfObstacles = 10;
	private int rndNr;
	private float distanceToFirstObstacle = 40.0f;
	//private Transform transform;
	//private float xPos = 3.3f;
	//private float yPos = 1.8f;

	// Use this for initialization
	void Start () {
		for (int n = 1; n < nrOfObstacles; n++) {
		rndNr = (int)(Random.value + 0.5);

			if (rndNr == 0) {
				obstacle = jumpObstacle;
			}
			else{
				obstacle = slideObstacle;
			}
			Instantiate(obstacle, new Vector3(0, 0, n * distanceToFirstObstacle), Quaternion.identity);

		}	
	}
}
