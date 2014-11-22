// ------------------------------------------------------------------------------
//  <autogenerated>
//      This code was generated by a tool.
//      Mono Runtime Version: 2.0.50727.1433
// 
//      Changes to this file may cause incorrect behavior and will be lost if 
//      the code is regenerated.
//  </autogenerated>
// ------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;


public abstract class PlayerControllerBase : EntityController {
    
    public abstract void InitializePlayer(PlayerViewModel player);
    
    public override ViewModel CreateEmpty() {
        return new PlayerViewModel(this);
    }
    
    public virtual PlayerViewModel CreatePlayer() {
        return ((PlayerViewModel)(this.Create()));
    }
    
    public override void Initialize(ViewModel viewModel) {
        base.Initialize(viewModel);
        this.InitializePlayer(((PlayerViewModel)(viewModel)));
    }
    
    public virtual void AddMultiplayerPart(PlayerViewModel player) {
    }
    
    public virtual void AddScore(PlayerViewModel player, Int32 arg) {
    }
    
    public virtual void AddMoney(PlayerViewModel player, Int32 arg) {
    }
}

public abstract class EntityControllerBase : Controller {
    
    public abstract void InitializeEntity(EntityViewModel entity);
    
    public virtual EntityViewModel CreateEntity() {
        return ((EntityViewModel)(this.Create()));
    }
    
    public override void Initialize(ViewModel viewModel) {
        this.InitializeEntity(((EntityViewModel)(viewModel)));
    }
    
    public virtual void TakeDamage(EntityViewModel entity, Int32 arg) {
    }
}

public abstract class EnemyControllerBase : EntityController {
    
    public abstract void InitializeEnemy(EnemyViewModel enemy);
    
    public override ViewModel CreateEmpty() {
        return new EnemyViewModel(this);
    }
    
    public virtual EnemyViewModel CreateEnemy() {
        return ((EnemyViewModel)(this.Create()));
    }
    
    public override void Initialize(ViewModel viewModel) {
        base.Initialize(viewModel);
        this.InitializeEnemy(((EnemyViewModel)(viewModel)));
    }
}

public abstract class BulletControllerBase : Controller {
    
    public abstract void InitializeBullet(BulletViewModel bullet);
    
    public override ViewModel CreateEmpty() {
        return new BulletViewModel(this);
    }
    
    public virtual BulletViewModel CreateBullet() {
        return ((BulletViewModel)(this.Create()));
    }
    
    public override void Initialize(ViewModel viewModel) {
        this.InitializeBullet(((BulletViewModel)(viewModel)));
    }
}

public abstract class DropControllerBase : Controller {
    
    public abstract void InitializeDrop(DropViewModel drop);
    
    public override ViewModel CreateEmpty() {
        return new DropViewModel(this);
    }
    
    public virtual DropViewModel CreateDrop() {
        return ((DropViewModel)(this.Create()));
    }
    
    public override void Initialize(ViewModel viewModel) {
        this.InitializeDrop(((DropViewModel)(viewModel)));
    }
    
    public virtual void PickUp(DropViewModel drop) {
    }
}

public abstract class CoinDropControllerBase : DropController {
    
    public abstract void InitializeCoinDrop(CoinDropViewModel coinDrop);
    
    public override ViewModel CreateEmpty() {
        return new CoinDropViewModel(this);
    }
    
    public virtual CoinDropViewModel CreateCoinDrop() {
        return ((CoinDropViewModel)(this.Create()));
    }
    
    public override void Initialize(ViewModel viewModel) {
        base.Initialize(viewModel);
        this.InitializeCoinDrop(((CoinDropViewModel)(viewModel)));
    }
}

public abstract class MultiplierDropControllerBase : DropController {
    
    public abstract void InitializeMultiplierDrop(MultiplierDropViewModel multiplierDrop);
    
    public override ViewModel CreateEmpty() {
        return new MultiplierDropViewModel(this);
    }
    
    public virtual MultiplierDropViewModel CreateMultiplierDrop() {
        return ((MultiplierDropViewModel)(this.Create()));
    }
    
    public override void Initialize(ViewModel viewModel) {
        base.Initialize(viewModel);
        this.InitializeMultiplierDrop(((MultiplierDropViewModel)(viewModel)));
    }
}
