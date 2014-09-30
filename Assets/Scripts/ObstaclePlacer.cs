using UnityEngine;
using System.Collections;

public class ObstaclePlacer : MonoBehaviour {

	public GameObject jumpObstacle;
	public GameObject slideObstacle;
	private GameObject obstacle;
	private int nrOfObstacles = 17;
	private int rndNr;
	private float distanceToObstacles = 30.0f;
	private Vector3 obstaclePosition;

	// Use this for initialization
	void Start () {
		for (int n = 1; n < nrOfObstacles; n++) {
		rndNr = (int)(Random.value + 0.5);
			obstaclePosition = new Vector3(0, 0, n * distanceToObstacles);

			if (rndNr == 0) {
				obstacle = jumpObstacle;
			}
			else{
				obstacle = slideObstacle;
			}
			GameObject obs = (GameObject)Instantiate(obstacle, obstaclePosition, Quaternion.identity);
			//Making obs childs of Obstacles
			obs.transform.parent = transform;

		}	
	}
}
