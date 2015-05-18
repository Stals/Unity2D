using UnityEngine;
using System.Collections;

public class SideWalls : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D hitInfo)
	{
		if(hitInfo.tag == gameObject.tag) {
			Rigidbody2D r = hitInfo.GetComponent<Rigidbody2D>();


			r.velocity = new Vector2(-r.velocity.x, r.velocity.y);
		}else{

		
			audio.Play();
			GameManager.Score(transform.name);
			GameManager.SpawnBall();

			hitInfo.gameObject.SendMessage("ResetBall");
		}
	}
}
