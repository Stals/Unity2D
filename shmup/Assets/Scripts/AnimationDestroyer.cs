using UnityEngine;
using System.Collections;

public class AnimationDestroyer : MonoBehaviour {
	
	public float timeBeforeDestroy = 0.2f;
	
	// Use this for initialization
	void Start () {

		AudioSource audio = GetComponent<AudioSource>();
		if (audio != null) {
			//audio.pitch = Random.Range(0.9f, 1.1f);
			audio.Play ();
			StartCoroutine (disableRenderDelayed ());
			Destroy (gameObject, timeBeforeDestroy); //waits till audio is finished playing before destroying.

		} else {
			StartCoroutine (destr ()); //this will run your timer
		}
		
	}
	
	
	IEnumerator destr() {
		yield return new WaitForSeconds(timeBeforeDestroy); //this will wait 5 seconds
		Destroy(gameObject);
	}


	IEnumerator disableRenderDelayed() {
		yield return new WaitForSeconds(timeBeforeDestroy); //this will wait 5 seconds
		renderer.enabled = false;
    }
    
}