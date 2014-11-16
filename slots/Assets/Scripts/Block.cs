using UnityEngine;
using System.Collections;

public class Block : MonoBehaviour {

	bool isTriggered = false;

	public int x;
	public int y;

    bool selected = false;

	// Use this for initialization
	void Start () {
        InvokeRepeating("updateRotation", 0.5f, 0.5f);
	}

    void updateRotation()
    {
        if (selected)
        {
            gameObject.RotateBy(new Vector3(0, 0, 1f), 0.3f, 0, EaseType.easeInOutSine, true);
        } else
        {
            gameObject.RotateBy(new Vector3(0, 0, 0.025f), 0.5f, 0, EaseType.easeInOutSine, LoopType.pingPong, true);
        }
    }

	public void setIDs(int _x, int _y)
	{
		x = _x;
		y = _y;
	}

	// Update is called once per frame
	void Update () {
        /*Vector2 newPos;

        newPos.x = Mathf.Lerp(transform.localPosition.x, targetPosition.x, Time.deltaTime * 8f);
        newPos.y = Mathf.Lerp(transform.localPosition.y, targetPosition.y, Time.deltaTime * 8f);

        transform.localPosition = newPos;*/


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
        //gameObject.MoveTo(Game.Instance.getBoardManager ().getPosition (x, y), 0.5f, 0f, EaseType.easeInOutSine);

        /*iTween.MoveTo(gameObject, iTween.Hash("position", Game.Instance.getBoardManager ().getPosition (x, y),
                                              "time", 0.3f, "delay", 0f, "islocal", true, 
                                              "easetype", "easeInOutSine"));*/

        iTween.MoveTo(gameObject, iTween.Hash("position", Game.Instance.getBoardManager ().getPosition (x, y),
                                              "time", 0.3f, "delay", 0f, "islocal", true, 
                                              "easetype", "easeInQuad"));

	}

    public string getImageName()
    {
        return GetComponentInChildren<SpriteRenderer>().sprite.name;
    }

    public void setSelected(bool _selected){
        if (!_selected && selected)
        {
            iTween.Stop(gameObject);
            gameObject.RotateTo(new Vector3(0, 0, 0), 0.2f, 0);
            //transform.localEulerAngles = new Vector3(0, 0, 4.5f);
        }

        if (_selected)
        {
            //audio.pitch = Random.Range(0.9f, 1.1f);
            audio.Play();
        }
        
        selected = _selected;
    }

    public bool isSelected()
    {
        return selected;
    }
}
