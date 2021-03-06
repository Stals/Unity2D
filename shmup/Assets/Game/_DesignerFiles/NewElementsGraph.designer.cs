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


[DiagramInfoAttribute("Game")]
public class PlayerViewModelBase : EntityViewModel {
    
    private IDisposable _isLowestMultiplierDisposable;
    
    public P<Int32> _scoreProperty;
    
    public P<Int32> _multiplayerProperty;
    
    public P<Int32> _partsProperty;
    
    public P<Int32> _moneyProperty;
    
    public P<Boolean> _canShootProperty;
    
    public P<Int32> _shotDelayProperty;
    
    public P<Single> _spawnChanceProperty;
    
    public P<Int32> _bulletsPerShotProperty;
    
    public P<Int32> _healthProperty;
    
    public P<Boolean> _IsInvurnalableProperty;
    
    public P<Boolean> _isLowestMultiplierProperty;
    
    protected CommandWithSender<PlayerViewModel> _AddMultiplayerPart;
    
    protected CommandWithSenderAndArgument<PlayerViewModel, Int32> _AddScore;
    
    protected CommandWithSenderAndArgument<PlayerViewModel, Int32> _AddMoney;
    
    protected CommandWithSender<PlayerViewModel> _onProgressBarEmpty;
    
    protected CommandWithSender<PlayerViewModel> _Shoot;
    
    public PlayerViewModelBase(PlayerControllerBase controller, bool initialize = true) : 
            base(controller, initialize) {
    }
    
    public PlayerViewModelBase() : 
            base() {
    }
    
    public override void Bind() {
        base.Bind();
        _scoreProperty = new P<Int32>(this, "score");
        _multiplayerProperty = new P<Int32>(this, "multiplayer");
        _partsProperty = new P<Int32>(this, "parts");
        _moneyProperty = new P<Int32>(this, "money");
        _canShootProperty = new P<Boolean>(this, "canShoot");
        _shotDelayProperty = new P<Int32>(this, "shotDelay");
        _spawnChanceProperty = new P<Single>(this, "spawnChance");
        _bulletsPerShotProperty = new P<Int32>(this, "bulletsPerShot");
        _healthProperty = new P<Int32>(this, "health");
        _IsInvurnalableProperty = new P<Boolean>(this, "IsInvurnalable");
        _isLowestMultiplierProperty = new P<Boolean>(this, "isLowestMultiplier");
        this.ResetisLowestMultiplier();
    }
    
    public virtual void ResetisLowestMultiplier() {
        if (_isLowestMultiplierDisposable != null) _isLowestMultiplierDisposable.Dispose();
        _isLowestMultiplierDisposable = _isLowestMultiplierProperty.ToComputed( ComputeisLowestMultiplier, this.GetisLowestMultiplierDependents().ToArray() ).DisposeWith(this);
    }
    
    public virtual Boolean ComputeisLowestMultiplier() {
        return default(Boolean);
    }
    
    public virtual IEnumerable<IObservableProperty> GetisLowestMultiplierDependents() {
        yield return _partsProperty;
        yield return _multiplayerProperty;
        yield break;
    }
}

public partial class PlayerViewModel : PlayerViewModelBase {
    
    public PlayerViewModel(PlayerControllerBase controller, bool initialize = true) : 
            base(controller, initialize) {
    }
    
    public PlayerViewModel() : 
            base() {
    }
    
    public virtual P<Int32> scoreProperty {
        get {
            return this._scoreProperty;
        }
    }
    
    public virtual Int32 score {
        get {
            return _scoreProperty.Value;
        }
        set {
            _scoreProperty.Value = value;
        }
    }
    
    public virtual P<Int32> multiplayerProperty {
        get {
            return this._multiplayerProperty;
        }
    }
    
    public virtual Int32 multiplayer {
        get {
            return _multiplayerProperty.Value;
        }
        set {
            _multiplayerProperty.Value = value;
        }
    }
    
