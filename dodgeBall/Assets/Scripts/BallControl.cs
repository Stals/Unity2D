using UnityEngine;
using System.Collections;

public class BallControl : MonoBehaviour {

	// Use this for initialization
	void Start() {
		//StartCoroutine(StartDelaied());
		StartDelaied(2);
	}

	/*IEnumerator StartDelaied()
	{
		Debug.Log("StartDelaied");
		yield return new WaitForSeconds(2);
		GoBall();
	}
	 */
	void StartDelaied(float delay)
	{
		Invoke("GoBall", delay);
	}

	void GoBall()
	{
		int rnd = Random.Range(0, 2);
		
		if(rnd == 0) {
			rigidbody2D.AddForce(new Vector2(50, 10));
		} else {
			rigidbody2D.AddForce(new Vector2(-50, -10));
		}
	}

	void ResetBall()
	{
		rigidbody2D.velocity = new Vector2(0, 0);
		rigidbody2D.position = new Vector2(0, 0);

		StartDelaied(0.5f);
	}

	void OnCollisionEnter2D (Collision2D colInfo) {
		/*if(colInfo.collider.tag == "Player") {
			//rigidbody2D.velocity.y = rigidbody2D.velocity.y / 2 + colInfo.collider.rigidbody2D.velocity.y / 3;
			rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x + , rigidbody2D.velocity.y / 2 + colInfo.collider.rigidbody2D.velocity.y / 3);
		}*/
		if(colInfo.collider.rigidbody2D != null){
			rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x + colInfo.collider.rigidbody2D.velocity.x, rigidbody2D.velocity.y / 2 + colInfo.collider.rigidbody2D.velocity.y / 3);
		}
		
		audio.pitch = Random.Range(0.8f, 1.2f);
		audio.Play();
	}
}
