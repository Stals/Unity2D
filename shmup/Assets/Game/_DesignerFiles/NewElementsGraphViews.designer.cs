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
    public Boolean _canShoot;
    
    [UFGroup("View Model Properties")]
    [UnityEngine.HideInInspector()]
    public Int32 _shotDelay;
    
    [UFGroup("View Model Properties")]
    [UnityEngine.HideInInspector()]
    public Single _spawnChance;
    
    [UFGroup("View Model Properties")]
    [UnityEngine.HideInInspector()]
    public Int32 _bulletsPerShot;
    
    [UFGroup("View Model Properties")]
    [UnityEngine.HideInInspector()]
    public Int32 _health;
    
    [UFGroup("View Model Properties")]
    [UnityEngine.HideInInspector()]
    public Boolean _IsInvurnalable;
    
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
        player.canShoot = this._canShoot;
        player.shotDelay = this._shotDelay;
        player.spawnChance = this._spawnChance;
        player.bulletsPerShot = this._bulletsPerShot;
        player.health = this._health;
        player.IsInvurnalable = this._IsInvurnalable;
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
    
    public virtual void ExecuteonProgressBarEmpty() {
        this.ExecuteCommand(Player.onProgressBarEmpty);
    }
    
    public virtual void ExecuteShoot() {
        this.ExecuteCommand(Player.Shoot);
    }
}

[DiagramInfoAttribute("Game")]
public abstract class EntityViewBase : ViewBase {
    
    [UFGroup("View Model Properties")]
    [UnityEngine.HideInInspector()]
    public Single _movementSpeed;
    
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
        EntityViewModel entity = ((EntityViewModel)(viewModel));
        entity.movementSpeed = this._movementSpeed;
    }
    
    public virtual void ExecuteTakeDamage(Int32 arg) {
        this.ExecuteCommand(Entity.TakeDamage, arg);
    }
}

[DiagramInfoAttribute("Game")]
public abstract class EnemyViewBase : EntityViewBase {
    
    [UFGroup("View Model Properties")]
    [UnityEngine.HideInInspector()]
    public Single _spawnChance;
    
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
        EnemyViewModel enemy = ((EnemyViewModel)(viewModel));
        enemy.spawnChance = this._spawnChance;
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

[DiagramInfoAttribute("Game")]
public abstract class UpgradeViewBase : ViewBase {
    
    [UFGroup("View Model Properties")]
    [UnityEngine.HideInInspector()]
    public Int32 _level;
    
    [UFGroup("View Model Properties")]
    [UnityEngine.HideInInspector()]
    public Int32 _basePrice;
    
    [UFGroup("View Model Properties")]
    [UnityEngine.HideInInspector()]
    public Int32 _price;
    
    public override System.Type ViewModelType {
        get {
            return typeof(UpgradeViewModel);
        }
    }
    
    public UpgradeViewModel Upgrade {
        get {
            return ((UpgradeViewModel)(this.ViewModelObject));
        }
        set {
            this.ViewModelObject = value;
        }
    }
    
    public override ViewModel CreateModel() {
        return this.RequestViewModel(GameManager.Container.Resolve<UpgradeController>());
    }
    
    protected override void InitializeViewModel(ViewModel viewModel) {
        UpgradeViewModel upgrade = ((UpgradeViewModel)(viewModel));
        upgrade.level = this._level;
        upgrade.basePrice = this._basePrice;
        upgrade.price = this._price;
    }
    
    public virtual void ExecuteUpgrade() {
        this.ExecuteCommand(Upgrade.Upgrade);
    }
}

[DiagramInfoAttribute("Game")]
public abstract class HealthUpgradeViewBase : UpgradeViewBase {
    
    public override System.Type ViewModelType {
        get {
            return typeof(HealthUpgradeViewModel);
        }
    }
    
    public HealthUpgradeViewModel HealthUpgrade {
        get {
            return ((HealthUpgradeViewModel)(this.ViewModelObject));
        }
        set {
            this.ViewModelObject = value;
        }
    }
    
    public override ViewModel CreateModel() {
        return this.RequestViewModel(GameManager.Container.Resolve<HealthUpgradeController>());
    }
    
    protected override void InitializeViewModel(ViewModel viewModel) {
        base.InitializeViewModel(viewModel);
    }
}

[DiagramInfoAttribute("Game")]
public abstract class DropUpgradeViewBase : UpgradeViewBase {
    
    public override System.Type ViewModelType {
        get {
            return typeof(DropUpgradeViewModel);
        }
    }
    
