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
using UnityEngine;
using UniRx;


[DiagramInfoAttribute("Game")]
public abstract class PlayerViewBase : EntityViewBase {
    
    [UFGroup("View Model Properties")]
    [UnityEngine.HideInInspector()]
    public Int32 _multiplayer;
    
    [UFGroup("View Model Properties")]
    [UnityEngine.HideInInspector()]
    public Int32 _parts;
    
    [UFGroup("View Model Properties")]
    [UnityEngine.HideInInspector()]
    public Int32 _money;
    
    [UFGroup("View Model Properties")]
    [UnityEngine.HideInInspector()]
    public Single _movementSpeed;
    
    public override System.Type ViewModelType {
        get {
            return typeof(PlayerViewModel);
        }
    }
    
    public PlayerViewModel Player {
        get {
            return ((PlayerViewModel)(this.ViewModelObject));
        }
        set {
            this.ViewModelObject = value;
        }
    }
    
    public override ViewModel CreateModel() {
        return this.RequestViewModel(GameManager.Container.Resolve<PlayerController>());
    }
    
    protected override void InitializeViewModel(ViewModel viewModel) {
        base.InitializeViewModel(viewModel);
        PlayerViewModel player = ((PlayerViewModel)(viewModel));
        player.multiplayer = this._multiplayer;
        player.parts = this._parts;
        player.money = this._money;
        player.movementSpeed = this._movementSpeed;
    }
    
    public virtual void ExecuteAddMultiplayerPart() {
        this.ExecuteCommand(Player.AddMultiplayerPart);
    }
    
    public virtual void ExecuteAddScore(Int32 arg) {
        this.ExecuteCommand(Player.AddScore, arg);
    }
    
    public virtual void ExecuteAddMoney(Int32 arg) {
        this.ExecuteCommand(Player.AddMoney, arg);
    }
}

[DiagramInfoAttribute("Game")]
public abstract class EntityViewBase : ViewBase {
    
    public override System.Type ViewModelType {
        get {
            return typeof(EntityViewModel);
        }
    }
    
    public EntityViewModel Entity {
        get {
            return ((EntityViewModel)(this.ViewModelObject));
        }
        set {
            this.ViewModelObject = value;
        }
    }
    
    public override ViewModel CreateModel() {
        return this.RequestViewModel(GameManager.Container.Resolve<EntityController>());
    }
    
    protected override void InitializeViewModel(ViewModel viewModel) {
    }
    
    public virtual void ExecuteTakeDamage(Int32 arg) {
        this.ExecuteCommand(Entity.TakeDamage, arg);
    }
}

[DiagramInfoAttribute("Game")]
public abstract class EnemyViewBase : EntityViewBase {
    
    public override System.Type ViewModelType {
        get {
            return typeof(EnemyViewModel);
        }
    }
    
    public EnemyViewModel Enemy {
        get {
            return ((EnemyViewModel)(this.ViewModelObject));
        }
        set {
            this.ViewModelObject = value;
        }
    }
    
    public override ViewModel CreateModel() {
        return this.RequestViewModel(GameManager.Container.Resolve<EnemyController>());
    }
    
    protected override void InitializeViewModel(ViewModel viewModel) {
        base.InitializeViewModel(viewModel);
    }
}

[DiagramInfoAttribute("Game")]
public abstract class BulletViewBase : ViewBase {
    
    [UFGroup("View Model Properties")]
    [UnityEngine.HideInInspector()]
    public Single _speed;
    
    public override System.Type ViewModelType {
        get {
            return typeof(BulletViewModel);
        }
    }
    
    public BulletViewModel Bullet {
        get {
            return ((BulletViewModel)(this.ViewModelObject));
        }
        set {
            this.ViewModelObject = value;
        }
    }
    
    public override ViewModel CreateModel() {
        return this.RequestViewModel(GameManager.Container.Resolve<BulletController>());
    }
    
    protected override void InitializeViewModel(ViewModel viewModel) {
        BulletViewModel bullet = ((BulletViewModel)(viewModel));
        bullet.speed = this._speed;
    }
}

[DiagramInfoAttribute("Game")]
public abstract class DropViewBase : ViewBase {
    
    [UFGroup("View Model Properties")]
    [UnityEngine.HideInInspector()]
    public Int32 _amount;
    
    public override System.Type ViewModelType {
        get {
            return typeof(DropViewModel);
        }
    }
    
    public DropViewModel Drop {
        get {
            return ((DropViewModel)(this.ViewModelObject));
        }
        set {
            this.ViewModelObject = value;
        }
    }
    
    public override ViewModel CreateModel() {
        return this.RequestViewModel(GameManager.Container.Resolve<DropController>());
    }
    
    protected override void InitializeViewModel(ViewModel viewModel) {
        DropViewModel drop = ((DropViewModel)(viewModel));
        drop.amount = this._amount;
    }
    
