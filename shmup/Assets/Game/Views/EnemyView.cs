using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UniRx;


public partial class EnemyView {

    void FixedUpdate()
    {
        transform.Translate(-Enemy.movementSpeed, 0, 0);

        /*if (!renderer.isVisible)
        {
            Destroy(gameObject);
        }*/
    }

    /// Invokes TakeDamageExecuted when the TakeDamage command is executed.
    public override void TakeDamageExecuted() {
        base.TakeDamageExecuted();

        Destroy(gameObject);
    }

}
