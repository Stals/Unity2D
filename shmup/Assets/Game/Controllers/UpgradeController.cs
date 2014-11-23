using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;


public class UpgradeController : UpgradeControllerBase {
    
    public override void InitializeUpgrade(UpgradeViewModel upgrade) {
        upgrade.price = upgrade.basePrice;
    } 
}
