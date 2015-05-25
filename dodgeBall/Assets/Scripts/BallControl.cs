using UnityEngine;
using System.Collections;

public class BallControl : MonoBehaviour {

	[SerializeField]
	Color trailColor;

    Vector2 startingSpeed = new Vector2(25, 10);
	

	// Use this for initialization
	void Start() {
		TrailRenderer trail = GetComponent<TrailRenderer>();
		//trail.colors[0] = trailColor;
		
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
            rigidbody2D.AddForce(startingSpeed);
		} else {
            rigidbody2D.AddForce(-startingSpeed);
		}
	}

	void ResetBall()
	{
		Destroy(this, 1f);
		//rigidbody2D.velocity = new Vector2(0, 0);
		//rigidbody2D.position = new Vector2(0, 0);

		//StartDelaied(0.5f);
	}

	void OnCollisionEnter2D (Collision2D colInfo) {
		int i = 0;
		/*if(colInfo.collider.tag == "Player") {
			//rigidbody2D.velocity.y = rigidbody2D.velocity.y / 2 + colInfo.collider.rigidbody2D.velocity.y / 3;
			rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x + , rigidbody2D.velocity.y / 2 + colInfo.collider.rigidbody2D.velocity.y / 3);
		}*/
		//if(colInfo.collider.rigidbody2D != null){
		//	rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x + colInfo.collider.rigidbody2D.velocity.x, rigidbody2D.velocity.y / 2 + colInfo.collider.rigidbody2D.velocity.y / 3);
		//}
		
		//rigidbody2D.AddRelativeForce
		
		//float speedFactor = 3f;
		
		//rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x * speedFactor, rigidbody2D.velocity.y * speedFactor);
		
		/*
		нужно добаивть сокрости в направлении от колизии - что он сам сдеает
		нужно сделать чтобы скорость была дофига большая но не на долго
		можно на шарик навесить провеку скорости
		и если скорость больше определенной то сбрасывать ее
		 
		а при колизии с падлом - но пока со всем - увеличивать ее скорость в несколько раз например
		*/
		
		audio.pitch = Random.Range(0.8f, 1.2f);
		audio.Play();
	}
	
	void FixedUpdate()
	{
		/*float maxSpeed = 100f;
		float slowFactor = 0.1f;
		
			Vector2 newVel = rigidbody2D.velocity;
		
			if(rigidbody2D.velocity.x > maxSpeed || rigidbody2D.velocity.x < -maxSpeed){
				newVel.x *= slowFactor;
				rigidbody2D.velocity = Vector2.zero;
			}
			
			if(rigidbody2D.velocity.y > maxSpeed || rigidbody2D.velocity.y < -maxSpeed){
				newVel.y *= slowFactor;
				rigidbody2D.velocity = Vector2.zero;
			}
			
			чет не работает
			
			//rigidbody2D.velocity = Vector2.zero;// newVel;
			*/
	}
}


