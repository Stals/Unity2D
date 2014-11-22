using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UniRx;


public partial class EnemyView { 

    /// Invokes TakeDamageExecuted when the TakeDamage command is executed.
    public override void TakeDamageExecuted() {
        base.TakeDamageExecuted();

        Destroy(gameObject);
    }

}
