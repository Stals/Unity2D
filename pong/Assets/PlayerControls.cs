using UnityEngine;
using System.Collections;

public class PlayerControls : MonoBehaviour {

	public KeyCode moveUp;
	public KeyCode moveDown;

	public float speed = 10;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		updateMovment();
	}

	void updateMovment(){
		Vector2 v = new Vector2(0f, 0f);
		
		if (Input.GetKey (moveUp)) 
		{
			v.y = speed;
		}
		else if (Input.GetKey (moveDown)) 
		{
			v.y = -speed;
		}
		else 
		{
			v.y = 0;
		}
		rigidbody2D.velocity = v;

	}
}
