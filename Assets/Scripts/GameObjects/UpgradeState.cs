using System.Collections.Generic;
using UI;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class UpgradeState : MonoBehaviour
{
    [System.Serializable]
    public class UpgradeSetting
    {
        public Button button;
        public Text price;
        public Text upgradeValue;
        public int priceUpgrade;
        public int stepUpgrade;
        public int stepUp;
        public int maxUpgrade;
    }
    [SerializeField]private List<UpgradeSetting> _upgrades;
   
    private Player.Player _player;
    private DisplayUIState _playerState;

    [Inject]
    private void Construct(Player.Player playerObj, DisplayUIState state)
    {
        _player= playerObj;
        _playerState= state;
    }
    
    void Start()
    {
        _upgrades[0].button.onClick.AddListener(UpgradeAttack);
        _upgrades[1].button.onClick.AddListener(UpgradeSpeedAttack);
        _upgrades[2].button.onClick.AddListener(UpgradeHP);

        SetButtonSettings(0);
        SetButtonSettings(1);
        SetButtonSettings(2);
    }

    void SetButtonSettings(int indexBtn)
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
            if (_playerState.HadCoins(upgrade.priceUpgrade))
            {
               // _player.MakeUpgrades(type, upgrade.stepUpgrade);
                upgrade.priceUpgrade += upgrade.stepUpgrade;
                upgrade.stepUpgrade +=upgrade.stepUp;
               
                upgrade.price.text=upgrade.priceUpgrade.ToString();
                upgrade.upgradeValue.text="+"+upgrade.stepUpgrade.ToString();
              
            }
        }
    }
}
