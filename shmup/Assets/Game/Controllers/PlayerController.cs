using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;

public class PlayerController : PlayerControllerBase {
    
	private const int MAX_PARTS = 5;

    public override void InitializePlayer(PlayerViewModel player) {
    } 

	public override void AddMultiplayerPart (PlayerViewModel player)
	{
        base.AddMultiplayerPart(player);

		++player.parts;

        Observable.Timer(TimeSpan.FromMilliseconds(250)).Subscribe(l =>
            {
                if (player.parts >= MAX_PARTS)
                {
                    ++player.multiplayer;
                    player.parts -= MAX_PARTS;
                }
            });		
	}

    public override void onProgressBarEmpty(PlayerViewModel player)
    {
        if (player.parts != 0) {
            player.parts = 0;

            //if (player.multiplayer != 1)
            //{
                player.multiplayer -= 1;
            //}
        }
        base.onProgressBarEmpty(player);
    }

    public override void Shoot(PlayerViewModel player)
    {
        base.Shoot(player);

        player.canShoot = false;

        Observable.Timer(TimeSpan.FromMilliseconds(player.shotDelay)).Subscribe(l =>
        {
            player.canShoot = true;
        });		
    }

    public override void AddMoney(PlayerViewModel player, int arg)
    {
        base.AddMoney(player, arg);

        player.money += (arg * player.multiplayer);
    }

    public override void TakeDamage(EntityViewModel entity, int arg)
    {
        base.TakeDamage(entity, arg);

        // TODO find better way without breaking OOP stuff
        GameSceneManager.player.Player.health -= arg;

        //GameSceneManager.world.cameraShake.Shake(0.1f, 0.02f);
        GameSceneManager.world.cameraShake.Shake(0.3f, 0.02f);
    }
}
