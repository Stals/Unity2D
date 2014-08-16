using UnityEngine;
using System.Collections;
using Vectrosity;

public class Rotator : MonoBehaviour {

	public float speedPerTick = 10f;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void FixedUpdate()
	{
		GetComponent<Elipse1>.pointRotation += speedPerTick;
	}
}
