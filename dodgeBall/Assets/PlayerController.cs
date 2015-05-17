using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	[SerializeField]
	PinController pin_1;

	[SerializeField]
	PinController pin_2;



	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		updateConnection();
	}

	void updateConnection()
	{
		// or line setactive - 
		if(pin_1.isSelected() && pin_2.isSelected()) {
		
		
		}
	}

}
