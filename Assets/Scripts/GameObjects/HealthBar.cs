using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private Text _damageTxt;

    private float _valueProgress = 0;
    private Camera _camera;

    private void Awake()
    {
        _camera = Camera.main;
    }
    private void Update()
    {
        _slider.transform.LookAt(transform.position + _camera.transform.forward);
    }

    public void SetOffSlider()
    {
        _slider.gameObject.SetActive(false);
    }

    public void SetOnSlider()
    {
        _slider.gameObject.SetActive(true);
    }
    public void SetMaxValus(float maxValues)
    {
        _slider.maxValue = maxValues;
        _slider.value = maxValues;
        _valueProgress = maxValues;
    }

    public void SetValues(float price, float time)
    {
        if (_valueProgress > 0)
        {
            _valueProgress -= price;
            _slider.DOValue(_valueProgress, time);
            _damageTxt.text = "-" + price.ToString();
            _damageTxt.gameObject.SetActive(true);
            _damageTxt.transform.DOLocalMoveY(80f, 0.2f)
                     .OnComplete(() =>
                     {
                         _damageTxt.gameObject.SetActive(false);
                         _damageTxt.gameObject.transform.DOLocalMoveY(20, 0f);
                     });
        }
    }

    public void UpgradeValue(float value, float time)
    {
        float updatedValue = _valueProgress + value;
        if (updatedValue > _slider.maxValue)
        {
            _slider.maxValue = updatedValue;
        }
        _valueProgress += value;
        _slider.DOValue(_valueProgress, time);
    }
}
