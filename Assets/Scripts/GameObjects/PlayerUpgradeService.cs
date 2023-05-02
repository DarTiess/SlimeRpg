using Infrastructure.GameStates;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(HealthBar))]
public class PlayerUpgradeService : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private int _health;

    private int HpNum
    {
        get { return PlayerPrefs.GetInt("Health"); }
        set { PlayerPrefs.SetInt("Health", value); }
    }

    private float _jumpDuration;
    private int _attackPower;
    private HealthBar _healthBar;
    private DisplayUIState _displayUIState;
    private Player.Player _player;
    private GameStates _gameStates;
    [Inject]
    private void Construct(DisplayUIState state, GameStates gameStates)
    {
        _displayUIState = state;
        _gameStates = gameStates;
    }

    public void InitPlayerService(int attack, float jumpDuration)
    {
        if (HpNum <= 0)
        {
            HpNum = _health;
        }
        else
        {
            _health = HpNum;
        }
        _displayUIState.SetHealthValue(HpNum);
        _attackPower = attack;
        _jumpDuration = jumpDuration;
        _displayUIState.SetAttackValue(_attackPower);
        _healthBar = GetComponent<HealthBar>();
        _healthBar.SetMaxValus(_health);
    }
  
    public void UpgradeHP(int hp)
    {
        _health += hp;
        HpNum = _health;
        _displayUIState.AddHP(hp);
        _healthBar.UpgradeValue(hp, 0.3f);
    }

    public void UpgradeAttackPower(int power)
    {
        _attackPower += power;
        _displayUIState.AddAttack(power);
    }
    public void UpgradeSpeedAttack(int speed)
    {
        _player._jumpDuration -= speed / 100f;
    }

    public void TakeDamage(int damage)
    {
        _health -= damage;
        HpNum = _health;
        _displayUIState.DamageHP(damage);

        _healthBar.SetValues(damage, 0.4f);

        if (_health <= 0)
        {
            _gameStates.LevelLost();
        }
    }
}