    public DropUpgradeViewModel DropUpgrade {
        get {
            return ((DropUpgradeViewModel)(this.ViewModelObject));
        }
        set {
            this.ViewModelObject = value;
        }
    }
    
    public override ViewModel CreateModel() {
        return this.RequestViewModel(GameManager.Container.Resolve<DropUpgradeController>());
    }
    
    protected override void InitializeViewModel(ViewModel viewModel) {
        base.InitializeViewModel(viewModel);
    }
}

[DiagramInfoAttribute("Game")]
public abstract class FireElementViewBase : UpgradeViewBase {
    
    public override System.Type ViewModelType {
        get {
            return typeof(FireElementViewModel);
        }
    }
    
    public FireElementViewModel FireElement {
        get {
            return ((FireElementViewModel)(this.ViewModelObject));
        }
        set {
            this.ViewModelObject = value;
        }
    }
    
    public override ViewModel CreateModel() {
        return this.RequestViewModel(GameManager.Container.Resolve<FireElementController>());
    }
    
    protected override void InitializeViewModel(ViewModel viewModel) {
        base.InitializeViewModel(viewModel);
    }
}

[DiagramInfoAttribute("Game")]
public abstract class BulletUpgradeViewBase : UpgradeViewBase {
    
    public override System.Type ViewModelType {
        get {
            return typeof(BulletUpgradeViewModel);
        }
    }
    
    public BulletUpgradeViewModel BulletUpgrade {
        get {
            return ((BulletUpgradeViewModel)(this.ViewModelObject));
        }
        set {
            this.ViewModelObject = value;
        }
    }
    
    public override ViewModel CreateModel() {
        return this.RequestViewModel(GameManager.Container.Resolve<BulletUpgradeController>());
    }
    
    protected override void InitializeViewModel(ViewModel viewModel) {
        base.InitializeViewModel(viewModel);
    }
}

public class PlayerViewViewBase : PlayerViewBase {
    
    [UFToggleGroup("Shoot")]
    [UnityEngine.HideInInspector()]
    public bool _BindShoot = true;
    
    [UFToggleGroup("IsInvurnalable")]
    [UnityEngine.HideInInspector()]
    [UFRequireInstanceMethod("IsInvurnalableChanged")]
    public bool _BindIsInvurnalable = true;
    
    [UFToggleGroup("health")]
    [UnityEngine.HideInInspector()]
    [UFRequireInstanceMethod("healthChanged")]
    public bool _Bindhealth = true;
    
    [UFToggleGroup("AddMoney")]
    [UnityEngine.HideInInspector()]
    public bool _BindAddMoney = true;
    
    public override ViewModel CreateModel() {
        return this.RequestViewModel(GameManager.Container.Resolve<PlayerController>());
    }
    
    /// Invokes ShootExecuted when the Shoot command is executed.
    public virtual void ShootExecuted() {
    }
    
    /// Subscribes to the property and is notified anytime the value changes.
    public virtual void IsInvurnalableChanged(Boolean value) {
    }
    
    /// Subscribes to the property and is notified anytime the value changes.
    public virtual void healthChanged(Int32 value) {
    }
    
    /// Invokes AddMoneyExecuted when the AddMoney command is executed.
    public virtual void AddMoneyExecuted() {
    }
    
    public override void Bind() {
        base.Bind();
        if (this._BindShoot) {
            this.BindCommandExecuted(Player.Shoot, ShootExecuted);
        }
        if (this._BindIsInvurnalable) {
            this.BindProperty(Player._IsInvurnalableProperty, this.IsInvurnalableChanged);
        }
        if (this._Bindhealth) {
            this.BindProperty(Player._healthProperty, this.healthChanged);
        }
        if (this._BindAddMoney) {
            this.BindCommandExecuted(Player.AddMoney, AddMoneyExecuted);
        }
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
    
    [UFToggleGroup("TakeDamage")]
    [UnityEngine.HideInInspector()]
    public bool _BindTakeDamage = true;
    
    public override ViewModel CreateModel() {
        return this.RequestViewModel(GameManager.Container.Resolve<EnemyController>());
    }
    
    /// Invokes TakeDamageExecuted when the TakeDamage command is executed.
    public virtual void TakeDamageExecuted() {
    }
    
    public override void Bind() {
        base.Bind();
        if (this._BindTakeDamage) {
            this.BindCommandExecuted(Enemy.TakeDamage, TakeDamageExecuted);
        }
    }
}

public partial class EnemyView : EnemyViewViewBase {
}

public class DropViewViewBase : DropViewBase {
    
