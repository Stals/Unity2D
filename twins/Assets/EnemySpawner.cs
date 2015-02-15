using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {

    [SerializeField]
    NewWaveAnimationController newWaveAnimationController;

    [SerializeField]
    GameObject newWaveForcePosition;

    int currentWave = 1;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
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
