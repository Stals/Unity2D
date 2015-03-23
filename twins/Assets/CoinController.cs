using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum CoinType{
    Bronze,
    Silver,
    Gold
};

[RequireComponent(typeof(TweenAlpha))]
public class CoinController : MonoBehaviour {

    [SerializeField]
    GameObject guiMoneyPrefab;

    int baseAmount = 1;

    [SerializeField]
    float fadeDelay = 1;

	// Use this for initialization
	void Start () {
        GetComponent<TweenAlpha>().delay = fadeDelay;
        GetComponent<TweenAlpha>().PlayForward();

        // TODO apply random force - mb not here
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void setType(CoinType type) {
        string imageName = "";
        switch (type)
        {
            case CoinType.Bronze:
                imageName = "bronze";
                break;
            case CoinType.Silver:
                imageName = "silver";
                break;
            case CoinType.Gold:
                imageName = "gold";
                break;
            default:
                break;

        }

        baseAmount = getAmountForType(type);

        Sprite s = Resources.Load("money/" + imageName, typeof(Sprite)) as Sprite;
        GetComponent<SpriteRenderer>().sprite = s;
    }

    public static int getAmountForType(CoinType type) {
        switch (type) { 
            case CoinType.Bronze:
                return 1;
            case CoinType.Silver:
                return 2;
            case CoinType.Gold:
                return 5;
            default:
                return 1;
        }
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.tag == "Player")
        {
            int amount = getAmount();
            coll.gameObject.GetComponent<PlayerController>().addMoney(amount);
            spawnGui(amount);
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
        return baseAmount;
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}
