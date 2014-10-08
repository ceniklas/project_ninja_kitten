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
	private int nrOfColRaysSideways = 10;

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
