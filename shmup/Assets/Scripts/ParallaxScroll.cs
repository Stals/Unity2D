using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class ParallaxScroll : MonoBehaviour {

    public Renderer image_1;
    public float speed_1 = 0.02f;

    public Renderer image_2;
    public float speed_2 = 0.06f;

    public Renderer image_3;
    public float speed_3 = 0.1f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        float offset_1 = Time.timeSinceLevelLoad * speed_1;
        float offset_2 = Time.timeSinceLevelLoad * speed_2;
        float offset_3 = Time.timeSinceLevelLoad * speed_3;
        
        if(image_1 != null)
        image_1.material.mainTextureOffset = new Vector2(offset_1, 0);

        if (image_2 != null)
        image_2.material.mainTextureOffset = new Vector2(offset_2, 0);

        if (image_3 != null)
        image_3.material.mainTextureOffset = new Vector2(offset_3, 0);
	}
}
