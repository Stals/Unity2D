using UnityEngine;
using System.Collections;

public class MyGameManager : MonoBehaviour {

    public CameraShake cameraShake;

    [SerializeField]
    public VectorGrid grid;

    [SerializeField]
    public PlayerController playerController;

	// Use this for initialization
	void Start () {
        Game.Instance.init(this);

        cameraShake = Camera.main.gameObject.AddComponent("CameraShake") as CameraShake;
	}
	
	// Update is called once per frame
	void Update () {
        
	}

    public void sleepTime(float ms)
    {
        StartCoroutine(sleep(ms));
    }


    IEnumerator sleep(float ms)
    {
        Time.timeScale = 0.0f;
        float pauseEndTime = Time.realtimeSinceStartup + (ms / 1000f);
        while (Time.realtimeSinceStartup < pauseEndTime)
        {
            yield return 0;
        }
        Time.timeScale = 1;
    }
}
