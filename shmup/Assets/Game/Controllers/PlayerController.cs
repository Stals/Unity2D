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


}