    public virtual P<Int32> partsProperty {
        get {
            return this._partsProperty;
        }
    }
    
    public virtual Int32 parts {
        get {
            return _partsProperty.Value;
        }
        set {
            _partsProperty.Value = value;
        }
    }
    
    public virtual P<Int32> moneyProperty {
        get {
            return this._moneyProperty;
        }
    }
    
    public virtual Int32 money {
        get {
            return _moneyProperty.Value;
        }
        set {
            _moneyProperty.Value = value;
        }
    }
    
    public virtual P<Boolean> canShootProperty {
        get {
            return this._canShootProperty;
        }
    }
    
    public virtual Boolean canShoot {
        get {
            return _canShootProperty.Value;
        }
        set {
            _canShootProperty.Value = value;
        }
    }
    
    public virtual P<Int32> shotDelayProperty {
        get {
            return this._shotDelayProperty;
        }
    }
    
    public virtual Int32 shotDelay {
        get {
            return _shotDelayProperty.Value;
        }
        set {
            _shotDelayProperty.Value = value;
        }
    }
    
    public virtual P<Single> spawnChanceProperty {
        get {
            return this._spawnChanceProperty;
        }
    }
    
    public virtual Single spawnChance {
        get {
            return _spawnChanceProperty.Value;
        }
        set {
            _spawnChanceProperty.Value = value;
        }
    }
    
    public virtual P<Int32> bulletsPerShotProperty {
        get {
            return this._bulletsPerShotProperty;
        }
    }
    
    public virtual Int32 bulletsPerShot {
        get {
            return _bulletsPerShotProperty.Value;
        }
        set {
            _bulletsPerShotProperty.Value = value;
        }
    }
    
    public virtual P<Int32> healthProperty {
        get {
            return this._healthProperty;
        }
    }
    
    public virtual Int32 health {
        get {
            return _healthProperty.Value;
        }
        set {
            _healthProperty.Value = value;
        }
    }
    
    public virtual P<Boolean> IsInvurnalableProperty {
        get {
            return this._IsInvurnalableProperty;
        }
    }
    
    public virtual Boolean IsInvurnalable {
        get {
            return _IsInvurnalableProperty.Value;
        }
        set {
            _IsInvurnalableProperty.Value = value;
        }
    }
    
    public virtual P<Boolean> isLowestMultiplierProperty {
        get {
            return this._isLowestMultiplierProperty;
        }
    }
    
    public virtual Boolean isLowestMultiplier {
        get {
            return _isLowestMultiplierProperty.Value;
        }
        set {
            _isLowestMultiplierProperty.Value = value;
        }
    }
    
    public virtual CommandWithSender<PlayerViewModel> AddMultiplayerPart {
        get {
            return _AddMultiplayerPart;
        }
        set {
            _AddMultiplayerPart = value;
        }
    }
    
    public virtual CommandWithSenderAndArgument<PlayerViewModel, Int32> AddScore {
        get {
            return _AddScore;
        }
        set {
            _AddScore = value;
        }
    }
    
    public virtual CommandWithSenderAndArgument<PlayerViewModel, Int32> AddMoney {
        get {
            return _AddMoney;
        }
        set {
            _AddMoney = value;
        }
    }
    
    public virtual CommandWithSender<PlayerViewModel> onProgressBarEmpty {
        get {
            return _onProgressBarEmpty;
        }
        set {
            _onProgressBarEmpty = value;
        }
    }
    
    public virtual CommandWithSender<PlayerViewModel> Shoot {
        get {
            return _Shoot;
        }
        set {
            _Shoot = value;
        }
    }
    
    protected override void WireCommands(Controller controller) {
        base.WireCommands(controller);
        var player = controller as PlayerControllerBase;
        this.AddMultiplayerPart = new CommandWithSender<PlayerViewModel>(this, player.AddMultiplayerPart);
        this.AddScore = new CommandWithSenderAndArgument<PlayerViewModel, Int32>(this, player.AddScore);
        this.AddMoney = new CommandWithSenderAndArgument<PlayerViewModel, Int32>(this, player.AddMoney);
        this.onProgressBarEmpty = new CommandWithSender<PlayerViewModel>(this, player.onProgressBarEmpty);
        this.Shoot = new CommandWithSender<PlayerViewModel>(this, player.Shoot);
    }
    