    public virtual void ExecutePickUp() {
        this.ExecuteCommand(Drop.PickUp);
    }
}

[DiagramInfoAttribute("Game")]
public abstract class CoinDropViewBase : DropViewBase {
    
    public override System.Type ViewModelType {
        get {
            return typeof(CoinDropViewModel);
        }
    }
    
    public CoinDropViewModel CoinDrop {
        get {
            return ((CoinDropViewModel)(this.ViewModelObject));
        }
        set {
            this.ViewModelObject = value;
        }
    }
    
    public override ViewModel CreateModel() {
        return this.RequestViewModel(GameManager.Container.Resolve<CoinDropController>());
    }
    
    protected override void InitializeViewModel(ViewModel viewModel) {
        base.InitializeViewModel(viewModel);
    }
}

[DiagramInfoAttribute("Game")]
public abstract class MultiplierDropViewBase : DropViewBase {
    
    public override System.Type ViewModelType {
        get {
            return typeof(MultiplierDropViewModel);
        }
    }
    
    public MultiplierDropViewModel MultiplierDrop {
        get {
            return ((MultiplierDropViewModel)(this.ViewModelObject));
        }
        set {
            this.ViewModelObject = value;
        }
    }
    
    public override ViewModel CreateModel() {
        return this.RequestViewModel(GameManager.Container.Resolve<MultiplierDropController>());
    }
    
    protected override void InitializeViewModel(ViewModel viewModel) {
        base.InitializeViewModel(viewModel);
    }
}

public class PlayerViewViewBase : PlayerViewBase {
    
    public override ViewModel CreateModel() {
        return this.RequestViewModel(GameManager.Container.Resolve<PlayerController>());
    }
    
    public override void Bind() {
        base.Bind();
    }
}

public partial class PlayerView : PlayerViewViewBase {
}

public class MultiplayerViewViewBase : PlayerViewBase {
    
    [UFToggleGroup("multiplayer")]
    [UnityEngine.HideInInspector()]
    [UFRequireInstanceMethod("multiplayerChanged")]
    public bool _Bindmultiplayer = true;
    
    [UFToggleGroup("parts")]
    [UnityEngine.HideInInspector()]
    [UFRequireInstanceMethod("partsChanged")]
    public bool _Bindparts = true;
    
    public override ViewModel CreateModel() {
        return this.RequestViewModel(GameManager.Container.Resolve<PlayerController>());
    }
    
    /// Subscribes to the property and is notified anytime the value changes.
    public virtual void multiplayerChanged(Int32 value) {
    }
    
    /// Subscribes to the property and is notified anytime the value changes.
    public virtual void partsChanged(Int32 value) {
    }
    
    public override void Bind() {
        base.Bind();
        if (this._Bindmultiplayer) {
            this.BindProperty(Player._multiplayerProperty, this.multiplayerChanged);
        }
        if (this._Bindparts) {
            this.BindProperty(Player._partsProperty, this.partsChanged);
        }
    }
}

public partial class MultiplayerView : MultiplayerViewViewBase {
}

public class PlayerScoreViewViewBase : PlayerViewBase {
    
    private IDisposable _scoreDisposable;
    
    public override ViewModel CreateModel() {
        return this.RequestViewModel(GameManager.Container.Resolve<PlayerController>());
    }
    
    public virtual void Resetscore() {
        if (_scoreDisposable != null) _scoreDisposable.Dispose();
        _scoreDisposable = GetscoreObservable().Subscribe(Player._scoreProperty).DisposeWith(this);
    }
    
    protected virtual Int32 Calculatescore() {
        return default(Int32);
    }
    
    protected virtual UniRx.IObservable<Int32> GetscoreObservable() {
        return this.UpdateAsObservable().Select(p => Calculatescore());
    }
    
    public override void Bind() {
        base.Bind();
        Resetscore();
    }
}

public partial class PlayerScoreView : PlayerScoreViewViewBase {
}

public class BulletViewViewBase : BulletViewBase {
    
    public override ViewModel CreateModel() {
        return this.RequestViewModel(GameManager.Container.Resolve<BulletController>());
    }
    
    public override void Bind() {
        base.Bind();
    }
}

public partial class BulletView : BulletViewViewBase {
}

public class EnemyViewViewBase : EnemyViewBase {
    
    public override ViewModel CreateModel() {
        return this.RequestViewModel(GameManager.Container.Resolve<EnemyController>());
    }
    
    public override void Bind() {
        base.Bind();
    }
}

public partial class EnemyView : EnemyViewViewBase {
}

public class DropViewViewBase : DropViewBase {
    
    public override ViewModel CreateModel() {
        return this.RequestViewModel(GameManager.Container.Resolve<DropController>());
    }
    
    public override void Bind() {
        base.Bind();
    }
}

public partial class DropView : DropViewViewBase {
}