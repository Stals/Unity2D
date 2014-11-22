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

		++player.parts;

		// TODO - do this after small delay

		if(player.parts >= MAX_PARTS){
			++player.multiplayer;
			player.parts -= MAX_PARTS;
		}
		base.AddMultiplayerPart (player);
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