    public override void Write(ISerializerStream stream) {
		base.Write(stream);
        stream.SerializeInt("score", this.score);
        stream.SerializeInt("multiplayer", this.multiplayer);
        stream.SerializeInt("parts", this.parts);
        stream.SerializeInt("money", this.money);
        stream.SerializeBool("canShoot", this.canShoot);
        stream.SerializeInt("shotDelay", this.shotDelay);
        stream.SerializeFloat("spawnChance", this.spawnChance);
        stream.SerializeInt("bulletsPerShot", this.bulletsPerShot);
        stream.SerializeInt("health", this.health);
        stream.SerializeBool("IsInvurnalable", this.IsInvurnalable);
    }
    
    public override void Read(ISerializerStream stream) {
		base.Read(stream);
        		this.score = stream.DeserializeInt("score");;
        		this.multiplayer = stream.DeserializeInt("multiplayer");;
        		this.parts = stream.DeserializeInt("parts");;
        		this.money = stream.DeserializeInt("money");;
        		this.canShoot = stream.DeserializeBool("canShoot");;
        		this.shotDelay = stream.DeserializeInt("shotDelay");;
        		this.spawnChance = stream.DeserializeFloat("spawnChance");;
        		this.bulletsPerShot = stream.DeserializeInt("bulletsPerShot");;
        		this.health = stream.DeserializeInt("health");;
        		this.IsInvurnalable = stream.DeserializeBool("IsInvurnalable");;
    }
    
    public override void Unbind() {
        base.Unbind();
    }
    
    protected override void FillProperties(List<ViewModelPropertyInfo> list) {
        base.FillProperties(list);;
        list.Add(new ViewModelPropertyInfo(_scoreProperty, false, false, false));
        list.Add(new ViewModelPropertyInfo(_multiplayerProperty, false, false, false));
        list.Add(new ViewModelPropertyInfo(_partsProperty, false, false, false));
        list.Add(new ViewModelPropertyInfo(_moneyProperty, false, false, false));
        list.Add(new ViewModelPropertyInfo(_canShootProperty, false, false, false));
        list.Add(new ViewModelPropertyInfo(_shotDelayProperty, false, false, false));
        list.Add(new ViewModelPropertyInfo(_spawnChanceProperty, false, false, false));
        list.Add(new ViewModelPropertyInfo(_bulletsPerShotProperty, false, false, false));
        list.Add(new ViewModelPropertyInfo(_healthProperty, false, false, false));
        list.Add(new ViewModelPropertyInfo(_IsInvurnalableProperty, false, false, false));
        list.Add(new ViewModelPropertyInfo(_isLowestMultiplierProperty, false, false, false, true));
    }
    
    protected override void FillCommands(List<ViewModelCommandInfo> list) {
        base.FillCommands(list);;
        list.Add(new ViewModelCommandInfo("AddMultiplayerPart", AddMultiplayerPart) { ParameterType = typeof(void) });
        list.Add(new ViewModelCommandInfo("AddScore", AddScore) { ParameterType = typeof(Int32) });
        list.Add(new ViewModelCommandInfo("AddMoney", AddMoney) { ParameterType = typeof(Int32) });
        list.Add(new ViewModelCommandInfo("onProgressBarEmpty", onProgressBarEmpty) { ParameterType = typeof(void) });
        list.Add(new ViewModelCommandInfo("Shoot", Shoot) { ParameterType = typeof(void) });
    }
}

[DiagramInfoAttribute("Game")]
public class EntityViewModelBase : ViewModel {
    
    public P<Single> _movementSpeedProperty;
    
    protected CommandWithSenderAndArgument<EntityViewModel, Int32> _TakeDamage;
    
