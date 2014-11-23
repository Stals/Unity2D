using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;


public class FireElementController : FireElementControllerBase {
    
    public override void InitializeFireElement(FireElementViewModel fireElement) {
    }

    public override void Upgrade(UpgradeViewModel upgrade)
    {
        base.Upgrade(upgrade);

        GameSceneManager.player.Player.shotDelay -= 25;
    }
}
