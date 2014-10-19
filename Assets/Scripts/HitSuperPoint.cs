using UnityEngine;
using System.Collections;

public class HitSuperPoint : MonoBehaviour {

	bool shouldBeDestroyed;

	public Color32 SuperCubeColor;
	//Note: Color c = new Color32 (0, 0, 255, 0); This would be equal to a blue Color32 SuperCube. 

	PointSystem thePointSystem;
	
	void Start () {
		shouldBeDestroyed = false;

		//Assign chosen color to the transform
		renderer.material.color = SuperCubeColor;

	}

	void Update(){
		if (shouldBeDestroyed) {
			Vector3 finalPos = new Vector3(6f, 12.85f, 0);

			if(transform.position.y > 14){
				Destroy(transform.parent.gameObject);
			}else{
				transform.position += new Vector3(6f, 12.85f, 0) * Time.deltaTime * 2;
			}
		}
	}

	void OnTriggerEnter(Collider other) {
		
		//Make sure it's the player that has entered
		if (other.tag == "Player") {
			//thePointSystem.addSuperPoint(SuperCubeColor);
			GameObject.Find ("PointSystem").GetComponent<PointSystem>().addSuperPoint(SuperCubeColor);
			Destroy(transform.parent.GetChild(1).gameObject);
			shouldBeDestroyed = true;
		}
		
	}

}
