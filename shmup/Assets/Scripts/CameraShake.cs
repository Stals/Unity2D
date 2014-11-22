using UnityEngine;
using System.Collections;
public class CameraShake : MonoBehaviour
{
	private Vector3 originPosition;
	private Quaternion originRotation;
	public float shake_decay;
	public float shake_intensity;
	
	/* public float set_intensity = .05f;
    public float set_decay = 0.02f;

    void OnGUI (){
        if (GUI.Button (new Rect (20,40,80,20), "Shake")){
            Shake ();
        }
    }*/
	
	void Update (){
		if (shake_intensity > 0){
            Vector3 newPos = originPosition + Random.insideUnitSphere * shake_intensity;
            newPos.z = -10;
            transform.position = newPos;
			//transform.position = new Vector3(transform.position.x, transform.position.y, originPosition.z);
			
			/*transform.rotation = new Quaternion(
                originRotation.x + Random.Range (-shake_intensity,shake_intensity) * .2f,
                originRotation.y + Random.Range (-shake_intensity,shake_intensity) * .2f,
                originRotation.z ,
                originRotation.w );*/
			shake_intensity -= shake_decay;
		}
	}
	
	public void Shake(float inten, float decay){
		if (shake_intensity <= 0)
		{
            originPosition = transform.position;
            originRotation = transform.rotation;

            originPosition.z = -10;

			//originRotation = tile.transform.rotation;
		}
		shake_intensity = inten;
		shake_decay = decay;
	}
	
	public void stopShake()
	{
		transform.position = originPosition;
		transform.rotation = originRotation;
		
		shake_intensity = 0;
		shake_decay = 0;
	}
}