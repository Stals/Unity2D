using UnityEngine;
using System.Collections;

public class WorldObject : MonoBehaviour {

    [SerializeField]
    float speed = 0.02f;

    [SerializeField]
    GameObject enemyPrefab;

    Transform _transform;

	// Use this for initialization
	void Start () {
        _transform = transform;

        InvokeRepeating("SpawnEnemy", 1f, 0.5f);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void FixedUpdate()
    {
        _transform.position = new Vector3(_transform.position.x - speed, _transform.position.y);
    }

    void SpawnEnemy()
    {
        //Vector3 size= Camera.main.WorldToScreenPoint(new Vector3(Screen.width, Screen.height));
        //float halfHeight = size.y / 2;
        float halfHeight = 4f;

        GameObject enemy = (GameObject)Instantiate(enemyPrefab, new Vector3(10f, UnityEngine.Random.Range(-halfHeight, halfHeight)), transform.rotation);
        enemy.transform.parent = transform;
    }
}
