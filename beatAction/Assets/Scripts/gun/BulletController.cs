using UnityEngine;
using System.Collections;

// TODO self destruct if off screen
public class BulletController : MonoBehaviour {

    public Vector3 speed = new Vector3(0.2f, 0, 0);
   
    bool seen = false;
    

	// Use this for initialization
	void Start () {
        //if(
        //speed.x = transform.rotation.x
        AudioSource audio = GetComponent<AudioSource>();
        audio.pitch = Random.Range(0.9f, 1.3f);
        audio.Play();

	}
	
	// Update is called once per frame
	void Update () {
        if (renderer.isVisible)
            seen = true;
        
        if (seen && !renderer.isVisible)
            Destroy(gameObject);
	}

    void FixedUpdate()
    {
        updateMovment();
    }

    void updateMovment()
    {
        Vector3 currentPosition = this.transform.position;
        this.transform.position = currentPosition + mult(transform.rotation, speed);
    }

    void OnCollisionEnter2D(Collision2D coll) {
        /*if (coll.gameObject.tag != this.gameObject.tag)
        {
            Destroy (gameObject);
            //Destroy(coll.gameObject);
        }*/
    }

    public static Vector3 mult(Quaternion quat, Vector3 vec){
        float num = quat.x * 2f;
        float num2 = quat.y * 2f;
        float num3 = quat.z * 2f;
        float num4 = quat.x * num;
        float num5 = quat.y * num2;
        float num6 = quat.z * num3;
        float num7 = quat.x * num2;
        float num8 = quat.x * num3;
        float num9 = quat.y * num3;
        float num10 = quat.w * num;
        float num11 = quat.w * num2;
        float num12 = quat.w * num3;
        Vector3 result;
        result.x = (1f - (num5 + num6)) * vec.x + (num7 - num12) * vec.y + (num8 + num11) * vec.z;
        result.y = (num7 + num12) * vec.x + (1f - (num4 + num6)) * vec.y + (num9 - num10) * vec.z;
        result.z = (num8 - num11) * vec.x + (num9 + num10) * vec.y + (1f - (num4 + num5)) * vec.z;
        return result;
    }
}

