using UnityEngine;
using System.Collections;

public class PlayerGuiController : MonoBehaviour {

    [SerializeField]
    UILabel moneyLabel;

    int money;
    int newMoney;

	// Use this for initialization
	void Start () {
       // Invoke Repeating("updateMoney", 0.3f, 0.3f);	
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    void FixedUpdate()
    {
        if(newMoney > 0){
            moneyLabel.text = money.ToString() + " + " + newMoney.ToString() + "";
        }else{
            moneyLabel.text = money.ToString();
        }
    }

    void updateMoney()
    {
        money += newMoney;
        newMoney = 0;
    }

    public void addMoney(int m) {
        newMoney += m;

        CancelInvoke("updateMoney");
        Invoke("updateMoney", 2f);
    }
}
