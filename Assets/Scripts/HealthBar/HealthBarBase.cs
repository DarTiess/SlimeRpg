using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace HealthBar
{
    public class HealthBarBase : MonoBehaviour
    {
        [SerializeField]
        protected Slider _slider;
        [SerializeField] private Text _damageTxt;
        protected float _valueProgress = 0;
        private Camera _camera;

        private void Start()
        {
            _camera = Camera.main;
        }

        private void Update()
        {
            _slider.transform.LookAt(transform.position + _camera.transform.forward);
        }

        public void Show()
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
    }
}