using System;
using Data;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UIPlayerStates : MonoBehaviour, IUIPlayerStates
    {
        public event Action<int>PayCoins;
        [SerializeField] private Text _coinTxt;
        [SerializeField] private Text _hpTxt;
        [SerializeField] private Text _attackTxt;
        [SerializeField] private float _scaleSpeed;
        [SerializeField] private float _growSpeed;

        private int _hpNum = 0;
        private int _attackNum = 0;
        private int _coinsNum;

        public void Init(IDataSaver data)
        {
            _hpNum = data.HpNum;
            _coinsNum = data.CoinNum;
            _attackNum = data.AttackNum;
            _coinTxt.text = _coinsNum.ToString();
            data.DamageHp += DamageHP;
            data.UpgradeAttack += AddAttack;
            SetHealthValue(_hpNum);
            SetAttackValue(_attackNum);
        }
        public void AddCoins(int coins)
        {
            AddValue(_coinTxt, _coinsNum, coins);
            MakeScale(_coinTxt, 1, 1.5f);
            _coinsNum+=coins;
        }
        public void AddHp(int hp)
        {
            AddValue(_hpTxt, _hpNum, hp);
            MakeScale(_hpTxt, 0.6f, 1.1f);
        }

        public void AddAttack(int attack)
        {
            AddValue(_attackTxt, _attackNum, attack);
            MakeScale(_attackTxt, 0.6f, 1.1f);
        }

        public bool HadCoins(int coins)
        {
            bool hadCoins = false;
            int newCount =  _coinsNum - coins;
            if (newCount >= 0)
            {
                int oldNum =  _coinsNum;
                _coinsNum = newCount;
                _coinTxt.DOCounter(oldNum,  _coinsNum, _growSpeed);
                PayCoins?.Invoke(coins);
                hadCoins = true;
            }
            return hadCoins;
        }

        private void DamageHP(int hp)
        {
            int oldNum = _hpNum;
            _hpNum -= hp;
            _hpTxt.DOCounter(oldNum, _hpNum, _growSpeed);
        }
        private void SetAttackValue(int power)
        {
            _attackNum = power;
            _attackTxt.text = _attackNum.ToString();
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
        private void SetHealthValue(int hp)
        {
            _hpNum = hp;
            _hpTxt.text = _hpNum.ToString();
        }
    }
}
