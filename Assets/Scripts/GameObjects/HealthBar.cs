using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Slider slider;
    float valueProgress = 0;
    Camera camera;
    [SerializeField]
    private Text damageTxt;

    private void Awake()
    {
        camera = Camera.main;
    }
    private void Update()
    {
        slider.transform.LookAt(transform.position + camera.transform.forward);
      
    }

    public void SetOffSlider()
    {
        slider.gameObject.SetActive(false);
    }

    public void SetOnSlider()
    {
        slider.gameObject.SetActive(true);
    }
    public void SetMaxValus(float maxValues)
    {
        slider.maxValue = maxValues;
        slider.value = maxValues;
        valueProgress = maxValues;
    }

    public void SetValues(float price, float time)
    {
        if (valueProgress > 0)
        {
            valueProgress -= price;
            slider.DOValue(valueProgress, time);
            damageTxt.text = "-"+price.ToString();
            damageTxt.gameObject.SetActive(true);
            damageTxt.transform.DOLocalMoveY(80f, 0.2f)
                     .OnComplete(() =>
                     {
                         damageTxt.gameObject.SetActive(false);
                         damageTxt.gameObject.transform.DOLocalMoveY(20, 0f);
                     });
        }


    }

    public void UpgradeValue(float value, float time)
    {
        float updatedValue = valueProgress + value;
        if (updatedValue > slider.maxValue)
        {
            slider.maxValue = updatedValue;
        }
        valueProgress += value;
        slider.DOValue(valueProgress, time);
    }
}
