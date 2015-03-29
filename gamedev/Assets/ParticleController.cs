using UnityEngine;
using System.Collections;

public class ParticleController : MonoBehaviour {

	// Use this for initialization
	void Start () {
        audio.pitch = Random.Range(1.2f, 1.5f);
        audio.Play();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
