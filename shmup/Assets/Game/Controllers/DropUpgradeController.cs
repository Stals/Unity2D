using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;


public class DropUpgradeController : DropUpgradeControllerBase {
    
    public override void InitializeDropUpgrade(DropUpgradeViewModel dropUpgrade) {
    }

    public override void Upgrade(UpgradeViewModel upgrade)
    {
        base.Upgrade(upgrade);

        GameSceneManager.player.Player.spawnChance += 0.05f;
    }
}
