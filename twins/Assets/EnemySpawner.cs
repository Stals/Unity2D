using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {

    [SerializeField]
    NewWaveAnimationController newWaveAnimationController;

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
    }
}
