using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace UI
{
    public abstract class PanelBase : MonoBehaviour
    {
        [SerializeField] private Button _button;
        public virtual event Action ClickedPanel;

        private void Start()
        {
            _button.onClick.AddListener(OnClickedPanel);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }

        protected virtual void OnClickedPanel()
        {
          
        }
    }
}