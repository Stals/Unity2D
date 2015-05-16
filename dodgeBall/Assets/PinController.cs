using UnityEngine;
using System.Collections;

public class PinController : MonoBehaviour {

	int currentTouchIDOwner = -1;
	

	public bool isSelected()
	{
		return hasOwnerTouch();
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	
		updateState();

		updatePosition();
	}

	bool hasOwnerTouch()
	{
		return currentTouchIDOwner != -1;
	}

	void updateState()
	{
		// check old touch
		if(hasOwnerTouch()){
			if(Input.GetTouch(currentTouchIDOwner).phase == TouchPhase.Ended)
			{
				setSelected(false);
				currentTouchIDOwner = -1;
			}
		}

		// check new touch
		for(int touchID = 0; touchID < Input.touchCount; ++touchID) {
			Touch touch = Input.GetTouch(touchID);

			if(startedTouchingPin(touch)){
				setSelected(true);
				currentTouchIDOwner = touchID;
			}
		}
	}


	void updatePosition()
	{
		if(hasOwnerTouch()) {

			Vector3 newPos = Camera.main.ScreenToWorldPoint(Input.GetTouch(currentTouchIDOwner).position);
			newPos.z = transform.position.z;

			transform.position = newPos;
		}
	}

	bool startedTouchingPin(Touch touch){

		if ( touch.phase == TouchPhase.Began)
		{
			Ray ray = Camera.main.ScreenPointToRay(touch.position);
			RaycastHit2D[] hits = Physics2D.GetRayIntersectionAll(ray,Mathf.Infinity);
			
			foreach (RaycastHit2D hit in hits)
			{
				if((hit.collider != null) && (hit.collider.transform == this.gameObject.transform)){
					return true;
				}
			}
		}
		return false;
	}

	void setSelected(bool b)
	{
		// todo update visual
	}
}
