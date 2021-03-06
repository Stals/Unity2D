using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;

public class PlayerController : PlayerControllerBase {
    
	private const int MAX_PARTS = 5;

    bool startedTimer = false;

    public override void InitializePlayer(PlayerViewModel player) {
    } 

	public override void AddMultiplayerPart (PlayerViewModel player)
	{
        // means we dissposed it, by adding another part 
        if (startedTimer) {
            processParts(player);
        }

        base.AddMultiplayerPart(player);

		++player.parts;

        startedTimer = true;
        Observable.Timer(TimeSpan.FromMilliseconds(250)).Subscribe(l =>
            {
                startedTimer = false;
                processParts(player);
            }).DisposeWhenChanged(player.partsProperty);		
	}

    void processParts(PlayerViewModel player)
    {
        if (player.parts >= MAX_PARTS)
        {
            ++player.multiplayer;
            player.parts -= MAX_PARTS;
        }
    }

    public override void onProgressBarEmpty(PlayerViewModel player)
    {
        if (player.multiplayer != 1)
        {
            if (player.parts == 0)
            {
                player.multiplayer -= 1;
            }
        } 

        player.parts = 0;



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

        int addMoney = (arg * player.multiplayer);

        player.money += addMoney;
        player.score += addMoney;
    }

    public override void TakeDamage(EntityViewModel entity, int arg)
    {
        base.TakeDamage(entity, arg);

        if(GameSceneManager.player.Player.IsInvurnalable){
            return;
        }

        // TODO find better way without breaking OOP stuff
        GameSceneManager.player.Player.health -= arg;

        GameSceneManager.world.cameraShake.Shake(0.4f, 0.015f);


        GameSceneManager.player.Player.IsInvurnalable = true;
        Observable.Timer(TimeSpan.FromMilliseconds(1000)).Subscribe(l =>
        {
            GameSceneManager.player.Player.IsInvurnalable = false;
        });	
        
    }
}
