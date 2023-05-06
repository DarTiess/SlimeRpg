using System;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class UIPlayerUpgradeWindow : MonoBehaviour
    {
        [SerializeField]private List<UpgradeSetting> _upgrades;

        public event Action<UpgradesType,int> MakeUpgrade;
        private IUIPlayerStates _playerPlayerStates;

        public void Init(IUIPlayerStates playerStates)
        {
            _playerPlayerStates= playerStates; 
            _upgrades[0].button.onClick.AddListener(UpgradeAttack);
            _upgrades[1].button.onClick.AddListener(UpgradeSpeedAttack);
            _upgrades[2].button.onClick.AddListener(UpgradeHP);
                                              
            SetButtonSettings(0);
            SetButtonSettings(1);
            SetButtonSettings(2);
        }
        private void SetButtonSettings(int indexBtn)
        {
            _upgrades[indexBtn].price.text=_upgrades[indexBtn].priceUpgrade.ToString();
            _upgrades[indexBtn].upgradeValue.text="+"+_upgrades[indexBtn].stepUpgrade.ToString();
            _upgrades[indexBtn].stepUp = _upgrades[indexBtn].stepUpgrade;
        }
        private void UpgradeHP()
        {
            UpgradeFunction(_upgrades[2], UpgradesType.HP);
        }

        private void UpgradeSpeedAttack()
        {
            UpgradeFunction(_upgrades[1],  UpgradesType.SpeedAttack);
        }

        private void UpgradeAttack()
        {
            UpgradeFunction(_upgrades[0], UpgradesType.AttackPower);
        }

        private void UpgradeFunction(UpgradeSetting upgrade, UpgradesType type)
        {
            if (upgrade.stepUpgrade < upgrade.maxUpgrade)
            {
                if (_playerPlayerStates.HadCoins(upgrade.priceUpgrade))
                {
                    MakeUpgrade?.Invoke(type, upgrade.stepUpgrade);
                    // _player.MakeUpgrades(type, upgrade.stepUpgrade);
                    upgrade.priceUpgrade += upgrade.stepUpgrade;
                    upgrade.stepUpgrade +=upgrade.stepUp;
               
                    upgrade.price.text=upgrade.priceUpgrade.ToString();
                    upgrade.upgradeValue.text="+"+upgrade.stepUpgrade;
              
                }
            }
        }
    }
}
