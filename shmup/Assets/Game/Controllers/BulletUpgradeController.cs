using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;


public class BulletUpgradeController : BulletUpgradeControllerBase {
    
    public override void InitializeBulletUpgrade(BulletUpgradeViewModel bulletUpgrade) {
    }

    public override void Upgrade(UpgradeViewModel upgrade)
    {
        base.Upgrade(upgrade);

        GameSceneManager.player.Player.bulletsPerShot += 1;
    }
}
