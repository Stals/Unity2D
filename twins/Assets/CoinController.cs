using UnityEngine;
using System.Collections;

public class CoinController : MonoBehaviour {

    [SerializeField]
    GameObject guiMoneyPrefab;

    [SerializeField]
    int baseAmout = 1;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.tag == "Player")
        {
            spawnGui(getAmount());
            // TODO give player
            Destroy(gameObject);
        }
    }


    void spawnGui(int amout) {
        Camera gameCamera = NGUITools.FindCameraForLayer(gameObject.layer);
        Camera uiCamera = UICamera.mainCamera;


        GameObject guiObject = NGUITools.AddChild(uiCamera.transform.parent.gameObject, guiMoneyPrefab);
        UILabel label = guiObject.GetComponentInChildren<UILabel>();
        label.text = amout.ToString();

        /* MOVE TO CORRECT POSITION*/
        // Get screen location of node
        Vector2 screenPos = Camera.main.WorldToScreenPoint(transform.position);

        // Move to node
        guiObject.transform.position = uiCamera.ScreenToWorldPoint(screenPos);
    }

    // TODO * for upgrades
    int getAmount()
    {
        return baseAmout;
    }
}
