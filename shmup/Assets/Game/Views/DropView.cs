using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UniRx;


public partial class DropView {

    void OnTriggerEnter2D(Collider2D coll)
    {
        ExecutePickUp();
        Destroy(this.gameObject);
    }

    //bool seen = false;
    /*
    void FixedUpdate()
    {
        if (renderer.isVisible)
            seen = true;

        if (seen && !renderer.isVisible)
            Destroy(gameObject);
    }*/
}
