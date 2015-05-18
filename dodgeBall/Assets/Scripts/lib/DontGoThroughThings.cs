using UnityEngine;
using System.Collections;
 
public class DontGoThroughThings : MonoBehaviour 
{
	public LayerMask layerMask; //make sure we aren't in this layer 
	public float skinWidth = 0.1f; //probably doesn't need to be changed 
 
	private float minimumExtent; 
	private float partialExtent; 
	private float sqrMinimumExtent; 
	private Vector2 previousPosition; 
	private Rigidbody2D myRigidbody; 
 
 
	//initialize values 
	void Awake() 
	{ 
	   myRigidbody = rigidbody2D; 
	   previousPosition = myRigidbody.position; 
	   minimumExtent = Mathf.Min(Mathf.Min(collider2D.bounds.extents.x, collider2D.bounds.extents.y), collider2D.bounds.extents.z); 
	   partialExtent = minimumExtent * (1.0f - skinWidth); 
	   sqrMinimumExtent = minimumExtent * minimumExtent; 
	} 
 
	void FixedUpdate() 
	{ 
	   //have we moved more than our minimum extent? 
	   Vector3 movementThisStep = myRigidbody.position - previousPosition; 
	   float movementSqrMagnitude = movementThisStep.sqrMagnitude;
 
	   if (movementSqrMagnitude > sqrMinimumExtent) 
		{ 
	      float movementMagnitude = Mathf.Sqrt(movementSqrMagnitude);
	      RaycastHit hitInfo; 
 
	      //check for obstructions we might have missed 
	      if (Physics.Raycast(previousPosition, movementThisStep, out hitInfo, movementMagnitude, layerMask.value)) 
	         myRigidbody.position = hitInfo.point - (movementThisStep/movementMagnitude)*partialExtent; 
	   } 
 
	   previousPosition = myRigidbody.position; 
	}
}