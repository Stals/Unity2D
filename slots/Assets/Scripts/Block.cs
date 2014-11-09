using UnityEngine;
using System.Collections;

public class Block : MonoBehaviour {

	bool isTriggered = false;

	public int x;
	public int y;

    bool selected = false;

	// Use this for initialization
	void Start () {
	
	}

	public void setIDs(int _x, int _y)
	{
		x = _x;
		y = _y;
	}

	// Update is called once per frame
	void Update () {
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
		transform.localPosition = Game.Instance.getBoardManager ().getPosition (x, y);
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
