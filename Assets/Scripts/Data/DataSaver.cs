using System;
using UI;
using UnityEngine;

namespace Data
{
    public class DataSaver : IDataSaver
    {
        public event Action<int> DamageHp;
        public event Action<int> UpgradeAttack;
        public event Action<UpgradesType, int> MakeUpgrade;
        public int HpNum
        {
            get { return PlayerPrefs.GetInt("Health"); }
            private set{ PlayerPrefs.SetInt("Health", value); }
       
        }
        private int _attack=0;
        public int AttackNum => _attack;
        public int CoinNum
        {
            get { return PlayerPrefs.GetInt("Coins"); }
            set { PlayerPrefs.SetInt("Coins", value); }
        }
        public void OnMakeUpgrade(UpgradesType typeUpgrade, int value)
        {
            MakeUpgrade?.Invoke(typeUpgrade, value);
            switch (typeUpgrade)
            {
                case UpgradesType.HP:
                    OnUpgradeHp(value);
                    break;
                case UpgradesType.AttackPower:
                    OnUpgradeAttackPower(value);
                    break;   
            }
        }

        public void PayCoins(int coins)
        {
            CoinNum -= coins;
        }

        private void OnUpgradeHp(int health)
        {
            HpNum += health;
        }

        public void TakeDamageHp(int damage)
        {
            HpNum-= damage;
            DamageHp?.Invoke(damage);
        }

        public void SetHp(int health)
        {
            HpNum = health;
        }

        private void OnUpgradeAttackPower(int attackPower)
        {
            _attack +=attackPower;
            UpgradeAttack?.Invoke(attackPower);
        
        }

        public void SetAttackPower(int attackPower)
        {
            _attack = attackPower;
            Debug.Log(_attack+ " Onset attack");
            UpgradeAttack?.Invoke(_attack);
        }
    }
}