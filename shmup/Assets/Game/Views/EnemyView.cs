using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UniRx;


public partial class EnemyView {

    [SerializeField]
    GameObject explosionPrefab;


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

        DestroySelf();
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

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.tag == "Player") {
            coll.gameObject.GetComponent<PlayerView>().ExecuteTakeDamage(1);
            DestroySelf();
        }
    }

    void DestroySelf()
    {
        createExplosion();
        Destroy(this.gameObject);
    }

    void createExplosion()
    {
        GameObject expl = (GameObject)(Instantiate(explosionPrefab,
                                                   new Vector3(transform.position.x, transform.position.y, 0),
                                                   transform.rotation));
    }
}
