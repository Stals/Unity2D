using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour {

    [SerializeField]
    NewWaveAnimationController newWaveAnimationController;

    [SerializeField]
    GameObject newWaveForcePosition;

    // used to easily get positions for spawning
    [SerializeField]
    GameObject corner;

    [SerializeField]
    GameObject normalSpawns;

    [SerializeField]
    GameObject screenSpawns;

    float width; float height;

    int currentWave = 1;

	// Use this for initialization
	void Start () {
        width = corner.transform.position.x;
        height = corner.transform.position.y;

        InvokeRepeating("spawnRandomNormalPattern", 5f, 5f);
        InvokeRepeating("spawnRandomScreenPattern", 15f, 20f);
	}
	
	// Update is called once per frame
	void Update () {
	}


    Vector2 getRandomPositionAround()
    {
        float perimeter = width * 2 + height * 2;
        float rnd = Random.Range(0f, perimeter);

        float x, y = 0;

        if (rnd <= width)
        {
            x = Random.Range(-width, width);
            y = height;
        }
        else if (rnd <= width + height)
        {
            x = width;
            y = Random.Range(-height, height);
        }
        else if (rnd <= (width * 2) + height)
        {
            x = Random.Range(-width, width);
            y = -height;
        }
        else
        {
            x = -width;
            y = Random.Range(-height, height);
        }

        return new Vector2(x, y);

    }

    public void spawnRandomNormalPattern()
    {
        GameObject pattern = getRandomChild(normalSpawns);
        GameObject enemy = (GameObject)Instantiate(pattern, getRandomPositionAround(), transform.rotation);
        enemy.SetActive(true);
    }

    public void spawnRandomScreenPattern()
    {
        GameObject pattern = getRandomChild(screenSpawns);
        GameObject enemy = (GameObject)Instantiate(pattern, new Vector3(0, 0), transform.rotation);
        enemy.SetActive(true);
    }

    GameObject getRandomChild(GameObject parent)
    {
        List<GameObject> children = new List<GameObject>();

        foreach (Transform child in parent.transform)
         {
             children.Add(child.gameObject);             
         }

        int rndID = Random.Range(0, children.Count);
        return children[rndID];
    }
    
    public void onWaveEnded()
    {
        ++currentWave;

        Game.Instance.getPlayer().weaponSlot.chooseRandomWeapon();
        newWaveAnimationController.showNewWave(currentWave);


        MyGameManager manager = Game.Instance.getManager();
        if (manager)
        {
            manager.grid.AddGridForce(newWaveForcePosition.transform.position, 0.15f, 0.5f, Color.green, false);
        }        
    }
}
