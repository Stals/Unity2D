using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WorldObject : MonoBehaviour {

    [SerializeField]
    ParallaxScroll parralax;

    [SerializeField]
    float speed = 0.02f;

    [SerializeField]
    float SpeedIncreaseDelay = 1f;

    [SerializeField]
    float SpeedIncreaseAmount = 0.005f;

    [SerializeField]
    float SpawnPatternDelay = 5f;

    [SerializeField]
    float SpawnEnemyDelay = 0.5f;

    [SerializeField]
    GameObject enemyPrefab;

    [SerializeField]
    float enemySpread = 20f;

    [SerializeField]
    float enemySpeadSpread = 0.025f;

    Transform _transform;

    [SerializeField]
    List<GameObject> coins;

    [SerializeField]
    List<float> coinsProb;

    [SerializeField]
    List<GameObject> patternPrefab;

    public CameraShake cameraShake;

    public float scaleVar = 0.01f;

	// Use this for initialization
	void Start () {
        GameSceneManager.world = this;

        cameraShake = Camera.main.gameObject.AddComponent("CameraShake") as CameraShake;

        _transform = transform;

        InvokeRepeating("IncreaseSpeed", SpeedIncreaseDelay, SpeedIncreaseDelay);

        Invoke("SpawnEnemy", 1f);

        //nvokeRepeating("SpawnPattern", 1f, 5f);
        Invoke("SpawnPattern", 5f);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void IncreaseSpeed()
    {
        scaleVar += 0.01f;

        speed += SpeedIncreaseAmount;

        parralax.speed_1 = speed * 4;
        parralax.speed_2 = speed * 8;
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

        enemy.transform.localEulerAngles = new Vector3(0, 0, Random.Range(-enemySpread, enemySpread));

        EnemyView enemyView = enemy.GetComponent<EnemyView>();
        enemyView.Enemy.movementSpeed += Random.Range(-enemySpeadSpread, enemySpeadSpread);

        Invoke("SpawnEnemy", SpawnEnemyDelay - (scaleVar / 2));
    }

    public GameObject randomCoin()
    {
        if (coins.Count != coinsProb.Count)
        {
            Debug.Log("ERROR: coins wrong size");
        }

        float p = UnityEngine.Random.Range(0, 1f);

        float totalP = 0;
        for (int i = 0; i < coins.Count; ++i)
        {
            totalP += coinsProb[i];
            if (p < totalP)
            {
                return coins[i];
            }
        }

        Debug.Log("Error randomCoin()");
        return coins[0];
    }

    public GameObject RandomPattern()
    {
        int rndID = Random.Range(0, patternPrefab.Count);
        return patternPrefab[rndID];
    }

    public void SpawnPattern()
    {
        GameObject patternPrefab = RandomPattern();

        GameObject pattern = (GameObject)Instantiate(patternPrefab);
        // 5 units per 0.12 speed
        pattern.transform.position = new Vector3((speed / 0.12f) * 5f, 0, 0);
        pattern.transform.parent = transform;
        pattern.SetActive(true);        

        Invoke("SpawnPattern", SpawnPatternDelay - scaleVar);
    }
}
