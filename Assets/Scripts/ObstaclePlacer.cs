using UnityEngine;
using System.Collections;

public class ObstaclePlacer : MonoBehaviour {

	public GameObject jumpObstacle;
	public GameObject slideObstacle;
	//public GameObject point;
	private GameObject obstacle;
	private int nrOfObstacles = 15;
	private int startPos = -10;
	private float distanceToObstacles = 30.0f;
	private Vector3 obstaclePosition;
	private int[] obstacleArray;
	private int[] posArray;
	private const int arrSize = 25;

	// Use this for initialization
	void Start () {
		obstacleArray = new int[arrSize]{0,0,0,1,1,1,0,0,0,0,1,1,1,0,0,1,1,0,0,1,1,0,0,1,1};
		//0 = middle, 1 = left, 2 = right;
		posArray = new int[arrSize]{0,0,0,0,0,0,2,2,1,1,2,1,2,1,1,2,0,0,2,1,2,0,0,0,0};


		for (int n = startPos; n < nrOfObstacles; n++) {


			if(posArray[n+10] == 2) {
				obstaclePosition = new Vector3(4, 0, n * distanceToObstacles);
			}
			else if(posArray[n+10] == 1){
				obstaclePosition = new Vector3(-4, 0, n * distanceToObstacles);
			}
			else{
				obstaclePosition = new Vector3(0, 0, n * distanceToObstacles);
			}
			/*switch (obstacle){
			case 1:
			case 2:

			}*/
			//n starts at -10, therefore a +10 is needed
			if (obstacleArray[n+10] == 0) {
				obstacle = jumpObstacle;
				//Instantiate(point, obstaclePosition + new Vector3(0, 4, 0), Quaternion.identity);
			}
			else{
				obstacle = slideObstacle;
				//Instantiate(point, obstaclePosition - new Vector3(0, 1, 0), Quaternion.identity);
			}
			GameObject obs = (GameObject)Instantiate(obstacle, obstaclePosition, Quaternion.identity);
			//Making obs childs of Obstacles
			obs.transform.parent = transform;
		}	
	}
}
