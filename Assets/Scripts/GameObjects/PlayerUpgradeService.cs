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
    private UIDisplay _uiState;
    private Player _player;
    private GameManager _gameManager;
    [Inject]
    private void Construct(UIDisplay state, GameManager gameManager)
    {
        _uiState = state;
        _gameManager = gameManager;
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
        _uiState.SetHealthValue(HpNum);
        _attackPower = attack;
        _jumpDuration = jumpDuration;
        _uiState.SetAttackValue(_attackPower);
        _healthBar = GetComponent<HealthBar>();
        _healthBar.SetMaxValus(_health);
    }
  
    public void UpgradeHP(int hp)
    {
        _health += hp;
        HpNum = _health;
        _uiState.AddHP(hp);
        _healthBar.UpgradeValue(hp, 0.3f);
    }

    public void UpgradeAttackPower(int power)
    {
        _attackPower += power;
        _uiState.AddAttack(power);
    }
    public void UpgradeSpeedAttack(int speed)
    {
        _player._jumpDuration -= speed / 100f;
    }

    public void TakeDamage(int damage)
    {
        _health -= damage;
        HpNum = _health;
        _uiState.DamageHP(damage);

        _healthBar.SetValues(damage, 0.4f);

        if (_health <= 0)
        {
            _gameManager.LevelLost();
        }
    }
}
