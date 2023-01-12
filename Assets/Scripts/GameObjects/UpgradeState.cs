using System.Collections.Generic;
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
    [SerializeField]private List<UpgradeSetting> upgrades;
    private Player player;
    private PlayerState playerState;

    [Inject]
    private void InitiallizeComponent(Player playerObj, PlayerState state)
    {
        player= playerObj;
        playerState= state;
    }
    // Start is called before the first frame update
    void Start()
    {
        upgrades[0].button.onClick.AddListener(UpgradeAttack);
        upgrades[1].button.onClick.AddListener(UpgradeSpeedAttack);
        upgrades[2].button.onClick.AddListener(UpgradeHP);

        SetButtonSettings(0);
        SetButtonSettings(1);
        SetButtonSettings(2);
    }

    void SetButtonSettings(int indexBtn)
    {
        upgrades[indexBtn].price.text=upgrades[indexBtn].priceUpgrade.ToString();
        upgrades[indexBtn].upgradeValue.text="+"+upgrades[indexBtn].stepUpgrade.ToString();
        upgrades[indexBtn].stepUp = upgrades[indexBtn].stepUpgrade;
    }
    private void UpgradeHP()
    {
       if (upgrades[2].stepUpgrade < upgrades[2].maxUpgrade)
       {
           if (playerState.HadCoins(upgrades[2].priceUpgrade))
           {
               player.UpgradeHP(upgrades[2].stepUpgrade);
               upgrades[2].priceUpgrade += upgrades[2].stepUpgrade;
               upgrades[2].stepUpgrade +=upgrades[2].stepUp;
               
               upgrades[2].price.text=upgrades[2].priceUpgrade.ToString();
               upgrades[2].upgradeValue.text="+"+upgrades[2].stepUpgrade.ToString();
              
           }
       }
    }

    private void UpgradeSpeedAttack()
    {
        if (upgrades[1].stepUpgrade < upgrades[1].maxUpgrade)
        {
            if (playerState.HadCoins(upgrades[1].priceUpgrade))
            {
                player.UpgradeSpeedAttack(upgrades[1].stepUpgrade);
                upgrades[1].priceUpgrade += upgrades[1].stepUpgrade;
                upgrades[1].stepUpgrade +=upgrades[1].stepUp;
               
                upgrades[1].price.text=upgrades[1].priceUpgrade.ToString();
                upgrades[1].upgradeValue.text="+"+upgrades[1].stepUpgrade.ToString();
              
            }
        }
    }

    private void UpgradeAttack()
    {
        if (upgrades[0].stepUpgrade < upgrades[0].maxUpgrade)
        {
            if (playerState.HadCoins(upgrades[0].priceUpgrade))
            {
                player.UpgradeAttackPower(upgrades[0].stepUpgrade);
                upgrades[0].priceUpgrade += upgrades[0].stepUpgrade;
                upgrades[0].stepUpgrade +=upgrades[0].stepUp;
               
                upgrades[0].price.text=upgrades[0].priceUpgrade.ToString();
                upgrades[0].upgradeValue.text="+"+upgrades[0].stepUpgrade.ToString();
              
            }
        }
    }
}