    public EntityViewModelBase(EntityControllerBase controller, bool initialize = true) : 
            base(controller, initialize) {
    }
    
    public EntityViewModelBase() : 
            base() {
    }
    
    public override void Bind() {
        base.Bind();
        _movementSpeedProperty = new P<Single>(this, "movementSpeed");
    }
}

public partial class EntityViewModel : EntityViewModelBase {
    
    public EntityViewModel(EntityControllerBase controller, bool initialize = true) : 
            base(controller, initialize) {
    }
    
    public EntityViewModel() : 
            base() {
    }
    
    public virtual P<Single> movementSpeedProperty {
        get {
            return this._movementSpeedProperty;
        }
    }
    
    public virtual Single movementSpeed {
        get {
            return _movementSpeedProperty.Value;
        }
        set {
            _movementSpeedProperty.Value = value;
        }
    }
    
    public virtual CommandWithSenderAndArgument<EntityViewModel, Int32> TakeDamage {
        get {
            return _TakeDamage;
        }
        set {
            _TakeDamage = value;
        }
    }
    
    protected override void WireCommands(Controller controller) {
        var entity = controller as EntityControllerBase;
        this.TakeDamage = new CommandWithSenderAndArgument<EntityViewModel, Int32>(this, entity.TakeDamage);
    }
    
    public override void Write(ISerializerStream stream) {
		base.Write(stream);
        stream.SerializeFloat("movementSpeed", this.movementSpeed);
    }
    
    public override void Read(ISerializerStream stream) {
		base.Read(stream);
        		this.movementSpeed = stream.DeserializeFloat("movementSpeed");;
    }
    
    public override void Unbind() {
        base.Unbind();
    }
    
    protected override void FillProperties(List<ViewModelPropertyInfo> list) {
        base.FillProperties(list);;
        list.Add(new ViewModelPropertyInfo(_movementSpeedProperty, false, false, false));
    }
    
    protected override void FillCommands(List<ViewModelCommandInfo> list) {
        base.FillCommands(list);;
        list.Add(new ViewModelCommandInfo("TakeDamage", TakeDamage) { ParameterType = typeof(Int32) });
    }
}

[DiagramInfoAttribute("Game")]
public class EnemyViewModelBase : EntityViewModel {
    
    public P<Single> _spawnChanceProperty;
    
    public EnemyViewModelBase(EnemyControllerBase controller, bool initialize = true) : 
            base(controller, initialize) {
    }
    
    public EnemyViewModelBase() : 
            base() {
    }
    
    public override void Bind() {
        base.Bind();
        _spawnChanceProperty = new P<Single>(this, "spawnChance");
    }
}

public partial class EnemyViewModel : EnemyViewModelBase {
    
    public EnemyViewModel(EnemyControllerBase controller, bool initialize = true) : 
            base(controller, initialize) {
    }
    
    public EnemyViewModel() : 
            base() {
    }
    
    public virtual P<Single> spawnChanceProperty {
        get {
            return this._spawnChanceProperty;
        }
    }
    
    public virtual Single spawnChance {
        get {
            return _spawnChanceProperty.Value;
        }
        set {
            _spawnChanceProperty.Value = value;
        }
    }
    
    protected override void WireCommands(Controller controller) {
        base.WireCommands(controller);
    }
    
    public override void Write(ISerializerStream stream) {
		base.Write(stream);
        stream.SerializeFloat("spawnChance", this.spawnChance);
    }
    
    public override void Read(ISerializerStream stream) {
		base.Read(stream);
        		this.spawnChance = stream.DeserializeFloat("spawnChance");;
    }
    
    public override void Unbind() {
        base.Unbind();
    }
    
    protected override void FillProperties(List<ViewModelPropertyInfo> list) {
        base.FillProperties(list);;
        list.Add(new ViewModelPropertyInfo(_spawnChanceProperty, false, false, false));
    }
    
    protected override void FillCommands(List<ViewModelCommandInfo> list) {
        base.FillCommands(list);;
    }
}

