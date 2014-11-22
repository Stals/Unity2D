using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public partial class PlayerViewModel {

    public override bool ComputeisLowestMultiplier()
    {
        return (multiplayer == 1) && (parts == 0);
    }
}