    [UFToggleGroup("PickUp")]
    [UnityEngine.HideInInspector()]
    public bool _BindPickUp = true;
    
    public override ViewModel CreateModel() {
        return this.RequestViewModel(GameManager.Container.Resolve<DropController>());
    }
    
    /// Invokes PickUpExecuted when the PickUp command is executed.
    public virtual void PickUpExecuted() {
    }
    
    public override void Bind() {
        base.Bind();
        if (this._BindPickUp) {
            this.BindCommandExecuted(Drop.PickUp, PickUpExecuted);
        }
    }
}

public partial class DropView : DropViewViewBase {
}

public class PlayerMoneyViewViewBase : PlayerViewBase {
    
    [UFToggleGroup("money")]
    [UnityEngine.HideInInspector()]
    [UFRequireInstanceMethod("moneyChanged")]
    public bool _Bindmoney = true;
    
    public override ViewModel CreateModel() {
        return this.RequestViewModel(GameManager.Container.Resolve<PlayerController>());
    }
    
    /// Subscribes to the property and is notified anytime the value changes.
    public virtual void moneyChanged(Int32 value) {
    }
    
    public override void Bind() {
        base.Bind();
        if (this._Bindmoney) {
            this.BindProperty(Player._moneyProperty, this.moneyChanged);
        }
    }
}

public partial class PlayerMoneyView : PlayerMoneyViewViewBase {
}

public class CoinDropViewViewBase : DropView {
    
    public CoinDropViewModel CoinDrop {
        get {
            return ((CoinDropViewModel)(this.ViewModelObject));
        }
        set {
            this.ViewModelObject = value;
        }
    }
    
    public override System.Type ViewModelType {
        get {
            return typeof(CoinDropViewModel);
        }
    }
    
    public override ViewModel CreateModel() {
        return this.RequestViewModel(GameManager.Container.Resolve<CoinDropController>());
    }
    
    public override void Bind() {
        base.Bind();
    }
    
    protected override void InitializeViewModel(ViewModel viewModel) {
        base.InitializeViewModel(viewModel);
    }
}

public partial class CoinDropView : CoinDropViewViewBase {
}

public class PlayerHealthViewViewBase : PlayerViewBase {
    
    [UFToggleGroup("health")]
    [UnityEngine.HideInInspector()]
    [UFRequireInstanceMethod("healthChanged")]
    public bool _Bindhealth = true;
    
    public override ViewModel CreateModel() {
        return this.RequestViewModel(GameManager.Container.Resolve<PlayerController>());
    }
    
    /// Subscribes to the property and is notified anytime the value changes.
    public virtual void healthChanged(Int32 value) {
    }
    
    public override void Bind() {
        base.Bind();
        if (this._Bindhealth) {
            this.BindProperty(Player._healthProperty, this.healthChanged);
        }
    }
}

public partial class PlayerHealthView : PlayerHealthViewViewBase {
}

public class UpgradeViewViewBase : UpgradeViewBase {
    
    [UFToggleGroup("price")]
    [UnityEngine.HideInInspector()]
    [UFRequireInstanceMethod("priceChanged")]
    public bool _Bindprice = true;
    
    [UFToggleGroup("basePrice")]
    [UnityEngine.HideInInspector()]
    [UFRequireInstanceMethod("basePriceChanged")]
    public bool _BindbasePrice = true;
    
    [UFToggleGroup("Upgrade")]
    [UnityEngine.HideInInspector()]
    public bool _BindUpgrade = true;
    
    public override ViewModel CreateModel() {
        return this.RequestViewModel(GameManager.Container.Resolve<UpgradeController>());
    }
    
    /// Subscribes to the property and is notified anytime the value changes.
    public virtual void priceChanged(Int32 value) {
    }
    
    /// Subscribes to the property and is notified anytime the value changes.
    public virtual void basePriceChanged(Int32 value) {
    }
    
    /// Invokes UpgradeExecuted when the Upgrade command is executed.
    public virtual void UpgradeExecuted() {
    }
    
    public override void Bind() {
        base.Bind();
        if (this._Bindprice) {
            this.BindProperty(Upgrade._priceProperty, this.priceChanged);
        }
        if (this._BindbasePrice) {
            this.BindProperty(Upgrade._basePriceProperty, this.basePriceChanged);
        }
        if (this._BindUpgrade) {
            this.BindCommandExecuted(Upgrade.Upgrade, UpgradeExecuted);
        }
    }
}

public partial class UpgradeView : UpgradeViewViewBase {
}

public class HealthUpgradeViewViewBase : UpgradeView {
    