[DiagramInfoAttribute("Game")]
public class BulletViewModelBase : ViewModel {
    
    public P<Single> _speedProperty;
    
    public BulletViewModelBase(BulletControllerBase controller, bool initialize = true) : 
            base(controller, initialize) {
    }
    
    public BulletViewModelBase() : 
            base() {
    }
    
    public override void Bind() {
        base.Bind();
        _speedProperty = new P<Single>(this, "speed");
    }
}

public partial class BulletViewModel : BulletViewModelBase {
    
    public BulletViewModel(BulletControllerBase controller, bool initialize = true) : 
            base(controller, initialize) {
    }
    
    public BulletViewModel() : 
            base() {
    }
    
    public virtual P<Single> speedProperty {
        get {
            return this._speedProperty;
        }
    }
    
    public virtual Single speed {
        get {
            return _speedProperty.Value;
        }
        set {
            _speedProperty.Value = value;
        }
    }
    
    protected override void WireCommands(Controller controller) {
    }
    
    public override void Write(ISerializerStream stream) {
		base.Write(stream);
        stream.SerializeFloat("speed", this.speed);
    }
    
    public override void Read(ISerializerStream stream) {
		base.Read(stream);
        		this.speed = stream.DeserializeFloat("speed");;
    }
    
    public override void Unbind() {
        base.Unbind();
    }
    
    protected override void FillProperties(List<ViewModelPropertyInfo> list) {
        base.FillProperties(list);;
        list.Add(new ViewModelPropertyInfo(_speedProperty, false, false, false));
    }
    
    protected override void FillCommands(List<ViewModelCommandInfo> list) {
        base.FillCommands(list);;
    }
}

[DiagramInfoAttribute("Game")]
public class DropViewModelBase : ViewModel {
    
    public P<Int32> _amountProperty;
    
    protected CommandWithSender<DropViewModel> _PickUp;
    
    public DropViewModelBase(DropControllerBase controller, bool initialize = true) : 
            base(controller, initialize) {
    }
    
    public DropViewModelBase() : 
            base() {
    }
    
    public override void Bind() {
        base.Bind();
        _amountProperty = new P<Int32>(this, "amount");
    }
}

public partial class DropViewModel : DropViewModelBase {
    
    public DropViewModel(DropControllerBase controller, bool initialize = true) : 
            base(controller, initialize) {
    }
    
    public DropViewModel() : 
            base() {
    }
    
    public virtual P<Int32> amountProperty {
        get {
            return this._amountProperty;
        }
    }
    
    public virtual Int32 amount {
        get {
            return _amountProperty.Value;
        }
        set {
            _amountProperty.Value = value;
        }
    }
    
    public virtual CommandWithSender<DropViewModel> PickUp {
        get {
            return _PickUp;
        }
        set {
            _PickUp = value;
        }
    }
    
    protected override void WireCommands(Controller controller) {
        var drop = controller as DropControllerBase;
        this.PickUp = new CommandWithSender<DropViewModel>(this, drop.PickUp);
    }
    
    public override void Write(ISerializerStream stream) {
		base.Write(stream);
        stream.SerializeInt("amount", this.amount);
    }
    
    public override void Read(ISerializerStream stream) {
		base.Read(stream);
        		this.amount = stream.DeserializeInt("amount");;
    }
    
    public override void Unbind() {
        base.Unbind();
    }
    
    protected override void FillProperties(List<ViewModelPropertyInfo> list) {
        base.FillProperties(list);;
        list.Add(new ViewModelPropertyInfo(_amountProperty, false, false, false));
    }
    
    protected override void FillCommands(List<ViewModelCommandInfo> list) {
        base.FillCommands(list);;
        list.Add(new ViewModelCommandInfo("PickUp", PickUp) { ParameterType = typeof(void) });
    }
}

[DiagramInfoAttribute("Game")]
public class CoinDropViewModelBase : DropViewModel {
    
