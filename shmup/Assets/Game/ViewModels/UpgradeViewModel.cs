using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public partial class UpgradeViewModel {

    public override bool ComputeisEnough()
    {
        if (GameSceneManager.player != null) {
            return GameSceneManager.player.Player.money >= price;
        }
        else { 
            return false; 
        }
        
    }
}
