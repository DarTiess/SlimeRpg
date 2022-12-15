using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class PlayerState : MonoBehaviour
{
   
    [SerializeField]private Text coinTxt;
    private int CoinNum
    {
        get { return PlayerPrefs.GetInt("Coins"); }
        set { PlayerPrefs.SetInt("Coins", value); }
    }
    [SerializeField]private Text hpTxt;
    private int _hpNum = 0;
    [SerializeField]private Text attackTxt;
    private int _attackNum=0;


    public void AddCoins(int coins)
    {
        int oldNum =CoinNum;
        CoinNum += coins;
        coinTxt.DOCounter(oldNum, CoinNum, 0.5f)
               .OnPlay(() =>
               {
                   coinTxt.transform.DOScale(1.5f, 0.5f)
                          .OnComplete(() =>
                          {
                              coinTxt.transform.DOScale(1f, 0.5f);
                          });
               });
    } 
    public void AddHP(int hp)
    {
        int oldNum = _hpNum;
        _hpNum += hp;
        hpTxt.DOCounter(oldNum, _hpNum, 0.5f)
               .OnPlay(() =>
               {
                   hpTxt.transform.DOScale(1.1f, 0.5f)
                          .OnComplete(() =>
                          {
                              hpTxt.transform.DOScale(0.6f, 0.5f);
                          });
               });
    }
    public void DamageHP(int hp)
    {
        int oldNum = _hpNum;
        _hpNum -= hp;
        hpTxt.DOCounter(oldNum, _hpNum, 0.5f);
    } 
    public void AddAttack(int attack)
    {
        int oldNum = _attackNum;
        _attackNum += attack;
        attackTxt.DOCounter(oldNum, _attackNum, 0.5f)
                 .OnPlay(() =>
                 {
                     attackTxt.transform.DOScale(1.1f, 0.5f)
                              .OnComplete(() =>
                              {
                                  attackTxt.transform.DOScale(0.6f, 0.5f);
                              });
                 });
    }

    // Start is called before the first frame update
    void Start()
    {
        coinTxt.text = CoinNum.ToString();
       
    }

    public void SetHealthValue(int hp)
    {
        _hpNum= hp;
        hpTxt.text = _hpNum.ToString();
    }
    public void SetAttackValue(int power)
    {
        _attackNum=power;
        attackTxt.text = _attackNum.ToString();
    }

    public bool HadCoins(int coins)
    {
        bool hadCoins=false;
        int newCount = CoinNum - coins;
        if (newCount >=0)
        {
            int oldNum =CoinNum;
            CoinNum = newCount;
           coinTxt.DOCounter(oldNum, CoinNum, 0.5f);
            hadCoins= true;
        }

        return hadCoins;
    }
}
