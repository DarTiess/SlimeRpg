using System;
using HealthBar;

namespace Player
{
    public class PlayerUpgradeStates 
    {
        public event Action DeadPlayer;
        private int _health;
        private int _attackPower;
        private float _attackDuration;
        private float _healthBarDuration;

        private PlayerHealthBar _healthBar;
        public PlayerUpgradeStates(int attack, int health, float attackDuration, float heathBarDuration, PlayerHealthBar healthBar)
        {   
            _attackPower = attack;
            _health = health;
            _attackDuration = attackDuration;
            _healthBarDuration = heathBarDuration;
            _healthBar = healthBar;
            _healthBar.SetMaxValus(_health);
       
        }
  
        public void UpgradeHP(int hp)
        {
            _health += hp;
            _healthBar.UpgradeValue(hp, _healthBarDuration);
        }

        public int UpgradeAttackPower(int power)
        {
            _attackPower += power;
            return _attackPower;
        }
        public float UpgradeSpeedAttack(int speed)
        {
            _attackDuration -= speed / 100f;
            return _attackDuration;
        }

        public void TakeDamage(int damage)
        {
            _health -= damage;
            _healthBar.SetValues(damage, _healthBarDuration);
            if (_health <= 0)
            {
                DeadPlayer?.Invoke();
            }
        }
    }
}
