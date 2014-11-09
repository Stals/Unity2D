using UnityEngine;
using System.Collections;

public class Block : MonoBehaviour {

	bool isTriggered = false;

	public int x;
	public int y;

    bool selected = false;

    Vector3 targetPosition;

	// Use this for initialization
	void Start () {
        targetPosition = transform.position;
	}

	public void setIDs(int _x, int _y)
	{
		x = _x;
		y = _y;
	}

	// Update is called once per frame
	void Update () {
        Vector2 newPos;

        newPos.x = Mathf.Lerp(transform.localPosition.x, targetPosition.x, Time.deltaTime * 8f);
        newPos.y = Mathf.Lerp(transform.localPosition.y, targetPosition.y, Time.deltaTime * 8f);

        transform.localPosition = newPos;


		if (isMouseOver ()) {
			if(!isTriggered){
				Game.Instance.getBoardManager ().onBlockTrigger (this);
				isTriggered = true;
			}
		} else {
			isTriggered = false;		
		}
	}

	bool isMouseOver()
	{
		if (Input.GetMouseButton(0))// GetMouseButtonDown(0))
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit2D[] hits = Physics2D.GetRayIntersectionAll(ray,Mathf.Infinity);
			
			foreach (RaycastHit2D hit in hits)
			{
				if((hit.collider != null) && (hit.collider.transform == this.gameObject.transform))
				{
					return true;
				}
			}
		}
		return false;
	}

	public void updatePosition()
	{
        targetPosition = Game.Instance.getBoardManager ().getPosition (x, y);
	}

    public string getImageName()
    {
        return GetComponentInChildren<SpriteRenderer>().sprite.name;
    }

    public void setSelected(bool _selected){
        // start rotating if true
        // 
        selected = _selected;
    }

    public bool isSelected()
    {
        return selected;
    }
}