    public CoinDropViewModelBase(CoinDropControllerBase controller, bool initialize = true) : 
            base(controller, initialize) {
    }
    
    public CoinDropViewModelBase() : 
            base() {
    }
    
    public override void Bind() {
        base.Bind();
    }
}

public partial class CoinDropViewModel : CoinDropViewModelBase {
    
    public CoinDropViewModel(CoinDropControllerBase controller, bool initialize = true) : 
            base(controller, initialize) {
    }
    
    public CoinDropViewModel() : 
            base() {
    }
    
    protected override void WireCommands(Controller controller) {
        base.WireCommands(controller);
    }
    
    public override void Write(ISerializerStream stream) {
		base.Write(stream);
    }
    
    public override void Read(ISerializerStream stream) {
		base.Read(stream);
    }
    
    public override void Unbind() {
        base.Unbind();
    }
    
    protected override void FillProperties(List<ViewModelPropertyInfo> list) {
        base.FillProperties(list);;
    }
    
    protected override void FillCommands(List<ViewModelCommandInfo> list) {
        base.FillCommands(list);;
    }
}

[DiagramInfoAttribute("Game")]
public class MultiplierDropViewModelBase : DropViewModel {
    
    public MultiplierDropViewModelBase(MultiplierDropControllerBase controller, bool initialize = true) : 
            base(controller, initialize) {
    }
    
    public MultiplierDropViewModelBase() : 
            base() {
    }
    
    public override void Bind() {
        base.Bind();
    }
}

public partial class MultiplierDropViewModel : MultiplierDropViewModelBase {
    
    public MultiplierDropViewModel(MultiplierDropControllerBase controller, bool initialize = true) : 
            base(controller, initialize) {
    }
    
    public MultiplierDropViewModel() : 
            base() {
    }
    
    protected override void WireCommands(Controller controller) {
        base.WireCommands(controller);
    }
    
    public override void Write(ISerializerStream stream) {
		base.Write(stream);
    }
    
    public override void Read(ISerializerStream stream) {
		base.Read(stream);
    }
    
    public override void Unbind() {
        base.Unbind();
    }
    
    protected override void FillProperties(List<ViewModelPropertyInfo> list) {
        base.FillProperties(list);;
    }
    
    protected override void FillCommands(List<ViewModelCommandInfo> list) {
        base.FillCommands(list);;
    }
}

[DiagramInfoAttribute("Game")]
public class UpgradeViewModelBase : ViewModel {
    
    private IDisposable _TooltipTextDisposable;
    
    public P<Int32> _levelProperty;
    
    public P<Int32> _basePriceProperty;
    
    public P<Int32> _priceProperty;
    
    public P<String> _TooltipTextProperty;
    
    protected CommandWithSender<UpgradeViewModel> _Upgrade;
    
    public UpgradeViewModelBase(UpgradeControllerBase controller, bool initialize = true) : 
            base(controller, initialize) {
    }
    
    public UpgradeViewModelBase() : 
            base() {
    }
    
    public override void Bind() {
        base.Bind();
        _levelProperty = new P<Int32>(this, "level");
        _basePriceProperty = new P<Int32>(this, "basePrice");
        _priceProperty = new P<Int32>(this, "price");
        _TooltipTextProperty = new P<String>(this, "TooltipText");
        this.ResetTooltipText();
    }
    
    public virtual void ResetTooltipText() {
        if (_TooltipTextDisposable != null) _TooltipTextDisposable.Dispose();
        _TooltipTextDisposable = _TooltipTextProperty.ToComputed( ComputeTooltipText, this.GetTooltipTextDependents().ToArray() ).DisposeWith(this);
    }
    
    public virtual String ComputeTooltipText() {
        return default(String);
    }
    
    public virtual IEnumerable<IObservableProperty> GetTooltipTextDependents() {
        yield return _basePriceProperty;
        yield break;
    }
}

public partial class UpgradeViewModel : UpgradeViewModelBase {
    
