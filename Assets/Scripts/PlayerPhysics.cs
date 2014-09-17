using UnityEngine;
using System.Collections;

[RequireComponent (typeof(BoxCollider))]
public class PlayerPhysics : MonoBehaviour {

	public LayerMask collisionMask;

	private BoxCollider boxCollider;
	private Ray ray;
	private RaycastHit rayHitDetector;

	private bool onGround;
	private const float skin = 0.005f;

	public bool getOnGround(){
		return onGround;
	}

	// Use this for initialization
	void Start () {
		boxCollider = GetComponent<BoxCollider>();
	}
	
	public void move(Vector3 movement){

		float deltaX = movement.x;
		float deltaY = movement.y;
		float deltaZ = movement.z;
		Vector3 pos = transform.position;

		//Check for collisions in y
		onGround = false;
		for (int i = 0; i<3; i++) {
			float dir = Mathf.Sign(deltaY);

			float x = (transform.position.x + boxCollider.center.x - boxCollider.size.x/2.0f) + boxCollider.size.x/2.0f;
			float y = transform.position.y + boxCollider.center.y + boxCollider.size.y/2.0f * dir;
			float z = (transform.position.z + boxCollider.center.z - boxCollider.size.z/2.0f) + i*boxCollider.size.z/2.0f;

			ray = new Ray(new Vector3(x, y, z), new Vector3(0, dir, 0));
			Debug.DrawRay(ray.origin,ray.direction);

			if (Physics.Raycast(ray, out rayHitDetector, Mathf.Abs(deltaY) + skin, collisionMask)) {

				//float distanceRayToHit = Vector3.Distance(ray.origin, rayHitDetector.point);
				float distanceRayToHit = rayHitDetector.distance;
				if(distanceRayToHit > skin){
					deltaY = distanceRayToHit * dir - dir*skin;
				}
				else{
					deltaY = 0;
				}

				onGround = true;
				break; //If a ray hits we don't have to check the rest.
			}
		}

		//Check for collisions in z
		for (int i = 0; i<3; i++) {
			float dir = Mathf.Sign(deltaZ);
			
			float x = (transform.position.x + boxCollider.center.x - boxCollider.size.x/2.0f) + boxCollider.size.x/2.0f;
			float y = (transform.position.y + boxCollider.center.y - boxCollider.size.y/2.0f) + i*boxCollider.size.y/2.0f;
			float z = transform.position.z + boxCollider.center.z + boxCollider.size.z/2.0f * dir;
			
			ray = new Ray(new Vector3(x, y, z), new Vector3(0, 0, dir));
			Debug.DrawRay(ray.origin,ray.direction);
			
			if (Physics.Raycast(ray, out rayHitDetector, Mathf.Abs(deltaZ) + skin, collisionMask)) {
				
				//float distanceRayToHit = Vector3.Distance(ray.origin, rayHitDetector.point);
				float distanceRayToHit = rayHitDetector.distance;
				if(distanceRayToHit > skin){
					deltaZ = distanceRayToHit * dir - dir*skin;
				}
				else{
					deltaZ = 0;
				}

				break; //If a ray hits we don't have to check the rest.
			}
		}

		//Check for collision in player direction
		if (!onGround) { //Here you can also check for collisions in z
			Vector3 playerDirection = new Vector3 (deltaX, deltaY, deltaZ);

			float o_x = (transform.position.x + boxCollider.center.x - boxCollider.size.x / 2.0f) + boxCollider.size.x / 2.0f;
			float o_y = transform.position.y + boxCollider.center.y + boxCollider.size.y / 2.0f * Mathf.Sign (deltaY);
			float o_z = transform.position.z + boxCollider.center.z + boxCollider.size.z / 2.0f * Mathf.Sign (deltaZ);

			Vector3 origin = new Vector3 (o_x, o_y, o_z);

			ray = new Ray (origin, playerDirection.normalized);
			Debug.DrawRay (ray.origin, ray.direction.normalized);

			if (Physics.Raycast (ray, out rayHitDetector, playerDirection.magnitude + skin, collisionMask)) {
				onGround = true;
				deltaY = 0;
			}
		}
		
		transform.Translate(new Vector3(deltaX, deltaY, deltaZ));
	}
}
