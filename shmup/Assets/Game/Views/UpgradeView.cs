using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UniRx;


public partial class UpgradeView { 

    /// Subscribes to the property and is notified anytime the value changes.
    public override void basePriceChanged(Int32 value) {
        base.basePriceChanged(value);

        Upgrade.price = Upgrade.basePrice;
    }
 

    /// Subscribes to the property and is notified anytime the value changes.
    public override void priceChanged(Int32 value) {
        base.priceChanged(value);
        priceLabel.text = value.ToString();
    }

    UILabel priceLabel;

    public override void Awake()
    {
        base.Awake();

        priceLabel = GetComponentInChildren<UILabel>();
    }

    public void purchase()
    {
        if (isEnough()) {
            ExecuteUpgrade();
        }
    }

    bool isEnough()
    {
        return GameSceneManager.player.Player.money >= Upgrade.price;
    }
}