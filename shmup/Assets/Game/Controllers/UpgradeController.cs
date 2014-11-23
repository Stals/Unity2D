using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;


public class UpgradeController : UpgradeControllerBase {
    
    public override void InitializeUpgrade(UpgradeViewModel upgrade) {
        upgrade.price = upgrade.basePrice;
    }

    public override void Upgrade(UpgradeViewModel upgrade)
    {
        base.Upgrade(upgrade);

        GameSceneManager.player.Player.money -= upgrade.price;
        upgrade.level += 1;

        upgrade.price = upgrade.price * 2;
    }
}
