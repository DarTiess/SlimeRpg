using System;
using UI;

namespace Data
{
    public interface IPlayerData
    {
        public event Action<UpgradesType, int> MakeUpgrade;
        public int HpNum { get; }
        public int AttackNum { get; }
        public void TakeDamageHp(int damage);
        public void SetHp(int health);
       
        void SetAttackPower(int attackPower);
    }
}