    public UpgradeViewModel(UpgradeControllerBase controller, bool initialize = true) : 
            base(controller, initialize) {
    }
    
    public UpgradeViewModel() : 
            base() {
    }
    
    public virtual P<Int32> levelProperty {
        get {
            return this._levelProperty;
        }
    }
    
    public virtual Int32 level {
        get {
            return _levelProperty.Value;
        }
        set {
            _levelProperty.Value = value;
        }
    }
    
    public virtual P<Int32> basePriceProperty {
        get {
            return this._basePriceProperty;
        }
    }
    
    public virtual Int32 basePrice {
        get {
            return _basePriceProperty.Value;
        }
        set {
            _basePriceProperty.Value = value;
        }
    }
    
    public virtual P<Int32> priceProperty {
        get {
            return this._priceProperty;
        }
    }
    
    public virtual Int32 price {
        get {
            return _priceProperty.Value;
        }
        set {
            _priceProperty.Value = value;
        }
    }
    
    public virtual P<String> TooltipTextProperty {
        get {
            return this._TooltipTextProperty;
        }
    }
    
    public virtual String TooltipText {
        get {
            return _TooltipTextProperty.Value;
        }
        set {
            _TooltipTextProperty.Value = value;
        }
    }
    
    public virtual CommandWithSender<UpgradeViewModel> Upgrade {
        get {
            return _Upgrade;
        }
        set {
            _Upgrade = value;
        }
    }
    
    protected override void WireCommands(Controller controller) {
        var upgrade = controller as UpgradeControllerBase;
        this.Upgrade = new CommandWithSender<UpgradeViewModel>(this, upgrade.Upgrade);
    }
    
    public override void Write(ISerializerStream stream) {
		base.Write(stream);
        stream.SerializeInt("level", this.level);
        stream.SerializeInt("basePrice", this.basePrice);
        stream.SerializeInt("price", this.price);
    }
    
    public override void Read(ISerializerStream stream) {
		base.Read(stream);
        		this.level = stream.DeserializeInt("level");;
        		this.basePrice = stream.DeserializeInt("basePrice");;
        		this.price = stream.DeserializeInt("price");;
    }
    
    public override void Unbind() {
        base.Unbind();
    }
    
    protected override void FillProperties(List<ViewModelPropertyInfo> list) {
        base.FillProperties(list);;
        list.Add(new ViewModelPropertyInfo(_levelProperty, false, false, false));
        list.Add(new ViewModelPropertyInfo(_basePriceProperty, false, false, false));
        list.Add(new ViewModelPropertyInfo(_priceProperty, false, false, false));
        list.Add(new ViewModelPropertyInfo(_TooltipTextProperty, false, false, false, true));
    }
    
    protected override void FillCommands(List<ViewModelCommandInfo> list) {
        base.FillCommands(list);;
        list.Add(new ViewModelCommandInfo("Upgrade", Upgrade) { ParameterType = typeof(void) });
    }
}

[DiagramInfoAttribute("Game")]
public class HealthUpgradeViewModelBase : UpgradeViewModel {
    
    public HealthUpgradeViewModelBase(HealthUpgradeControllerBase controller, bool initialize = true) : 
            base(controller, initialize) {
    }
    
    public HealthUpgradeViewModelBase() : 
            base() {
    }
    
    public override void Bind() {
        base.Bind();
    }
}

public partial class HealthUpgradeViewModel : HealthUpgradeViewModelBase {
    
    public HealthUpgradeViewModel(HealthUpgradeControllerBase controller, bool initialize = true) : 
            base(controller, initialize) {
    }
    
    public HealthUpgradeViewModel() : 
            base() {
    }
    
    protected override void WireCommands(Controller controller) {
        base.WireCommands(controller);
    }
    
    public override void Write(ISerializerStream stream) {
		base.Write(stream);
    }
    
    public override void Read(ISerializerStream stream) {
		base.Read(stream);
    }
    
    public override void Unbind() {
        base.Unbind();
    }
    
