using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;


public class HealthUpgradeController : HealthUpgradeControllerBase {
    
    public override void InitializeHealthUpgrade(HealthUpgradeViewModel healthUpgrade) {
    }
    public override void Upgrade(UpgradeViewModel upgrade)
    {
        base.Upgrade(upgrade);

        GameSceneManager.player.Player.health += 1;
    }
}
