using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BallSpawner : MonoBehaviour {

	// Use this for initialization
	void Start () {
			
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void SpawnBall()
	{
		BallControl[] balls = GetComponentsInChildren<BallControl>(true);
		GameObject ball = balls[Random.Range(0, balls.Length)].gameObject;
		GameObject go = Instantiate(ball, ball.transform.position, ball.transform.rotation) as GameObject;
		go.SetActive(true);
	}
}