    protected override void FillProperties(List<ViewModelPropertyInfo> list) {
        base.FillProperties(list);;
    }
    
    protected override void FillCommands(List<ViewModelCommandInfo> list) {
        base.FillCommands(list);;
    }
}

[DiagramInfoAttribute("Game")]
public class DropUpgradeViewModelBase : UpgradeViewModel {
    
    public DropUpgradeViewModelBase(DropUpgradeControllerBase controller, bool initialize = true) : 
            base(controller, initialize) {
    }
    
    public DropUpgradeViewModelBase() : 
            base() {
    }
    
    public override void Bind() {
        base.Bind();
    }
}

public partial class DropUpgradeViewModel : DropUpgradeViewModelBase {
    
    public DropUpgradeViewModel(DropUpgradeControllerBase controller, bool initialize = true) : 
            base(controller, initialize) {
    }
    
    public DropUpgradeViewModel() : 
            base() {
    }
    
    protected override void WireCommands(Controller controller) {
        base.WireCommands(controller);
    }
    
    public override void Write(ISerializerStream stream) {
		base.Write(stream);
    }
    
    public override void Read(ISerializerStream stream) {
		base.Read(stream);
    }
    
    public override void Unbind() {
        base.Unbind();
    }
    
    protected override void FillProperties(List<ViewModelPropertyInfo> list) {
        base.FillProperties(list);;
    }
    
    protected override void FillCommands(List<ViewModelCommandInfo> list) {
        base.FillCommands(list);;
    }
}

[DiagramInfoAttribute("Game")]
public class FireElementViewModelBase : UpgradeViewModel {
    
    public FireElementViewModelBase(FireElementControllerBase controller, bool initialize = true) : 
            base(controller, initialize) {
    }
    
    public FireElementViewModelBase() : 
            base() {
    }
    
    public override void Bind() {
        base.Bind();
    }
}

public partial class FireElementViewModel : FireElementViewModelBase {
    
    public FireElementViewModel(FireElementControllerBase controller, bool initialize = true) : 
            base(controller, initialize) {
    }
    
    public FireElementViewModel() : 
            base() {
    }
    
    protected override void WireCommands(Controller controller) {
        base.WireCommands(controller);
    }
    
    public override void Write(ISerializerStream stream) {
		base.Write(stream);
    }
    
    public override void Read(ISerializerStream stream) {
		base.Read(stream);
    }
    
    public override void Unbind() {
        base.Unbind();
    }
    
    protected override void FillProperties(List<ViewModelPropertyInfo> list) {
        base.FillProperties(list);;
    }
    
    protected override void FillCommands(List<ViewModelCommandInfo> list) {
        base.FillCommands(list);;
    }
}

[DiagramInfoAttribute("Game")]
public class BulletUpgradeViewModelBase : UpgradeViewModel {
    
    public BulletUpgradeViewModelBase(BulletUpgradeControllerBase controller, bool initialize = true) : 
            base(controller, initialize) {
    }
    
    public BulletUpgradeViewModelBase() : 
            base() {
    }
    
    public override void Bind() {
        base.Bind();
    }
}

public partial class BulletUpgradeViewModel : BulletUpgradeViewModelBase {
    
    public BulletUpgradeViewModel(BulletUpgradeControllerBase controller, bool initialize = true) : 
            base(controller, initialize) {
    }
    
    public BulletUpgradeViewModel() : 
            base() {
    }
    
    protected override void WireCommands(Controller controller) {
        base.WireCommands(controller);
    }
    
    public override void Write(ISerializerStream stream) {
		base.Write(stream);
    }
    
    public override void Read(ISerializerStream stream) {
		base.Read(stream);
    }
    
    public override void Unbind() {
        base.Unbind();
    }
    
    protected override void FillProperties(List<ViewModelPropertyInfo> list) {
        base.FillProperties(list);;
    }
    
    protected override void FillCommands(List<ViewModelCommandInfo> list) {
        base.FillCommands(list);;
    }
}
