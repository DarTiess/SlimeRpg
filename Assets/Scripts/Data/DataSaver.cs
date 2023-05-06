using System;
using UI;
using UnityEngine;

namespace Data
{
    public interface ISceneData
    {
        int CurrentScene { get; set; }
    }
    public class DataSaver : IDataSaver, ISceneData
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
        public int CurrentScene
        {
            get { return PlayerPrefs.GetInt("NumberScene"); }
            set { PlayerPrefs.SetInt("NumberScene", value); }
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

        public void SetAttackPower(int attackPower)
        {
            _attack = attackPower;
            UpgradeAttack?.Invoke(_attack);
        }

        public void SetHp(int health)
        {
            HpNum = health;
        }

        public void PayCoins(int coins)
        {
            CoinNum -= coins;
        }

        public void TakeDamageHp(int damage)
        {
            HpNum-= damage;
            DamageHp?.Invoke(damage);
        }

        private void OnUpgradeHp(int health)
        {
            HpNum += health;
        }

        private void OnUpgradeAttackPower(int attackPower)
        {
            _attack +=attackPower;
            UpgradeAttack?.Invoke(attackPower);
        }
    }
}