using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UniRx;


public partial class EnemyView {


    void FixedUpdate()
    {
        transform.Translate(-Enemy.movementSpeed, 0, 0);

        /*if (!renderer.isVisible)
        {
            Destroy(gameObject);
        }*/
    }

    /// Invokes TakeDamageExecuted when the TakeDamage command is executed.
    public override void TakeDamageExecuted() {
        base.TakeDamageExecuted();

        if (dropLoot()) {
            spawn(GameSceneManager.world.randomCoin());
        }

        Destroy(gameObject);
    }

    bool dropLoot()
    {
        float p = UnityEngine.Random.Range(0, 1f);
        return p < (GameSceneManager.player.Player.spawnChance + Enemy.spawnChance);
    }

    void spawn(GameObject go)
    {
        GameObject newGo = (GameObject)Instantiate(go, transform.position, Quaternion.identity);
        newGo.transform.parent = GameSceneManager.world.transform;
    }

}
