using DG.Tweening;

namespace HealthBar
{
    public class PlayerHealthBar : HealthBarBase
    {
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
}
