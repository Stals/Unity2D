using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UniRx;


public partial class UpgradeView { 

    /// Invokes UpgradeExecuted when the Upgrade command is executed.
    public override void UpgradeExecuted() {
        base.UpgradeExecuted();

        audio.Play();
    }
 

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
    UIButton button;

    public override void Awake()
    {
        base.Awake();

        priceLabel = GetComponentInChildren<UILabel>();
        button = GetComponent<UIButton>();

        button.isEnabled = false;
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

    void FixedUpdate()
    {
        button.isEnabled = isEnough();
    }

  
}
