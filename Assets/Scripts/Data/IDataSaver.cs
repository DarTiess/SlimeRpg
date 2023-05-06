using System;
using UI;

namespace Data
{
    public interface IDataSaver: IPlayerData
    {
        // public event Action<int> UpgradeHp; 
        public event Action<int> DamageHp;
        public event Action<int> UpgradeAttack; 
        public int CoinNum { get; set; }
        void OnMakeUpgrade(UpgradesType typeUpgrade, int value);
        void PayCoins(int coins);
    }
}