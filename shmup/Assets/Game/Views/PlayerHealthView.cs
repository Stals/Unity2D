using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UniRx;


public partial class PlayerHealthView { 

    /// Subscribes to the property and is notified anytime the value changes.
    public override void healthChanged(Int32 value) {
        base.healthChanged(value);

        int size = grid.GetChildList().size;

        while (value > size) {
            NGUITools.AddChild(grid.gameObject, healthPrefab);
            grid.Reposition();
            ++size;
        }

        while (value < size && (size > 0)) {
            Transform t = grid.GetChild(0);
            grid.RemoveChild(t);
            NGUITools.Destroy(t.gameObject);
            --size;
        }
    }


    [SerializeField]
    GameObject healthPrefab;

    UIGrid grid;

    public override void Awake()
    {
        base.Awake();

        grid = GetComponent<UIGrid>();
    }

}
