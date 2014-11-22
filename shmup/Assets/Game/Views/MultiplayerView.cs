using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UniRx;


public partial class MultiplayerView { 

    private const int MAX_PARTS = 5;

    /// Subscribes to the property and is notified anytime the value changes.
    public override void multiplayerChanged(Int32 value) {
        //base.multiplayerChanged(value);

		Multiplayer.text = "X" + value.ToString ();
    }

    /// Subscribes to the property and is notified anytime the value changes.
    public override void partsChanged(Int32 value) {
        if (value > MAX_PARTS) {
            return;
        }

        if (value == 0)
        {
            for (int i = 0; i < MAX_PARTS; ++i)
            {
                setPartSelected(i, false);
            }
        }
        else
        {
            setPartSelected(value - 1, true);
        }
    }

    void setPartSelected(int index, bool selected) {
        UISprite part = (currentParts.GetChild(index).gameObject).GetComponent<UISprite>();
        if (selected)
        {
            part.spriteName = "part_selected";
        }
        else
        {
            part.spriteName = "part_normal";
        }
    }

	[SerializeField]
	UILabel Multiplayer;

	[SerializeField]
	UIGrid currentParts; 
}
