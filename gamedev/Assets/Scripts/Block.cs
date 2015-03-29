using UnityEngine;
using System.Collections;

public class Block : MonoBehaviour {

	[SerializeField]
	GameObject uiTarget;

	[SerializeField]
	GameObject fakeBlock;

	bool isTriggered = false;

	public int x;
	public int y;

    bool selected = false;

	// Use this for initialization
	void Start () {
        //InvokeRepeating("updateRotation", 0.5f, 0.5f);
	}

    public void updateRotation()
    {
        if (selected)
        {
            gameObject.RotateBy(new Vector3(0, 0, 1f), 0.48f, 0, EaseType.easeInOutSine, true);
        } else
        {
            //gameObject.RotateBy(new Vector3(0, 0, 0.025f), 0.5f, 0, EaseType.easeInOutSine, LoopType.pingPong, true);
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

		Vector3 currentPosition = transform.position;
		Vector3 targetPosition = Game.Instance.getBoardManager ().getPosition (x, y);

		float distance = Vector3.Distance(currentPosition, targetPosition);

        iTween.MoveTo(gameObject, iTween.Hash("position", Game.Instance.getBoardManager ().getPosition (x, y),
		                                      "time", distance * 0.5f, "delay", 0f, "islocal", true, 
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

	public void destroyBlock()
	{
        Camera gameCamera = NGUITools.FindCameraForLayer(gameObject.layer);
        Camera uiCamera = UICamera.mainCamera;


        GameObject guiObject = NGUITools.AddChild(uiCamera.transform.parent.gameObject, fakeBlock);
        


       /* UILabel label = guiObject.GetComponentInChildren<UILabel>();
        label.text = amout.ToString();*/

        /* MOVE TO CORRECT POSITION*/
        // Get screen location of node
        Vector2 screenPos = Camera.main.WorldToScreenPoint(transform.position);

        // Move to node
        guiObject.transform.position = uiCamera.ScreenToWorldPoint(screenPos);


        // радиус окружности на которой находятся точки начала и коца
        float r = (Vector3.Distance(guiObject.transform.position, uiTarget.transform.position) / 2);

        Vector3 bezierPoint = new Vector3(Random.Range(-r, r), Random.Range(-r, r), 0);      

        Vector3[] path = new Vector3[3] {guiObject.transform.position, bezierPoint,  uiTarget.transform.position};

        float time = Random.Range(0.8f, 1.5f);
        float delay = Random.Range(0.05f, 0.15f);

        guiObject.MoveTo(path, time, delay, EaseType.easeInSine);
        guiObject.RotateBy(new Vector3(0, 0, 1f), 0.5f, 0, EaseType.easeInOutSine, LoopType.loop);

        guiObject.GetComponent<FakeBlockController>().Invoke("OnFinishAnimation", time + delay);

        guiObject.GetComponent<UISprite>().spriteName = GetComponentInChildren<SpriteRenderer>().sprite.name; ;
        guiObject.GetComponent<UISprite>().MarkAsChanged();

        uiTarget.GetComponent<ProgressController>().Invoke("addBlock", time + delay);

		Destroy (this.gameObject);
        //TODO finish sound
	}
}
