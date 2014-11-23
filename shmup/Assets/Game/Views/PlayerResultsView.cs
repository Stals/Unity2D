using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UniRx;


public partial class PlayerResultsView {

    [SerializeField]
    UILabel scoreLabel;

    [SerializeField]
    UILabel multiplierLabel;

    int maxMultiplier = 0;

    /// Subscribes to the property and is notified anytime the value changes.
    public override void scoreChanged(Int32 value) {
        base.scoreChanged(value);

        scoreLabel.text = "Final Score: " + value.ToString();
    }
    
    /// Subscribes to the property and is notified anytime the value changes.
    public override void multiplayerChanged(Int32 value) {
        base.multiplayerChanged(value);

        if (value > maxMultiplier)
        {
            maxMultiplier = value;
            multiplierLabel.text = "MAX Multiplier: " + maxMultiplier.ToString();
        }
    }
}
