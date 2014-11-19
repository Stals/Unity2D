using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UniRx;


public partial class MultiplayerView { 

    /// Subscribes to the property and is notified anytime the value changes.
    public override void multiplayerChanged(Int32 value) {
        //base.multiplayerChanged(value);

		Multiplayer.text = "X" + value.ToString ();
    }

    /// Subscribes to the property and is notified anytime the value changes.
    public override void partsChanged(Int32 value) {
        //base.partsChanged(value);

		currentParts.text = value.ToString ();
    }

	[SerializeField]
	UILabel Multiplayer;

	[SerializeField]
	UILabel currentParts; // TODO to be replaced
}