    public HealthUpgradeViewModel HealthUpgrade {
        get {
            return ((HealthUpgradeViewModel)(this.ViewModelObject));
        }
        set {
            this.ViewModelObject = value;
        }
    }
    
    public override System.Type ViewModelType {
        get {
            return typeof(HealthUpgradeViewModel);
        }
    }
    
    public override ViewModel CreateModel() {
        return this.RequestViewModel(GameManager.Container.Resolve<HealthUpgradeController>());
    }
    
    public override void Bind() {
        base.Bind();
    }
    
    protected override void InitializeViewModel(ViewModel viewModel) {
        base.InitializeViewModel(viewModel);
    }
}

public partial class HealthUpgradeView : HealthUpgradeViewViewBase {
}

public class DropUpgradeViewViewBase : UpgradeView {
    
    public DropUpgradeViewModel DropUpgrade {
        get {
            return ((DropUpgradeViewModel)(this.ViewModelObject));
        }
        set {
            this.ViewModelObject = value;
        }
    }
    
    public override System.Type ViewModelType {
        get {
            return typeof(DropUpgradeViewModel);
        }
    }
    
    public override ViewModel CreateModel() {
        return this.RequestViewModel(GameManager.Container.Resolve<DropUpgradeController>());
    }
    
    public override void Bind() {
        base.Bind();
    }
    
    protected override void InitializeViewModel(ViewModel viewModel) {
        base.InitializeViewModel(viewModel);
    }
}

public partial class DropUpgradeView : DropUpgradeViewViewBase {
}

public class FireElementViewViewBase : UpgradeView {
    
    public FireElementViewModel FireElement {
        get {
            return ((FireElementViewModel)(this.ViewModelObject));
        }
        set {
            this.ViewModelObject = value;
        }
    }
    
    public override System.Type ViewModelType {
        get {
            return typeof(FireElementViewModel);
        }
    }
    
    public override ViewModel CreateModel() {
        return this.RequestViewModel(GameManager.Container.Resolve<FireElementController>());
    }
    
    public override void Bind() {
        base.Bind();
    }
    
    protected override void InitializeViewModel(ViewModel viewModel) {
        base.InitializeViewModel(viewModel);
    }
}

public partial class FireElementView : FireElementViewViewBase {
}

public class BulletUpgradeViewViewBase : UpgradeView {
    
    public BulletUpgradeViewModel BulletUpgrade {
        get {
            return ((BulletUpgradeViewModel)(this.ViewModelObject));
        }
        set {
            this.ViewModelObject = value;
        }
    }
    
    public override System.Type ViewModelType {
        get {
            return typeof(BulletUpgradeViewModel);
        }
    }
    
    public override ViewModel CreateModel() {
        return this.RequestViewModel(GameManager.Container.Resolve<BulletUpgradeController>());
    }
    
    public override void Bind() {
        base.Bind();
    }
    
    protected override void InitializeViewModel(ViewModel viewModel) {
        base.InitializeViewModel(viewModel);
    }
}

public partial class BulletUpgradeView : BulletUpgradeViewViewBase {
}

public class PlayerResultsViewViewBase : PlayerViewBase {
    
    [UFToggleGroup("score")]
    [UnityEngine.HideInInspector()]
    [UFRequireInstanceMethod("scoreChanged")]
    public bool _Bindscore = true;
    
    [UFToggleGroup("multiplayer")]
    [UnityEngine.HideInInspector()]
    [UFRequireInstanceMethod("multiplayerChanged")]
    public bool _Bindmultiplayer = true;
    
    public override ViewModel CreateModel() {
        return this.RequestViewModel(GameManager.Container.Resolve<PlayerController>());
    }
    
    /// Subscribes to the property and is notified anytime the value changes.
    public virtual void scoreChanged(Int32 value) {
    }
    
    /// Subscribes to the property and is notified anytime the value changes.
    public virtual void multiplayerChanged(Int32 value) {
    }
    
    public override void Bind() {
        base.Bind();
        if (this._Bindscore) {
            this.BindProperty(Player._scoreProperty, this.scoreChanged);
        }
        if (this._Bindmultiplayer) {
            this.BindProperty(Player._multiplayerProperty, this.multiplayerChanged);
        }
    }
}

public partial class PlayerResultsView : PlayerResultsViewViewBase {
}
