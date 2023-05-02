using DG.Tweening;
using System.Drawing;
using UnityEngine;
using UnityEngine.UI;

public class DisplayUIState : MonoBehaviour
{

    [SerializeField] private Text _coinTxt;
    [SerializeField] private Text _hpTxt;
    [SerializeField] private Text _attackTxt;
    [SerializeField] private float _scaleSpeed;
    [SerializeField] private float _growSpeed;
    private int CoinNum
    {
        get { return PlayerPrefs.GetInt("Coins"); }
        set { PlayerPrefs.SetInt("Coins", value); }
    }

    private int _hpNum = 0;
    private int _attackNum = 0;

    void Start()
    {
        _coinTxt.text = CoinNum.ToString();
    }

    public void AddCoins(int coins)
    {
        AddValue(_coinTxt, CoinNum, coins);
        MakeScale(_coinTxt, 1, 1.5f);
        CoinNum+=coins;
    }

    public void AddHP(int hp)
    {
        AddValue(_hpTxt, _hpNum, hp);
        MakeScale(_hpTxt, 0.6f, 1.1f);

    }
    public void AddAttack(int attack)
    {
        AddValue(_attackTxt, _attackNum, attack);
        MakeScale(_attackTxt, 0.6f, 1.1f);

    }

    private void AddValue(Text uitext, int currentValue, int addValue)
    {
        int oldNum = currentValue;
        currentValue += addValue;
        uitext.DOCounter(oldNum, currentValue, _growSpeed);
    }
    private void MakeScale(Text uiTxt, float from, float to)
    {
        uiTxt.transform.DOScale(to, _scaleSpeed)
                        .OnComplete(() =>
                        {
                            uiTxt.transform.DOScale(from, _scaleSpeed);
                        });
    }
    public void DamageHP(int hp)
    {
        int oldNum = _hpNum;
        _hpNum -= hp;
        _hpTxt.DOCounter(oldNum, _hpNum, _growSpeed);
    }

    public void SetHealthValue(int hp)
    {
        _hpNum = hp;
        _hpTxt.text = _hpNum.ToString();
    }
    public void SetAttackValue(int power)
    {
        _attackNum = power;
        _attackTxt.text = _attackNum.ToString();
    }

    public bool HadCoins(int coins)
    {
        bool hadCoins = false;
        int newCount = CoinNum - coins;
        if (newCount >= 0)
        {
            int oldNum = CoinNum;
            CoinNum = newCount;
            _coinTxt.DOCounter(oldNum, CoinNum, _growSpeed);
            hadCoins = true;
        }

        return hadCoins;
    }
}
