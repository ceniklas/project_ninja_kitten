using UnityEngine;
using System.Collections;

[RequireComponent (typeof(BoxCollider))]
public class PlayerPhysics : MonoBehaviour {

	public LayerMask collisionMask;

	private BoxCollider boxCollider;
	private Ray ray;
	private RaycastHit rayHitDetector;

	private Vector3 originalSize;
	private Vector3 originalCenter;
	//Colliderscale, the scaling factor is set to a float beacuse of uniform scaling
	private float colliderScale;
	private Vector3 scaledBoxColliderCenter;
	private Vector3 scaledBoxColliderSize;

	//private int nrOfColRaysX = 0;
	private int nrOfColRaysUpDown = 5;
	private int nrOfColRaysForwardBack = 10;

	private bool onGround;
	private const float skin = 0.05f;

	public bool getOnGround(){
		return onGround;
	}

	// Use this for initialization
	void Start () {
		boxCollider = GetComponent<BoxCollider>();
		colliderScale = transform.localScale.z;
		originalSize = boxCollider.size;
		//originalSize = new Vector3 (2.33f, 10.2f, 3.11f);
		originalCenter = boxCollider.center;
		//originalCenter = new Vector3 (-0.21f, 5.1f, 0);

		SetColliderSize (originalSize,originalCenter);
	}

	public void move(Vector3 movement, Vector3 gravity){   

		float deltaX = movement.x;
		float deltaY = movement.y;
		float deltaZ = movement.z;
		Vector3 pos = transform.position;

		//Don't allow the player to run off the way.
		float xPos = transform.position.x;
		if (Mathf.Abs (xPos) > 8.5f) { //Todo - use wall positions instead of 8.5
			if(xPos > 0 && deltaX > 0){
				deltaX = 0;
			}
			else if(xPos < 0 && deltaX < 0){
				deltaX = 0;
			}
		}
		
		//Check for collisions in y
		onGround = false;
		for (int i = 0; i<nrOfColRaysUpDown; i++) {

			//float dir = Mathf.Sign(deltaY);
			Vector3 dir = Vector3.Scale(gravity.normalized, movement.normalized);

			float x = (pos.x + scaledBoxColliderCenter.x - scaledBoxColliderSize.x/2.0f) + scaledBoxColliderSize.x/2.0f;
			float y =  pos.y + scaledBoxColliderCenter.y - scaledBoxColliderSize.y/2.0f * dir.y;
			float z = (pos.z + scaledBoxColliderCenter.z - scaledBoxColliderSize.z/2.0f) + i*scaledBoxColliderSize.z/(nrOfColRaysUpDown-1);

			ray = new Ray(new Vector3(x, y, z), dir);
			Debug.DrawRay(ray.origin,ray.direction);

			if (Physics.Raycast(ray, out rayHitDetector, Mathf.Abs(deltaY) + skin, collisionMask)) {

				//float distanceRayToHit = Vector3.Distance(ray.origin, rayHitDetector.point);
				float distanceRayToHit = rayHitDetector.distance;
				if(distanceRayToHit > skin){
					deltaY = distanceRayToHit * -dir.y + dir.y*skin;
				}
				else{
					deltaY = 0;
				}

				onGround = true;
				break; //If a ray hits we don't have to check the rest.
			}
		}

		//Check for collisions in z
		for (int i = 0; i<nrOfColRaysForwardBack; i++) {
			float dir = Mathf.Sign(deltaZ);
			
			float x = (pos.x + scaledBoxColliderCenter.x - scaledBoxColliderSize.x/2.0f) + scaledBoxColliderSize.x/2.0f;
			float y = (pos.y + scaledBoxColliderCenter.y - scaledBoxColliderSize.y/2.0f) + i*scaledBoxColliderSize.y/(nrOfColRaysForwardBack-1);
			float z = pos.z + scaledBoxColliderCenter.z + scaledBoxColliderSize.z/2.0f * dir;
			
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

				Destroy(rayHitDetector.transform.gameObject);
			
				break; //If a ray hits we don't have to check the rest.
			}
		}

		//Check for collision in player direction
		if (!onGround) { //Here you can also check for collisions in z
			Vector3 playerDirection = new Vector3 (deltaX, deltaY, deltaZ);

			float o_x = (pos.x + scaledBoxColliderCenter.x - scaledBoxColliderSize.x / 2.0f) + scaledBoxColliderSize.x / 2.0f;
			float o_y = pos.y + scaledBoxColliderCenter.y + scaledBoxColliderSize.y / 2.0f * Mathf.Sign (deltaY);
			float o_z = pos.z + scaledBoxColliderCenter.z + scaledBoxColliderSize.z / 2.0f * Mathf.Sign (deltaZ);

			Vector3 origin = new Vector3 (o_x, o_y, o_z);

			ray = new Ray (origin, playerDirection.normalized);
			Debug.DrawRay (ray.origin, ray.direction.normalized);

			if (Physics.Raycast (ray, out rayHitDetector, playerDirection.magnitude + skin, collisionMask)) {
				onGround = true;
				deltaY = 0;
			}
		}
		
		transform.Translate(new Vector3(deltaX, deltaY, deltaZ), Space.World);
	}

	public void SetColliderSize(Vector3 size, Vector3 center)
	{
		boxCollider.size = size;
		boxCollider.center = center;

		scaledBoxColliderCenter = boxCollider.center * colliderScale;
		scaledBoxColliderSize = boxCollider.size * colliderScale;
	}

	public void ResetColliderSize()
	{
		SetColliderSize (originalSize, originalCenter);
	}
}
