using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class Money : MonoBehaviour
{
   
    [SerializeField]private Text coinTxt;
    private int _coinNum=0;


    public void AddCoins(int coins)
    {
        int oldNum = _coinNum;
        _coinNum += coins;
        coinTxt.DOCounter(oldNum, _coinNum, 0.5f)
               .OnPlay(() =>
               {
                   coinTxt.transform.DOScale(1.5f, 0.5f)
                          .OnComplete(() =>
                          {
                              coinTxt.transform.DOScale(1f, 0.5f);
                          });
               });
    }

    // Start is called before the first frame update
    void Start()
    {
        coinTxt.text = _coinNum.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
